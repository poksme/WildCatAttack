using UnityEngine;
using System.Collections;

[RequireComponent (typeof (InputManager))]
public class WildCatController : MonoBehaviour {
	[SerializeField]private HingeJoint leftFrontHinge;
	[SerializeField]private HingeJoint leftBackHinge;
	[SerializeField]private HingeJoint rightFrontHinge;
	[SerializeField]private HingeJoint rightBackHinge;
	private InputManager inputMngr;
	private Rigidbody[]	rigidBodies;
	private SoundChannelManager scm;
	public AudioClip hitSFX;
	public AudioClip flySFX;
	public AudioClip lowHitSFX;
	public AudioClip hingeUnfold;
	public AudioClip hingeFold;
	private bool hingeBackUnfold = false;
	private bool hingeFrontUnfold = true;
	private float curHeat = 0f;
	[SerializeField]private float maxHeat = 100f;
	[SerializeField]private float CoolingDown = 10f;
	[SerializeField]private float DashCost = 33f;

	public bool isOverHeat {
		get { return curHeat >= maxHeat; }
	}

	// Use this for initialization
	void Start () {
		scm = SoundChannelManager.GetInstance();
		Physics.gravity = new Vector3 (0, -30.0F, 0); // Manually change the gravity
		inputMngr = GetComponent<InputManager>();
		rigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
		// Relay Collission to every parents with the function OnCollisionInChildren
		foreach (Rigidbody rb in rigidBodies) {
			rb.gameObject.AddComponent<RelayCollision>();
		}
	}


	// Update is called once per frame
	void Update () {
		UpdateHingeJoints();
		// FOR DEBUG PURPOSE
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel (Application.loadedLevel);
		}
		DecreaseHeat();
		// FOR DEBUG PURPOSE
	}

	private void UpdateHingeJoints() {
		// Front Part
		if (inputMngr.RightButtonUnrealised) {
			if (!hingeFrontUnfold) {
				hingeFrontUnfold = true;
				SoundChannelManager.GetInstance().PlayClipAtPoint(hingeUnfold, leftFrontHinge.transform);
			}
			JointSpring	JPTemp = leftFrontHinge.spring;
			JPTemp.targetPosition = -10;
			leftFrontHinge.spring = JPTemp;
			rightFrontHinge.spring = JPTemp;
		} else {
			if (hingeFrontUnfold) {
				hingeFrontUnfold = false;
				SoundChannelManager.GetInstance().PlayClipAtPoint(hingeFold, leftFrontHinge.transform);
			}
			JointSpring	JPTemp = leftFrontHinge.spring;
			JPTemp.targetPosition = -120;
			leftFrontHinge.spring = JPTemp;
			rightFrontHinge.spring = JPTemp;
		}

		// Back Part
		if (inputMngr.LeftButtonUnrealised) {
			if (!hingeBackUnfold) {
				hingeBackUnfold = true;
				SoundChannelManager.GetInstance().PlayClipAtPoint(hingeUnfold, leftBackHinge.transform);
			}
			JointSpring	JPTemp = leftBackHinge.spring;
			JPTemp.targetPosition = -10;
			leftBackHinge.spring = JPTemp;
			rightBackHinge.spring = JPTemp;
		} else {
			if (hingeBackUnfold) {
				hingeBackUnfold = false;
				SoundChannelManager.GetInstance().PlayClipAtPoint(hingeFold, leftBackHinge.transform);
			}
			JointSpring	JPTemp = leftBackHinge.spring;
			JPTemp.targetPosition = -120;
			leftBackHinge.spring = JPTemp;
			rightBackHinge.spring = JPTemp;
		}
	}

	public void AddHeat ()
	{
		if (!isOverHeat)
			curHeat += DashCost;
	}

	void DecreaseHeat ()
	{
		curHeat -= Time.deltaTime * CoolingDown;
		if (curHeat < 0f)
			curHeat = 0f;
	}
}
