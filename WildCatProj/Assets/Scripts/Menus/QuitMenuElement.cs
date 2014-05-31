using UnityEngine;
using System.Collections;

public class QuitMenuElement : MonoBehaviour {

	//public attributes
	public	Transform	AnimationCameraAnchor;
	public	Transform	AnimationCameraLookAtAnchor;

	
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
			"position", AnimationCameraAnchor,
			"looktarget", AnimationCameraLookAtAnchor,
			"time", 0.5f,
			"looktime", 0.5f,
			"easetype", iTween.EaseType.easeOutQuad,
			"oncompletetarget", this.gameObject,
			"oncomplete", "OnAnimationDone"
			));
		iTween.CameraFadeAdd();
		iTween.CameraFadeDepth(12);
		iTween.CameraFadeTo(iTween.Hash(
			"amount", 1.0f,
			"time", 0.5f,
			"easetype", iTween.EaseType.easeInOutExpo,
			"oncompletetarget", this.gameObject,
			"oncomplete", "OnFadeOutDone"
			));
	}

	private	void	OnFadeOutDone() {
	}
	
	private	void	OnAnimationDone() {
		this.Animating = false;
		Application.Quit();
	}

}
