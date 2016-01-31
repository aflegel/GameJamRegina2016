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

		public ProfessionDescriptionAttribute(string description)
		{
			Description = description;
		}

		public ProfessionDescriptionAttribute(string description, Gender gender)
		{
			Description = description;
			Gender = gender;
		}
	}
}
