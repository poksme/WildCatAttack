﻿using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public MenuElement[]	MenuElements;
	public float			CameraSpeed = 3f;
	public float			CameraLookAtSpeed = 5f;
	public float			CameraDepth = 0.1f;
	public GameObject		Ceiling;
//	public Transform		CameraLookAtTransform;

	private int				TotalElements;
	private int				CurrentElement;
	private Vector3			CurrentCameraLookAt;
	private	bool			Animating;
	private	bool			dirty;

	private void Start () {
		this.TotalElements = this.MenuElements.Length;
		this.CurrentElement = 0;
		this.Animating = false;
		dirty = true;
		this.Ceiling.SetActive(true);
	}
	
	private void Update () {
		int oldCurrentElement = this.CurrentElement;

		if (!this.MenuElements[this.CurrentElement].HasFocus) {
			if (Input.GetKeyDown("d") || Input.GetKeyDown("l")) {
				if (this.CurrentElement == (this.TotalElements - 1))
					this.CurrentElement = 0;
				else
					this.CurrentElement += 1;
				dirty = true;
			}
			else if (Input.GetKeyDown("a") || Input.GetKeyDown("j")) {
				if (this.CurrentElement == 0)
					this.CurrentElement = (this.TotalElements - 1);
				else
					this.CurrentElement -= 1;
				dirty = true;
			}
			else if (Input.GetKeyDown(KeyCode.Return)) {
				if (!this.Animating) {
					this.MenuElements[this.CurrentElement].MenuAction();
				}
			}
		}
		//selection/deselection
		if (dirty) {
			this.MenuElements[oldCurrentElement].Select();
			this.MenuElements[this.CurrentElement].UnSelect();
			this.Animating = true;
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
				"position", this.MenuElements[this.CurrentElement].CameraPosition.position,
				"looktarget", this.MenuElements[this.CurrentElement].CameraLookAt.position,
				"time", 1.0f,
				"looktime", 1.0f,
				"easetype", iTween.EaseType.easeOutQuad,
				"oncompletetarget", this.gameObject,
				"oncomplete", "OnAnimationComplete"
			));
		}

		this.dirty = false;

		//Camera movement
//		Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, this.MenuElements[this.CurrentElement].CameraPosition.position, CameraSpeed * Time.deltaTime);
//		this.CurrentCameraLookAt = Vector3.Lerp(this.CurrentCameraLookAt, this.MenuElements[this.CurrentElement].CameraLookAt.position, CameraLookAtSpeed * Time.deltaTime);
//		Camera.main.transform.LookAt(this.CurrentCameraLookAt);
//		Camera.main.transform.LookAt(this.CameraLookAtTransform.position);
	}

	private	void	OnAnimationComplete() {
		this.Animating = false;
	}
}