using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FlyAnimationController : NetworkBehaviour{
	[SerializeField] float acceleration = 3f;
	[SerializeField] float deceleration = 4f;
	Animator animator;
	float vel_X = 0f;
	float vel_Z = 0f;
	void Start(){
		if(!isLocalPlayer) return;
		animator = GetComponent<Animator>();
	}

	void Update(){
		if(!isLocalPlayer) return;

		bool forwardPressed = Input.GetKey("w");
		bool leftPressed = Input.GetKey("a");
		bool backPressed = Input.GetKey("s");
		bool rightPressed = Input.GetKey("d");
		
		//Acceleration
		if(forwardPressed && vel_Z < 1f){
			vel_Z += Time.deltaTime * acceleration;
		}
		if(backPressed && vel_Z > -1f){
			vel_Z -= Time.deltaTime * acceleration;
		}
		if(rightPressed && vel_X < 1f){
			vel_X += Time.deltaTime * acceleration;
		}
		if(leftPressed && vel_X > -1f){
			vel_X -= Time.deltaTime * acceleration;
		}
		
		//Deceleration
		if(!forwardPressed && vel_Z > 0f){
			vel_Z -= Time.deltaTime * deceleration;
		}
		if(!backPressed && vel_Z < 0f){
			vel_Z += Time.deltaTime * deceleration;
		}
		if(!leftPressed && vel_X < 0f){
			vel_X += Time.deltaTime * deceleration;
		}
		if(!rightPressed && vel_X > 0f){
			vel_X -= Time.deltaTime * deceleration;
		}
		
		animator.SetFloat("FlyX", vel_X);
		animator.SetFloat("FlyZ", vel_Z);
	}
}
