using UnityEngine;
using System.Collections;

public class FutureDoor : MonoBehaviour {

	public	GameObject[]	anchors;

	public	bool		isOpen {
		get; private set;
	}

	public	void	Open() {
		if (this.isOpen) return;
		this.isOpen = true;
		foreach (GameObject go in this.anchors) {
			go.GetComponent<Animator>().SetBool("Open", this.isOpen);
		}
	}
}
