using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.CultSimulator
{
	class GameStateManager : MonoBehaviour
	{
		private List<PersonReference> _sacrificeCandidates;
		private List<PersonReference> _cultistCandidates;
		private List<PersonReference> _cultists;
		private YearTarget _currentTarget;
		private int _seasonNumber;

		public PeoplePool peoplePool { get; set; }

		// Public interactions
		public GameStateManager()
		{
			_seasonNumber = 1;

			_sacrificeCandidates = new List<PersonReference>();
			_cultistCandidates = new List<PersonReference>();
			_cultists = new List<PersonReference>();
			_currentTarget = new YearTarget();
		}

		public void AddSacrificeCandidate(int personID)
		{
			_sacrificeCandidates.Add(new PersonReference { PersonID = personID, Instruction = null });
		}

		public IEnumerable<PersonReference> GetSacrificeCandidates()
		{
			return _sacrificeCandidates;
		}

		public void RemoveSacrificeCandidate(int personID)
		{
			_sacrificeCandidates.Remove(_sacrificeCandidates.Find(person => person.PersonID == personID));
		}

		public IEnumerable<PersonReference> GetCultistCandidates()
		{
			return _cultistCandidates;
		}

		public void AddCultistCandidate(int personID)
		{
			_cultistCandidates.Add(new PersonReference { PersonID = personID, Instruction = null });
		}

		public void RemoveCultistCandidate(int personID)
		{
			_cultistCandidates.Remove(_cultistCandidates.Find(person => person.PersonID == personID));
		}

		public void AddCultist(int personID)
		{
			_cultists.Add(new PersonReference { PersonID = personID, Instruction = null });
		}

		public IEnumerable<PersonReference> GetCurrentCultists()
		{
			return _cultists;
		}

		public void SetCultistInstruction(int personID, ActionType action, int targetPersonID)
		{
			var cultist = _cultists.Find(cult => cult.PersonID == personID);
			cultist.Instruction = new Instruction { Action = action, TargetID =  targetPersonID};
		}

		public void SetNewTarget(int numberOfCultists, SearchableAsset[] sacrificeTargets)
		{
			_currentTarget = new YearTarget()
			{
				NumberOfCultists = numberOfCultists,
				SacrificeTargets = sacrificeTargets
			};
		}

		public YearTarget GetCurrentTarget()
		{
			return _currentTarget;
		}

		public int GetSeasonNumber()
		{
			return _seasonNumber;
		}

		public void IncrementSeason()
		{
			_seasonNumber += 1;
		}
	}
}
