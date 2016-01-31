using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	public class YearTarget
	{
		public int NumberOfCultists { get; set; }
		public SearchableAsset[] SacrificeTargets { get; set; }
	}

	public static class YearTargetFactory
	{
		public static YearTarget GetNextYearTargets(int yearNumber)
		{
			var nextYearTargets = new YearTarget();
			Random randomNumber = new Random();

			switch (yearNumber)
			{
				case 1:

					break;
			}

			return nextYearTargets;
		}
	}
}
