using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    float				lockPos = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, lockPos);
	}
}
