using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	public struct FullPerson
	{
		public int personID;
		public Person person;
		public SearchableAsset assets;
	}

	public struct SearchableAsset
	{
		public Profession profession;
		public List<Trait> traits;
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
		farmer = 8
	}

	public enum Trait
	{
		none = 0,
		wrath = 1,
		pride = 2,
		greed = 3,
		gluttony = 4,
		sloth = 5,
		lust = 6,
		envy = 7,
		forgiveness = 8,
		humility = 9,
		charity = 10,
		temperance = 11,
		diligence = 12,
		chastity = 13,
		kindness = 14

	}
}
