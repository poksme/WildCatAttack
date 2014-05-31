using UnityEngine;
using System.Collections;

public class SwitchOptions : MonoBehaviour {

	public	GameObject	Switch;
	public	Transform	Anchor;
	public	bool		Toggled;

	private	void	Start() {
		this.PlayToggleAnimation();
	}

	public	void	Toggle() {
		this.Toggled = !this.Toggled;
		this.PlayToggleAnimation();
	}

	private	void	PlayToggleAnimation() {
		Vector3 eulerAngles = this.Switch.transform.eulerAngles;
		if (this.Toggled) {
			eulerAngles.y = 50;
		} else {
			eulerAngles.y = -50;
		}
		this.Switch.transform.eulerAngles = eulerAngles;
	}
}
