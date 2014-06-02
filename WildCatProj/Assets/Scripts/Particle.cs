using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	ParticleSystem		ParticleComp;

	// Use this for initialization
	void Start () {
		ParticleComp = this.gameObject.GetComponent<ParticleSystem>();
		ParticleComp.emissionRate = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockPos, lockPos);
		if( Input.GetKeyDown( KeyCode.A ) ||  Input.GetKeyDown( KeyCode.D )) {
			particleSystem.Emit(5);
		}
	}
}
