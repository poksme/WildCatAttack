using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {

	public	List<GameObject> BrokenWildCats;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnObject", 0, 5);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void SpawnObject() {
		Instantiate(BrokenWildCats[(Random.Range(0, BrokenWildCats.Count))], this.transform.position, Random.rotation);
	}
}