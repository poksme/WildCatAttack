using UnityEngine;
using System.Collections;

public class GameModes : MonoBehaviour {

	OptionManager options; 
	public GameObject	floor;

	// Use this for initialization
	void Start () {
		options = OptionManager.GetInstance();

		if (options.snowModeActivated) {
			floor.collider.material = (PhysicMaterial)Resources.Load("PhysicMaterials/Ice");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
