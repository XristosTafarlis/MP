using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WalkAnimationsController : NetworkBehaviour{
	
	CharacterController characterController;
	Animator animator;
	PlayerController playerController;
	
	float vel_X = 0f;
	float vel_Z = 0f;
	
	void Start() {
		animator = GetComponent<Animator>();
		characterController = GetComponent<CharacterController>();
		playerController = GetComponent<PlayerController>();
	}
	
	void Update() {
		if(playerController.canMove == false){
			animator.SetFloat("vel_X", 0, 1f, 0.1f);
			animator.SetFloat("vel_Z", 0, 1f, 0.1f);
			return;
		}
		
		vel_X = Input.GetAxis("Horizontal");
		vel_Z = Input.GetAxis("Vertical");
		
		if(characterController.velocity.magnitude > (playerController.walkingSpeed + 0.01f) && (vel_Z >= 1f || vel_Z <= -1f)){
			vel_Z = 2 * Input.GetAxis("Vertical");
		}
		
		if(characterController.velocity.magnitude > (playerController.walkingSpeed + 0.01f) && (vel_X >= 1f || vel_X <= -1f)){
			vel_X = 2 * Input.GetAxis("Horizontal");
		}
		
		animator.SetFloat("vel_X", vel_X, 1f, 0.1f);
		animator.SetFloat("vel_Z", vel_Z, 1f, 0.1f);
	}
}
