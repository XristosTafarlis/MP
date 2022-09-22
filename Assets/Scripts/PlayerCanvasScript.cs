using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasScript : MonoBehaviour{
	bool isHiden = true;
	[SerializeField] Image customizationButtonSprite;
	[SerializeField] GameObject customizationObjects;
	public void customization(){
		if(isHiden){
			customizationObjects.SetActive(true);
			isHiden = false;
			customizationButtonSprite.enabled = false;
		}else{
			customizationObjects.SetActive(false);
			isHiden = true;
			customizationButtonSprite.enabled = true;
		}
	}
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!isHiden){
				isHiden = true;
				customizationObjects.SetActive(false);
				customizationButtonSprite.enabled = true;
			}
		}
	}
}
