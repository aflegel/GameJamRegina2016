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

	public GameObject Cultist1;
	public GameObject Cultist2;
	public GameObject Cultist3;
	public GameObject Cultist4;
	public GameObject Cultist5;
	public GameObject Cultist6;
	public GameObject Cultist7;
	public GameObject Cultist8;

	private GameObject[] Cultists;

	private int activeCultistIndex;

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
		var cultists = GameState.GetCurrentCultists().ToArray();
		for (int i = 0; i < cultists.Length; ++i)
			Cultists[i].SetActive(cultists[i] != null);
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
