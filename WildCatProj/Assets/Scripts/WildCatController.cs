using UnityEngine;
using System.Collections;

[RequireComponent (typeof (InputManager))]
[RequireComponent (typeof (Dash))]
public class WildCatController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3 (0, -30.0F, 0); // Manually change the gravity
	}

	// Update is called once per frame
	void Update () {
	}
}
