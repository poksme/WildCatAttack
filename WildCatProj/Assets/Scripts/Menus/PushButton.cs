using UnityEngine;
using System.Collections;

public class PushButton : MonoBehaviour {

	public	AudioClip	PushSound;

	public	void		Push() {
		this.GetComponent<Animator>().SetTrigger("Push");
		if (this.PushSound) {
			SoundChannelManager.GetInstance().PlayClipAtPoint(this.PushSound, this.transform);
		}
	}
}
