using UnityEngine;
using System.Collections;

public class BackLeg: MonoBehaviour {

	HingeJoint	HJComponent;

	// Use this for initialization
	void Start () {
		HJComponent = this.GetComponent<HingeJoint> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Let's lock the Z Axis
//		Vector3 pos = transform.position;
//		pos.z = 0;
//		transform.position = pos;
	}

	void LateUpdate() {
		InputListener ();
	}

	void InputListener() {
		if (Input.GetKey("a")) {
			JointSpring	JPTemp = hingeJoint.spring;
			JPTemp.targetPosition = -10;
			HJComponent.spring = JPTemp;
		} else {
			JointSpring	JPTemp = hingeJoint.spring;
			JPTemp.targetPosition = -120;
			HJComponent.spring = JPTemp;
		}
	}
}
