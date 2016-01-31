using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Assets.CultSimulator
{
	class FlavourPool
	{
		public Dictionary<InvestigationMap, string> ProfessionPool { get; set; }

		//public Dictionary<InvestigationMap, string> ProfessionPool { get; set; }

		public FlavourPool()
		{
			ProfessionPool = new Dictionary<InvestigationMap, string>();
			GenerateTraits();
		}

		public string GetInvestigationValue(Profession profession, SuccessRating rating)
		{
			InvestigationMap getKey = new InvestigationMap(profession, rating);

			if (ProfessionPool.ContainsKey(getKey))
				return ProfessionPool[getKey];
			else
				return "";
		}
		public void GenerateTraits()
		{
			string filename = "professionFlavour";

			StreamReader reader = new StreamReader(@"Assets\TextAssets\" + filename + ".csv", Encoding.Default);
			string regLine = ",(?=(?:[^" + '"' + "]*" + '"' + "[^" + '"' + "]*" + '"' + ")*[^" + '"' + "]*$)";

			//strip header line
			reader.ReadLine().Split(',');

			//loop for remaining enteries
			while (!reader.EndOfStream)
			{
				string[] input = Regex.Split(reader.ReadLine(), regLine);


				//object secondType = null;
				//if(Enum.Try Parse(typeof(SuccessRating), input[0]))


				if (input[0] != null && input[0].Trim().Length > 0)
					ProfessionPool.Add(new InvestigationMap(Enum.Parse(typeof(Profession),input[0]), Enum.Parse(typeof(SuccessRating), input[0])), input[2]);
			}
			reader.Close();


		}

	}

	struct InvestigationMap
	{
		object firstKey;
		object secondKey;

		public InvestigationMap(object key1, object key2)
		{
			firstKey = key1;
			secondKey = key2;
		}

		public override int GetHashCode()
		{
			return firstKey.GetHashCode() + secondKey.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Equals(firstKey, secondKey);
		}
	}
}
