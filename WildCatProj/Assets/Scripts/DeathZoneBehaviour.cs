using UnityEngine;
using System.Collections;

public class DeathZoneBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player1") || other.CompareTag("Player2")) {
			other.transform.root.gameObject.GetComponent<WildCatController>().Die();
		}
	}
}
