using UnityEngine;
using Mirror;

public class BoxTest : NetworkBehaviour {
	
	[SerializeField] GameObject [] items;
	
	void Update(){
		if(!isLocalPlayer) return;
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			CMDShowFirst();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			CMDShowSecond();
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)){
			CMDShowThird();
		}
	}
	
	//1st
	[Command]
	void CMDShowFirst(){
		PRCShowfirst();
	}
	[ClientRpc]
	void PRCShowfirst(){
		items[0].SetActive(true);
		items[1].SetActive(false);
		items[2].SetActive(false);
	}
	
	//3rd
	[Command]
	void CMDShowSecond(){
		PRCShowSecond();
	}
	
	[ClientRpc]
	void PRCShowSecond(){
		items[0].SetActive(false);
		items[1].SetActive(true);
		items[2].SetActive(false);
	}
	//
	[Command]
	void CMDShowThird(){
		PRCShowThird();
	}
	
	[ClientRpc]
	void PRCShowThird(){
		items[0].SetActive(false);
		items[1].SetActive(false);
		items[2].SetActive(true);
	}
}