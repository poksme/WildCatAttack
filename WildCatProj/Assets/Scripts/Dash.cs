using UnityEngine;
using System.Collections;

[RequireComponent (typeof (InputManager))]
[RequireComponent (typeof (WildCatController))]
public class Dash : MonoBehaviour {
	
	// Timer variables
	[SerializeField] private float dashTimerMax = 0.3f; //How long will we dash ?
	private float dashTimerCur = 0f;
	private bool dashTimerIsActive = false;

	// Force Variables
	[SerializeField] private float dashStrength = 15f;
	private InputManager inputMngr;
	private Rigidbody[]	rigidBodies;
	private InputManager.Direction dashingDirection = InputManager.Direction.None;

	// Wildcat
	WildCatController wildCat;

	public bool IsDashing {
		get {
			return dashTimerIsActive;
		}
	}

	public InputManager.Direction DashingDirections {
		get {
			return dashingDirection;
		}
	}

	// Use this for initialization
	void Start () {
		rigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
		wildCat = GetComponent<WildCatController>();
		inputMngr = GetComponent<InputManager>();
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
				dashingDirection = InputManager.Direction.None;
				StopDash();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		UpdateDashTimer();
		if (!dashTimerIsActive && inputMngr.DoubleTapDirection != InputManager.Direction.None && !wildCat.isOverHeat) {
			StartDashTimer();
			StartDash(inputMngr.DoubleTapVector);
			dashingDirection = inputMngr.DoubleTapDirection;
		}
	}

	// Applies a force on all the children rigidbodies
	void StartDash(Vector3 direction) {
		foreach (Rigidbody rb in rigidBodies)
			rb.AddForce(direction * dashStrength, ForceMode.Impulse);
		wildCat.AddHeat();
	}

	// Set the velocity to 0 to all the children rigidbodies
	void StopDash() {
		foreach (Rigidbody rb in rigidBodies) {
			if (!rb.isKinematic)
				rb.velocity = Vector3.zero;
		}
	}
}
