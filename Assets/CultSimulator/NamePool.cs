using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CultSimulator
{
	class NamePool
	{
		public HashSet<string> usedNames;
		public List<string> lastNames;
		public List<string> femaleNames;
		public List<string> maleNames;


		public NamePool()
		{
			usedNames = new HashSet<string>();
			lastNames = new List<string>();
			femaleNames = new List<string>();
			maleNames = new List<string>();
			AddNames();
		}

		public string GetNextName(int? count, bool gender, Random randomNumber)
		{
			//sets an internal counter and escape
			count = (count ?? 0);
			if (count == 50)
			{
				return "";
			}

			string freshName = "";

			freshName = gender ? femaleNames[randomNumber.Next(0, femaleNames.Count)] : maleNames[randomNumber.Next(0, maleNames.Count)];
			freshName += " " + lastNames[randomNumber.Next(0, lastNames.Count)];

			if (usedNames.Contains(freshName))
			{
				return GetNextName(count + 1, gender, randomNumber);
			}
			else
			{
				usedNames.Add(freshName);
				return freshName;
			}
		}

		protected void AddNames()
		{
			lastNames.Add("Flegel");
			lastNames.Add("Boutin");
			lastNames.Add("Dusyk");
			lastNames.Add("Moersch");
			lastNames.Add("Test");
			lastNames.Add("test");
			lastNames.Add("test2");

			femaleNames.Add("Jeannine");
			femaleNames.Add("Danielle");
			femaleNames.Add("Jill");
			femaleNames.Add("Heather");
			femaleNames.Add("Johannes");
			femaleNames.Add("Ager");
			femaleNames.Add("test");


			maleNames.Add("Alex");
			maleNames.Add("Alexander");
			maleNames.Add("Chris");
			maleNames.Add("Christopher");
			maleNames.Add("Johannes");
			maleNames.Add("Farron");
			maleNames.Add("test");
		}

	}
}
