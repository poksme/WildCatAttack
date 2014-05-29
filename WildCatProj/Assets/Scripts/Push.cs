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
		GameObject me = transform.gameObject;
		GameObject other = collision.collider.transform.root.gameObject;
		if ((me.CompareTag("Player1") && other.CompareTag("Player2")) || 
		    (me.CompareTag("Player2") && other.CompareTag("Player1"))) {
			if (collision.collider.rigidbody && me.GetComponent<Dash>().IsDashing && !other.GetComponent<Dash>().IsDashing) {
				// The feet don't have rigidbody this can be a problem 
				SoundChannelManager.GetInstance().PlayClipAtPoint(me.GetComponent<WildCatController>().hitSFX, me.transform);
				SoundChannelManager.GetInstance().PlayClipAtPoint(me.GetComponent<WildCatController>().flySFX, me.transform);
				collision.collider.rigidbody.AddForce(collision.rigidbody.velocity * 5f, ForceMode.Impulse);
			} else {
				SoundChannelManager.GetInstance().PlayClipAtPoint(me.GetComponent<WildCatController>().lowHitSFX, me.transform);
			}
		}
	}
}
