using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour{
	[SerializeField] TextMeshProUGUI FpsText;
	float pollingTime = 0.3f;
	float time;
	int frameCount;
	
	void Update(){
		time += Time.deltaTime;
		frameCount ++;
		
		if(time >= pollingTime){
			int frameRate = Mathf.RoundToInt(frameCount / time);
			FpsText.text = "FPS : " + frameRate.ToString();
			
			time -= pollingTime;
			frameCount = 0;
		}
	}
}
