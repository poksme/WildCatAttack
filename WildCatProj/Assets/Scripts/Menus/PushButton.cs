using UnityEngine;
using System.Collections;

public class PushButton : MonoBehaviour {

	public	AudioClip	PushSound;

	public	void		Push() {
		this.GetComponent<Animator>().SetTrigger("Push");
		if (this.PushSound) {
			AudioSource.PlayClipAtPoint(this.PushSound, this.transform.position);
		}
	}
}
