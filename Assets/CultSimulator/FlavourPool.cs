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
		public List<string> AmazingList { get; set; }
		public List<string> GoodList { get; set; }
		public List<string> NormalList { get; set; }
		public List<string> BadList { get; set; }
		public List<string> TerribleList { get; set; }

		public List<string> AmazingListTarget { get; set; }
		public List<string> GoodListTarget { get; set; }
		public List<string> NormalListTarget { get; set; }
		public List<string> BadListTarget { get; set; }
		public List<string> TerribleListTarget { get; set; }

		public Dictionary<SuccessRating, string> SuccessPool {get; set; }

		public FlavourPool()
		{
			ProfessionPool = new Dictionary<InvestigationMap, string>();
			AmazingList = new List<string>();
			GoodList = new List<string>();
			NormalList = new List<string>();
			BadList = new List<string>();
			TerribleList = new List<string>();

			AmazingListTarget = new List<string>();
			GoodListTarget = new List<string>();
			NormalListTarget = new List<string>();
			BadListTarget = new List<string>();
			TerribleListTarget = new List<string>();

			SuccessPool = new Dictionary<SuccessRating, string>();

			GenerateText();
			//GeneratePools();
		}

		public void GenerateText()
		{

			AmazingList.Add("I am the best there is for this");
			AmazingList.Add("I am in my element");
			AmazingList.Add("there really isn’t a better fit for me");

			GoodList.Add("I should be well qualified for this");
			GoodList.Add("I am confident in my success");
			GoodList.Add("I think I’m a pretty good fit for this");

			NormalList.Add("I guess I can do this");
			NormalList.Add("this might work for me");
			NormalList.Add("I might be able to finish this");

			BadList.Add("I am not well qualified for this");
			BadList.Add("I am not confident in my success");
			BadList.Add("I think I could be better used elsewhere");

			TerribleList.Add("I am not at all qualified for this");
			TerribleList.Add("I am way out of my element");
			TerribleList.Add("there really isn’t a worse fit for me");

			AmazingListTarget.Add("This burden will result in certain victory");
			AmazingListTarget.Add("Your demands are effortless");
			AmazingListTarget.Add("Completion of the task ahead is promising");

			GoodListTarget.Add("This burden will come with its simplicities");
			GoodListTarget.Add("Your demands are feasible");
			GoodListTarget.Add("Completion of the task ahead is doubtless");

			NormalListTarget.Add("This burden’s results are yet unclear");
			NormalListTarget.Add("Your demands are fair");
			NormalListTarget.Add("Completion of the task ahead is uncertain");

			BadListTarget.Add("This burden will come with its challenges");
			BadListTarget.Add("Your demands are unfeasible");
			BadListTarget.Add("Completion of the task ahead is doubtful");

			TerribleListTarget.Add("This burden will result in certain death");
			TerribleListTarget.Add("Your demands are suicide");
			TerribleListTarget.Add("Completion of the task ahead may be hopeless");



			SuccessPool.Add(SuccessRating.GreatSuccess, "1");
			SuccessPool.Add(SuccessRating.GoodSuccess, "2");
			SuccessPool.Add(SuccessRating.NormalSuccess, "3");
			SuccessPool.Add(SuccessRating.Failure, "4");
			SuccessPool.Add(SuccessRating.BadFailure, "5");
			SuccessPool.Add(SuccessRating.TerribleFailure, "6");

		}

		public string GetInvestigationValue(object profession, object virtueSin)
		{
			InvestigationMap getKey = new InvestigationMap(profession, virtueSin);

			if (ProfessionPool.ContainsKey(getKey))
				return ProfessionPool[getKey];
			else
				return "";
		}

		public void GeneratePools()
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


				object secondType = null;
				try
				{
					secondType = Enum.Parse(typeof(Sin), input[0]);
				}
				catch
				{
					secondType = Enum.Parse(typeof(Virtue), input[0]);
				}


				if (input[0] != null && input[0].Trim().Length > 0)
					ProfessionPool.Add(new InvestigationMap(Enum.Parse(typeof(Profession), input[1]), secondType), input[2]);
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
