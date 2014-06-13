using UnityEngine;
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

		// Sound logic
		if (this.name == "SoundController") {
			OptionManager.GetInstance().soundIsMuted = !this.Toggled;
			if (playSound)
				SoundChannelManager.GetInstance().PlayClipAtPoint(this.ToggleSound, this.transform);
		} else if (this.name == "MusicController") {
			OptionManager.GetInstance().musicIsMuted = !this.Toggled;
			if (playSound)
				SoundChannelManager.GetInstance().PlayClipAtPoint(this.ToggleSound, this.transform);
		}
	}
}
