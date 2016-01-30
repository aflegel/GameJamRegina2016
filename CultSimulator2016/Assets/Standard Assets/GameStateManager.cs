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
		private List<FullPerson> _sacrificeCandidates = new List<FullPerson>();
		private List<FullPerson> _cultistCandidates = new List<FullPerson>();
		private YearTarget _currentTarget;

		// Public interactions
		public int SeasonNumber { get; set; }

		public void AddSacrificeCandidate(FullPerson newCandidate)
		{
			_sacrificeCandidates.Add(newCandidate);
		}

		public FullPerson[] GetSacrificeCandidates()
		{
			return _sacrificeCandidates.ToArray();
		}

		public void RemoveSacrificeCandidate(int personId)
		{
			_sacrificeCandidates.Remove(_sacrificeCandidates.Find(x => x.personID == personId));
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
	}
}
