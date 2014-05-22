using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

	Rigidbody	RigidComp;
	bool TimerActivated = false;
	private float dashTimerCur = 0f;
	GameObject Body;
	public Rigidbody[] rigidBodies;
	public const float dashTimerMax = 2f;

	// Use this for initialization
	void Start () {
		RigidComp = this.GetComponent<Rigidbody>();
		Body = GameObject.FindGameObjectWithTag("Body");
	}
	
	// Update is called once per frame
	void Update () {
		bool test;
		if (Input.GetKey("a")) {
			if (!TimerActivated) {
				dashTimerCur = dashTimerMax;
				DoDash(Vector3.left);
				TimerActivated = true;
			}
		} else {
			TimerActivated = false;
		}
		UpdateTimer();
	}

	void UpdateTimer() {
		if (!TimerActivated) {
			dashTimerCur += Time.deltaTime;
		}
		if (dashTimerCur > dashTimerMax) {
			foreach (Rigidbody rb in rigidBodies)
				rb.velocity = Vector3.zero;
			dashTimerCur = 0f;
		}
	}

	void DoDash(Vector3 direction) {
		rigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in rigidBodies) {
			rb.AddForce(direction * 15, ForceMode.Impulse);
	}
		// start timer
		//this.animation.Play("Dash");
	}
}
