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

	public Text SkillSuccess;

	public GameObject ResultsPanel;
	public Text ResultsText;
	public Button ResultsCloseButton;

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
				SacrificeTargetIndex = -1;
				FindRecruits = false;
				FindSacrifices = false;
				SacrificeTarget = -1;
				RecruitTarget = -1;
			}
			BuildSubUI();
		}
	}

	private int sacrificeTargetIndex = -1;
	private int SacrificeTargetIndex
	{
		get { return sacrificeTargetIndex; }
		set
		{
			if (sacrificeTargetIndex == value)
				return;
			sacrificeTargetIndex = value;
			if (sacrificeTargetIndex >= 0)
			{
				Recruiting = false;
				FindRecruits = false;
				FindSacrifices = false;
				SacrificeTarget = -1;
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
				FindSacrifices = false;
				SacrificeTarget = -1;
				RecruitTarget = -1;
			}
			BuildSubSubUI();
		}
	}

	private bool findSacrifices;
	private bool FindSacrifices
	{
		get { return findSacrifices; }
		set
		{
			if (findSacrifices == value)
				return;
			findSacrifices = value;
			if (findSacrifices)
			{
				FindRecruits = false;
				SacrificeTarget = -1;
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
				FindSacrifices = false;
				SacrificeTarget = -1;
			}
			BuildSubSubUI();
		}
	}

	private int sacrificeTarget = -1;
	private int SacrificeTarget
	{
		get { return sacrificeTarget; }
		set
		{
			if (sacrificeTarget == value)
				return;
			sacrificeTarget = value;
			if (sacrificeTarget >= 0)
			{
				FindRecruits = false;
				FindSacrifices = false;
				RecruitTarget = -1;
			}
			BuildSubSubUI();
		}
	}

	private bool Investigating { get; set; }

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

		InvestigateButton.onClick.AddListener(SetInvestigating);

		ResultsPanel.SetActive(false);
		ResultsCloseButton.onClick.AddListener(CloseResults);
	}

	private void SetInvestigating()
	{
		Investigating = true;
	}

	private void CloseResults()
	{
		ResultsPanel.SetActive(false);
		ResultsText.text = String.Empty;
	}

	private void AssignTask()
	{
		if (FindRecruits)
			GameState.SetCultistInstruction(ActionType.Investigate, null, activeCultistIndex);
		else if (FindSacrifices)
			GameState.SetCultistInstruction(ActionType.Investigate, null, activeCultistIndex);
		else if (Investigating && (RecruitTarget >= 0 || SacrificeTarget >= 0))
		{
			int investigationTarget;

			if (RecruitTarget >= 0 && SacrificeTarget < 0)
			{
				var recruits = GameState.GetCultistCandidates().ToArray();
				var currentRecruit = recruits[RecruitTarget];

				investigationTarget = currentRecruit.PersonID;
			}
			else
			{
				var sacrifices = GameState.GetCultistCandidates().ToArray();
				var currentSacrifice = sacrifices[SacrificeTarget];

				investigationTarget = currentSacrifice.PersonID;
			}

			GameState.SetCultistInstruction(ActionType.Investigate, investigationTarget, activeCultistIndex);
		}
		else if (RecruitTarget >= 0)
		{
			var recruits = GameState.GetCultistCandidates().ToArray();
			var currentRecruit = recruits[RecruitTarget];
			GameState.SetCultistInstruction(ActionType.Recruit, currentRecruit.PersonID, activeCultistIndex);
		}
		else if (SacrificeTarget >= 0)
		{
			var sacrifices = GameState.GetSacrificeCandidates().ToArray();
			var currentSacrifice = sacrifices[SacrificeTarget];
			GameState.SetCultistInstruction(ActionType.Abduct, currentSacrifice.PersonID, activeCultistIndex);
		}

		var cultist = GameState.GetCultist(activeCultistIndex);

		System.Random randomNumber = new System.Random();

		SkillSuccess.text = GameState.GetSkillText(cultist, randomNumber);

		SubSubPane.SetActive(false);
		SubSubCenterText.text = String.Empty;
		SubSubFlavourText.text = String.Empty;
		SubSubName.text = String.Empty;
		SubSubProfession.text = String.Empty;
		activeCultistIndex = -1;
		CultistUIScript.SetCultistInformation(null);
		Investigating = false;
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

		Investigating = false;

		ResultsPanel.SetActive(true);
		ResultsText.text = String.Empty;

		foreach (string result in GameState.ResultTextList)
		{
			ResultsText.text += result + Environment.NewLine;
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
		SetButtons(Panel1, new string[] { "Recruit" }.Concat(currentTarget.SacrificeTargets.Select((a, i) => GetNumberString(i + 1) + " Sacrifice Target")).ToArray(),
		new UnityAction[] { () => Recruiting = true }.Concat(currentTarget.SacrificeTargets.Select((a, i) => new UnityAction(() => SacrificeTargetIndex = i))).ToArray());
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
		else if (sacrificeTargetIndex >= 0)
		{
			var target = currentTarget.SacrificeTargets.Skip(sacrificeTargetIndex).First();
			SubTitleText.text = GetNumberString(sacrificeTargetIndex + 1) + " Sacrifice Target";
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

			SetButtons(Panel2, new string[] { "Find Sacrifices" }.Concat(GameState.GetSacrificeCandidates().Select(p => GameState.GetPerson(p.PersonID).Name)).ToArray(),
			new UnityAction[] { () => FindSacrifices = true }.Concat(GameState.GetSacrificeCandidates().Select((p, i) => new UnityAction(() => SacrificeTarget = i))).ToArray());
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
		else if (FindSacrifices)
		{
			SubSubCenterText.text = "Find Sacrifices";
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
		else if (SacrificeTarget >= 0)
		{
			var sacrifice = GameState.GetSacrificeCandidates().ToArray()[SacrificeTarget];
			var person = GameState.GetPerson(sacrifice.PersonID);
			SubSubName.text = person.Name;
			SubSubProfession.text = person.ProfessionDescription;
			if (sacrifice.IndepthInvestigated)
			{
				SubSubFlavourText.text = person.FlavourText;
				SubSubCenterText.text = String.Empty;
			}
			else
			{
				SubSubFlavourText.text = String.Empty;
				SubSubCenterText.text = "Investigate for more Information";
			}
			AcceptButton.GetComponentInChildren<Text>().text = "Sacrifice";
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

		if (activeCultistIndex >= 0 && (FindRecruits || FindSacrifices || RecruitTarget >= 0 || SacrificeTarget >= 0) && cultists[activeCultistIndex].Instruction == null)
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
