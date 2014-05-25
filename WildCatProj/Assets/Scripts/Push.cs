using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag.Equals("Body")){
			Debug.Log ("Salut je suis une fonction");
			collision.rigidbody.AddForce(this.rigidbody.velocity * 1000f);
		}
	}
}
