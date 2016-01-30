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
		none = 10,
		forgiveness = 11,
		humility = 12,
		charity = 13,
		temperance = 14,
		diligence = 15,
		chastity = 16,
		kindness = 17
	}
}
