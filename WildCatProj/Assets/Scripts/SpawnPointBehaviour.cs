using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPointBehaviour : MonoBehaviour {

	public	List<GameObject> BrokenWildCats;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnObject", 0, 5);
	}

	void SpawnObject() {
		Instantiate(BrokenWildCats[(Random.Range(0, BrokenWildCats.Count))], this.transform.position, Random.rotation);
	}
}