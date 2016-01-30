using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	class PeoplePool
	{
		public Dictionary<int, FullPerson> activePool;
		public Dictionary<Trait, Dictionary<Trait, int>> traitMapping;

		public Trait trailPool = new Trait();

		public Dictionary<int, FullPerson> searchPeople(SearchableAsset assets)
		{
			return new Dictionary<int, FullPerson>();
		}

		public Dictionary<int, FullPerson> searchPeopleByID(int id)
		{
			return new Dictionary<int, FullPerson>();
		}

		public int getTraitValue(Trait keyValue, Trait matchValue)
		{
			if (traitMapping.ContainsKey(keyValue))
				return traitMapping[keyValue].ContainsKey(matchValue) ? traitMapping[keyValue][matchValue] : 0;
			else
				return traitMapping[matchValue].ContainsKey(keyValue) ? traitMapping[matchValue][keyValue] : 0;
		}

		public void initialize()
		{

		}
	}
}
