using UnityEngine;
using System.Collections;

public class OpionsMenuElement : MonoBehaviour {

	//public attributes
	public	SwitchOptions[]	SwitchAnchors;
	

	//private attributes
	private	bool		Animating = false;
	private	bool		MonitorHasFocus = false;
	private	int			CurrentSwitch;

	private MenuElement	menuElement;

	//private Unity Methods
	private	void	Start() {
		this.CurrentSwitch = 0;
		this.menuElement = this.GetComponent<MenuElement>();
		
		this.menuElement.OnMenuAction += OnMenuAction;
	}


	private	void	Update() {
		bool dirty = false;
		if (this.MonitorHasFocus && !this.Animating) {
			//detect keys
			if (Input.GetKeyDown(KeyCode.Escape)) {
				this.UnFocusMonitor();
			}
			else if (Input.GetKeyDown(KeyCode.Return)) {
				this.SwitchAnchors[this.CurrentSwitch].Toggle();
			}
			else if (Input.GetKeyDown("d") || Input.GetKeyDown("l")) {
				if (this.CurrentSwitch == (this.SwitchAnchors.Length - 1))
					this.CurrentSwitch = 0;
				else
					this.CurrentSwitch += 1;
				dirty = true;
			}
			else if (Input.GetKeyDown("a") || Input.GetKeyDown("j")) {
				if (this.CurrentSwitch == 0)
					this.CurrentSwitch = (this.SwitchAnchors.Length - 1);
				else
					this.CurrentSwitch -= 1;
				dirty = true;
			}
		}
		if (dirty) {
			this.Animating = true;
			iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
				"position", SwitchAnchors[this.CurrentSwitch].Anchor.position,
				"looktarget", SwitchAnchors[this.CurrentSwitch].Anchor.position + SwitchAnchors[this.CurrentSwitch].Anchor.forward,
				"time", 0.5f,
				"looktime", 0.5f,
				"easetype", iTween.EaseType.easeOutQuad,
				"oncompletetarget", this.gameObject,
				"oncomplete", "OnMoveSwitchAnimationDone"
				));
		}
	}

	//private callbacks
	private	void	OnMenuAction() {
		if (this.Animating) return;
		this.Animating = true;
		
		this.menuElement.Focus();
		
		iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
			"position", SwitchAnchors[0].Anchor.position,
			"looktarget", SwitchAnchors[0].Anchor.position + SwitchAnchors[0].Anchor.forward,
			"time", 0.5f,
			"looktime", 0.5f,
			"easetype", iTween.EaseType.easeOutQuad,
			"oncompletetarget", this.gameObject,
			"oncomplete", "OnCameraSetupAnimationDone"
			));
	}

	private	void	UnFocusMonitor() {
		if (this.Animating) return;
		this.Animating = true;

		iTween.MoveTo(Camera.main.gameObject, iTween.Hash(
			"position", this.menuElement.CameraPosition,
			"looktarget", this.menuElement.CameraLookAt,
			"time", 0.5f,
			"looktime", 0.5f,
			"easetype", iTween.EaseType.easeOutQuad,
			"oncompletetarget", this.gameObject,
			"oncomplete", "OnCameraUnSetupAnimationDone"
			));
	}

	private	void	OnMoveSwitchAnimationDone() {
		this.Animating = false;
	}

	private void	OnCameraSetupAnimationDone() {
		this.MonitorHasFocus = true;
		this.Animating = false;

		StartCoroutine(this.MonitorSetupCoroutine());
	}

	private void	OnCameraUnSetupAnimationDone() {
		this.Animating = false;
		this.MonitorHasFocus = false;

		this.menuElement.UnFocus();

		StartCoroutine(this.MonitorUnSetupCoroutine());
	}

	private IEnumerator		MonitorSetupCoroutine() {
		yield return null;
	}

	private IEnumerator		MonitorUnSetupCoroutine() {
		yield return null;
	}

}
