using UnityEngine;
using System.Collections;

public class PushButton : MonoBehaviour {

	public	void		Push() {
		this.GetComponent<Animator>().SetTrigger("Push");
	}
}
