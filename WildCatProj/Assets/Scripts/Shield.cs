using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    float				lockPos = 0;
	public float		YRotation;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, YRotation, lockPos);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, YRotation, lockPos);
	}
}
