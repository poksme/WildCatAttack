﻿using UnityEngine;
using System.Collections;

public class RelayCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		SendMessageUpwards("OnCollisionInChildren", collision, SendMessageOptions.DontRequireReceiver);
	}
}
