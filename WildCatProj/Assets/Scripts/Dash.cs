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
	public GameObject RightShieldObject;
	public GameObject LeftShieldObject;
	private Color colorStart;
	private Color colorEnd;
	private float fadeDuration = 0.5f;
	private float rightCurrentFadeTime = 0.5f;
	private float leftCurrentFadeTime = 0.5f;

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
		//colorStart = RightShieldObject.renderer.material.color;
		colorEnd = new Color(colorStart.r, colorStart.g, colorStart.b, 0.0f);
		FadeOut (dashingDirection);
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

		if (dashTimerIsActive && dashingDirection == InputManager.Direction.Right)
			RightShieldObject.SetActive(true);
		else if (!dashTimerIsActive && rightCurrentFadeTime >= fadeDuration)
			RightShieldObject.SetActive(false);

		if (dashTimerIsActive && dashingDirection == InputManager.Direction.Left)
			LeftShieldObject.SetActive(true);
		else if (!dashTimerIsActive && leftCurrentFadeTime >= fadeDuration)
			LeftShieldObject.SetActive(false);

		//FadeOut the Shield if not active
		if (!dashTimerIsActive && rightCurrentFadeTime < fadeDuration) {
			RightShieldObject.renderer.material.color = Color.Lerp (colorStart, colorEnd, rightCurrentFadeTime/fadeDuration);
			rightCurrentFadeTime += Time.deltaTime;
		}
		if (!dashTimerIsActive && leftCurrentFadeTime < fadeDuration) {
			LeftShieldObject.renderer.material.color = Color.Lerp (colorStart, colorEnd, leftCurrentFadeTime/fadeDuration);
			leftCurrentFadeTime += Time.deltaTime;
		}
	}

	// Applies a force on all the children rigidbodies
	void StartDash(Vector3 direction) {
		foreach (Rigidbody rb in rigidBodies)
			rb.AddForce(direction * dashStrength, ForceMode.Impulse);
		wildCat.AddHeat();
		FadeIn(dashingDirection);
	}

	// Set the velocity to 0 to all the children rigidbodies
	void StopDash() {
		foreach (Rigidbody rb in rigidBodies) {
			if (!rb.isKinematic)
				rb.velocity = Vector3.zero;
		}
	}

	void FadeOut (InputManager.Direction direction)
	{
		for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime) {
			if (direction == InputManager.Direction.Right)
				RightShieldObject.renderer.material.color = Color.Lerp (colorStart, colorEnd, t/fadeDuration);
			else if (direction == InputManager.Direction.Left)
				LeftShieldObject.renderer.material.color = Color.Lerp (colorStart, colorEnd, t/fadeDuration);
			else {
				RightShieldObject.renderer.material.color = Color.Lerp (colorStart, colorEnd, t/fadeDuration);
				LeftShieldObject.renderer.material.color = Color.Lerp (colorStart, colorEnd, t/fadeDuration);
			}

		}
	}

	void FadeIn (InputManager.Direction direction)
	{
		for (float t = 0.0f; t < fadeDuration; t += Time.deltaTime) {
			if (direction == InputManager.Direction.Right)
				RightShieldObject.renderer.material.color = Color.Lerp (colorEnd, colorStart, t/fadeDuration);
			else if (direction == InputManager.Direction.Left)
				LeftShieldObject.renderer.material.color = Color.Lerp (colorEnd, colorStart, t/fadeDuration);
			else {
				RightShieldObject.renderer.material.color = Color.Lerp (colorEnd, colorStart, t/fadeDuration);
			}
		}
		rightCurrentFadeTime = 0f;
		leftCurrentFadeTime = 0f;
	}

}
