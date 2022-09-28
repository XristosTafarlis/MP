using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour{
	[SerializeField] TextMeshProUGUI FpsText;
	float pollingTime = 0.3f;	//Refresh rate of the number on screen, every
	float time;	//Variable for counting passed time
	int frameCount;	//Actual number of Frames per second
	
	void Update(){
		time += Time.deltaTime;
		frameCount ++;
		
		if(time >= pollingTime){	//If enough time has passed, show the frame Count
			int frameRate = Mathf.RoundToInt(frameCount / time);
			FpsText.text = "FPS : " + frameRate.ToString();
			
			time -= pollingTime;	//Reset the time
			frameCount = 0;
		}
	}
}
