using UnityEngine;
using System.Collections;

public class CultistScript : MonoBehaviour
{
	public UIScript UIController;

	public int CultistIndex;

	private SpriteRenderer spriteRender;

	private bool isActive = false;
	private bool isMouseOver = false;

	public void SetActive()
	{
		isActive = true;
		UpdateColour();
	}

	public void SetInactive()
	{
		isActive = false;
		UpdateColour();
	}

	void Start()
	{
		spriteRender = GetComponent<SpriteRenderer>();
		UpdateColour();
	}

	void OnMouseDown()
	{
		UIController.SetActiveCultist(CultistIndex);
	}

	void OnMouseEnter()
	{
		isMouseOver = true;
		UpdateColour();
	}

	void OnMouseExit()
	{
		isMouseOver = false;
		UpdateColour();
	}

	private void UpdateColour()
	{
		if (isActive)
			spriteRender.color = new Color(1.0f, 1.0f, 1.0f);
		else if (isMouseOver)
			spriteRender.color = new Color(0.8f, 0.8f, 0.8f);
		else
			spriteRender.color = new Color(0.6f, 0.6f, 0.6f);
	}
}
