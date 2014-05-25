using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionInChildren(Collision collision) {
		if (collision.collider.gameObject.tag.Equals("Player2")) { // Need a more generic
			if (collision.collider.rigidbody) // The feet don't have rigidbody this can be a problem
				collision.collider.rigidbody.AddForce(collision.rigidbody.velocity * 10f, ForceMode.Impulse);
		}
	}
}
