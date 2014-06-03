using UnityEngine;
using System.Collections;

public class PlayMenuElement : MonoBehaviour {

	//public attributes
	public	Transform	FirstStepCameraAnchor;
	public	Transform	FirstStepCameraLookAtAnchor;
	public	Transform	SecondStepCameraAnchor;
	public	Transform	SecondStepCameraLookAtAnchor;
	public	FutureDoor	Door;

	
	//private attributes
	private	bool		Animating = false;

	//private Unity Methods
	private	void	Start() {
		MenuElement me = this.GetComponent<MenuElement>();

		me.OnMenuAction += OnMenuAction;
	}

	private	void	OnMenuAction() {
		if (this.Animating) return;
		this.Animating = true;
		MenuElement me = this.GetComponent<MenuElement>();

		me.Focus();

		iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
			"position", FirstStepCameraAnchor,
			"looktarget", FirstStepCameraLookAtAnchor,
			"time", 0.5f,
			"looktime", 0.5f,
			"easetype", iTween.EaseType.easeOutQuad,
			"oncompletetarget", this.gameObject,
			"oncomplete", "OnFirstStepAnimationDone"
		));
	}
	
	private	void	OnFirstStepAnimationDone() {
		this.Door.Open();
		iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
			"position", SecondStepCameraAnchor,
			"looktarget", SecondStepCameraLookAtAnchor,
			"delay", 0.5f,
			"time", 0.6f,
			"looktime", 0.5f,
			"easetype", iTween.EaseType.easeOutQuad,
			"oncompletetarget", this.gameObject,
			"oncomplete", "OnSecondStepAnimationDone"
			));
		iTween.CameraFadeAdd();
		iTween.CameraFadeDepth(12);
		iTween.CameraFadeTo(iTween.Hash(
			"amount", 1.0f,
			"delay", 0.5f,
			"time", 0.5f,
			"easetype", iTween.EaseType.easeInOutExpo,
			"oncompletetarget", this.gameObject,
			"oncomplete", "OnFadeOutDone"
			));
	}

	private	void	OnFadeOutDone() {
//		iTween.CameraFadeDestroy();
	}

	private	void	OnSecondStepAnimationDone() {
		this.Animating = false;
		Application.LoadLevel("cedric");
	}
}
