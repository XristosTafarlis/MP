using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WalkAnimationsController : NetworkBehaviour{
	[SerializeField] Animator animator;
	
	float vel_X = 0f;
	float vel_Z = 0f;
	
	void Update() {
		// W
		if(Input.GetKeyDown(KeyCode.W)){
			vel_X += 1f;
		}
		if(Input.GetKeyUp(KeyCode.W)){
			vel_X -= 1f;
		}
		
		//S
		if(Input.GetKeyDown(KeyCode.S)){
			vel_X -= 1f;
		}
		if(Input.GetKeyUp(KeyCode.S)){
			vel_X += 1f;
		}
		
		//A
		if(Input.GetKeyDown(KeyCode.A)){
			vel_Z -= 1f;
		}
		if(Input.GetKeyUp(KeyCode.A)){
			vel_Z += 1f;
		}
		
		//S
		if(Input.GetKeyDown(KeyCode.S)){
			vel_Z += 1f;
		}
		if(Input.GetKeyUp(KeyCode.S)){
			vel_Z -= 1f;
		}
		
		//Shift
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			vel_X *= 2f;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)){
			vel_X /= 2f;
		}
	}
}
