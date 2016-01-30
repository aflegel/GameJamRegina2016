using UnityEngine;
using System.Collections;

public class CultistScript : MonoBehaviour
{
	public UIScript UIController;

	public int CultistIndex;

	void Start()
	{

	}

	void Update()
	{

	}

	void OnMouseDown()
	{
		UIController.SetActiveCultist(CultistIndex);
	}
}
