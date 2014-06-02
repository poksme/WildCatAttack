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
				SoundChannelManager.GetInstance().PlayClipAtPoint(me.GetComponent<WildCatController>().hitSFX, collision.transform);
				SoundChannelManager.GetInstance().PlayClipAtPoint(me.GetComponent<WildCatController>().flySFX, collision.transform);
				collision.collider.rigidbody.AddForce(collision.rigidbody.velocity + (me.GetComponent<Dash>().DashingDirections == InputManager.Direction.Left ? Vector3.left : Vector3.right) * 15f, ForceMode.Impulse);
			} else {
				SoundChannelManager.GetInstance().PlayClipAtPoint(me.GetComponent<WildCatController>().lowHitSFX, collision.transform);
			}
		}
	}
}
