using UnityEngine;
using System.Collections;

public class Dash2 : MonoBehaviour {

	Rigidbody	RigidComp;
	bool prout=false;
	float	CountDown = 0f;
	public const float dashTimerMax = 2f;
	private float dashTimerCur = 0f;
	GameObject Body;
	public Rigidbody[] rigidBodies;

	// Use this for initialization
	void Start () {
		RigidComp = this.GetComponent<Rigidbody>();
		Body = GameObject.FindGameObjectWithTag("Body");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("a")) {
			//Vector3 target = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
			//float step = 10 * Time.deltaTime;
			//RigidComp.AddForce(Vector3.right * 1000);
			//transform.position = 
			//CountDown = 1f;
			if (!prout) {
				dashTimerCur = dashTimerMax;
			DoDash(Vector3.left);
				prout = true;
			}
		} else if (Input.GetKey("d")) {
			if (!prout) {
				dashTimerCur = dashTimerMax;
				DoDash(Vector3.right);
				prout = true;
			}
		} else {
			prout = false;
		}
		if (!prout) {
			dashTimerCur += Time.deltaTime;
		}
		if (dashTimerCur > dashTimerMax) {
			foreach (Rigidbody rb in rigidBodies)
				rb.velocity = Vector3.zero;
			dashTimerCur = 0f;
		}
		if (CountDown >= 0f)
		{
			Debug.Log("COUOCU");
			CountDown -= Time.deltaTime;
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
		}
		// if timer finished
		// rigidbody.velocity = Vector3.zero;
		    
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
