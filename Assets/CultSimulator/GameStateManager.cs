using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Standard_Assets.Models;

namespace Assets.Standard_Assets
{
	class GameStateManager : MonoBehaviour
	{
		private List<Person> _sacrificeCandidates;
		private List<Person> _cultistCandidates;
		private List<Person> _cultists;
		private YearTarget _currentTarget;
		private int _seasonNumber;

		// Public interactions
		public GameStateManager()
		{
			_seasonNumber = 1;

			_sacrificeCandidates = new List<Person>();
			_cultistCandidates = new List<Person>();
			_cultists = new List<Person>();
			_currentTarget = new YearTarget();
		}

		public void AddSacrificeCandidate(Person newCandidate)
		{
			_sacrificeCandidates.Add(newCandidate);
		}

		public IEnumerable<Person> GetSacrificeCandidates()
		{
			return _sacrificeCandidates;
		}

		public void RemoveSacrificeCandidate(int personId)
		{
			_sacrificeCandidates.Remove(_sacrificeCandidates.Find(x => x.personID == personId));
		}

		public void AddCultist(Person newCultist)
		{
			_cultists.Add(newCultist);
		}

		public IEnumerable<Person> GetCurrentCultists()
		{
			return _cultists;
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
