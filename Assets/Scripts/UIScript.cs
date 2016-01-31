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

	public Text SubSubCenterText;

	public Button AssignButton;

	public GameObject SubSubPane;
	public Text SubSubName;
	public Text SubSubProfession;
	public Text SubSubFlavourText;
	public Button InvestigateButton;
	public Button AcceptButton;
	public Button ProgressButton;

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
			{
				SacraficeTargetIndex = -1;
				FindRecruits = false;
				FindSacrafices = false;
				SacraficeTarget = -1;
				RecruitTarget = -1;
			}
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
			{
				Recruiting = false;
				FindRecruits = false;
				FindSacrafices = false;
				SacraficeTarget = -1;
				RecruitTarget = -1;
			}
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

	private int RecruitTargetKey
	{
		get
		{
			return RecruitTarget - 1;
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

		AssignButton.onClick.AddListener(AssignTask);

		ProgressButton.onClick.AddListener(ProgressSeason);
	}

	private void AssignTask()
	{
		if (FindRecruits)
			GameState.SetCultistInstruction(ActionType.Investigate, -1, activeCultistIndex);
		else if (FindSacrafices)
			GameState.SetCultistInstruction(ActionType.Investigate, -1, activeCultistIndex);
		else if (RecruitTarget >= 0)
		{
			var recruits = GameState.GetCultistCandidates().ToArray();
			var currentRecruit = recruits[RecruitTarget];
			GameState.SetCultistInstruction(ActionType.Recruit, currentRecruit.PersonID, activeCultistIndex);
		}

		SubSubPane.SetActive(false);
		SubSubCenterText.text = String.Empty;
		SubSubFlavourText.text = String.Empty;
		SubSubName.text = String.Empty;
		SubSubProfession.text = String.Empty;
		activeCultistIndex = -1;
		CultistUIScript.SetCultistInformation(null);
	}

	private void ProgressSeason()
	{
		var cultists = GameState.GetCurrentCultists();

		GameState.IncrementSeason();

		for (int i = 0; i < cultists.Length; i++)
		{
			if (cultists[i] != null)
				cultists[i].Instruction = null;
		}
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
		BuildSubUI();
	}

	void BuildSubUI()
	{
		if (recruiting)
		{
			SubTitleText.text = "Recruit";
			SubConditionsText.text = "Increase your ranks";

			SetButtons(Panel2, new string[] { "Find Recruits" }.Concat(GameState.GetCultistCandidates().Select(p => GameState.GetPerson(p.PersonID).Name)).ToArray(),
			new UnityAction[] { () => FindRecruits = true }.Concat(GameState.GetCultistCandidates().Select((p, i) => new UnityAction(() => RecruitTarget = i))).ToArray());
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

			SetButtons(Panel2, new string[] { "Find Sacrafices" }.Concat(GameState.GetSacrificeCandidates().Select(p => GameState.GetPerson(p.PersonID).Name)).ToArray(),
			new UnityAction[] { () => FindSacrafices = true }.Concat(GameState.GetSacrificeCandidates().Select((p, i) => new UnityAction(() => SacraficeTarget = i))).ToArray());
		}
		BuildSubSubUI();
	}

	void BuildSubSubUI()
	{
		if (FindRecruits)
		{
			SubSubCenterText.text = "Find Recruits";
			SubSubPane.SetActive(false);
		}
		else if (FindSacrafices)
		{
			SubSubCenterText.text = "Find Sacrafices";
			SubSubPane.SetActive(false);
		}
		else if (RecruitTarget >= 0)
		{
			var recruit = GameState.GetCultistCandidates().ToArray()[RecruitTarget];
			var person = GameState.GetPerson(recruit.PersonID);
			SubSubName.text = person.Name;
			SubSubProfession.text = person.ProfessionDescription;
			if (recruit.IndepthInvestigated)
			{
				SubSubFlavourText.text = person.FlavourText;
				SubSubCenterText.text = String.Empty;
			}
			else
			{
				SubSubFlavourText.text = String.Empty;
				SubSubCenterText.text = "Investigate for more Information";
			}
			AcceptButton.GetComponentInChildren<Text>().text = "Recruit";
			SubSubPane.SetActive(true);
		}
		else if (SacraficeTarget >= 0)
		{
			var sacrafice = GameState.GetSacrificeCandidates().ToArray()[SacraficeTarget];
			var person = GameState.GetPerson(sacrafice.PersonID);
			SubSubName.text = person.Name;
			SubSubProfession.text = person.ProfessionDescription;
			if (sacrafice.IndepthInvestigated)
			{
				SubSubFlavourText.text = person.FlavourText;
				SubSubCenterText.text = String.Empty;
			}
			else
			{
				SubSubFlavourText.text = String.Empty;
				SubSubCenterText.text = "Investigate for more Information";
			}
			AcceptButton.GetComponentInChildren<Text>().text = "Sacrafice";
			SubSubPane.SetActive(true);
		}
		else
		{
			SubSubCenterText.text = String.Empty;
			SubSubPane.SetActive(false);
		}

		UpdateAssignButton();
	}

	void UpdateAssignButton()
	{
		var cultists = GameState.GetCurrentCultists();

		if (activeCultistIndex >= 0 && (FindRecruits || FindSacrafices || RecruitTarget >= 0 || SacraficeTarget >= 0) && cultists[activeCultistIndex].Instruction == null)
			AssignButton.interactable = true;
		else
			AssignButton.interactable = false;
	}

	void Update()
	{
		var cultists = GameState.GetCurrentCultists();
		for (int i = 0; i < cultists.Length; ++i)
			Cultists[i].gameObject.SetActive(cultists[i] != null);

		if (activeCultistIndex != -1 && cultists[activeCultistIndex] == null)
		{
			activeCultistIndex = -1;
			UpdateAssignButton();
		}

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
		UpdateAssignButton();
	}
}
