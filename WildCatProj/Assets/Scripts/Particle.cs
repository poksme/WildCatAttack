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
		if( Input.GetKeyDown( KeyCode.A ) ||  Input.GetKeyDown( KeyCode.D )) {
			particleSystem.Emit(10);
		}
	}
}
