using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	class Trait
	{

		public Dictionary<trait, Dictionary<trait, int>> traitMapping;


		public int getValue(trait val1, trait val2)
		{
			if (traitMapping.ContainsKey(val1))
				return traitMapping[val1].ContainsKey(val2) ? traitMapping[val1][val2] : 0;
			else
				return traitMapping[val2].ContainsKey(val1) ? traitMapping[val2][val1] : 0;
		}

		public void initialize()
		{

		}

	}
}
