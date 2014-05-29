using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public MenuElement[]	MenuElements;
	public float			CameraSpeed = 3f;
	public float			CameraDepth = 0.1f;

	private int				TotalElements;
	private int				CurrentElement;
	private Vector3			CurrentCameraLookAt;

	private void Start () {
		this.TotalElements = this.MenuElements.Length;
		this.CurrentElement = 0;
	}
	
	private void Update () {
		int oldCurrentElement = this.CurrentElement;
		bool dirty = false;
		if (Input.GetKeyDown("d")) {
			if (this.CurrentElement == (this.TotalElements - 1))
				this.CurrentElement = 0;
			else
				this.CurrentElement += 1;
			dirty = true;
		}
		else if (Input.GetKeyDown("a")) {
			if (this.CurrentElement == 0)
				this.CurrentElement = (this.TotalElements - 1);
			else
				this.CurrentElement -= 1;
			dirty = true;
		}
		//selection/deselection
		if (dirty) {
			this.MenuElements[oldCurrentElement].Select();
			this.MenuElements[this.CurrentElement].UnSelect();
		}

		
		//Camera movement
		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, this.MenuElements[this.CurrentElement].CameraPosition.position, CameraSpeed * Time.deltaTime);
		this.CurrentCameraLookAt = Vector3.Lerp(this.CurrentCameraLookAt, this.MenuElements[this.CurrentElement].CameraLookAt.position, CameraSpeed * Time.deltaTime);
		Vector3 pos = Camera.main.transform.position;
		pos.y += CameraDepth;
		Camera.main.transform.position = pos;
		Camera.main.transform.LookAt(this.CurrentCameraLookAt);
	}
}
