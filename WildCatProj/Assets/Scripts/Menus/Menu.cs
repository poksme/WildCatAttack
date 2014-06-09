using UnityEngine;
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
		this.CurrentElement = -1;
		this.Animating = true;
		dirty = true;
		this.Ceiling.SetActive(true);

		GameObject fadeText = iTween.CameraFadeAdd();
		iTween.CameraFadeDepth(12);
		Color ncolor = fadeText.guiTexture.color;
		ncolor.a = 1.0f;
		fadeText.guiTexture.color = ncolor;
		iTween.CameraFadeTo(iTween.Hash(
			"amount", 0.0f,
			"time", 1.5f,
			"easetype", iTween.EaseType.easeOutExpo
		));
		//			"oncompletetarget", this.gameObject,
		//			"oncomplete", "OnStartAnimationDone"
	}
	
	private void Update () {
		int oldCurrentElement = this.CurrentElement;

		if (this.CurrentElement == -1) this.CurrentElement = 0;
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
			this.MenuElements[this.CurrentElement ].Select();
			if (oldCurrentElement >= 0)
				this.MenuElements[oldCurrentElement].UnSelect();
			this.Animating = true;
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
				"position", this.MenuElements[this.CurrentElement].CameraPosition.position,
				"looktarget", this.MenuElements[this.CurrentElement].CameraLookAt.position,
				"time", 1.0f,
				"looktime", 1.0f,
				"easetype", iTween.EaseType.easeOutQuad,
				"oncompletetarget", this.gameObject,
				"oncomplete", "OnTransitionDone"
			));
		}

		this.dirty = false;
	}

	private	void	OnTransitionDone() {
		this.Animating = false;
	}

	private	void	OnStartAnimationDone() {
		this.Animating = false;
	}
}
