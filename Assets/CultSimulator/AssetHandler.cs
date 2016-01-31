using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	public static class AssetHandler
	{
	}

	public struct SearchableAsset
	{
		public Profession Profession;
		public Sin Sin;
		public Virtue Virtue;
	}

	public enum Profession
	{
		None = 0,
		Medical = 1,
		Religious = 2,
		Law = 3,
		Politics = 4,
		Trades = 5,
		Merchant = 6,
		Educator = 7,
		Farmer = 8,
		Cow = 9,
		Goat = 10
	}

	public enum Sin
	{
		None = 0,
		Wrathful = 1,
		Proud = 2,
		Greedy = 3,
		Gluttonous = 4,
		Lazy = 5,
		Lusty = 6,
		Envious = 7
	}

	public enum Virtue
	{
		None = 10,
		Forgiving = 11,
		Humble = 12,
		Charitable = 13,
		Temperant = 14,
		Diligent = 15,
		Chaste = 16,
		Kind = 17
	}
}
