using UnityEngine;
using System.Collections;

public class WildCatController2D : MonoBehaviour {

	public	HingeJoint2D	BackShoulder;
	public	HingeJoint2D	FrontShoulder;
	public	HingeJoint2D	BackElbow;
	public	HingeJoint2D	FrontElbow;

	public	float			force = 500.0f;

	void Update () {
		if (Input.GetMouseButton(0)) {
			JointMotor2D motor = new JointMotor2D();
			motor.maxMotorTorque = BackShoulder.motor.maxMotorTorque;
			motor.motorSpeed = -this.force;
			BackShoulder.motor = motor;
		} 
		else {
			JointMotor2D motor = new JointMotor2D();
			motor.maxMotorTorque = BackShoulder.motor.maxMotorTorque;
			motor.motorSpeed = this.force;
			BackShoulder.motor = motor;
		}



		if (Input.GetMouseButton(1)) {
			JointMotor2D motor = new JointMotor2D();
			motor.maxMotorTorque = FrontShoulder.motor.maxMotorTorque;
			motor.motorSpeed = -this.force;
//			BackShoulder.motor = motor;
			FrontShoulder.motor = motor;
		} 
		else {
			JointMotor2D motor = new JointMotor2D();
			motor.maxMotorTorque = FrontShoulder.motor.maxMotorTorque;
			motor.motorSpeed = this.force;
			//			BackShoulder.motor = motor;
			FrontShoulder.motor = motor;
		}
	}
}
