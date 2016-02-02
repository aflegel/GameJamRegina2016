using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

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

		public string GetNextName(int? count, Gender gender, bool animal, System.Random randomNumber)
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

			lastNames = BuildNamesFromFile("lastNames");
			maleNames = BuildNamesFromFile("maleNames");
			femaleNames = BuildNamesFromFile("femaleNames");
			animalNames = BuildNamesFromFile("animalNames");
		}

		public List<string> BuildNamesFromFile(string filename)
		{
			List<string> nameList = new List<string>();

			TextAsset file = (TextAsset) Resources.Load(filename);
			StreamReader reader = new StreamReader(new MemoryStream(file.bytes), Encoding.Default);

			string regLine = ",(?=(?:[^" + '"' + "]*" + '"' + "[^" + '"' + "]*" + '"' + ")*[^" + '"' + "]*$)";

			//strip header line
			reader.ReadLine().Split(',');

			//loop for remaining enteries
			while (!reader.EndOfStream)
			{
				string[] input = Regex.Split(reader.ReadLine(), regLine);

				if (input[0] != null && input[0].Trim().Length > 0)
					nameList.Add(input[0]);
			}
			reader.Close();

			return nameList;
		}

	}
}
