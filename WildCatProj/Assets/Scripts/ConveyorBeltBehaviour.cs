using UnityEngine;
using System.Collections;

public class ConveyorBeltBehaviour : MonoBehaviour {

	public	bool	reverseDirection = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionStay(Collision collision) {
		if (reverseDirection == true)
			collision.gameObject.GetComponentInChildren<Rigidbody>().velocity = Vector3.left * 5;
		else
			collision.gameObject.GetComponentInChildren<Rigidbody>().velocity = Vector3.right * 5;
	}
}
