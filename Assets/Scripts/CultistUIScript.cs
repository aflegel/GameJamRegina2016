using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.CultSimulator;

public class CultistUIScript : MonoBehaviour
{
	public Text CultistName;

	public Text CultistProfession; 

	public Text CultistTraits;

	public GameObject EmptyText;

	public GameObject InformationBlock;

	private int lastCultistID = -1;

	public void SetCultistInformation(Person cultist)
	{
		if (cultist == null)
		{
			if (lastCultistID != -1)
			{
				InformationBlock.SetActive(false);
				EmptyText.SetActive(true);
				lastCultistID = -1;
			}
		}
		else if (cultist.PersonID != lastCultistID)
		{
			CultistName.text = cultist.Name;
			CultistProfession.text = cultist.assets.Profession.ToString();
			CultistTraits.text = cultist.assets.Virtue.ToString() + " and " + cultist.assets.Sin.ToString();
			InformationBlock.SetActive(true);
			EmptyText.SetActive(false);
			lastCultistID = cultist.PersonID;
		}
	}
}