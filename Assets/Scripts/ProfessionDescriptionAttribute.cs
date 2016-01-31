using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.CultSimulator;

namespace Assets.Scripts
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class ProfessionDescriptionAttribute : Attribute
	{
		public string Description { get; private set; }

		public Gender? Gender { get; private set; }

		public bool IsAnimal { get; private set; }

		public ProfessionDescriptionAttribute(string description)
		{
			Description = description;
			IsAnimal = false;
		}

		public ProfessionDescriptionAttribute(string description, Gender gender)
		{
			Description = description;
			Gender = gender;
			IsAnimal = false;
		}

		public ProfessionDescriptionAttribute(string description, bool isAnimal)
		{
			Description = description;
			IsAnimal = isAnimal;
		}

		public ProfessionDescriptionAttribute(bool isAnimal)
		{
			IsAnimal = isAnimal;
		}
	}
}
