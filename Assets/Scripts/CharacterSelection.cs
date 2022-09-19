using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterSelection : NetworkBehaviour{
	[Header("Hair")]
	[SerializeField] GameObject [] hairStyles;
	[SerializeField] Material [] hairStyle1Materials;
	SkinnedMeshRenderer hairStyle1SkinnedMeshRenderer;
	[SerializeField] Material [] hairStyle2Materials;
	SkinnedMeshRenderer hairStyle2SkinnedMeshRenderer;
	[SerializeField] Material [] hairStyle3Materials;
	SkinnedMeshRenderer hairStyle3SkinnedMeshRenderer;

	[Header("Face")]
	[SerializeField] GameObject face;
	[SerializeField] Material [] faceMaterials;
	SkinnedMeshRenderer faceSkinnedMeshRenderer;
	
	[Header("Shirt and Pants")]
	[SerializeField] GameObject [] shirtAndPantStyles;
	[SerializeField] Material [] shirtMaterials;
	[SerializeField] Material [] pantsMaterials;
	[SerializeField] Material [] tieBowtieMaterials;
	SkinnedMeshRenderer shirtAndPantStyle1SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle2SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle3SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle4SkinnedMeshRenderer;
	
	void Start(){
		if(!isLocalPlayer) return;
		hairStyle1SkinnedMeshRenderer = hairStyles[0].GetComponent<SkinnedMeshRenderer>();
		hairStyle2SkinnedMeshRenderer = hairStyles[1].GetComponent<SkinnedMeshRenderer>();
		hairStyle3SkinnedMeshRenderer = hairStyles[2].GetComponent<SkinnedMeshRenderer>();
		
		faceSkinnedMeshRenderer = face.GetComponent<SkinnedMeshRenderer>();
		
		shirtAndPantStyle1SkinnedMeshRenderer = shirtAndPantStyles[0].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle2SkinnedMeshRenderer = shirtAndPantStyles[1].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle3SkinnedMeshRenderer = shirtAndPantStyles[2].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle4SkinnedMeshRenderer = shirtAndPantStyles[3].GetComponent<SkinnedMeshRenderer>();
		Debug.Log("Style 1 : " + shirtAndPantStyle1SkinnedMeshRenderer.materials.Length);
		Debug.Log("Style 2 : " + shirtAndPantStyle2SkinnedMeshRenderer.materials.Length);
		Debug.Log("Style 3 : " + shirtAndPantStyle3SkinnedMeshRenderer.materials.Length);
		Debug.Log("Style 4 : " + shirtAndPantStyle4SkinnedMeshRenderer.materials.Length);
	}
	
	void Update(){
		if(!isLocalPlayer) return;
	}
	
	#region Hair
	public void ChangeHairStyle(int value){
		foreach (GameObject model in hairStyles){
			model.SetActive(false);
		}
		if(value == 0){
			hairStyles[0].SetActive(true);
		}else if(value == 1){
			hairStyles[1].SetActive(true);
		}else if(value == 2){
			hairStyles[2].SetActive(true);
		}
	}
	
	public void ChangeHairColor(int value){
		if(value == 0){
			hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[0];
			hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[0];
			hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[0];
		}else if(value == 1){
			hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[1];
			hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[1];
			hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[1];
		}else if(value == 2){
			hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[2];
			hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[2];
			hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[2];
		}else if(value == 3){
			hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[3];
			hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[3];
			hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[3];
		}else if(value == 4){
			hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[4];
			hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[4];
			hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[4];
		}else if(value == 5){
			hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[5];
			hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[5];
			hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[5];
		}else if(value == 6){
			hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[6];
			hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[6];
			hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[6];
		}else if(value == 7){
			hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[7];
			hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[7];
			hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[7];
		}
	}
	#endregion
	
	#region Face
	public void ChangeFaceStyle(int value){
		if(value == 0) faceSkinnedMeshRenderer.material = faceMaterials[0];
		if(value == 1) faceSkinnedMeshRenderer.material = faceMaterials[1];
		if(value == 2) faceSkinnedMeshRenderer.material = faceMaterials[2];
		if(value == 3) faceSkinnedMeshRenderer.material = faceMaterials[3];
	}
	#endregion
	
	#region Shirt and Pants
	public void ChangeShirtAndPantsStyle(int value){
		foreach (GameObject model in shirtAndPantStyles){
			model.SetActive(false);
		}
		
		if(value == 0){
			shirtAndPantStyles[0].SetActive(true);
		}else if(value == 1){
			shirtAndPantStyles[1].SetActive(true);
		}else if(value == 2){
			shirtAndPantStyles[2].SetActive(true);
		}else if(value == 3){
			shirtAndPantStyles[3].SetActive(true);
		}
	}
	
	public void ChangeShirtColor(int value){
		Material [] materials1 = shirtAndPantStyle1SkinnedMeshRenderer.materials;
		Material [] materials2 = shirtAndPantStyle2SkinnedMeshRenderer.materials;
		Material [] materials3 = shirtAndPantStyle3SkinnedMeshRenderer.materials;
		Material [] materials4 = shirtAndPantStyle4SkinnedMeshRenderer.materials;
		if(value == 0){
			materials1[0] = shirtMaterials[0];
			materials2[0] = shirtMaterials[0];
			materials3[0] = shirtMaterials[0];
			materials4[0] = shirtMaterials[0];
		}else if(value == 1){
			materials1[0] = shirtMaterials[1];
			materials2[0] = shirtMaterials[1];
			materials3[0] = shirtMaterials[1];
			materials4[0] = shirtMaterials[1];
		}else if(value == 2){
			materials1[0] = shirtMaterials[2];
			materials2[0] = shirtMaterials[2];
			materials3[0] = shirtMaterials[2];
			materials4[0] = shirtMaterials[2];
		}else if(value == 3){
			materials1[0] = shirtMaterials[3];
			materials2[0] = shirtMaterials[3];
			materials3[0] = shirtMaterials[3];
			materials4[0] = shirtMaterials[3];
		}else if(value == 4){
			materials1[0] = shirtMaterials[4];
			materials2[0] = shirtMaterials[4];
			materials3[0] = shirtMaterials[4];
			materials4[0] = shirtMaterials[4];
		}else if(value == 5){
			materials1[0] = shirtMaterials[5];
			materials2[0] = shirtMaterials[5];
			materials3[0] = shirtMaterials[5];
			materials4[0] = shirtMaterials[5];
		}else if(value == 6){
			materials1[0] = shirtMaterials[6];
			materials2[0] = shirtMaterials[6];
			materials3[0] = shirtMaterials[6];
			materials4[0] = shirtMaterials[6];
		}else if(value == 7){
			materials1[0] = shirtMaterials[7];
			materials2[0] = shirtMaterials[7];
			materials3[0] = shirtMaterials[7];
			materials4[0] = shirtMaterials[7];
		}
		shirtAndPantStyle1SkinnedMeshRenderer.materials = materials1;
		shirtAndPantStyle2SkinnedMeshRenderer.materials = materials2;
		shirtAndPantStyle3SkinnedMeshRenderer.materials = materials3;
		shirtAndPantStyle4SkinnedMeshRenderer.materials = materials4;
	}
	
	public void ChangePantsColor(int value){
		Material [] materials1 = shirtAndPantStyle1SkinnedMeshRenderer.materials;
		Material [] materials2 = shirtAndPantStyle2SkinnedMeshRenderer.materials;
		Material [] materials3 = shirtAndPantStyle3SkinnedMeshRenderer.materials;
		Material [] materials4 = shirtAndPantStyle4SkinnedMeshRenderer.materials;
		if(value == 0){
			materials1[1] = pantsMaterials[0];
			materials2[1] = pantsMaterials[0];
			materials3[1] = pantsMaterials[0];
			materials4[1] = pantsMaterials[0];
		}else if(value == 1){
			materials1[1] = pantsMaterials[1];
			materials2[1] = pantsMaterials[1];
			materials3[1] = pantsMaterials[1];
			materials4[1] = pantsMaterials[1];
		}else if(value == 2){
			materials1[1] = pantsMaterials[2];
			materials2[1] = pantsMaterials[2];
			materials3[1] = pantsMaterials[2];
			materials4[1] = pantsMaterials[2];
		}else if(value == 3){
			materials1[1] = pantsMaterials[3];
			materials2[1] = pantsMaterials[3];
			materials3[1] = pantsMaterials[3];
			materials4[1] = pantsMaterials[3];
		}else if(value == 4){
			materials1[1] = pantsMaterials[4];
			materials2[1] = pantsMaterials[4];
			materials3[1] = pantsMaterials[4];
			materials4[1] = pantsMaterials[4];
		}
		shirtAndPantStyle1SkinnedMeshRenderer.materials = materials1;
		shirtAndPantStyle2SkinnedMeshRenderer.materials = materials2;
		shirtAndPantStyle3SkinnedMeshRenderer.materials = materials3;
		shirtAndPantStyle4SkinnedMeshRenderer.materials = materials4;
	}
	
	public void ChangeTieBowtieColor(int value){
		Material [] materials2 = shirtAndPantStyle2SkinnedMeshRenderer.materials;
		Material [] materials3 = shirtAndPantStyle3SkinnedMeshRenderer.materials;
		Material [] materials4 = shirtAndPantStyle4SkinnedMeshRenderer.materials;
		if(value == 0){
			materials2[2] = tieBowtieMaterials[0];
			materials3[2] = tieBowtieMaterials[0];
			materials4[2] = tieBowtieMaterials[0];
		}else if(value == 1){
			materials2[2] = tieBowtieMaterials[1];
			materials3[2] = tieBowtieMaterials[1];
			materials4[2] = tieBowtieMaterials[1];
		}else if(value == 2){
			materials2[2] = tieBowtieMaterials[2];
			materials3[2] = tieBowtieMaterials[2];
			materials4[2] = tieBowtieMaterials[2];
		}else if(value == 3){
			materials2[2] = tieBowtieMaterials[3];
			materials3[2] = tieBowtieMaterials[3];
			materials4[2] = tieBowtieMaterials[3];
		}else if(value == 4){
			materials2[2] = tieBowtieMaterials[4];
			materials3[2] = tieBowtieMaterials[4];
			materials4[2] = tieBowtieMaterials[4];
		}else if(value == 5){
			materials2[2] = tieBowtieMaterials[5];
			materials3[2] = tieBowtieMaterials[5];
			materials4[2] = tieBowtieMaterials[5];
		}else if(value == 6){
			materials2[2] = tieBowtieMaterials[6];
			materials3[2] = tieBowtieMaterials[6];
			materials4[2] = tieBowtieMaterials[6];
		}else if(value == 7){
			materials2[2] = tieBowtieMaterials[7];
			materials3[2] = tieBowtieMaterials[7];
			materials4[2] = tieBowtieMaterials[7];
		}else if(value == 8){
			materials2[2] = tieBowtieMaterials[8];
			materials3[2] = tieBowtieMaterials[8];
			materials4[2] = tieBowtieMaterials[8];
		}else if(value == 9){
			materials2[2] = tieBowtieMaterials[9];
			materials3[2] = tieBowtieMaterials[9];
			materials4[2] = tieBowtieMaterials[9];
		}else if(value == 10){
			materials2[2] = tieBowtieMaterials[10];
			materials3[2] = tieBowtieMaterials[10];
			materials4[2] = tieBowtieMaterials[10];
		}else if(value == 11){
			materials2[2] = tieBowtieMaterials[11];
			materials3[2] = tieBowtieMaterials[11];
			materials4[2] = tieBowtieMaterials[11];
		}
		shirtAndPantStyle2SkinnedMeshRenderer.materials = materials2;
		shirtAndPantStyle3SkinnedMeshRenderer.materials = materials3;
		shirtAndPantStyle4SkinnedMeshRenderer.materials = materials4;
	}
	#endregion
	
}