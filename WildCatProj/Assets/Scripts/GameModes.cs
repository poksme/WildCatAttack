using UnityEngine;
using System.Collections;

public class GameModes : MonoBehaviour {

	OptionManager options; 
	private	Object[]	allObjects;
	public GameObject	floor;

	// Use this for initialization
	void Start () {
		options = OptionManager.GetInstance();

		if (options.snowModeActivated) {
			Debug.Log("Plop");
			floor.collider.material = (PhysicMaterial)Resources.Load("Physic Materials/Ice");
		}

		if (options.windModeActivated) {
			objects = GameObject.FindObjectsOfType(typeof(MonoBehaviour));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (options.windModeActivated) {
			foreach(object thisObject in allObjects){

			}
		}
	}
}
