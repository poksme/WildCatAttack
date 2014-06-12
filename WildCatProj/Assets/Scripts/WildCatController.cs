using UnityEngine;
using System.Collections;

[RequireComponent (typeof (InputManager))]
public class WildCatController : MonoBehaviour {
	private InputManager inputMngr;
	private Rigidbody[]	rigidBodies;
	private Vector3[] rbInitPosition;
	private Quaternion[] rbInitRotation;
	private GameObject[] lifeIcons;

	// Sound
	private SoundChannelManager scm;
	public AudioClip hitSFX;
	public AudioClip flySFX;
	public AudioClip lowHitSFX;

	// Hinge
	[SerializeField]private HingeJoint leftFrontHinge;
	[SerializeField]private HingeJoint leftBackHinge;
	[SerializeField]private HingeJoint rightFrontHinge;
	[SerializeField]private HingeJoint rightBackHinge;
	public AudioClip hingeUnfold;
	public AudioClip hingeFold;
	private bool hingeBackUnfold = false;
	private bool hingeFrontUnfold = true;

	// Heat logic
	private float curHeat = 0f;
	[SerializeField]private float maxHeat = 100f;
	[SerializeField]private float CoolingDown = 10f;
	[SerializeField]private float DashCost = 33f;
	public bool isOverHeat { get { return curHeat >= maxHeat; } }

	// Heat bar
	[SerializeField]private GameObject heatBar;
	private ParticleEmitter heatBarParticleSystem;
	private GameObject heatBarTube;
	private Color defaultHeatBarTubeColor;
	private bool isOpaq = false;
	[SerializeField]private float maxTubeTransparency = 0.8f;
	private const float maxHeatParticleX = 0.424342f;
	private const float minHeatParticleX = -1.5f;


	// Life logic
	[SerializeField]private int lives = 3;
	[SerializeField]private GameObject HeartPrefab;

	// Use this for initialization
	void Start () {
		scm = SoundChannelManager.GetInstance();
		Physics.gravity = new Vector3 (0, -30.0F, 0); // Manually change the gravity
		inputMngr = GetComponent<InputManager>();
		rigidBodies = gameObject.GetComponentsInChildren<Rigidbody>();
		rbInitPosition = new Vector3[rigidBodies.Length];		
		rbInitRotation = new Quaternion[rigidBodies.Length];
		lifeIcons = new GameObject[lives];
		// Relay Collission to every parents with the function OnCollisionInChildren
		int i = 0;
		foreach (Rigidbody rb in rigidBodies) {
			rb.gameObject.AddComponent<RelayCollision>();
			rbInitPosition[i] = rb.transform.position;
			rbInitRotation[i] = rb.transform.rotation;
			i++;
		}
		if (heatBar != null) {
			heatBarParticleSystem = heatBar.GetComponentInChildren<ParticleEmitter>();
			heatBarTube = heatBar.transform.FindChild("HeatBarModel").FindChild("TransparentTube").gameObject;
			defaultHeatBarTubeColor = heatBarTube.renderer.material.color;
		} else {
			Debug.LogWarning("heatbar is unset");
		}
		createLivesHud();
	}

	void createLivesHud ()
	{
		float delta = -12.25f + 9.1f;
		float x = 0f;
		if (this.CompareTag("Player1")) {
			x = -12.25f;
		} else if (this.CompareTag("Player2")) {
			x =3.15f;
		}
		for (int i = 0; i < lives; i++) {
			// old y -6.650559f
			GameObject heart = (GameObject)Instantiate(HeartPrefab, new Vector3(x - delta / (lives - 1) * i, -7f, -7.264833f), Quaternion.Euler(-60, 0, 0));
			heart.layer = LayerMask.NameToLayer("HUD");
			heart.transform.localScale = new Vector3(25f, 25f, 25f);
			lifeIcons[i] = heart;
			/*
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.layer = LayerMask.NameToLayer("HUD");
			sphere.transform.position = new Vector3(x - delta / (lives - 1) * i, -6.650559f, -7.264833f);
			sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			sphere.renderer.material.shader = Shader.Find("Specular");
			sphere.renderer.material.SetColor("_Color", new Color(0.7f, 0, 0));
			sphere.renderer.material.SetColor("_SpecColor", new Color(1f, 0.7f, 0.7f));
			lifeIcons[i] = sphere;
			*/
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHingeJoints();
		// FOR DEBUG PURPOSE
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("Menu");
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
		heatBarParticleSystem.transform.localScale = new Vector3(3.5f * heatPercentage, 
		                                                         heatBarParticleSystem.transform.localScale.y, 
		                                                         heatBarParticleSystem.transform.localScale.z);
		heatBarParticleSystem.transform.localPosition = new Vector3( minHeatParticleX + (maxHeatParticleX - minHeatParticleX) * heatPercentage, 
		                                                            heatBarParticleSystem.transform.localPosition.y, 
		                                                            heatBarParticleSystem.transform.localPosition.z);
	}

	void UpdateHeatBarStyle () {
		if (heatBar != null) {
			UpdateHeatBarColor();
			UpdateHeatBarParticleSystem();
		}
	}

	void teleportObject(string name, Vector3 newPosition) {
		Transform target = transform.FindChild(name);
		target.gameObject.SetActive(false);
		target.position = newPosition;
		target.gameObject.SetActive(true);
	}

	public void Die () {
		int i = 0;
		foreach (Rigidbody rb in rigidBodies) {
			if (!rb.isKinematic) {
				rb.Sleep();
				rb.transform.position = rbInitPosition[i];
				rb.transform.rotation = rbInitRotation[i];
				rb.WakeUp();
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
			}
			i++;
		}
		lives--;
		lifeIcons[lives].SetActive(false);
		curHeat = 0f;
		// DEBUG PURPOSE
		if (lives <= 0) {
			bool	lol = false;
			if (lol) {
				Application.LoadLevel ("Menu");
			} else {
				Application.LoadLevel (Application.loadedLevel);
			}
			//for now return to main menu
		}
	}
}
