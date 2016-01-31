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
					nextYearTargets.NumberOfCultists = 3;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 2:
					nextYearTargets.NumberOfCultists = 4;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 3:
					nextYearTargets.NumberOfCultists = 5;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 4:
					nextYearTargets.NumberOfCultists = 6;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 5:
					nextYearTargets.NumberOfCultists = 7;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 6:
					nextYearTargets.NumberOfCultists = 7;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 7:
					nextYearTargets.NumberOfCultists = 8;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 8:
					nextYearTargets.NumberOfCultists = 8;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 9:
					nextYearTargets.NumberOfCultists = 8;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
				case 10:
					nextYearTargets.NumberOfCultists = 8;

					nextYearTargets.SacrificeTargets = new List<SearchableAsset>()
					{
						TraitPool.GenerateRandomAsset(true, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, false, true, false, randomNumber),
						TraitPool.GenerateRandomAsset(false, true, false, false, randomNumber),
						TraitPool.GenerateRandomAsset(true, false, false, true, randomNumber)
					};
					break;
			}

			return nextYearTargets;
		}
	}
}
