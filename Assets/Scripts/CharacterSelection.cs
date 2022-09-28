using UnityEngine;
using Mirror;

public class CharacterSelection : NetworkBehaviour{
	[Header("Hair")]
	[SerializeField] GameObject [] hairStyles;
	//[SerializeField] TMP_Dropdown hairStylesDD;
	//int hairStylesIndex;
	//[SerializeField] Dropdown hairStylesDD;
	[SerializeField] Material [] hairStyle1Materials;
	SkinnedMeshRenderer hairStyle1SkinnedMeshRenderer;
	[SerializeField] Material [] hairStyle2Materials;
	SkinnedMeshRenderer hairStyle2SkinnedMeshRenderer;
	[SerializeField] Material [] hairStyle3Materials;
	SkinnedMeshRenderer hairStyle3SkinnedMeshRenderer;

	[Header("Face and Glasses")]
	[SerializeField] GameObject face;
	[SerializeField] Material [] faceMaterials;
	SkinnedMeshRenderer faceSkinnedMeshRenderer;
	
	[SerializeField] GameObject [] glassesStyles;
	//For materials we use the same as the Tie and Bowtie materials below
	SkinnedMeshRenderer glasses1SkinnedMeshRenderer;
	SkinnedMeshRenderer glasses2SkinnedMeshRenderer;
	SkinnedMeshRenderer glasses3SkinnedMeshRenderer;
	
	[Header("Shirt and Pants")]
	[SerializeField] GameObject [] shirtAndPantStyles;
	[SerializeField] Material [] shirtMaterials;
	[SerializeField] Material [] pantsMaterials;
	[SerializeField] Material [] tieBowtieGlassesMaterials;
	SkinnedMeshRenderer shirtAndPantStyle1SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle2SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle3SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle4SkinnedMeshRenderer;
	
	[Header("Shoes")]
	[SerializeField] GameObject [] shoesStyles;
	[SerializeField] Material [] buisnessShoesMaterials; 
	[SerializeField] Material [] casualShoesMaterials; 
	SkinnedMeshRenderer businessStyleSkinnedMeshRenderer;
	SkinnedMeshRenderer casualStyleSkinnedMeshRenderer;
	
	void Start(){
		if(!isLocalPlayer) return;
		//Hair
		hairStyle1SkinnedMeshRenderer = hairStyles[0].GetComponent<SkinnedMeshRenderer>();
		hairStyle2SkinnedMeshRenderer = hairStyles[1].GetComponent<SkinnedMeshRenderer>();
		hairStyle3SkinnedMeshRenderer = hairStyles[2].GetComponent<SkinnedMeshRenderer>();
		
		//Face and glasses
		faceSkinnedMeshRenderer = face.GetComponent<SkinnedMeshRenderer>();
		glasses1SkinnedMeshRenderer = glassesStyles[0].GetComponent<SkinnedMeshRenderer>();
		glasses2SkinnedMeshRenderer = glassesStyles[1].GetComponent<SkinnedMeshRenderer>();
		glasses3SkinnedMeshRenderer = glassesStyles[2].GetComponent<SkinnedMeshRenderer>();
		
		//Shirt and pants
		shirtAndPantStyle1SkinnedMeshRenderer = shirtAndPantStyles[0].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle2SkinnedMeshRenderer = shirtAndPantStyles[1].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle3SkinnedMeshRenderer = shirtAndPantStyles[2].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle4SkinnedMeshRenderer = shirtAndPantStyles[3].GetComponent<SkinnedMeshRenderer>();
		
		//Shoes
		businessStyleSkinnedMeshRenderer = shoesStyles[0].GetComponent<SkinnedMeshRenderer>();
		casualStyleSkinnedMeshRenderer = shoesStyles[1].GetComponent<SkinnedMeshRenderer>();
	}
	
	void Update(){
		if(!isLocalPlayer) return;
		//Testing with buttons 1, 2 and 3
		//if(Input.GetKeyDown(KeyCode.Alpha1)){
		//	CMDChangeHairStyle(0);
		//}
		//if(Input.GetKeyDown(KeyCode.Alpha2)){
		//	CMDChangeHairStyle(1);
		//}
		//if(Input.GetKeyDown(KeyCode.Alpha3)){
		//	CMDChangeHairStyle(2);
		//}
	}
	
	#region Hair
	//public void ChangeHairStyle(int value){
	//	CMDChangeHairStyle(value);
	//}
	//
	//[Command]
	//void CMDChangeHairStyle(int value){
	//	RPCChangeHairStyel(value);
	//}
	//
	//[ClientRpc]
	//void RPCChangeHairStyel(int value){ changed name to "public void ChangeHairStyle(int value){" right bellow
	
