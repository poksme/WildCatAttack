using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

	Rigidbody			RigidComp;
	GameObject			Body;
	bool				TimerActivated = false;
	float				dashTimerCur = 0f;
	float				tapCooler = 0.5f;
	int					tapCount = 0;
	public Rigidbody[]	rigidBodies;
	public const float	dashTimerMax = 0.3f; //How long will we dash ?



	// Use this for initialization
	void Start () {
		RigidComp = this.GetComponent<Rigidbody>();
		Body = GameObject.FindGameObjectWithTag("Body");
	}
	
	// Update is called once per frame
	void Update () {
		bool leftKeyUsed = false; //To know in which direction we will dash
		if (Input.GetKeyDown("d") || (leftKeyUsed = Input.GetKeyDown("a"))) {
			if (tapCooler > 0 && tapCount == 1) {
				if (!TimerActivated) {
					dashTimerCur = dashTimerMax;
					if (leftKeyUsed)
						DoDash(Vector3.left);
					else
						DoDash(Vector3.right);
					TimerActivated = true;
				}
			}
			else {
				tapCooler = 0.5f;
				tapCount += 1;
			}
		} 
		UpdateTimer();
		//Update TapCount
		if (tapCooler > 0)
			tapCooler -= 1 * Time.deltaTime;
		else 
			tapCount = 0;
	}

	void UpdateTimer() {
		if (TimerActivated) {
			dashTimerCur -= Time.deltaTime;
		}
		if (dashTimerCur <= 0) {
			foreach (Rigidbody rb in rigidBodies)
				rb.velocity = Vector3.zero;
			dashTimerCur = dashTimerMax;
			TimerActivated = false;
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
