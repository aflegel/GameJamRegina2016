using UnityEngine;
using System.Collections;

public class CultistScript : MonoBehaviour
{
	public UIScript UIController;

	public int CultistIndex;

	void OnMouseDown()
	{
		UIController.SetActiveCultist(CultistIndex);
	}
}
