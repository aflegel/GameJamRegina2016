﻿using System;
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
		public List<string> animalNames;


		public NamePool()
		{
			usedNames = new HashSet<string>();
			lastNames = new List<string>();
			femaleNames = new List<string>();
			maleNames = new List<string>();
			animalNames = new List<string>();
			AddNames();
		}

		public string GetNextName(int? count, Gender gender, bool animal, Random randomNumber)
		{
			//sets an internal counter and escape
			count = (count ?? 0);
			if (count == 50)
			{
				return "";
			}

			string freshName = "";

			if (animal)
			{
				return animalNames[randomNumber.Next(0, animalNames.Count)];
			}
			else
			{
				freshName = gender == Gender.Female ? femaleNames[randomNumber.Next(0, femaleNames.Count)] : maleNames[randomNumber.Next(0, maleNames.Count)];
				freshName += " " + lastNames[randomNumber.Next(0, lastNames.Count)];
			}

			if (usedNames.Contains(freshName))
			{
				return GetNextName(count + 1, gender, animal, randomNumber);
			}
			else
			{
				usedNames.Add(freshName);
				return freshName;
			}
		}

		protected void AddNames()
		{
			animalNames.Add("Bessy");
			animalNames.Add("Buddy");
			animalNames.Add("123456");
			animalNames.Add("Dutch");
			animalNames.Add("snickerdoodle");



			lastNames.Add("Flegel");
			lastNames.Add("Boutin");
			lastNames.Add("Dusyk");
			lastNames.Add("Moersch");
			lastNames.Add("Ager");
			lastNames.Add("test");
			lastNames.Add("test1");
			lastNames.Add("test2");
			lastNames.Add("test3");
			lastNames.Add("test4");
			lastNames.Add("test5");
			lastNames.Add("test6");
			lastNames.Add("test7");
			lastNames.Add("test8");
			lastNames.Add("test9");
			lastNames.Add("test2");
			lastNames.Add("test23");
			lastNames.Add("test24");
			lastNames.Add("test25");
			lastNames.Add("test26");
			lastNames.Add("test27");
			lastNames.Add("test28");
			lastNames.Add("test29");
			lastNames.Add("test20");

			femaleNames.Add("Jeannine");
			femaleNames.Add("Danielle");
			femaleNames.Add("Jill");
			femaleNames.Add("Heather");
			femaleNames.Add("Sarah");
			femaleNames.Add("Your Mom");


			maleNames.Add("Alex");
			maleNames.Add("Alexander");
			maleNames.Add("Chris");
			maleNames.Add("Christopher");
			maleNames.Add("Johannes");
			maleNames.Add("Farron");
			maleNames.Add("test");
			maleNames.Add("Kris");
		}

	}
}
