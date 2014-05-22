using UnityEngine;
using System.Collections;

public class WildCatController : MonoBehaviour {

	public	HingeJoint[]		Shoulders;
	public	HingeJoint[]		Elbows;

	public	float			force = 500.0f;

	void Update () {
		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;

		if (Input.GetMouseButton(0)) {
//			JointMotor2D motor = new JointMotor2D();
//			motor.maxMotorTorque = BackShoulder.motor.maxMotorTorque;
//			motor.motorSpeed = -this.force;
//			BackShoulder.motor = motor;
		} 
		else {
//			JointMotor2D motor = new JointMotor2D();
//			motor.maxMotorTorque = BackShoulder.motor.maxMotorTorque;
//			motor.motorSpeed = this.force;
//			BackShoulder.motor = motor;
		}
//
//
//
//		if (Input.GetMouseButton(1)) {
//			JointMotor2D motor = new JointMotor2D();
//			motor.maxMotorTorque = FrontShoulder.motor.maxMotorTorque;
//			motor.motorSpeed = -this.force;
////			BackShoulder.motor = motor;
//			FrontShoulder.motor = motor;
//		} 
//		else {
//			JointMotor2D motor = new JointMotor2D();
//			motor.maxMotorTorque = FrontShoulder.motor.maxMotorTorque;
//			motor.motorSpeed = this.force;
//			//			BackShoulder.motor = motor;
//			FrontShoulder.motor = motor;
//		}
	}
}
