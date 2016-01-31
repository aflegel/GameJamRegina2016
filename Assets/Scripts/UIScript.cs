using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.CultSimulator;
using System.Linq;

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

	void Start()
	{
		SetButtons(Panel1, new[] { "Hello", "Cruel", "World" });
		SetButtons(Panel2, new[] { "Hello", "Cruel", "World" });
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

	private void SetButtons(GameObject panel, string[] text)
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
			newButton.GetComponent<Button>().onClick.AddListener(() => Debug.Log(text[index] + " Pressed"));
		}
		panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, text.Length * 30);
	}

	public void SetActiveCultist(int cultistIndex)
	{
		activeCultistIndex = cultistIndex;
	}
}
