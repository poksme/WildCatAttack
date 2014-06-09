﻿using UnityEngine;
using System.Collections;

public class SwitchOptions : MonoBehaviour {

	public	GameObject	Switch;
	public	Transform	Anchor;
	public	bool		Toggled;
	public	AudioClip	ToggleSound;

	private	void	Start() {
		this.PlayToggleAnimation(false);
	}

	public	void	Toggle() {
		this.Toggled = !this.Toggled;
		this.PlayToggleAnimation(true);
	}

	private	void	PlayToggleAnimation(bool playSound) {
		Vector3 eulerAngles = this.Switch.transform.eulerAngles;
		if (this.Toggled) {
			eulerAngles.y = 50;
		} else {
			eulerAngles.y = -50;
		}
		this.Switch.transform.eulerAngles = eulerAngles;
		if (playSound) AudioSource.PlayClipAtPoint(this.ToggleSound, this.transform.position);
	}
}
