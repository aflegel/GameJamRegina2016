using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.CultSimulator;

public class CultistUIScript : MonoBehaviour
{
	public Text CultistName;

	public Text CultistProfession;

	public Text CultistTraits;

	public void SetCultistInformation(Person person)
	{
		if (person == null)
			gameObject.SetActive(false);
		else
		{
			CultistName.text = person.Name;
			CultistProfession.text = person.assets.profession.ToString();
			CultistTraits.text = person.assets.virtue.ToString() + " / " + person.assets.sin.ToString();
			gameObject.SetActive(true);
		}
	}
}