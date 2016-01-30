using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	class PeoplePool
	{
		public Dictionary<int, FullPerson> activePool;
		public Dictionary<Trait, Dictionary<Trait, int>> traitPool;

		public PeoplePool()
		{
			activePool = new Dictionary<int, FullPerson>();
			traitPool = new Dictionary<Trait, Dictionary<Trait, int>>();
		}


		public Dictionary<int, FullPerson> searchPeople(SearchableAsset assets)
		{
			return new Dictionary<int, FullPerson>();
		}

		public Dictionary<int, FullPerson> searchPeopleByID(int id)
		{
			return new Dictionary<int, FullPerson>();
		}

		public int getTraitValue(SearchableAsset keyValues, SearchableAsset matchValues)
		{
			foreach(keyVaules. )

			return 0;
		}

		public int getTraitValue(Trait keyValue, Trait matchValue)
		{
			if (traitPool.ContainsKey(keyValue))
				return traitPool[keyValue].ContainsKey(matchValue) ? traitPool[keyValue][matchValue] : 0;
			else
				return traitPool[matchValue].ContainsKey(keyValue) ? traitPool[matchValue][keyValue] : 0;
		}
	}
}
