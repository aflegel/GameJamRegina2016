using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.CultSimulator
{
	class GameStateManager : MonoBehaviour
	{
		private List<PersonReference> sacrificeCandidates;
		private List<PersonReference> cultistCandidates;
		private List<PersonReference> cultists;
		private YearTarget currentTarget;
		private int seasonNumber;
		private int yearNumber;
		private PeoplePool peoplePool { get; set; }

		// Public interactions
		public GameStateManager()
		{
			seasonNumber = 1;
			yearNumber = 1;

			sacrificeCandidates = new List<PersonReference>();
			cultistCandidates = new List<PersonReference>();
			cultists = new List<PersonReference>();
			currentTarget = new YearTarget();
			peoplePool = new PeoplePool();
		}

		public void AddSacrificeCandidate(int personID)
		{
			sacrificeCandidates.Add(new PersonReference { PersonID = personID, Instruction = null });
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
			cultistCandidates.Add(new PersonReference { PersonID = personID, Instruction = null });
		}

		public void RemoveCultistCandidate(int personID)
		{
			cultistCandidates.Remove(cultistCandidates.Find(person => person.PersonID == personID));
		}

		public void AddCultist(int personID)
		{
			cultists.Add(new PersonReference { PersonID = personID, Instruction = null });
		}

		public IEnumerable<PersonReference> GetCurrentCultists()
		{
			return cultists;
		}

		public void SetCultistInstruction(int personID, ActionType action, int targetPersonID)
		{
			var cultist = cultists.Find(cult => cult.PersonID == personID);
			cultist.Instruction = new Instruction { Action = action, TargetID =  targetPersonID};
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
			peoplePool.GeneratePeople(20, null);
		}
	}
}
