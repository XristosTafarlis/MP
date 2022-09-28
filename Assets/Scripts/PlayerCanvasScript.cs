using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasScript : MonoBehaviour{
	
	bool isHiden = true; //Is the menu hidden
	public static bool isInCustomization; //The player is in the customization menu
	public static bool canClick;	//The player is able to click
	[SerializeField] Image customizationButtonSprite;
	[SerializeField] GameObject customizationObjects;
	
	[SerializeField] GameObject upper;
	[SerializeField] GameObject lower;
	
	[SerializeField] GameObject playerController;
	
	void Update(){
		if(!isHiden) canClick = true;
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!isHiden){
				isHiden = true;
				customizationObjects.SetActive(false);
				customizationButtonSprite.enabled = true;
				canClick = false;
				
				playerController.GetComponent<PlayerController>().myCamera = PlayerController.CameraState.ThirdPesonCamera;
				playerController.GetComponent<PlayerController>().CammeraChange();
			}
		}
	}
	
	public void customization(){
		if(isHiden){
			customizationObjects.SetActive(true);
			isHiden = false;
			customizationButtonSprite.enabled = false;
			
			//Making camera look uper body
			playerController.GetComponent<PlayerController>().myCamera = PlayerController.CameraState.BotCamera;
			playerController.GetComponent<PlayerController>().CustomizationCamera();
		}
	}
	
	public void pickSide(int value){
		if(value == 0){
			upper.SetActive(true);
			lower.SetActive(false);
			playerController.GetComponent<PlayerController>().myCamera = PlayerController.CameraState.BotCamera;
			playerController.GetComponent<PlayerController>().CustomizationCamera();
		}else if (value == 1){
			upper.SetActive(false);
			lower.SetActive(true);
			playerController.GetComponent<PlayerController>().myCamera = PlayerController.CameraState.TopCamera;
			playerController.GetComponent<PlayerController>().CustomizationCamera();
		}
	}
	
	public void EnableCanClick(){
		canClick = true;
	}
	
	public void DisablecanClick(){
		canClick = false;
	}
	
}
