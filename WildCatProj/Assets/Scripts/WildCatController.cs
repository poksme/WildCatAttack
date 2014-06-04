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

	// Heat bar specific
	[SerializeField]private GameObject heatBar;
	private ParticleEmitter heatBarParticleSystem;
	private GameObject heatBarTube;
	private Color defaultHeatBarTubeColor;
	private bool isOpaq = false;
	[SerializeField]private float maxTubeTransparency = 0.8f;
	private float maxHeatParticleX = 0.424342f;
	private float minHeatParticleX = -1.5f;

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
		if (heatBar != null) {
			heatBarParticleSystem = heatBar.GetComponentInChildren<ParticleEmitter>();
			heatBarTube = heatBar.transform.FindChild("HeatBarModel").FindChild("TransparentTube").gameObject;
			defaultHeatBarTubeColor = heatBarTube.renderer.material.color;
		} else {
			Debug.Log("heatbar is unset");
		}
	}


	// Update is called once per frame
	void Update () {
		UpdateHingeJoints();
		// FOR DEBUG PURPOSE
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel (Application.loadedLevel);
		}
		// FOR DEBUG PURPOSE
		DecreaseHeat();
		UpdateHeatBarStyle();
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

	void UpdateHeatBarColor () {
		if (isOverHeat) {
			float alpha;
			if (!isOpaq) {
				// try to turn opaq
				alpha = Mathf.Lerp(heatBarTube.renderer.material.color.a, maxTubeTransparency, 0.1f);
				if (alpha >= maxTubeTransparency - 0.01f) {
					isOpaq = true;
				}
			} else {
				alpha = Mathf.Lerp(heatBarTube.renderer.material.color.a, defaultHeatBarTubeColor.a, 0.1f);
				if (alpha <= defaultHeatBarTubeColor.a + 0.01f) {
					isOpaq = false;
				}
			}
			heatBarTube.renderer.material.color = new Color(
				Mathf.Lerp(heatBarTube.renderer.material.color.r, 0.6f, 0.1f),
				Mathf.Lerp(heatBarTube.renderer.material.color.g, 0, 0.1f),
				Mathf.Lerp(heatBarTube.renderer.material.color.b, 0, 0.1f),
				alpha
				);
		} else {
			heatBarTube.renderer.material.color = new Color(
				Mathf.Lerp(heatBarTube.renderer.material.color.r, defaultHeatBarTubeColor.r, 0.1f),
				Mathf.Lerp(heatBarTube.renderer.material.color.g, defaultHeatBarTubeColor.g, 0.1f),
				Mathf.Lerp(heatBarTube.renderer.material.color.b, defaultHeatBarTubeColor.b, 0.1f),
				Mathf.Lerp(heatBarTube.renderer.material.color.a, defaultHeatBarTubeColor.a, 0.1f)
				);
		}
	}

	void UpdateHeatBarParticleSystem () {
		float heatPercentage = (curHeat > maxHeat ? 1f : curHeat / maxHeat);
		heatBarParticleSystem.maxEmission = 100 * heatPercentage;
		heatBarParticleSystem.transform.localScale = new Vector3(3.5f * heatPercentage, heatBarParticleSystem.transform.localScale.y, heatBarParticleSystem.transform.localScale.z);
		heatBarParticleSystem.transform.localPosition = new Vector3( minHeatParticleX + (maxHeatParticleX - minHeatParticleX) * heatPercentage, heatBarParticleSystem.transform.localPosition.y, heatBarParticleSystem.transform.localPosition.z);
	}

	void UpdateHeatBarStyle () {
		if (heatBar != null) {
			UpdateHeatBarColor();
			UpdateHeatBarParticleSystem();
		}
	}
}
