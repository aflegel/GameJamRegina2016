using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
	public GameObject Panel1;
	public GameObject Panel2;

	public GameObject ButtonPrefab;

	public CultistUIScript CultistUIScript;

	void Start()
	{
		SetButtons(Panel1, new[] { "Hello", "Cruel", "World" });
		SetButtons(Panel2, new[] { "Hello", "Cruel", "World" });
	}

	void Update()
	{

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
}
