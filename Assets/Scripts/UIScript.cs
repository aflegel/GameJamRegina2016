using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.CultSimulator;
using System.Linq;
using System;
using UnityEngine.Events;

public class UIScript : MonoBehaviour
{
	public GameObject Panel1;
	public GameObject Panel2;

	public GameObject ButtonPrefab;

	public GameStateManager GameState;

	public CultistUIScript CultistUIScript;

	public CultistScript Cultist1;
	public CultistScript Cultist2;
	public CultistScript Cultist3;
	public CultistScript Cultist4;
	public CultistScript Cultist5;
	public CultistScript Cultist6;
	public CultistScript Cultist7;
	public CultistScript Cultist8;

	public Text SubTitleText;
	public Text SubConditionsText;

	private CultistScript[] Cultists;

	private int activeCultistIndex = -1;

	private YearTarget currentTarget;

	private bool recruiting = false;
	private bool Recruiting
	{
		get { return recruiting; }
		set
		{
			if (recruiting == value)
				return;
			recruiting = value;
			if (recruiting)
				sacraficeTargetIndex = -1;
			BuildSubUI();
		}
	}

	private int sacraficeTargetIndex = -1;
	private int SacraficeTargetIndex
	{
		get { return sacraficeTargetIndex; }
		set
		{
			if (sacraficeTargetIndex == value)
				return;
			sacraficeTargetIndex = value;
			if (sacraficeTargetIndex >= 0)
				recruiting = false;
			BuildSubUI();
		}
	}

	private bool findRecruits;
	private bool FindRecruits
	{
		get { return findRecruits; }
		set
		{
			if (findRecruits == value)
				return;
			findRecruits = value;
			if (findRecruits)
			{
				FindSacrafices = false;
				SacraficeTarget = -1;
				RecruitTarget = -1;
			}
			BuildSubSubUI();
		}
	}

	private bool findSacrafices;
	private bool FindSacrafices
	{
		get { return findSacrafices; }
		set
		{
			if (findSacrafices == value)
				return;
			findSacrafices = value;
			if (findSacrafices)
			{
				FindRecruits = false;
				SacraficeTarget = -1;
				RecruitTarget = -1;
			}
			BuildSubSubUI();
		}
	}

	private int recruitTarget = -1;
	private int RecruitTarget
	{
		get { return recruitTarget; }
		set
		{
			if (recruitTarget == value)
				return;
			recruitTarget = value;
			if (recruitTarget >= 0)
			{
				FindRecruits = false;
				FindSacrafices = false;
				SacraficeTarget = -1;
			}
			BuildSubSubUI();
		}
	}

	private int sacraficeTarget = -1;
	private int SacraficeTarget
	{
		get { return sacraficeTarget; }
		set
		{
			if (sacraficeTarget == value)
				return;
			sacraficeTarget = value;
			if (sacraficeTarget >= 0)
			{
				FindRecruits = false;
				FindSacrafices = false;
				RecruitTarget = -1;
			}
			BuildSubSubUI();
		}
	}

	void Start()
	{
		Cultists = new[]
		{
			Cultist1,
			Cultist2,
			Cultist3,
			Cultist4,
			Cultist5,
			Cultist6,
			Cultist7,
			Cultist8
		};
		currentTarget = GameState.GetCurrentTarget();
		BuildTopUI();
	}

	private string GetNumberString(int number)
	{
		switch (number)
		{
			case 1:
				return "1st";
			case 2:
				return "2nd";
			case 3:
				return "3rd";
			default:
				return number + "th";
		}
	}

	void BuildTopUI()
	{
		SetButtons(Panel1, new string[] { "Recruit" }.Concat(currentTarget.SacrificeTargets.Select((a, i) => GetNumberString(i + 1) + " Sacrafice Target")).ToArray(),
		new UnityAction[] { () => Recruiting = true }.Concat(currentTarget.SacrificeTargets.Select((a, i) => new UnityAction(() => SacraficeTargetIndex = i))).ToArray());
	}

	void BuildSubUI()
	{
		if (recruiting)
		{
			SubTitleText.text = "Recruit";
			SubConditionsText.text = "Increase your ranks";

			SetButtons(Panel2, new string[] { "Find Recruits" }, new UnityAction[] { () => FindRecruits = true });
		}
		else if (sacraficeTargetIndex >= 0)
		{
			var target = currentTarget.SacrificeTargets.Skip(sacraficeTargetIndex).First();
			SubTitleText.text = GetNumberString(sacraficeTargetIndex + 1) + " Sacrafice Target";
			string conditions = String.Empty;
			if (target.Profession != Profession.None)
			{
				conditions += target.Profession.ToString();
				if (target.Sin != Sin.None || target.Virtue != Virtue.None)
					conditions += " and ";
			}
			if (target.Sin != Sin.None)
				conditions += target.Sin.ToString();
			if (target.Virtue != Virtue.None)
				conditions += target.Virtue.ToString();
			SubConditionsText.text = conditions;

			SetButtons(Panel2, new string[] { "Find Sacrafices" }, new UnityAction[] { () => FindSacrafices = true });
		}
	}

	void BuildSubSubUI()
	{
	}

	void Update()
	{
		var cultists = GameState.GetCurrentCultists();
		for (int i = 0; i < cultists.Length; ++i)
			Cultists[i].gameObject.SetActive(cultists[i] != null);

		if (activeCultistIndex != -1 && cultists[activeCultistIndex] == null)
			activeCultistIndex = -1;

		for (int i = 0; i < Cultists.Length; ++i)
		{
			if (i == activeCultistIndex)
				Cultists[i].SetActive();
			else
				Cultists[i].SetInactive();
		}

		if (activeCultistIndex == -1)
			CultistUIScript.SetCultistInformation(null);
		else
			CultistUIScript.SetCultistInformation(GameState.GetPerson(cultists[activeCultistIndex].PersonID));
	}

	private void SetButtons(GameObject panel, string[] text, UnityAction[] actions)
	{
		foreach (Transform transform in panel.transform)
			Destroy(transform);
		for (int i = 0; i < text.Length; ++i)
		{
			var newButton = GameObject.Instantiate(ButtonPrefab);
			newButton.GetComponent<RectTransform>().position += new Vector3(0, i * -40, 0);
			newButton.transform.SetParent(panel.transform, false);
			newButton.GetComponentInChildren<Text>().text = text[i];

			var index = i;
			newButton.GetComponent<Button>().onClick.AddListener(actions[i]);
		}
		panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, text.Length * 40);
	}

	public void SetActiveCultist(int cultistIndex)
	{
		activeCultistIndex = cultistIndex;
	}
}
