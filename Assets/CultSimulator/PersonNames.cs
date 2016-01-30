using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	class PersonNames
	{
		public HashSet<string> usedNames;
		public List<string> lastNames;
		public List<string> firstNames;


		public PersonNames()
		{
			usedNames = new HashSet<string>();
			lastNames = new List<string>();
			firstNames = new List<string>();
			AddNames();
		}



		public string GetNextName(int? count)
		{
			//sets an internal counter and escape
			count = (count ?? 0);
			if(count == 50)
			{
				return "";
			}

			string freshName = "";

			Random x = new Random();
			int firstIndex = x.Next(0, firstNames.Count);
			int lastIndex = x.Next(0, firstNames.Count);

			freshName = firstNames[firstIndex] + " " + lastNames[lastIndex];

			if (usedNames.Contains(freshName))
			{
				return GetNextName(count);
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

			firstNames.Add("Alex");
			firstNames.Add("Alexander");
			firstNames.Add("Chris");
			firstNames.Add("Christopher");
			firstNames.Add("Johannes");
			firstNames.Add("testet");
			firstNames.Add("test");
		}

	}
}
