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

	private CultistScript[] Cultists;

	private int activeCultistIndex = -1;

	private YearTarget currentTarget;

	private bool recruiting = false;
	private bool Recruiting
	{
		get { return recruiting; }
		set
		{
			recruiting = value;
		}
	}

	private int sacraficeTargetIndex = -1;
	public int SacraficeTargetIndex
	{
		get { return sacraficeTargetIndex; }
		set
		{
			sacraficeTargetIndex = value;
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

	void BuildUI()
	{
		SetButtons(Panel1, new string[] { "Recruit" }.Concat(currentTarget.SacrificeTargets.Select((a, i) => GetNumberString(i) + "Sacrafice Target")).ToArray(),
		new UnityAction[] { () => Recruiting = true }.Concat(currentTarget.SacrificeTargets.Select((a, i) => new UnityAction(() => SacraficeTargetIndex = i))).ToArray());
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
			newButton.GetComponent<RectTransform>().position += new Vector3(0, i * -30, 0);
			newButton.transform.SetParent(panel.transform, false);
			newButton.GetComponentInChildren<Text>().text = text[i];

			var index = i;
			newButton.GetComponent<Button>().onClick.AddListener(actions[i]);
		}
		panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, text.Length * 30);
	}

	public void SetActiveCultist(int cultistIndex)
	{
		activeCultistIndex = cultistIndex;
	}
}
