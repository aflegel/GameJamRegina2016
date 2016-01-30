using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	void Update()
	{
		Camera.main.orthographicSize = 10.0f / Screen.width * Screen.height;
	}
}
