using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

	GameObject			Body;
	Rigidbody			BodyRigidComp;
	bool				TimerActivated = false;
	bool				leftKeyUsed = false; //To know in which direction we will dash
	bool				rightKeyUsed = false; //To know in which direction we will dash
	float				dashTimerCur = 0f;
	float				tapLeftCooler = 0.04f;
	float				tapRightCooler = 0.04f;
	int					tapLeftCount = 0;
	int					tapRightCount = 0;
	public Rigidbody[]	rigidBodies;
	public const float	dashTimerMax = 0.3f; //How long will we dash ?
	
	
	
	// Use this for initialization
	void Start () {
		Body = GameObject.FindGameObjectWithTag("Body");
		Physics.gravity = new Vector3 (0, -30.0F, 0); // Manually change the gravity
		rigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
		BodyRigidComp = Body.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((leftKeyUsed = Input.GetKeyDown("a")) || (rightKeyUsed = Input.GetKeyDown("d"))) {
			Debug.Log ("left : " + tapLeftCount + " right : " + tapRightCount);
			if ((tapLeftCooler > 0 && tapLeftCount == 1) || (tapRightCooler > 0 && tapRightCount == 1)) {
				if (!TimerActivated) {
					dashTimerCur = dashTimerMax;
					if (leftKeyUsed && tapLeftCount == 1) {
						DoDash(Vector3.left);
						TimerActivated = true;
					}
					else if (rightKeyUsed && tapRightCount == 1) {
						TimerActivated = true;
						DoDash(Vector3.right);
					}
				}
			}
			else {
				if (rightKeyUsed) {
					tapRightCooler = 0.5f;
					tapRightCount += 1;
				}
				if (leftKeyUsed) {
					tapLeftCooler = 0.5f;
					tapLeftCount += 1;
				}
				
			}
			leftKeyUsed = false;
			rightKeyUsed = false;
		} 
		UpdateTimer();
		//Update TapCount
		tapLeftCooler = (tapLeftCooler > 0) ? tapLeftCooler - 1 * Time.deltaTime : tapLeftCount = 0;
		tapRightCooler = (tapRightCooler > 0) ? tapRightCooler - 1 * Time.deltaTime : tapRightCount = 0;

		//Power of the Dash
//		if (TimerActivated) {
//				BodyRigidComp.rigidbody.mass = 90000f;
//		}
//		else {
//			BodyRigidComp.rigidbody.mass = 1f;
//		}
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
		foreach (Rigidbody rb in rigidBodies) {
			rb.AddForce(direction * 15, ForceMode.Impulse);
		}
		// start timer
		//this.animation.Play("Dash");
	}
}
