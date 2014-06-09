using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	ParticleSystem		ParticleComp;
	WildCatController	wildCat;

	// Use this for initialization
	void Start () {
		ParticleComp = this.gameObject.GetComponent<ParticleSystem>();
		ParticleComp.emissionRate = 0;
		wildCat = transform.root.gameObject.GetComponent<WildCatController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(wildCat.isOverHeat) {
			particleSystem.Emit(1);
		}
	}
}
