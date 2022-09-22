using UnityEngine;

public class CameraRenderer : MonoBehaviour{
	[SerializeField] GameObject playerModel;
	//Hide the player model on First Person View
	void OnPreCull() {
		playerModel.SetActive(false);
	}
	void OnPostRender() {
		playerModel.SetActive(true);
	}
}