	public void ChangeHairStyle(int value){
		//if(!isLocalPlayer) return;
		if(value == 0){
			hairStyles[0].SetActive(true);
			hairStyles[1].SetActive(false);
			hairStyles[2].SetActive(false);
		}else if(value == 1){
			hairStyles[0].SetActive(false);
			hairStyles[1].SetActive(true);
			hairStyles[2].SetActive(false);
		}else if(value == 2){
			hairStyles[0].SetActive(false);
			hairStyles[1].SetActive(false);
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
	
	#region Face and Glasses
	public void ChangeFaceStyle(int value){
		if(value == 0) faceSkinnedMeshRenderer.material = faceMaterials[0];
		if(value == 1) faceSkinnedMeshRenderer.material = faceMaterials[1];
		if(value == 2) faceSkinnedMeshRenderer.material = faceMaterials[2];
		if(value == 3) faceSkinnedMeshRenderer.material = faceMaterials[3];
	}
	
	public void ChangeGlassesStyle(int value){
		foreach (GameObject model in glassesStyles){
			model.SetActive(false);
		}
		if(value == 0){
			return;
		}else if(value == 1){
			glassesStyles[0].SetActive(true);
		}else if(value == 2){
			glassesStyles[1].SetActive(true);
		}else if(value == 3){
			glassesStyles[2].SetActive(true);
		}
	}
	
	public void ChangeGlassesColor(int value){
		if(value == 0){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[0];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[0];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[0];
		}else if(value == 1){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[1];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[1];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[1];
		}else if(value == 2){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[2];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[2];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[2];
		}else if(value == 3){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[3];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[3];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[3];
		}else if(value == 4){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[4];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[4];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[4];
		}else if(value == 5){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[5];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[5];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[5];
		}else if(value == 6){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[6];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[6];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[6];
		}else if(value == 7){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[7];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[7];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[7];
		}else if(value == 8){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[8];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[8];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[8];
		}else if(value == 9){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[9];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[9];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[9];
		}else if(value == 10){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[10];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[10];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[10];
		}else if(value == 11){
			glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[11];
			glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[11];
			glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[11];
		}
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
			materials2[2] = tieBowtieGlassesMaterials[0];
			materials3[2] = tieBowtieGlassesMaterials[0];
			materials4[2] = tieBowtieGlassesMaterials[0];
		}else if(value == 1){
			materials2[2] = tieBowtieGlassesMaterials[1];
			materials3[2] = tieBowtieGlassesMaterials[1];
			materials4[2] = tieBowtieGlassesMaterials[1];
		}else if(value == 2){
			materials2[2] = tieBowtieGlassesMaterials[2];
			materials3[2] = tieBowtieGlassesMaterials[2];
			materials4[2] = tieBowtieGlassesMaterials[2];
		}else if(value == 3){
			materials2[2] = tieBowtieGlassesMaterials[3];
			materials3[2] = tieBowtieGlassesMaterials[3];
			materials4[2] = tieBowtieGlassesMaterials[3];
		}else if(value == 4){
			materials2[2] = tieBowtieGlassesMaterials[4];
			materials3[2] = tieBowtieGlassesMaterials[4];
			materials4[2] = tieBowtieGlassesMaterials[4];
		}else if(value == 5){
			materials2[2] = tieBowtieGlassesMaterials[5];
			materials3[2] = tieBowtieGlassesMaterials[5];
			materials4[2] = tieBowtieGlassesMaterials[5];
		}else if(value == 6){
			materials2[2] = tieBowtieGlassesMaterials[6];
			materials3[2] = tieBowtieGlassesMaterials[6];
			materials4[2] = tieBowtieGlassesMaterials[6];
		}else if(value == 7){
			materials2[2] = tieBowtieGlassesMaterials[7];
			materials3[2] = tieBowtieGlassesMaterials[7];
			materials4[2] = tieBowtieGlassesMaterials[7];
		}else if(value == 8){
			materials2[2] = tieBowtieGlassesMaterials[8];
			materials3[2] = tieBowtieGlassesMaterials[8];
			materials4[2] = tieBowtieGlassesMaterials[8];
		}else if(value == 9){
			materials2[2] = tieBowtieGlassesMaterials[9];
			materials3[2] = tieBowtieGlassesMaterials[9];
			materials4[2] = tieBowtieGlassesMaterials[9];
		}else if(value == 10){
			materials2[2] = tieBowtieGlassesMaterials[10];
			materials3[2] = tieBowtieGlassesMaterials[10];
			materials4[2] = tieBowtieGlassesMaterials[10];
		}else if(value == 11){
			materials2[2] = tieBowtieGlassesMaterials[11];
			materials3[2] = tieBowtieGlassesMaterials[11];
			materials4[2] = tieBowtieGlassesMaterials[11];
		}
		shirtAndPantStyle2SkinnedMeshRenderer.materials = materials2;
		shirtAndPantStyle3SkinnedMeshRenderer.materials = materials3;
		shirtAndPantStyle4SkinnedMeshRenderer.materials = materials4;
	}
	#endregion
	
	#region Shoes
	public void ChangeShoesStyle(int value){
		foreach (GameObject model in shoesStyles){
			model.SetActive(false);
		}
		if(value == 0){
			shoesStyles[0].SetActive(true);
		}else if(value == 1){
			shoesStyles[1].SetActive(true);
		}
	}
	
	public void ChangeBuisnessShoesColor(int value){
		if(value == 0){
			businessStyleSkinnedMeshRenderer.material = buisnessShoesMaterials[0];
		}else if(value == 1){
			businessStyleSkinnedMeshRenderer.material = buisnessShoesMaterials[1];
		}else if(value == 2){
			businessStyleSkinnedMeshRenderer.material = buisnessShoesMaterials[2];
		}
	}
	
	public void ChangeCasualShoesColor(int value){
		if(value == 0){
			casualStyleSkinnedMeshRenderer.material = casualShoesMaterials[0];
		}else if(value == 1){
			casualStyleSkinnedMeshRenderer.material = casualShoesMaterials[1];
		}else if(value == 2){
			casualStyleSkinnedMeshRenderer.material = casualShoesMaterials[2];
		}else if(value == 3){
			casualStyleSkinnedMeshRenderer.material = casualShoesMaterials[3];
		}
	}
	#endregion
}