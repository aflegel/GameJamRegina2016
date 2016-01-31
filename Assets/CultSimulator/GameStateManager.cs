using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.CultSimulator
{
	public class GameStateManager : MonoBehaviour
	{
		static int NUMBER_OF_CULTISTS = 8;

		private List<PersonReference> sacrificeCandidates;
		private List<PersonReference> cultistCandidates;
		private Cultist[] cultists;
		private int numberOfCultists;
		private int currentPeoplePoolIndex;
		private YearTarget currentTarget;
		private int seasonNumber;
		private int yearNumber;
		private PeoplePool peoplePool { get; set; }
		private TraitPool traitPool { get; set; }
		private FlavourPool flavourPool { get; set; }
		private bool gameWillEnd;

		public List<string> ResultTextList { get; set; }

		private void GetNewPools(IEnumerable<SearchableAsset> sacrificeAssets, IEnumerable<SearchableAsset> cultistAssets, int size)
		{
			peoplePool.GeneratePeople(size, sacrificeAssets.ToList(), false, true);
			sacrificeCandidates = new List<PersonReference>();

			for (int i = currentPeoplePoolIndex + 1; i < peoplePool.activePool.Count - 1; i++)
			{
				sacrificeCandidates.Add(new PersonReference() { PersonID = peoplePool.activePool[i].PersonID, IndepthInvestigated = false });
			}

			currentPeoplePoolIndex += size;

			peoplePool.GeneratePeople(size, cultistAssets.ToList(), false, false);
			cultistCandidates = new List<PersonReference>();

			for (int i = currentPeoplePoolIndex + 1; i < peoplePool.activePool.Count - 1; i++)
			{
				cultistCandidates.Add(new PersonReference() { PersonID = peoplePool.activePool[i].PersonID, IndepthInvestigated = false });
			}
		}

		// Public interactions
		void Awake()
		{
			seasonNumber = 1;
			yearNumber = 1;

			sacrificeCandidates = new List<PersonReference>();
			cultistCandidates = new List<PersonReference>();
			currentTarget = YearTargetFactory.GetYearTargets(yearNumber);
			peoplePool = new PeoplePool();
			traitPool = new TraitPool();
			flavourPool = new FlavourPool();
			cultists = new Cultist[NUMBER_OF_CULTISTS];

			var cultistAssets = new List<SearchableAsset>
			{
				new SearchableAsset() { Profession = Profession.None, Sin = Sin.None, Virtue = Virtue.None }
			};

			// Create the starting Cultists
			numberOfCultists = 2;
			currentPeoplePoolIndex = 2;
			peoplePool.GeneratePeople(numberOfCultists, cultistAssets, false, false);

			cultists[0] = new Cultist() { PersonID = peoplePool.activePool[1].PersonID, Instruction = null };
			cultists[1] = new Cultist() { PersonID = peoplePool.activePool[2].PersonID, Instruction = null };

			GetNewPools(currentTarget.SacrificeTargets, cultistAssets, 20);

			gameWillEnd = false;
		}

		public void AddSacrificeCandidate(int personID)
		{
			sacrificeCandidates.Add(new PersonReference { PersonID = personID, IndepthInvestigated = false });
		}

		public IEnumerable<PersonReference> GetSacrificeCandidates()
		{
			return sacrificeCandidates;
		}

		public void RemoveSacrificeCandidate(int personID)
		{
			sacrificeCandidates.Remove(sacrificeCandidates.Find(person => person.PersonID == personID));
		}

		public IEnumerable<PersonReference> GetCultistCandidates()
		{
			return cultistCandidates;
		}

		public void AddCultistCandidate(int personID)
		{
			cultistCandidates.Add(new PersonReference { PersonID = personID, IndepthInvestigated = false });
		}

		public void RemoveCultistCandidate(int personID)
		{
			cultistCandidates.Remove(cultistCandidates.Find(person => person.PersonID == personID));
		}

		public void AddCultist(int personID, int cultistIndex)
		{
			cultists[cultistIndex] = new Cultist() { PersonID = personID, Instruction = null };
		}

		public Cultist[] GetCurrentCultists()
		{
			return cultists;
		}

		public void SetCultistInstruction(ActionType action, int? targetPersonID, int cultistIndex)
		{
			cultists[cultistIndex].Instruction = new Instruction { Action = action, TargetID = targetPersonID };
		}

		public Cultist GetCultist(int cultistIndex)
		{
			return cultists[cultistIndex];
		}

		public void SetNewTarget(int numberOfCultists, SearchableAsset[] sacrificeTargets)
		{
			currentTarget = new YearTarget()
			{
				NumberOfCultists = numberOfCultists,
				SacrificeTargets = sacrificeTargets
			};
		}

		public YearTarget GetCurrentTarget()
		{
			return currentTarget;
		}

		public bool IsGameOver()
		{
			return gameWillEnd;
		}

		public int GetSeasonNumber()
		{
			return seasonNumber;
		}

		public bool IncrementSeason()
		{
			bool gameOver = false;

			ResultTextList = new List<string>();

			ProcessActions();

			seasonNumber += 1;

			if (HasPlayerLost())
			{
				gameWillEnd = true;
				gameOver = true;
			}

			for (int i = 0; i < 8; i++)
			{
				if (cultists[i] != null)
					cultists[i].Instruction = null;
			}

			if (seasonNumber == 5 && !gameWillEnd)
			{
				IncrementYear();
			}

			return gameOver;
		}

		public void ProcessActions()
		{

			int result;
			System.Random randomNumber = new System.Random();

			foreach (Cultist cultist in cultists)
			{
				if (cultist != null)
				{
					//grab the difference for the current action
					result = GetSkillDifference(cultist);

					result += randomNumber.Next(0, 100);

					ResultTextList.Add(GetPerson(cultist.PersonID).Name);

					if (result > 80)
						ProcessSuccess(SuccessRating.GreatSuccess, cultist.Instruction);
					else if (result > 50)
						ProcessSuccess(SuccessRating.GoodSuccess, cultist.Instruction);
					else if (result > 20)
						ProcessSuccess(SuccessRating.NormalSuccess, cultist.Instruction);
					else if (result > 0)
						ProcessSuccess(SuccessRating.Failure, cultist.Instruction);
					else if (result > -20)
						ProcessSuccess(SuccessRating.BadFailure, cultist.Instruction);
					else
						ProcessSuccess(SuccessRating.TerribleFailure, cultist.Instruction);
				}
			}
		}

		public int GetSkillDifference(Cultist cultist)
		{
			Person attacker = GetPerson(cultist.PersonID);
			Person defender;
			int result = 0;
			switch (cultist.Instruction.Action)
			{
				case ActionType.Abduct:
					if (cultist.Instruction.TargetID.HasValue)
					{
						defender = GetPerson(cultist.Instruction.TargetID.Value);

						result = attacker.Abduction - defender.AbductionDefense + traitPool.GetTraitValue(attacker.assets, defender.assets);
					}

					break;
				case ActionType.Investigate:
					if (cultist.Instruction.TargetID.HasValue)
					{
						defender = GetPerson(cultist.Instruction.TargetID.Value);

						result = attacker.Investigation - defender.InvestigationDefense + traitPool.GetTraitValue(attacker.assets, defender.assets);
					}
					else
					{
						result = attacker.Investigation;
					}
					break;
				case ActionType.Recruit:
					if (cultist.Instruction.TargetID.HasValue)
					{
						defender = GetPerson(cultist.Instruction.TargetID.Value);

						result = attacker.Recruitment - defender.RecruitmentDefense + traitPool.GetTraitValue(attacker.assets, defender.assets);
					}
					break;
				case ActionType.None:
					break;
				default:
					break;
			}

			return result;
		}

		public string GetSkillText(Cultist cultist, System.Random randomNumber)
		{
			Person attacker = GetPerson(cultist.PersonID);
			Person defender;
			int attackertResult = 0;
			int defenderResult = 0;
			switch (cultist.Instruction.Action)
			{
				case ActionType.Abduct:
					if (cultist.Instruction.TargetID.HasValue)
					{
						defender = GetPerson(cultist.Instruction.TargetID.Value);

						attackertResult = attacker.Abduction + traitPool.GetTraitValue(attacker.assets, defender.assets);
						defenderResult = defender.AbductionDefense;
					}

					break;
				case ActionType.Investigate:
					if (cultist.Instruction.TargetID.HasValue)
					{
						defender = GetPerson(cultist.Instruction.TargetID.Value);

						attackertResult = attacker.Investigation + traitPool.GetTraitValue(attacker.assets, defender.assets);
						defenderResult = defender.InvestigationDefense;
					}
					else
					{
						attackertResult = attacker.Investigation;
						defenderResult = 0;
					}
					break;
				case ActionType.Recruit:
					if (cultist.Instruction.TargetID.HasValue)
					{
						defender = GetPerson(cultist.Instruction.TargetID.Value);

						attackertResult = attacker.Recruitment + traitPool.GetTraitValue(attacker.assets, defender.assets);
						defenderResult = defender.RecruitmentDefense;

					}
					break;
				case ActionType.None:
					break;
				default:
					break;
			}

			string attackerString = "";

			if (attackertResult > 80)
			{
				attackerString = flavourPool.AmazingList[randomNumber.Next(0, flavourPool.AmazingList.Count)];
			}
			else if (attackertResult > 60)
			{
				attackerString = flavourPool.GoodList[randomNumber.Next(0, flavourPool.GoodList.Count)];
			}
			else if (attackertResult > 40)
			{
				attackerString = flavourPool.NormalList[randomNumber.Next(0, flavourPool.NormalList.Count)];
			}
			else if (attackertResult > 20)
			{
				attackerString = flavourPool.BadList[randomNumber.Next(0, flavourPool.BadList.Count)];
			}
			else
			{
				attackerString = flavourPool.TerribleList[randomNumber.Next(0, flavourPool.TerribleList.Count)];
			}

			return attackerString;
		}

		public void ProcessSuccess(SuccessRating rating, Instruction action)
		{
			ResultTextList.Add(string.Format("{0} at {1}", rating.ToString(), action.Action.ToString()));

			action.IsSuccess = rating;
			switch (action.Action)
			{
				case ActionType.Abduct:
					switch (action.IsSuccess)
					{
						case SuccessRating.GoodSuccess:
						case SuccessRating.GreatSuccess:
						case SuccessRating.NormalSuccess:
							int newIndex = 0;
							for (int i = 0; i < 8; i++)
							{
								newIndex = i;

								if (cultists[i] == null)
								{
									i = 10;
								}
							}

							cultists[newIndex] = new Cultist()
							{
								PersonID = (int)action.TargetID,
								Instruction = null
							};

							++numberOfCultists;

							sacrificeCandidates.Remove(sacrificeCandidates.Find(sa => sa.PersonID == action.TargetID));
							break;
						case SuccessRating.BadFailure:
						case SuccessRating.TerribleFailure:
						case SuccessRating.Failure:
							break;
					}
					peoplePool.activePool[(int)action.TargetID].Active = false;
					break;
				case ActionType.Investigate:
					switch (action.IsSuccess)
					{
						case SuccessRating.GoodSuccess:
						case SuccessRating.GreatSuccess:
						case SuccessRating.NormalSuccess:
							if (sacrificeCandidates.Exists(sa => sa.PersonID == action.TargetID))
							{
								var sacrifice = sacrificeCandidates.Find(sa => sa.PersonID == action.TargetID);
								sacrifice.IndepthInvestigated = true;
								Person sac = GetPerson(sacrifice.PersonID);
								sac.FlavourText = flavourPool.GetInvestigationValue(sac.assets.Profession, sac.assets.Sin) + " " + flavourPool.GetInvestigationValue(sac.assets.Profession, sac.assets.Virtue);
							}
							else
							{
								var cultist = cultistCandidates.Find(cult => cult.PersonID == action.TargetID);
								cultist.IndepthInvestigated = true;
							}
							break;
						case SuccessRating.BadFailure:
						case SuccessRating.TerribleFailure:
						case SuccessRating.Failure:
							break;
					}
					break;
				case ActionType.Recruit:
					switch (action.IsSuccess)
					{
						case SuccessRating.GoodSuccess:
						case SuccessRating.GreatSuccess:
						case SuccessRating.NormalSuccess:
							int newIndex = 0;
							for (int i = 0; i < 8; i++)
							{
								newIndex = i;

								if (cultists[i] == null)
								{
									i = 10;
								}
							}

							cultists[newIndex] = new Cultist()
							{
								PersonID = (int)action.TargetID,
								Instruction = null
							};

							++numberOfCultists;
							break;
						case SuccessRating.BadFailure:
						case SuccessRating.TerribleFailure:
						case SuccessRating.Failure:
							break;
					}
					peoplePool.activePool[(int)action.TargetID].Active = false;
					cultistCandidates.Remove(cultistCandidates.Find(cult => cult.PersonID == action.TargetID));
					break;
				case ActionType.None:
					break;
				default:
					break;
			}
		}

		public int GetYearNumber()
		{
			return yearNumber;
		}

		private bool HasPlayerLost()
		{
			bool gameOver = false;

			if (numberOfCultists <= 0)
			{
				gameOver = true;
				gameWillEnd = true;
			}

			return gameOver;
		}

		public bool IncrementYear()
		{
			bool gameOver = false;

			if (gameWillEnd)
			{
				gameOver = true;
			}
			else
			{
				yearNumber += 1;
				seasonNumber = 1;

				currentTarget = YearTargetFactory.GetYearTargets(yearNumber);

				var sacrificeAssets = currentTarget.SacrificeTargets;

				var cultistAssets = new List<SearchableAsset>
				{
					new SearchableAsset() { Profession = Profession.None, Sin = Sin.None, Virtue = Virtue.None }
				};

				GetNewPools(sacrificeAssets, cultistAssets, 20);
			}

			return gameOver;
		}

		public Person GetPerson(int personID)
		{
			return peoplePool.SearchPeopleByID(personID);
		}
	}
}
