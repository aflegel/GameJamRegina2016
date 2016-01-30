using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour
{
	public GameObject Panel1;

	public GameObject ButtonPrefab;

	void Start()
	{
		for (int i = 0; i < 10; ++i)
		{
			var newButton = GameObject.Instantiate(ButtonPrefab);
			newButton.GetComponent<RectTransform>().position += new Vector3(0, i * -30, 0);
			newButton.transform.parent = Panel1.transform;
		}
		Panel1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 10 * 30);
	}

	void Update()
	{

	}
}
