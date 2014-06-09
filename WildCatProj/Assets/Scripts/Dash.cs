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

	//Shield effect
	public GameObject shieldObject;
	private Color colorStart;
	private Color colorEnd;
	private float fadeDuration = 100.0f;

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
		if (shieldObject == null)
			Debug.Log ("Shield Object is not set");
		colorStart = shieldObject.renderer.material.color;
		colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0.0f);
		FadeOut();
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
		//shieldObject.SetActive(true);
		//FadeShield(100f,2.0f);
		FadeIn();
	}

	// Set the velocity to 0 to all the children rigidbodies
	void StopDash() {
		foreach (Rigidbody rb in rigidBodies) {
			if (!rb.isKinematic)
				rb.velocity = Vector3.zero;
		}
		//shieldObject.SetActive(false);
		//FadeShield(100f,0.3f);
		FadeOut();
	}

	void FadeOut ()
	{
		for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime) {
			shieldObject.renderer.material.color = Color.Lerp (colorStart, colorEnd, t/fadeDuration);
		}
	}

	void FadeIn ()
	{
		for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime) {
			shieldObject.renderer.material.color = Color.Lerp (colorEnd, colorStart, t/fadeDuration);
		}
	}

}
