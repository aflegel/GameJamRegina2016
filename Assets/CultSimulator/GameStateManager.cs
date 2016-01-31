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
		private YearTarget currentTarget;
		private int seasonNumber;
		private int yearNumber;
		private PeoplePool peoplePool { get; set; }

		private void GetNewPools(List<SearchableAsset> sacrificeAssets, List<SearchableAsset> cultistAssets)
		{
			peoplePool.GeneratePeople(20, sacrificeAssets);
			sacrificeCandidates = new List<PersonReference>();

			for (int i = peoplePool.activePool.Count - 21; i < peoplePool.activePool.Count - 1; i++)
			{
				sacrificeCandidates.Add(new PersonReference() { PersonID = peoplePool.activePool[i].PersonID, IndepthInvestigated = false });
			}

			peoplePool.GeneratePeople(20, cultistAssets);
			cultistCandidates = new List<PersonReference>();

			for (int i = peoplePool.activePool.Count - 21; i < peoplePool.activePool.Count - 1; i++)
			{
				cultistCandidates.Add(new PersonReference() { PersonID = peoplePool.activePool[i].PersonID, IndepthInvestigated = false });
			}
		}

		// Public interactions
		void Start()
		{
			seasonNumber = 1;
			yearNumber = 1;

			sacrificeCandidates = new List<PersonReference>();
			cultistCandidates = new List<PersonReference>();
			currentTarget = new YearTarget();
			peoplePool = new PeoplePool();
			peoplePool.GeneratePeople(20, new List<SearchableAsset> {
				new SearchableAsset() { Profession = Profession.Educator, Sin = Sin.Envious, Virtue = Virtue.Temperant },
				new SearchableAsset() { Profession = Profession.Law, Sin = Sin.Lusty, Virtue = Virtue.Kind }
			});

			cultists = new Cultist[NUMBER_OF_CULTISTS];

			cultists[0] = new Cultist() { PersonID = peoplePool.activePool[1].PersonID, Instruction = null };
			cultists[1] = new Cultist() { PersonID = peoplePool.activePool[2].PersonID, Instruction = null };
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

		public void SetCultistInstruction(ActionType action, int targetPersonID, int cultistIndex)
		{
			cultists[cultistIndex].Instruction = new Instruction { Action = action, TargetID =  targetPersonID};
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

		public int GetSeasonNumber()
		{
			return seasonNumber;
		}

		public void IncrementSeason()
		{
			seasonNumber += 1;

			if (seasonNumber == 5)
			{
				IncrementYear();
			}
		}

		public int GetYearNumber()
		{
			return yearNumber;
		}

		public void IncrementYear()
		{
			yearNumber += 1;
			seasonNumber = 1;

			var sacrificeAssets = new List<SearchableAsset> {
				new SearchableAsset() { Profession = Profession.Educator, Sin = Sin.Envious, Virtue = Virtue.Temperant },
				new SearchableAsset() { Profession = Profession.Law, Sin = Sin.Lusty, Virtue = Virtue.Kind },
				new SearchableAsset() { Profession = Profession.Medical, Sin = Sin.Proud, Virtue = Virtue.Charitable } };

			var cultistAssets = new List<SearchableAsset> {
				new SearchableAsset() { Profession = Profession.Educator, Sin = Sin.Envious, Virtue = Virtue.Temperant },
				new SearchableAsset() { Profession = Profession.Law, Sin = Sin.Lusty, Virtue = Virtue.Kind },
				new SearchableAsset() { Profession = Profession.Medical, Sin = Sin.Proud, Virtue = Virtue.Charitable } };

			GetNewPools(sacrificeAssets, cultistAssets);
		}

		public Person GetPerson(int personID)
		{
			return peoplePool.SearchPeopleByID(personID);
		}
	}
}
