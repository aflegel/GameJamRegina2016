using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;

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
		[ProfessionDescription("Unemployed")]
		None = 0,
		[ProfessionDescription("Cardiologist")]
		[ProfessionDescription("Nurse")]
		[ProfessionDescription("Pathologist")]
		[ProfessionDescription("Surgeon")]
		[ProfessionDescription("Orderly")]
		[ProfessionDescription("Physician")]
		[ProfessionDescription("Pharmacist")]
		Medical = 1,
		[ProfessionDescription("Pastor")]
		[ProfessionDescription("Nun", Gender.Female)]
		[ProfessionDescription("Priest", Gender.Male)]
		[ProfessionDescription("Deacon")]
		[ProfessionDescription("Prioress", Gender.Female)]
		[ProfessionDescription("Chaplain")]
		[ProfessionDescription("Vicar")]
		Religious = 2,
		[ProfessionDescription("Chief of Police")]
		[ProfessionDescription("Detective")]
		[ProfessionDescription("Sheriff")]
		[ProfessionDescription("Firefighter")]
		[ProfessionDescription("Inspector")]
		[ProfessionDescription("Patrol Officer")]
		[ProfessionDescription("Deputy Chief")]
		Law = 3,
		[ProfessionDescription("Mayor")]
		[ProfessionDescription("Mayor’s Aid")]
		[ProfessionDescription("Councillor")]
		[ProfessionDescription("Treasurer")]
		[ProfessionDescription("County Clerk")]
		[ProfessionDescription("Governor")]
		[ProfessionDescription("Administrator")]
		Politics = 4,
		[ProfessionDescription("Plumber")]
		[ProfessionDescription("Carpenter")]
		[ProfessionDescription("Gravedigger")]
		[ProfessionDescription("Electrician")]
		[ProfessionDescription("Mechanic")]
		[ProfessionDescription("Trucker")]
		[ProfessionDescription("Welder")]
		Trades = 5,
		[ProfessionDescription("Banker")]
		[ProfessionDescription("Grocer")]
		[ProfessionDescription("Postmaster")]
		[ProfessionDescription("Cashier")]
		[ProfessionDescription("Butcher")]
		[ProfessionDescription("Liquor Store Manager")]
		[ProfessionDescription("Gas Station Manager")]
		Merchant = 6,
		[ProfessionDescription("English Teacher")]
		[ProfessionDescription("Math Teacher")]
		[ProfessionDescription("Librarian")]
		[ProfessionDescription("Band Teacher")]
		[ProfessionDescription("Principal")]
		[ProfessionDescription("Guidance Counselor")]
		[ProfessionDescription("Shop Teacher")]
		Educator = 7,
		[ProfessionDescription("Farmer")]
		[ProfessionDescription("Rancher")]
		[ProfessionDescription("Hermit")]
		[ProfessionDescription("Forester")]
		[ProfessionDescription("Dairy Hand")]
		[ProfessionDescription("Livestock Driver")]
		[ProfessionDescription("Orchard Worker")]
		Rural = 8,
		[ProfessionDescription("Cow", true)]
		Cow = 9,
		[ProfessionDescription("Goat", true)]
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

	public enum Gender
	{
		Male,
		Female
	}
}
