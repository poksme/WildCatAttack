using UnityEngine;
using System.Collections;

[RequireComponent (typeof (InputManager))]
public class Dash : MonoBehaviour {
	
	// Timer variables
	[SerializeField] private float dashTimerMax = 0.3f; //How long will we dash ?
	private float dashTimerCur = 0f;
	private bool dashTimerIsActive = false;

	// Force Variables
	[SerializeField] private float dashStrength = 15f;
	private InputManager inputMngr;
	private Rigidbody[]	rigidBodies;

	// Use this for initialization
	void Start () {
		rigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
		inputMngr = GetComponent<InputManager>();
		// Need to move this to a more general script
		Physics.gravity = new Vector3 (0, -30.0F, 0); // Manually change the gravity
	}

	private void StartDashTimer() {
		dashTimerIsActive = true;
		dashTimerCur = 0f;
	}
	
	private void UpdateDashTimer() {
		if (dashTimerIsActive) {
			dashTimerCur += Time.deltaTime;
			if (dashTimerCur > dashTimerMax) {
				dashTimerIsActive = false;
				StopDash();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		UpdateDashTimer();
		if (!dashTimerIsActive && inputMngr.DoubleTapDirection != InputManager.Direction.None) {
			StartDashTimer();
			StartDash(inputMngr.DoubleTapVector);
		}
	}

	// Applies a force on all the children rigidbodies
	void StartDash(Vector3 direction) {
		foreach (Rigidbody rb in rigidBodies)
			rb.AddForce(direction * dashStrength, ForceMode.Impulse);
	}

	// Set the velocity to 0 to all the children rigidbodies
	void StopDash() {
		foreach (Rigidbody rb in rigidBodies)
			rb.velocity = Vector3.zero;
	}
}
