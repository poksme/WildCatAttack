using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	GameObject[]	MenuElements;
	int				TotalElements;
	int				CurrentElement;
	float			CameraSpeed;
	float			CameraDepth;

	// Use this for initialization
	void Start () {
		MenuElements = GameObject.FindGameObjectsWithTag ("MenuElement");
		TotalElements = MenuElements.Length;
		CurrentElement = 0;
		CameraSpeed = 3f;
		CameraDepth = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Current Element : " + CurrentElement + " Total : " + TotalElements);
		if (Input.GetKeyDown("d")) {
			if (CurrentElement == (TotalElements - 1))
				CurrentElement = 0;
			else
				CurrentElement += 1;
		}
		else if (Input.GetKeyDown("a")) {
			if (CurrentElement == 0)
				CurrentElement = (TotalElements - 1);
			else
				CurrentElement -= 1;
		}
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, MenuElements[CurrentElement].transform.position, CameraSpeed * Time.deltaTime);
		Vector3 pos = Camera.main.transform.position;
		pos.y += CameraDepth;
		Camera.main.transform.position = pos;
	}
}
