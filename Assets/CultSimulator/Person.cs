using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	public class Person
	{
		public int PersonID { get; set; }
		public bool Active { get; set; }
		public string Name { get; set; }
		public string FlavourText { get; set; }
		public double Abduction { get; set; }
		public double AbductionDefense { get; set; }
		public double Investigation { get; set; }
		public double InvestigationDefense { get; set; }
		public double Recruitment { get; set; }
		public double RecruitmentDefense { get; set; }
		public bool Gender { get; set; }

		public SearchableAsset assets;
	}

	public struct SearchableAsset
	{
		public Profession profession;
		public Sin sin;
		public Virtue virtue;
	}

	public enum Profession
	{
		none = 0,
		medical = 1,
		religious = 2,
		law = 3,
		politics = 4,
		trades = 5,
		merchant = 6,
		educator = 7,
		farmer = 8,
		cow = 9,
		goat = 10
	}

	public enum Sin
	{
		none = 0,
		wrath = 1,
		pride = 2,
		greed = 3,
		gluttony = 4,
		sloth = 5,
		lust = 6,
		envy = 7
	}

	public enum Virtue
	{
		none = 0,
		forgiveness = 1,
		humility = 2,
		charity = 3,
		temperance = 4,
		diligence = 5,
		chastity = 6,
		kindness = 7
	}
}
