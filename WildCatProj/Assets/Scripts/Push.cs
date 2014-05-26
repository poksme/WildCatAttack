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
		if ((transform.CompareTag("Player1") && collision.collider.gameObject.tag.Equals("Player2")) || 
		    (transform.CompareTag("Player2") && collision.collider.gameObject.tag.Equals("Player1"))) { // Need to check if in dash state
			Debug.Log("Touch player2");
			if (collision.collider.rigidbody) // The feet don't have rigidbody this can be a problem
				collision.collider.rigidbody.AddForce(collision.rigidbody.velocity * 5f, ForceMode.Impulse);
		}
	}
}
