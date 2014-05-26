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
	}

	private void UpdateHingeJoints() {
		// Front Part
		if (inputMngr.RightButtonUnrealised) {
			JointSpring	JPTemp = leftFrontHinge.spring;
			JPTemp.targetPosition = -10;
			leftFrontHinge.spring = JPTemp;
			rightFrontHinge.spring = JPTemp;
		} else {
			JointSpring	JPTemp = leftFrontHinge.spring;
			JPTemp.targetPosition = -120;
			leftFrontHinge.spring = JPTemp;
			rightFrontHinge.spring = JPTemp;
		}

		// Back Part
		if (inputMngr.LeftButtonUnrealised) {
			JointSpring	JPTemp = leftBackHinge.spring;
			JPTemp.targetPosition = -10;
			leftBackHinge.spring = JPTemp;
			rightBackHinge.spring = JPTemp;
		} else {
			JointSpring	JPTemp = leftBackHinge.spring;
			JPTemp.targetPosition = -120;
			leftBackHinge.spring = JPTemp;
			rightBackHinge.spring = JPTemp;
		}
	}
}
