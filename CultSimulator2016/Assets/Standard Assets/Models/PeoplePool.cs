﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Models
{
	class PeoplePool
	{
		public List<FullPerson> activePool;

		public Trait trailPool = new Trait();

		public List<FullPerson> searchPeople()
		{
			return new List<FullPerson>();
		}
	}
}
