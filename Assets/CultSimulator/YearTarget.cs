using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	public class YearTarget
	{
		public int NumberOfCultists { get; set; }
		public IEnumerable<SearchableAsset> SacrificeTargets { get; set; }
	}

	public static class YearTargetFactory
	{
		public static YearTarget GetYearTargets(int yearNumber)
		{
			var nextYearTargets = new YearTarget();
			Random randomNumber = new Random();

			switch (yearNumber)
			{
				case 1:
					// 3 cultists
					nextYearTargets.NumberOfCultists = 3;

					// Kill goat
					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(false, false, false, true, randomNumber)
					};
					break;
				case 2:
					// 4 cultists
					nextYearTargets.NumberOfCultists = 4;

					// Kill cow
					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(false, false, false, true, randomNumber)
					};
					break;
				case 3:
					// 5 cultists
					nextYearTargets.NumberOfCultists = 5;

					// Kill goat
					// Kill virtue
					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, false, true, randomNumber)
					};
					break;
				case 4:
					// 6 cultists
					nextYearTargets.NumberOfCultists = 6;

					// Kill cow
					// Kill 1 virtue, 1 profression
					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, false, true, randomNumber)
					};
					break;
				case 5:
					// 7 cultists
					nextYearTargets.NumberOfCultists = 7;

					// Kill goat
					// Kill 1 profession, 1 sin
					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, false, true, randomNumber)
					};
					break;
				case 6:
					// 7 cultists
					nextYearTargets.NumberOfCultists = 7;

					// Kill goat
					// Kill 1 virtue, 1 profession, 1 sin
					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, false, true, randomNumber)
					};
					break;
				case 7:
					// Kill cow
					// Kill 1 virtue, 1 profession, 1 sin
					// 8 cultists
					break;
				case 8:
					// Kill goat
					// Kill 1 virtue, 1 profession, 1 sin
					break;
				case 9:
					// Kill cow
					// Kill 2 virtues, 1 profession, 1 sin
					break;
				case 10:
					// Kill goat
					// Kill 2 virtues, 2 professions, 1 sin
					break;
			}

			return nextYearTargets;
		}
	}
}
