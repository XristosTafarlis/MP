using UnityEngine;
using Mirror;

public class CharacterSelection : NetworkBehaviour{
	#region Declerations
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

	[Header("Face, Glasses and Eyes")]
	[SerializeField] GameObject face;
	[SerializeField] Material [] faceMaterials;
	SkinnedMeshRenderer faceSkinnedMeshRenderer;
	
	[SerializeField] GameObject [] glassesStyles;
	//For materials we use the same as the Tie and Bowtie materials below
	SkinnedMeshRenderer glasses1SkinnedMeshRenderer;
	SkinnedMeshRenderer glasses2SkinnedMeshRenderer;
	SkinnedMeshRenderer glasses3SkinnedMeshRenderer;
	
	[SerializeField] GameObject eyes;
	[SerializeField] Material [] eyesMaterials;
	SkinnedMeshRenderer eyesSkinnedMeshRenderer;
	
	[Header("Shirt and Pants")]
	[SerializeField] GameObject [] shirtAndPantStyles;
	[SerializeField] Material [] shirtMaterials;
	[SerializeField] Material [] pantsMaterials;
	[SerializeField] Material [] tieBowtieGlassesMaterials;
	SkinnedMeshRenderer shirtAndPantStyle1SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle2SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle3SkinnedMeshRenderer;
	SkinnedMeshRenderer shirtAndPantStyle4SkinnedMeshRenderer;
	
	enum ShirtColor{ //We use this enum to keep track of the shirt's color. We need the shirt's color when we add a vest.
		Black,
		Blue,
		Gray,
		Green,
		Pink,
		Red,
		White,
		Yellow
	}
	ShirtColor myShirtColor = ShirtColor.Black;
	
	[Header("Shoes")]
	[SerializeField] GameObject [] shoesStyles;
	[SerializeField] Material [] buisnessShoesMaterials; 
	[SerializeField] Material [] casualShoesMaterials; 
	SkinnedMeshRenderer businessStyleSkinnedMeshRenderer;
	SkinnedMeshRenderer casualStyleSkinnedMeshRenderer;
	[Space(10)]
	[SerializeField] GameObject casualShoesDropDown;
	[SerializeField] GameObject businessShoesDropDown;
	
	[Header("Suits")]
	[SerializeField] GameObject [] suitStyles;
	//For Shirt and Pants materials we use the same as the Shirt and Pants materials above
	//For Tie and Bowtie materials we use the same as the Tie and Bowtie materials above
	[SerializeField] Material [] closedJacketMaterials;
	[SerializeField] Material [] openJacketMaterials;
	SkinnedMeshRenderer suitStyle1SkinnedMeshRenderer;//Closed suits
	SkinnedMeshRenderer suitStyle2SkinnedMeshRenderer;
	SkinnedMeshRenderer suitStyle3SkinnedMeshRenderer;
	SkinnedMeshRenderer suitStyle4SkinnedMeshRenderer;
	SkinnedMeshRenderer suitStyle5SkinnedMeshRenderer;//Open suits
	SkinnedMeshRenderer suitStyle6SkinnedMeshRenderer;
	SkinnedMeshRenderer suitStyle7SkinnedMeshRenderer;
	SkinnedMeshRenderer suitStyle8SkinnedMeshRenderer;
	[Space(10)]
	[SerializeField] Material [] blackShirtVestMaterials;
	[SerializeField] Material [] blueShirtVestMaterials;
	[SerializeField] Material [] grayShirtVestMaterials;
	[SerializeField] Material [] greenShirtVestMaterials;
	[SerializeField] Material [] pinkShirtVestMaterials;
	[SerializeField] Material [] redShirtVestMaterials;
	[SerializeField] Material [] whiteShirtVestMaterials;
	[SerializeField] Material [] yellowShirtVestMaterials;
	[Space(10)]
	[SerializeField] GameObject suitStyleText;
	[SerializeField] GameObject suitStyleDropDown;
	[SerializeField] GameObject jacketAndVestText;
	[SerializeField] GameObject jacketColorDropDown;
	[SerializeField] GameObject vestColorDropDown;
	
	enum VestColor{
		AsterFlowers,
		Black,
		Brown,
		Coral,
		Gray,
		Houndstooth,
		NavyStripe,
		Red,
		Tartan,
		White
	}
	VestColor? myVestColor = null; //This is set to null by default because the player has no vest.
	//The above enum should be used in simmilar fashion like the myShirtColor enum in ChangeShirtColor(); function
	#endregion
	
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
		eyesSkinnedMeshRenderer = eyes.GetComponent<SkinnedMeshRenderer>();
		
		//Shirt and pants
		shirtAndPantStyle1SkinnedMeshRenderer = shirtAndPantStyles[0].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle2SkinnedMeshRenderer = shirtAndPantStyles[1].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle3SkinnedMeshRenderer = shirtAndPantStyles[2].GetComponent<SkinnedMeshRenderer>();
		shirtAndPantStyle4SkinnedMeshRenderer = shirtAndPantStyles[3].GetComponent<SkinnedMeshRenderer>();
		
		//Shoes
		businessStyleSkinnedMeshRenderer = shoesStyles[0].GetComponent<SkinnedMeshRenderer>();
		casualStyleSkinnedMeshRenderer = shoesStyles[1].GetComponent<SkinnedMeshRenderer>();
		
		//Suits
		suitStyle1SkinnedMeshRenderer = suitStyles[0].GetComponent<SkinnedMeshRenderer>();
		suitStyle2SkinnedMeshRenderer = suitStyles[1].GetComponent<SkinnedMeshRenderer>();
		suitStyle3SkinnedMeshRenderer = suitStyles[2].GetComponent<SkinnedMeshRenderer>();
		suitStyle4SkinnedMeshRenderer = suitStyles[3].GetComponent<SkinnedMeshRenderer>();
		suitStyle5SkinnedMeshRenderer = suitStyles[4].GetComponent<SkinnedMeshRenderer>();
		suitStyle6SkinnedMeshRenderer = suitStyles[5].GetComponent<SkinnedMeshRenderer>();
		suitStyle7SkinnedMeshRenderer = suitStyles[6].GetComponent<SkinnedMeshRenderer>();
		suitStyle8SkinnedMeshRenderer = suitStyles[7].GetComponent<SkinnedMeshRenderer>();
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
	//void RPCChangeHairStyel(int value){ /*changed name to "public void ChangeHairStyle(int value){" right bellow*/
	
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
	
	#region Face, Glasses and Eyes
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
	
	public void ChangeEyesColor(int value){
		switch (value){
			case 0:
				eyesSkinnedMeshRenderer.material = eyesMaterials[0];
				break;
			case 1:
				eyesSkinnedMeshRenderer.material = eyesMaterials[1];
				break;
			case 2:
				eyesSkinnedMeshRenderer.material = eyesMaterials[2];
				break;
			case 3:
				eyesSkinnedMeshRenderer.material = eyesMaterials[3];
				break;
			case 4:
				eyesSkinnedMeshRenderer.material = eyesMaterials[4];
				break;
			case 5:
				eyesSkinnedMeshRenderer.material = eyesMaterials[5];
				break;
			case 6:
				eyesSkinnedMeshRenderer.material = eyesMaterials[6];
				break;
			case 7:
				eyesSkinnedMeshRenderer.material = eyesMaterials[7];
				break;
			case 8:
				eyesSkinnedMeshRenderer.material = eyesMaterials[8];
				break;
			case 9:
				eyesSkinnedMeshRenderer.material = eyesMaterials[9];
				break;
		}
	}
	#endregion
	
	#region Shirt and Pants
	public void ChangeShirtAndPantsStyle(int value){
		foreach (GameObject model in shirtAndPantStyles){//Disable all shirts so we enable only the one we want
			model.SetActive(false);
		}
		foreach (GameObject model in suitStyles){//Disable all suits if we choose simple Shirt
			model.SetActive(false);
		}
		
		switch (value){
			case 0:
				shirtAndPantStyles[0].SetActive(true);
				HideSuitOptions();
				break;
			case 1:
				shirtAndPantStyles[1].SetActive(true);
				HideSuitOptions();
				break;
			case 2:
				shirtAndPantStyles[2].SetActive(true);
				HideSuitOptions();
				break;
			case 3:
				shirtAndPantStyles[3].SetActive(true);
				HideSuitOptions();
				break;
			case 4:
				suitStyles[0].SetActive(true); //Make the 1st suit visible
				suitStyleDropDown.SetActive(true);
				suitStyleText.SetActive(true);
				jacketAndVestText.SetActive(true);
				jacketColorDropDown.SetActive(true);
				vestColorDropDown.SetActive(true);
				break;
		}
	}
	
	//Quick small function called only above ^ to hide the Suit stuff
	void HideSuitOptions(){
		suitStyleDropDown.SetActive(false);
		suitStyleText.SetActive(false);
		jacketAndVestText.SetActive(false);
		jacketColorDropDown.SetActive(false);
		vestColorDropDown.SetActive(false);
	}
	
	public void ChangeShirtColor(int value){
		Material [] materials1 = shirtAndPantStyle1SkinnedMeshRenderer.materials;
		Material [] materials2 = shirtAndPantStyle2SkinnedMeshRenderer.materials;
		Material [] materials3 = shirtAndPantStyle3SkinnedMeshRenderer.materials;
		Material [] materials4 = shirtAndPantStyle4SkinnedMeshRenderer.materials;
		
		Material [] materialSuit1 = suitStyle1SkinnedMeshRenderer.materials;
		Material [] materialSuit2 = suitStyle2SkinnedMeshRenderer.materials;
		Material [] materialSuit3 = suitStyle3SkinnedMeshRenderer.materials;
		Material [] materialSuit4 = suitStyle4SkinnedMeshRenderer.materials;
		Material [] materialSuit5 = suitStyle5SkinnedMeshRenderer.materials;
		Material [] materialSuit6 = suitStyle6SkinnedMeshRenderer.materials;
		Material [] materialSuit7 = suitStyle7SkinnedMeshRenderer.materials;
		Material [] materialSuit8 = suitStyle8SkinnedMeshRenderer.materials;
		if(value == 0){
			materials1[0] = shirtMaterials[0];
			materials2[0] = shirtMaterials[0];
			materials3[0] = shirtMaterials[0];
			materials4[0] = shirtMaterials[0];
			
			materialSuit1[1] = shirtMaterials[0];
			materialSuit2[1] = shirtMaterials[0];
			materialSuit3[2] = shirtMaterials[0];
			materialSuit4[1] = shirtMaterials[0];
			materialSuit5[1] = shirtMaterials[0];
			materialSuit6[1] = shirtMaterials[0];
			materialSuit7[1] = shirtMaterials[0];
			materialSuit8[1] = shirtMaterials[0];
			myShirtColor = ShirtColor.Black;
		}else if(value == 1){
			materials1[0] = shirtMaterials[1];
			materials2[0] = shirtMaterials[1];
			materials3[0] = shirtMaterials[1];
			materials4[0] = shirtMaterials[1];
			
			materialSuit1[1] = shirtMaterials[1];
			materialSuit2[1] = shirtMaterials[1];
			materialSuit3[2] = shirtMaterials[1];
			materialSuit4[1] = shirtMaterials[1];
			materialSuit5[1] = shirtMaterials[1];
			materialSuit6[1] = shirtMaterials[1];
			materialSuit7[1] = shirtMaterials[1];
			materialSuit8[1] = shirtMaterials[1];
			myShirtColor = ShirtColor.Blue;
		}else if(value == 2){
			materials1[0] = shirtMaterials[2];
			materials2[0] = shirtMaterials[2];
			materials3[0] = shirtMaterials[2];
			materials4[0] = shirtMaterials[2];
			
			materialSuit1[1] = shirtMaterials[2];
			materialSuit2[1] = shirtMaterials[2];
			materialSuit3[2] = shirtMaterials[2];
			materialSuit4[1] = shirtMaterials[2];
			materialSuit5[1] = shirtMaterials[2];
			materialSuit6[1] = shirtMaterials[2];
			materialSuit7[1] = shirtMaterials[2];
			materialSuit8[1] = shirtMaterials[2];
			myShirtColor = ShirtColor.Gray;
		}else if(value == 3){
			materials1[0] = shirtMaterials[3];
			materials2[0] = shirtMaterials[3];
			materials3[0] = shirtMaterials[3];
			materials4[0] = shirtMaterials[3];
			
			materialSuit1[1] = shirtMaterials[3];
			materialSuit2[1] = shirtMaterials[3];
			materialSuit3[2] = shirtMaterials[3];
			materialSuit4[1] = shirtMaterials[3];
			materialSuit5[1] = shirtMaterials[3];
			materialSuit6[1] = shirtMaterials[3];
			materialSuit7[1] = shirtMaterials[3];
			materialSuit8[1] = shirtMaterials[3];
			myShirtColor = ShirtColor.Green;
		}else if(value == 4){
			materials1[0] = shirtMaterials[4];
			materials2[0] = shirtMaterials[4];
			materials3[0] = shirtMaterials[4];
			materials4[0] = shirtMaterials[4];
			
			materialSuit1[1] = shirtMaterials[4];
			materialSuit2[1] = shirtMaterials[4];
			materialSuit3[2] = shirtMaterials[4];
			materialSuit4[1] = shirtMaterials[4];
			materialSuit5[1] = shirtMaterials[4];
			materialSuit6[1] = shirtMaterials[4];
			materialSuit7[1] = shirtMaterials[4];
			materialSuit8[1] = shirtMaterials[4];
			myShirtColor = ShirtColor.Pink;
		}else if(value == 5){
			materials1[0] = shirtMaterials[5];
			materials2[0] = shirtMaterials[5];
			materials3[0] = shirtMaterials[5];
			materials4[0] = shirtMaterials[5];
			
			materialSuit1[1] = shirtMaterials[5];
			materialSuit2[1] = shirtMaterials[5];
			materialSuit3[2] = shirtMaterials[5];
			materialSuit4[1] = shirtMaterials[5];
			materialSuit5[1] = shirtMaterials[5];
			materialSuit6[1] = shirtMaterials[5];
			materialSuit7[1] = shirtMaterials[5];
			materialSuit8[1] = shirtMaterials[5];
			myShirtColor = ShirtColor.Red;
		}else if(value == 6){
			materials1[0] = shirtMaterials[6];
			materials2[0] = shirtMaterials[6];
			materials3[0] = shirtMaterials[6];
			materials4[0] = shirtMaterials[6];
			
			materialSuit1[1] = shirtMaterials[6];
			materialSuit2[1] = shirtMaterials[6];
			materialSuit3[2] = shirtMaterials[6];
			materialSuit4[1] = shirtMaterials[6];
			materialSuit5[1] = shirtMaterials[6];
			materialSuit6[1] = shirtMaterials[6];
			materialSuit7[1] = shirtMaterials[6];
			materialSuit8[1] = shirtMaterials[6];
			myShirtColor = ShirtColor.White;
		}else if(value == 7){
			materials1[0] = shirtMaterials[7];
			materials2[0] = shirtMaterials[7];
			materials3[0] = shirtMaterials[7];
			materials4[0] = shirtMaterials[7];
			
			materialSuit1[1] = shirtMaterials[7];
			materialSuit2[1] = shirtMaterials[7];
			materialSuit3[2] = shirtMaterials[7];
			materialSuit4[1] = shirtMaterials[7];
			materialSuit5[1] = shirtMaterials[7];
			materialSuit6[1] = shirtMaterials[7];
			materialSuit7[1] = shirtMaterials[7];
			materialSuit8[1] = shirtMaterials[7];
			myShirtColor = ShirtColor.Yellow;
		}
		shirtAndPantStyle1SkinnedMeshRenderer.materials = materials1;
		shirtAndPantStyle2SkinnedMeshRenderer.materials = materials2;
		shirtAndPantStyle3SkinnedMeshRenderer.materials = materials3;
		shirtAndPantStyle4SkinnedMeshRenderer.materials = materials4;
		
		suitStyle1SkinnedMeshRenderer.materials = materialSuit1;
		suitStyle2SkinnedMeshRenderer.materials = materialSuit2;
		suitStyle3SkinnedMeshRenderer.materials = materialSuit3;
		suitStyle4SkinnedMeshRenderer.materials = materialSuit4;
		suitStyle5SkinnedMeshRenderer.materials = materialSuit5;
		suitStyle6SkinnedMeshRenderer.materials = materialSuit6;
		suitStyle7SkinnedMeshRenderer.materials = materialSuit7;
		suitStyle8SkinnedMeshRenderer.materials = materialSuit8;
	}
	
	public void ChangePantsColor(int value){
		Material [] materials1 = shirtAndPantStyle1SkinnedMeshRenderer.materials;
		Material [] materials2 = shirtAndPantStyle2SkinnedMeshRenderer.materials;
		Material [] materials3 = shirtAndPantStyle3SkinnedMeshRenderer.materials;
		Material [] materials4 = shirtAndPantStyle4SkinnedMeshRenderer.materials;
		
		Material [] materialSuit1 = suitStyle1SkinnedMeshRenderer.materials;
		Material [] materialSuit2 = suitStyle2SkinnedMeshRenderer.materials;
		Material [] materialSuit3 = suitStyle3SkinnedMeshRenderer.materials;
		Material [] materialSuit4 = suitStyle4SkinnedMeshRenderer.materials;
		Material [] materialSuit5 = suitStyle5SkinnedMeshRenderer.materials;
		Material [] materialSuit6 = suitStyle6SkinnedMeshRenderer.materials;
		Material [] materialSuit7 = suitStyle7SkinnedMeshRenderer.materials;
		Material [] materialSuit8 = suitStyle8SkinnedMeshRenderer.materials;
		if(value == 0){
			materials1[1] = pantsMaterials[0];
			materials2[1] = pantsMaterials[0];
			materials3[1] = pantsMaterials[0];
			materials4[1] = pantsMaterials[0];
			
			materialSuit1[0] = pantsMaterials[0];
			materialSuit2[0] = pantsMaterials[0];
			materialSuit3[0] = pantsMaterials[0];
			materialSuit4[0] = pantsMaterials[0];
			materialSuit5[2] = pantsMaterials[0];
			materialSuit6[2] = pantsMaterials[0];
			materialSuit7[2] = pantsMaterials[0];
			materialSuit8[2] = pantsMaterials[0];
		}else if(value == 1){
			materials1[1] = pantsMaterials[1];
			materials2[1] = pantsMaterials[1];
			materials3[1] = pantsMaterials[1];
			materials4[1] = pantsMaterials[1];
			
			materialSuit1[0] = pantsMaterials[1];
			materialSuit2[0] = pantsMaterials[1];
			materialSuit3[0] = pantsMaterials[1];
			materialSuit4[0] = pantsMaterials[1];
			materialSuit5[2] = pantsMaterials[1];
			materialSuit6[2] = pantsMaterials[1];
			materialSuit7[2] = pantsMaterials[1];
			materialSuit8[2] = pantsMaterials[1];
		}else if(value == 2){
			materials1[1] = pantsMaterials[2];
			materials2[1] = pantsMaterials[2];
			materials3[1] = pantsMaterials[2];
			materials4[1] = pantsMaterials[2];
			
			materialSuit1[0] = pantsMaterials[2];
			materialSuit2[0] = pantsMaterials[2];
			materialSuit3[0] = pantsMaterials[2];
			materialSuit4[0] = pantsMaterials[2];
			materialSuit5[2] = pantsMaterials[2];
			materialSuit6[2] = pantsMaterials[2];
			materialSuit7[2] = pantsMaterials[2];
			materialSuit8[2] = pantsMaterials[2];
		}else if(value == 3){
			materials1[1] = pantsMaterials[3];
			materials2[1] = pantsMaterials[3];
			materials3[1] = pantsMaterials[3];
			materials4[1] = pantsMaterials[3];
			
			materialSuit1[0] = pantsMaterials[3];
			materialSuit2[0] = pantsMaterials[3];
			materialSuit3[0] = pantsMaterials[3];
			materialSuit4[0] = pantsMaterials[3];
			materialSuit5[2] = pantsMaterials[3];
			materialSuit6[2] = pantsMaterials[3];
			materialSuit7[2] = pantsMaterials[3];
			materialSuit8[2] = pantsMaterials[3];
		}else if(value == 4){
			materials1[1] = pantsMaterials[4];
			materials2[1] = pantsMaterials[4];
			materials3[1] = pantsMaterials[4];
			materials4[1] = pantsMaterials[4];
			
			materialSuit1[0] = pantsMaterials[4];
			materialSuit2[0] = pantsMaterials[4];
			materialSuit3[0] = pantsMaterials[4];
			materialSuit4[0] = pantsMaterials[4];
			materialSuit5[2] = pantsMaterials[4];
			materialSuit6[2] = pantsMaterials[4];
			materialSuit7[2] = pantsMaterials[4];
			materialSuit8[2] = pantsMaterials[4];
		}
		shirtAndPantStyle1SkinnedMeshRenderer.materials = materials1;
		shirtAndPantStyle2SkinnedMeshRenderer.materials = materials2;
		shirtAndPantStyle3SkinnedMeshRenderer.materials = materials3;
		shirtAndPantStyle4SkinnedMeshRenderer.materials = materials4;
		
		suitStyle1SkinnedMeshRenderer.materials = materialSuit1;
		suitStyle2SkinnedMeshRenderer.materials = materialSuit2;
		suitStyle3SkinnedMeshRenderer.materials = materialSuit3;
		suitStyle4SkinnedMeshRenderer.materials = materialSuit4;
		suitStyle5SkinnedMeshRenderer.materials = materialSuit5;
		suitStyle6SkinnedMeshRenderer.materials = materialSuit6;
		suitStyle7SkinnedMeshRenderer.materials = materialSuit7;
		suitStyle8SkinnedMeshRenderer.materials = materialSuit8;
		Debug.Log(myShirtColor);
	}
	
	public void ChangeTieBowtieColor(int value){
		Material [] materials2 = shirtAndPantStyle2SkinnedMeshRenderer.materials;
		Material [] materials3 = shirtAndPantStyle3SkinnedMeshRenderer.materials;
		Material [] materials4 = shirtAndPantStyle4SkinnedMeshRenderer.materials;
		
		Material [] materialSuit1 = suitStyle1SkinnedMeshRenderer.materials;
		//Material [] materialSuit2 = suitStyle2SkinnedMeshRenderer.materials;	//Has no Tie or Bowtie
		Material [] materialSuit3 = suitStyle3SkinnedMeshRenderer.materials;
		Material [] materialSuit4 = suitStyle4SkinnedMeshRenderer.materials;
		Material [] materialSuit5 = suitStyle5SkinnedMeshRenderer.materials;
		//Material [] materialSuit6 = suitStyle6SkinnedMeshRenderer.materials;	//Has no Tie or Bowtie
		Material [] materialSuit7 = suitStyle7SkinnedMeshRenderer.materials;
		Material [] materialSuit8 = suitStyle8SkinnedMeshRenderer.materials;
		if(value == 0){
			materials2[2] = tieBowtieGlassesMaterials[0];
			materials3[2] = tieBowtieGlassesMaterials[0];
			materials4[2] = tieBowtieGlassesMaterials[0];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[0];
			materialSuit3[3] = tieBowtieGlassesMaterials[0];
			materialSuit4[3] = tieBowtieGlassesMaterials[0];
			materialSuit5[3] = tieBowtieGlassesMaterials[0];
			materialSuit7[3] = tieBowtieGlassesMaterials[0];
			materialSuit8[3] = tieBowtieGlassesMaterials[0];
		}else if(value == 1){
			materials2[2] = tieBowtieGlassesMaterials[1];
			materials3[2] = tieBowtieGlassesMaterials[1];
			materials4[2] = tieBowtieGlassesMaterials[1];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[1];
			materialSuit3[3] = tieBowtieGlassesMaterials[1];
			materialSuit4[3] = tieBowtieGlassesMaterials[1];
			materialSuit5[3] = tieBowtieGlassesMaterials[1];
			materialSuit7[3] = tieBowtieGlassesMaterials[1];
			materialSuit8[3] = tieBowtieGlassesMaterials[1];
		}else if(value == 2){
			materials2[2] = tieBowtieGlassesMaterials[2];
			materials3[2] = tieBowtieGlassesMaterials[2];
			materials4[2] = tieBowtieGlassesMaterials[2];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[2];
			materialSuit3[3] = tieBowtieGlassesMaterials[2];
			materialSuit4[3] = tieBowtieGlassesMaterials[2];
			materialSuit5[3] = tieBowtieGlassesMaterials[2];
			materialSuit7[3] = tieBowtieGlassesMaterials[2];
			materialSuit8[3] = tieBowtieGlassesMaterials[2];
		}else if(value == 3){
			materials2[2] = tieBowtieGlassesMaterials[3];
			materials3[2] = tieBowtieGlassesMaterials[3];
			materials4[2] = tieBowtieGlassesMaterials[3];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[3];
			materialSuit3[3] = tieBowtieGlassesMaterials[3];
			materialSuit4[3] = tieBowtieGlassesMaterials[3];
			materialSuit5[3] = tieBowtieGlassesMaterials[3];
			materialSuit7[3] = tieBowtieGlassesMaterials[3];
			materialSuit8[3] = tieBowtieGlassesMaterials[3];
		}else if(value == 4){
			materials2[2] = tieBowtieGlassesMaterials[4];
			materials3[2] = tieBowtieGlassesMaterials[4];
			materials4[2] = tieBowtieGlassesMaterials[4];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[4];
			materialSuit3[3] = tieBowtieGlassesMaterials[4];
			materialSuit4[3] = tieBowtieGlassesMaterials[4];
			materialSuit5[3] = tieBowtieGlassesMaterials[4];
			materialSuit7[3] = tieBowtieGlassesMaterials[4];
			materialSuit8[3] = tieBowtieGlassesMaterials[4];
		}else if(value == 5){
			materials2[2] = tieBowtieGlassesMaterials[5];
			materials3[2] = tieBowtieGlassesMaterials[5];
			materials4[2] = tieBowtieGlassesMaterials[5];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[5];
			materialSuit3[3] = tieBowtieGlassesMaterials[5];
			materialSuit4[3] = tieBowtieGlassesMaterials[5];
			materialSuit5[3] = tieBowtieGlassesMaterials[5];
			materialSuit7[3] = tieBowtieGlassesMaterials[5];
			materialSuit8[3] = tieBowtieGlassesMaterials[5];
		}else if(value == 6){
			materials2[2] = tieBowtieGlassesMaterials[6];
			materials3[2] = tieBowtieGlassesMaterials[6];
			materials4[2] = tieBowtieGlassesMaterials[6];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[6];
			materialSuit3[3] = tieBowtieGlassesMaterials[6];
			materialSuit4[3] = tieBowtieGlassesMaterials[6];
			materialSuit5[3] = tieBowtieGlassesMaterials[6];
			materialSuit7[3] = tieBowtieGlassesMaterials[6];
			materialSuit8[3] = tieBowtieGlassesMaterials[6];
		}else if(value == 7){
			materials2[2] = tieBowtieGlassesMaterials[7];
			materials3[2] = tieBowtieGlassesMaterials[7];
			materials4[2] = tieBowtieGlassesMaterials[7];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[7];
			materialSuit3[3] = tieBowtieGlassesMaterials[7];
			materialSuit4[3] = tieBowtieGlassesMaterials[7];
			materialSuit5[3] = tieBowtieGlassesMaterials[7];
			materialSuit7[3] = tieBowtieGlassesMaterials[7];
			materialSuit8[3] = tieBowtieGlassesMaterials[7];
		}else if(value == 8){
			materials2[2] = tieBowtieGlassesMaterials[8];
			materials3[2] = tieBowtieGlassesMaterials[8];
			materials4[2] = tieBowtieGlassesMaterials[8];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[8];
			materialSuit3[3] = tieBowtieGlassesMaterials[8];
			materialSuit4[3] = tieBowtieGlassesMaterials[8];
			materialSuit5[3] = tieBowtieGlassesMaterials[8];
			materialSuit7[3] = tieBowtieGlassesMaterials[8];
			materialSuit8[3] = tieBowtieGlassesMaterials[8];
		}else if(value == 9){
			materials2[2] = tieBowtieGlassesMaterials[9];
			materials3[2] = tieBowtieGlassesMaterials[9];
			materials4[2] = tieBowtieGlassesMaterials[9];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[9];
			materialSuit3[3] = tieBowtieGlassesMaterials[9];
			materialSuit4[3] = tieBowtieGlassesMaterials[9];
			materialSuit5[3] = tieBowtieGlassesMaterials[9];
			materialSuit7[3] = tieBowtieGlassesMaterials[9];
			materialSuit8[3] = tieBowtieGlassesMaterials[9];
		}else if(value == 10){
			materials2[2] = tieBowtieGlassesMaterials[10];
			materials3[2] = tieBowtieGlassesMaterials[10];
			materials4[2] = tieBowtieGlassesMaterials[10];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[10];
			materialSuit3[3] = tieBowtieGlassesMaterials[10];
			materialSuit4[3] = tieBowtieGlassesMaterials[10];
			materialSuit5[3] = tieBowtieGlassesMaterials[10];
			materialSuit7[3] = tieBowtieGlassesMaterials[10];
			materialSuit8[3] = tieBowtieGlassesMaterials[10];
		}else if(value == 11){
			materials2[2] = tieBowtieGlassesMaterials[11];
			materials3[2] = tieBowtieGlassesMaterials[11];
			materials4[2] = tieBowtieGlassesMaterials[11];
			
			materialSuit1[2] = tieBowtieGlassesMaterials[11];
			materialSuit3[3] = tieBowtieGlassesMaterials[11];
			materialSuit4[3] = tieBowtieGlassesMaterials[11];
			materialSuit5[3] = tieBowtieGlassesMaterials[11];
			materialSuit7[3] = tieBowtieGlassesMaterials[11];
			materialSuit8[3] = tieBowtieGlassesMaterials[11];
		}
		shirtAndPantStyle2SkinnedMeshRenderer.materials = materials2;
		shirtAndPantStyle3SkinnedMeshRenderer.materials = materials3;
		shirtAndPantStyle4SkinnedMeshRenderer.materials = materials4;
		
		suitStyle1SkinnedMeshRenderer.materials = materialSuit1;
		//suitStyle2SkinnedMeshRenderer.materials = materialSuit2;
		suitStyle3SkinnedMeshRenderer.materials = materialSuit3;
		suitStyle4SkinnedMeshRenderer.materials = materialSuit4;
		suitStyle5SkinnedMeshRenderer.materials = materialSuit5;
		//suitStyle6SkinnedMeshRenderer.materials = materialSuit6;
		suitStyle7SkinnedMeshRenderer.materials = materialSuit7;
		suitStyle8SkinnedMeshRenderer.materials = materialSuit8;
	}
	
	public void ChangeJacketColor(int value){
		Material [] materialSuit1 = suitStyle1SkinnedMeshRenderer.materials;
		Material [] materialSuit2 = suitStyle2SkinnedMeshRenderer.materials;
		Material [] materialSuit3 = suitStyle3SkinnedMeshRenderer.materials;
		Material [] materialSuit4 = suitStyle4SkinnedMeshRenderer.materials;
		Material [] materialSuit5 = suitStyle5SkinnedMeshRenderer.materials;
		Material [] materialSuit6 = suitStyle6SkinnedMeshRenderer.materials;
		Material [] materialSuit7 = suitStyle7SkinnedMeshRenderer.materials;
		Material [] materialSuit8 = suitStyle8SkinnedMeshRenderer.materials;
		switch (value){
			case 0:
				materialSuit1[3] = closedJacketMaterials[0];
				materialSuit2[2] = closedJacketMaterials[0];
				materialSuit3[1] = closedJacketMaterials[0];
				materialSuit4[2] = closedJacketMaterials[0];
				materialSuit5[0] = openJacketMaterials[0];
				materialSuit6[0] = openJacketMaterials[0];
				materialSuit7[0] = openJacketMaterials[0];
				materialSuit8[0] = openJacketMaterials[0];
				break;
			case 1:
				materialSuit1[3] = closedJacketMaterials[1];
				materialSuit2[2] = closedJacketMaterials[1];
				materialSuit3[1] = closedJacketMaterials[1];
				materialSuit4[2] = closedJacketMaterials[1];
				materialSuit5[0] = openJacketMaterials[1];
				materialSuit6[0] = openJacketMaterials[1];
				materialSuit7[0] = openJacketMaterials[1];
				materialSuit8[0] = openJacketMaterials[1];
			break;
			case 2:
				materialSuit1[3] = closedJacketMaterials[2];
				materialSuit2[2] = closedJacketMaterials[2];
				materialSuit3[1] = closedJacketMaterials[2];
				materialSuit4[2] = closedJacketMaterials[2];
				materialSuit5[0] = openJacketMaterials[2];
				materialSuit6[0] = openJacketMaterials[2];
				materialSuit7[0] = openJacketMaterials[2];
				materialSuit8[0] = openJacketMaterials[2];
			break;
			case 3:
				materialSuit1[3] = closedJacketMaterials[3];
				materialSuit2[2] = closedJacketMaterials[3];
				materialSuit3[1] = closedJacketMaterials[3];
				materialSuit4[2] = closedJacketMaterials[3];
				materialSuit5[0] = openJacketMaterials[3];
				materialSuit6[0] = openJacketMaterials[3];
				materialSuit7[0] = openJacketMaterials[3];
				materialSuit8[0] = openJacketMaterials[3];
			break;
			case 4:
				materialSuit1[3] = closedJacketMaterials[4];
				materialSuit2[2] = closedJacketMaterials[4];
				materialSuit3[1] = closedJacketMaterials[4];
				materialSuit4[2] = closedJacketMaterials[4];
				materialSuit5[0] = openJacketMaterials[4];
				materialSuit6[0] = openJacketMaterials[4];
				materialSuit7[0] = openJacketMaterials[4];
				materialSuit8[0] = openJacketMaterials[4];
			break;
		}
		suitStyle1SkinnedMeshRenderer.materials = materialSuit1;
		suitStyle2SkinnedMeshRenderer.materials = materialSuit2;
		suitStyle3SkinnedMeshRenderer.materials = materialSuit3;
		suitStyle4SkinnedMeshRenderer.materials = materialSuit4;
		suitStyle5SkinnedMeshRenderer.materials = materialSuit5;
		suitStyle6SkinnedMeshRenderer.materials = materialSuit6;
		suitStyle7SkinnedMeshRenderer.materials = materialSuit7;
		suitStyle8SkinnedMeshRenderer.materials = materialSuit8;
	}
	
	public void ChangeVestColor(int value){
		Material [] materialSuit1 = suitStyle1SkinnedMeshRenderer.materials;
		Material [] materialSuit2 = suitStyle2SkinnedMeshRenderer.materials;
		Material [] materialSuit3 = suitStyle3SkinnedMeshRenderer.materials;
		Material [] materialSuit4 = suitStyle4SkinnedMeshRenderer.materials;
		Material [] materialSuit5 = suitStyle5SkinnedMeshRenderer.materials;
		Material [] materialSuit6 = suitStyle6SkinnedMeshRenderer.materials;
		Material [] materialSuit7 = suitStyle7SkinnedMeshRenderer.materials;
		Material [] materialSuit8 = suitStyle8SkinnedMeshRenderer.materials;
		if(myShirtColor == ShirtColor.Black){
			switch (value){
				case 0:
					materialSuit1[1] = blackShirtVestMaterials[0];
					materialSuit2[1] = blackShirtVestMaterials[0];
					materialSuit3[2] = blackShirtVestMaterials[0];
					materialSuit4[1] = blackShirtVestMaterials[0];
					materialSuit5[1] = blackShirtVestMaterials[0];
					materialSuit6[1] = blackShirtVestMaterials[0];
					materialSuit7[1] = blackShirtVestMaterials[0];
					materialSuit8[1] = blackShirtVestMaterials[0];
					break;
				case 1:
					materialSuit1[1] = blackShirtVestMaterials[1];
					materialSuit2[1] = blackShirtVestMaterials[1];
					materialSuit3[2] = blackShirtVestMaterials[1];
					materialSuit4[1] = blackShirtVestMaterials[1];
					materialSuit5[1] = blackShirtVestMaterials[1];
					materialSuit6[1] = blackShirtVestMaterials[1];
					materialSuit7[1] = blackShirtVestMaterials[1];
					materialSuit8[1] = blackShirtVestMaterials[1];
					break;
				case 2:
					materialSuit1[1] = blackShirtVestMaterials[2];
					materialSuit2[1] = blackShirtVestMaterials[2];
					materialSuit3[2] = blackShirtVestMaterials[2];
					materialSuit4[1] = blackShirtVestMaterials[2];
					materialSuit5[1] = blackShirtVestMaterials[2];
					materialSuit6[1] = blackShirtVestMaterials[2];
					materialSuit7[1] = blackShirtVestMaterials[2];
					materialSuit8[1] = blackShirtVestMaterials[2];
					break;
				case 3:
					materialSuit1[1] = blackShirtVestMaterials[3];
					materialSuit2[1] = blackShirtVestMaterials[3];
					materialSuit3[2] = blackShirtVestMaterials[3];
					materialSuit4[1] = blackShirtVestMaterials[3];
					materialSuit5[1] = blackShirtVestMaterials[3];
					materialSuit6[1] = blackShirtVestMaterials[3];
					materialSuit7[1] = blackShirtVestMaterials[3];
					materialSuit8[1] = blackShirtVestMaterials[3];
					break;
				case 4:
					materialSuit1[1] = blackShirtVestMaterials[4];
					materialSuit2[1] = blackShirtVestMaterials[4];
					materialSuit3[2] = blackShirtVestMaterials[4];
					materialSuit4[1] = blackShirtVestMaterials[4];
					materialSuit5[1] = blackShirtVestMaterials[4];
					materialSuit6[1] = blackShirtVestMaterials[4];
					materialSuit7[1] = blackShirtVestMaterials[4];
					materialSuit8[1] = blackShirtVestMaterials[4];
					break;
				case 5:
					materialSuit1[1] = blackShirtVestMaterials[5];
					materialSuit2[1] = blackShirtVestMaterials[5];
					materialSuit3[2] = blackShirtVestMaterials[5];
					materialSuit4[1] = blackShirtVestMaterials[5];
					materialSuit5[1] = blackShirtVestMaterials[5];
					materialSuit6[1] = blackShirtVestMaterials[5];
					materialSuit7[1] = blackShirtVestMaterials[5];
					materialSuit8[1] = blackShirtVestMaterials[5];
					break;
				case 6:
					materialSuit1[1] = blackShirtVestMaterials[6];
					materialSuit2[1] = blackShirtVestMaterials[6];
					materialSuit3[2] = blackShirtVestMaterials[6];
					materialSuit4[1] = blackShirtVestMaterials[6];
					materialSuit5[1] = blackShirtVestMaterials[6];
					materialSuit6[1] = blackShirtVestMaterials[6];
					materialSuit7[1] = blackShirtVestMaterials[6];
					materialSuit8[1] = blackShirtVestMaterials[6];
					break;
				case 7:
					materialSuit1[1] = blackShirtVestMaterials[7];
					materialSuit2[1] = blackShirtVestMaterials[7];
					materialSuit3[2] = blackShirtVestMaterials[7];
					materialSuit4[1] = blackShirtVestMaterials[7];
					materialSuit5[1] = blackShirtVestMaterials[7];
					materialSuit6[1] = blackShirtVestMaterials[7];
					materialSuit7[1] = blackShirtVestMaterials[7];
					materialSuit8[1] = blackShirtVestMaterials[7];
					break;
				case 8:
					materialSuit1[1] = blackShirtVestMaterials[8];
					materialSuit2[1] = blackShirtVestMaterials[8];
					materialSuit3[2] = blackShirtVestMaterials[8];
					materialSuit4[1] = blackShirtVestMaterials[8];
					materialSuit5[1] = blackShirtVestMaterials[8];
					materialSuit6[1] = blackShirtVestMaterials[8];
					materialSuit7[1] = blackShirtVestMaterials[8];
					materialSuit8[1] = blackShirtVestMaterials[8];
					break;
				case 9:
					materialSuit1[1] = blackShirtVestMaterials[9];
					materialSuit2[1] = blackShirtVestMaterials[9];
					materialSuit3[2] = blackShirtVestMaterials[9];
					materialSuit4[1] = blackShirtVestMaterials[9];
					materialSuit5[1] = blackShirtVestMaterials[9];
					materialSuit6[1] = blackShirtVestMaterials[9];
					materialSuit7[1] = blackShirtVestMaterials[9];
					materialSuit8[1] = blackShirtVestMaterials[9];
					break;
			}
		}else if(myShirtColor == ShirtColor.Blue){
			switch (value){
				case 0:
					materialSuit1[1] = blueShirtVestMaterials[0];
					materialSuit2[1] = blueShirtVestMaterials[0];
					materialSuit3[2] = blueShirtVestMaterials[0];
					materialSuit4[1] = blueShirtVestMaterials[0];
					materialSuit5[1] = blueShirtVestMaterials[0];
					materialSuit6[1] = blueShirtVestMaterials[0];
					materialSuit7[1] = blueShirtVestMaterials[0];
					materialSuit8[1] = blueShirtVestMaterials[0];
					break;
				case 1:
					materialSuit1[1] = blueShirtVestMaterials[1];
					materialSuit2[1] = blueShirtVestMaterials[1];
					materialSuit3[2] = blueShirtVestMaterials[1];
					materialSuit4[1] = blueShirtVestMaterials[1];
					materialSuit5[1] = blueShirtVestMaterials[1];
					materialSuit6[1] = blueShirtVestMaterials[1];
					materialSuit7[1] = blueShirtVestMaterials[1];
					materialSuit8[1] = blueShirtVestMaterials[1];
					break;
				case 2:
					materialSuit1[1] = blueShirtVestMaterials[2];
					materialSuit2[1] = blueShirtVestMaterials[2];
					materialSuit3[2] = blueShirtVestMaterials[2];
					materialSuit4[1] = blueShirtVestMaterials[2];
					materialSuit5[1] = blueShirtVestMaterials[2];
					materialSuit6[1] = blueShirtVestMaterials[2];
					materialSuit7[1] = blueShirtVestMaterials[2];
					materialSuit8[1] = blueShirtVestMaterials[2];
					break;
				case 3:
					materialSuit1[1] = blueShirtVestMaterials[3];
					materialSuit2[1] = blueShirtVestMaterials[3];
					materialSuit3[2] = blueShirtVestMaterials[3];
					materialSuit4[1] = blueShirtVestMaterials[3];
					materialSuit5[1] = blueShirtVestMaterials[3];
					materialSuit6[1] = blueShirtVestMaterials[3];
					materialSuit7[1] = blueShirtVestMaterials[3];
					materialSuit8[1] = blueShirtVestMaterials[3];
					break;
				case 4:
					materialSuit1[1] = blueShirtVestMaterials[4];
					materialSuit2[1] = blueShirtVestMaterials[4];
					materialSuit3[2] = blueShirtVestMaterials[4];
					materialSuit4[1] = blueShirtVestMaterials[4];
					materialSuit5[1] = blueShirtVestMaterials[4];
					materialSuit6[1] = blueShirtVestMaterials[4];
					materialSuit7[1] = blueShirtVestMaterials[4];
					materialSuit8[1] = blueShirtVestMaterials[4];
					break;
				case 5:
					materialSuit1[1] = blueShirtVestMaterials[5];
					materialSuit2[1] = blueShirtVestMaterials[5];
					materialSuit3[2] = blueShirtVestMaterials[5];
					materialSuit4[1] = blueShirtVestMaterials[5];
					materialSuit5[1] = blueShirtVestMaterials[5];
					materialSuit6[1] = blueShirtVestMaterials[5];
					materialSuit7[1] = blueShirtVestMaterials[5];
					materialSuit8[1] = blueShirtVestMaterials[5];
					break;
				case 6:
					materialSuit1[1] = blueShirtVestMaterials[6];
					materialSuit2[1] = blueShirtVestMaterials[6];
					materialSuit3[2] = blueShirtVestMaterials[6];
					materialSuit4[1] = blueShirtVestMaterials[6];
					materialSuit5[1] = blueShirtVestMaterials[6];
					materialSuit6[1] = blueShirtVestMaterials[6];
					materialSuit7[1] = blueShirtVestMaterials[6];
					materialSuit8[1] = blueShirtVestMaterials[6];
					break;
				case 7:
					materialSuit1[1] = blueShirtVestMaterials[7];
					materialSuit2[1] = blueShirtVestMaterials[7];
					materialSuit3[2] = blueShirtVestMaterials[7];
					materialSuit4[1] = blueShirtVestMaterials[7];
					materialSuit5[1] = blueShirtVestMaterials[7];
					materialSuit6[1] = blueShirtVestMaterials[7];
					materialSuit7[1] = blueShirtVestMaterials[7];
					materialSuit8[1] = blueShirtVestMaterials[7];
					break;
				case 8:
					materialSuit1[1] = blueShirtVestMaterials[8];
					materialSuit2[1] = blueShirtVestMaterials[8];
					materialSuit3[2] = blueShirtVestMaterials[8];
					materialSuit4[1] = blueShirtVestMaterials[8];
					materialSuit5[1] = blueShirtVestMaterials[8];
					materialSuit6[1] = blueShirtVestMaterials[8];
					materialSuit7[1] = blueShirtVestMaterials[8];
					materialSuit8[1] = blueShirtVestMaterials[8];
					break;
				case 9:
					materialSuit1[1] = blueShirtVestMaterials[9];
					materialSuit2[1] = blueShirtVestMaterials[9];
					materialSuit3[2] = blueShirtVestMaterials[9];
					materialSuit4[1] = blueShirtVestMaterials[9];
					materialSuit5[1] = blueShirtVestMaterials[9];
					materialSuit6[1] = blueShirtVestMaterials[9];
					materialSuit7[1] = blueShirtVestMaterials[9];
					materialSuit8[1] = blueShirtVestMaterials[9];
					break;
			}
		}else if(myShirtColor == ShirtColor.Gray){
			switch (value){
				case 0:
					materialSuit1[1] = grayShirtVestMaterials[0];
					materialSuit2[1] = grayShirtVestMaterials[0];
					materialSuit3[2] = grayShirtVestMaterials[0];
					materialSuit4[1] = grayShirtVestMaterials[0];
					materialSuit5[1] = grayShirtVestMaterials[0];
					materialSuit6[1] = grayShirtVestMaterials[0];
					materialSuit7[1] = grayShirtVestMaterials[0];
					materialSuit8[1] = grayShirtVestMaterials[0];
					break;
				case 1:
					materialSuit1[1] = grayShirtVestMaterials[1];
					materialSuit2[1] = grayShirtVestMaterials[1];
					materialSuit3[2] = grayShirtVestMaterials[1];
					materialSuit4[1] = grayShirtVestMaterials[1];
					materialSuit5[1] = grayShirtVestMaterials[1];
					materialSuit6[1] = grayShirtVestMaterials[1];
					materialSuit7[1] = grayShirtVestMaterials[1];
					materialSuit8[1] = grayShirtVestMaterials[1];
					break;
				case 2:
					materialSuit1[1] = grayShirtVestMaterials[2];
					materialSuit2[1] = grayShirtVestMaterials[2];
					materialSuit3[2] = grayShirtVestMaterials[2];
					materialSuit4[1] = grayShirtVestMaterials[2];
					materialSuit5[1] = grayShirtVestMaterials[2];
					materialSuit6[1] = grayShirtVestMaterials[2];
					materialSuit7[1] = grayShirtVestMaterials[2];
					materialSuit8[1] = grayShirtVestMaterials[2];
					break;
				case 3:
					materialSuit1[1] = grayShirtVestMaterials[3];
					materialSuit2[1] = grayShirtVestMaterials[3];
					materialSuit3[2] = grayShirtVestMaterials[3];
					materialSuit4[1] = grayShirtVestMaterials[3];
					materialSuit5[1] = grayShirtVestMaterials[3];
					materialSuit6[1] = grayShirtVestMaterials[3];
					materialSuit7[1] = grayShirtVestMaterials[3];
					materialSuit8[1] = grayShirtVestMaterials[3];
					break;
				case 4:
					materialSuit1[1] = grayShirtVestMaterials[4];
					materialSuit2[1] = grayShirtVestMaterials[4];
					materialSuit3[2] = grayShirtVestMaterials[4];
					materialSuit4[1] = grayShirtVestMaterials[4];
					materialSuit5[1] = grayShirtVestMaterials[4];
					materialSuit6[1] = grayShirtVestMaterials[4];
					materialSuit7[1] = grayShirtVestMaterials[4];
					materialSuit8[1] = grayShirtVestMaterials[4];
					break;
				case 5:
					materialSuit1[1] = grayShirtVestMaterials[5];
					materialSuit2[1] = grayShirtVestMaterials[5];
					materialSuit3[2] = grayShirtVestMaterials[5];
					materialSuit4[1] = grayShirtVestMaterials[5];
					materialSuit5[1] = grayShirtVestMaterials[5];
					materialSuit6[1] = grayShirtVestMaterials[5];
					materialSuit7[1] = grayShirtVestMaterials[5];
					materialSuit8[1] = grayShirtVestMaterials[5];
					break;
				case 6:
					materialSuit1[1] = grayShirtVestMaterials[6];
					materialSuit2[1] = grayShirtVestMaterials[6];
					materialSuit3[2] = grayShirtVestMaterials[6];
					materialSuit4[1] = grayShirtVestMaterials[6];
					materialSuit5[1] = grayShirtVestMaterials[6];
					materialSuit6[1] = grayShirtVestMaterials[6];
					materialSuit7[1] = grayShirtVestMaterials[6];
					materialSuit8[1] = grayShirtVestMaterials[6];
					break;
				case 7:
					materialSuit1[1] = grayShirtVestMaterials[7];
					materialSuit2[1] = grayShirtVestMaterials[7];
					materialSuit3[2] = grayShirtVestMaterials[7];
					materialSuit4[1] = grayShirtVestMaterials[7];
					materialSuit5[1] = grayShirtVestMaterials[7];
					materialSuit6[1] = grayShirtVestMaterials[7];
					materialSuit7[1] = grayShirtVestMaterials[7];
					materialSuit8[1] = grayShirtVestMaterials[7];
					break;
				case 8:
					materialSuit1[1] = grayShirtVestMaterials[8];
					materialSuit2[1] = grayShirtVestMaterials[8];
					materialSuit3[2] = grayShirtVestMaterials[8];
					materialSuit4[1] = grayShirtVestMaterials[8];
					materialSuit5[1] = grayShirtVestMaterials[8];
					materialSuit6[1] = grayShirtVestMaterials[8];
					materialSuit7[1] = grayShirtVestMaterials[8];
					materialSuit8[1] = grayShirtVestMaterials[8];
					break;
				case 9:
					materialSuit1[1] = grayShirtVestMaterials[9];
					materialSuit2[1] = grayShirtVestMaterials[9];
					materialSuit3[2] = grayShirtVestMaterials[9];
					materialSuit4[1] = grayShirtVestMaterials[9];
					materialSuit5[1] = grayShirtVestMaterials[9];
					materialSuit6[1] = grayShirtVestMaterials[9];
					materialSuit7[1] = grayShirtVestMaterials[9];
					materialSuit8[1] = grayShirtVestMaterials[9];
					break;
			}
		}else if(myShirtColor == ShirtColor.Green){
			switch (value){
				case 0:
					materialSuit1[1] = greenShirtVestMaterials[0];
					materialSuit2[1] = greenShirtVestMaterials[0];
					materialSuit3[2] = greenShirtVestMaterials[0];
					materialSuit4[1] = greenShirtVestMaterials[0];
					materialSuit5[1] = greenShirtVestMaterials[0];
					materialSuit6[1] = greenShirtVestMaterials[0];
					materialSuit7[1] = greenShirtVestMaterials[0];
					materialSuit8[1] = greenShirtVestMaterials[0];
					break;
				case 1:
					materialSuit1[1] = greenShirtVestMaterials[1];
					materialSuit2[1] = greenShirtVestMaterials[1];
					materialSuit3[2] = greenShirtVestMaterials[1];
					materialSuit4[1] = greenShirtVestMaterials[1];
					materialSuit5[1] = greenShirtVestMaterials[1];
					materialSuit6[1] = greenShirtVestMaterials[1];
					materialSuit7[1] = greenShirtVestMaterials[1];
					materialSuit8[1] = greenShirtVestMaterials[1];
					break;
				case 2:
					materialSuit1[1] = greenShirtVestMaterials[2];
					materialSuit2[1] = greenShirtVestMaterials[2];
					materialSuit3[2] = greenShirtVestMaterials[2];
					materialSuit4[1] = greenShirtVestMaterials[2];
					materialSuit5[1] = greenShirtVestMaterials[2];
					materialSuit6[1] = greenShirtVestMaterials[2];
					materialSuit7[1] = greenShirtVestMaterials[2];
					materialSuit8[1] = greenShirtVestMaterials[2];
					break;
				case 3:
					materialSuit1[1] = greenShirtVestMaterials[3];
					materialSuit2[1] = greenShirtVestMaterials[3];
					materialSuit3[2] = greenShirtVestMaterials[3];
					materialSuit4[1] = greenShirtVestMaterials[3];
					materialSuit5[1] = greenShirtVestMaterials[3];
					materialSuit6[1] = greenShirtVestMaterials[3];
					materialSuit7[1] = greenShirtVestMaterials[3];
					materialSuit8[1] = greenShirtVestMaterials[3];
					break;
				case 4:
					materialSuit1[1] = greenShirtVestMaterials[4];
					materialSuit2[1] = greenShirtVestMaterials[4];
					materialSuit3[2] = greenShirtVestMaterials[4];
					materialSuit4[1] = greenShirtVestMaterials[4];
					materialSuit5[1] = greenShirtVestMaterials[4];
					materialSuit6[1] = greenShirtVestMaterials[4];
					materialSuit7[1] = greenShirtVestMaterials[4];
					materialSuit8[1] = greenShirtVestMaterials[4];
					break;
				case 5:
					materialSuit1[1] = greenShirtVestMaterials[5];
					materialSuit2[1] = greenShirtVestMaterials[5];
					materialSuit3[2] = greenShirtVestMaterials[5];
					materialSuit4[1] = greenShirtVestMaterials[5];
					materialSuit5[1] = greenShirtVestMaterials[5];
					materialSuit6[1] = greenShirtVestMaterials[5];
					materialSuit7[1] = greenShirtVestMaterials[5];
					materialSuit8[1] = greenShirtVestMaterials[5];
					break;
				case 6:
					materialSuit1[1] = greenShirtVestMaterials[6];
					materialSuit2[1] = greenShirtVestMaterials[6];
					materialSuit3[2] = greenShirtVestMaterials[6];
					materialSuit4[1] = greenShirtVestMaterials[6];
					materialSuit5[1] = greenShirtVestMaterials[6];
					materialSuit6[1] = greenShirtVestMaterials[6];
					materialSuit7[1] = greenShirtVestMaterials[6];
					materialSuit8[1] = greenShirtVestMaterials[6];
					break;
				case 7:
					materialSuit1[1] = greenShirtVestMaterials[7];
					materialSuit2[1] = greenShirtVestMaterials[7];
					materialSuit3[2] = greenShirtVestMaterials[7];
					materialSuit4[1] = greenShirtVestMaterials[7];
					materialSuit5[1] = greenShirtVestMaterials[7];
					materialSuit6[1] = greenShirtVestMaterials[7];
					materialSuit7[1] = greenShirtVestMaterials[7];
					materialSuit8[1] = greenShirtVestMaterials[7];
					break;
				case 8:
					materialSuit1[1] = greenShirtVestMaterials[8];
					materialSuit2[1] = greenShirtVestMaterials[8];
					materialSuit3[2] = greenShirtVestMaterials[8];
					materialSuit4[1] = greenShirtVestMaterials[8];
					materialSuit5[1] = greenShirtVestMaterials[8];
					materialSuit6[1] = greenShirtVestMaterials[8];
					materialSuit7[1] = greenShirtVestMaterials[8];
					materialSuit8[1] = greenShirtVestMaterials[8];
					break;
				case 9:
					materialSuit1[1] = greenShirtVestMaterials[9];
					materialSuit2[1] = greenShirtVestMaterials[9];
					materialSuit3[2] = greenShirtVestMaterials[9];
					materialSuit4[1] = greenShirtVestMaterials[9];
					materialSuit5[1] = greenShirtVestMaterials[9];
					materialSuit6[1] = greenShirtVestMaterials[9];
					materialSuit7[1] = greenShirtVestMaterials[9];
					materialSuit8[1] = greenShirtVestMaterials[9];
					break;
			}
		}else if(myShirtColor == ShirtColor.Pink){
			switch (value){
				case 0:
					materialSuit1[1] = pinkShirtVestMaterials[0];
					materialSuit2[1] = pinkShirtVestMaterials[0];
					materialSuit3[2] = pinkShirtVestMaterials[0];
					materialSuit4[1] = pinkShirtVestMaterials[0];
					materialSuit5[1] = pinkShirtVestMaterials[0];
					materialSuit6[1] = pinkShirtVestMaterials[0];
					materialSuit7[1] = pinkShirtVestMaterials[0];
					materialSuit8[1] = pinkShirtVestMaterials[0];
					break;
				case 1:
					materialSuit1[1] = pinkShirtVestMaterials[1];
					materialSuit2[1] = pinkShirtVestMaterials[1];
					materialSuit3[2] = pinkShirtVestMaterials[1];
					materialSuit4[1] = pinkShirtVestMaterials[1];
					materialSuit5[1] = pinkShirtVestMaterials[1];
					materialSuit6[1] = pinkShirtVestMaterials[1];
					materialSuit7[1] = pinkShirtVestMaterials[1];
					materialSuit8[1] = pinkShirtVestMaterials[1];
					break;
				case 2:
					materialSuit1[1] = pinkShirtVestMaterials[2];
					materialSuit2[1] = pinkShirtVestMaterials[2];
					materialSuit3[2] = pinkShirtVestMaterials[2];
					materialSuit4[1] = pinkShirtVestMaterials[2];
					materialSuit5[1] = pinkShirtVestMaterials[2];
					materialSuit6[1] = pinkShirtVestMaterials[2];
					materialSuit7[1] = pinkShirtVestMaterials[2];
					materialSuit8[1] = pinkShirtVestMaterials[2];
					break;
				case 3:
					materialSuit1[1] = pinkShirtVestMaterials[3];
					materialSuit2[1] = pinkShirtVestMaterials[3];
					materialSuit3[2] = pinkShirtVestMaterials[3];
					materialSuit4[1] = pinkShirtVestMaterials[3];
					materialSuit5[1] = pinkShirtVestMaterials[3];
					materialSuit6[1] = pinkShirtVestMaterials[3];
					materialSuit7[1] = pinkShirtVestMaterials[3];
					materialSuit8[1] = pinkShirtVestMaterials[3];
					break;
				case 4:
					materialSuit1[1] = pinkShirtVestMaterials[4];
					materialSuit2[1] = pinkShirtVestMaterials[4];
					materialSuit3[2] = pinkShirtVestMaterials[4];
					materialSuit4[1] = pinkShirtVestMaterials[4];
					materialSuit5[1] = pinkShirtVestMaterials[4];
					materialSuit6[1] = pinkShirtVestMaterials[4];
					materialSuit7[1] = pinkShirtVestMaterials[4];
					materialSuit8[1] = pinkShirtVestMaterials[4];
					break;
				case 5:
					materialSuit1[1] = pinkShirtVestMaterials[5];
					materialSuit2[1] = pinkShirtVestMaterials[5];
					materialSuit3[2] = pinkShirtVestMaterials[5];
					materialSuit4[1] = pinkShirtVestMaterials[5];
					materialSuit5[1] = pinkShirtVestMaterials[5];
					materialSuit6[1] = pinkShirtVestMaterials[5];
					materialSuit7[1] = pinkShirtVestMaterials[5];
					materialSuit8[1] = pinkShirtVestMaterials[5];
					break;
				case 6:
					materialSuit1[1] = pinkShirtVestMaterials[6];
					materialSuit2[1] = pinkShirtVestMaterials[6];
					materialSuit3[2] = pinkShirtVestMaterials[6];
					materialSuit4[1] = pinkShirtVestMaterials[6];
					materialSuit5[1] = pinkShirtVestMaterials[6];
					materialSuit6[1] = pinkShirtVestMaterials[6];
					materialSuit7[1] = pinkShirtVestMaterials[6];
					materialSuit8[1] = pinkShirtVestMaterials[6];
					break;
				case 7:
					materialSuit1[1] = pinkShirtVestMaterials[7];
					materialSuit2[1] = pinkShirtVestMaterials[7];
					materialSuit3[2] = pinkShirtVestMaterials[7];
					materialSuit4[1] = pinkShirtVestMaterials[7];
					materialSuit5[1] = pinkShirtVestMaterials[7];
					materialSuit6[1] = pinkShirtVestMaterials[7];
					materialSuit7[1] = pinkShirtVestMaterials[7];
					materialSuit8[1] = pinkShirtVestMaterials[7];
					break;
				case 8:
					materialSuit1[1] = pinkShirtVestMaterials[8];
					materialSuit2[1] = pinkShirtVestMaterials[8];
					materialSuit3[2] = pinkShirtVestMaterials[8];
					materialSuit4[1] = pinkShirtVestMaterials[8];
					materialSuit5[1] = pinkShirtVestMaterials[8];
					materialSuit6[1] = pinkShirtVestMaterials[8];
					materialSuit7[1] = pinkShirtVestMaterials[8];
					materialSuit8[1] = pinkShirtVestMaterials[8];
					break;
				case 9:
					materialSuit1[1] = pinkShirtVestMaterials[9];
					materialSuit2[1] = pinkShirtVestMaterials[9];
					materialSuit3[2] = pinkShirtVestMaterials[9];
					materialSuit4[1] = pinkShirtVestMaterials[9];
					materialSuit5[1] = pinkShirtVestMaterials[9];
					materialSuit6[1] = pinkShirtVestMaterials[9];
					materialSuit7[1] = pinkShirtVestMaterials[9];
					materialSuit8[1] = pinkShirtVestMaterials[9];
					break;
			}
		}else if(myShirtColor == ShirtColor.Red){
			switch (value){
				case 0:
					materialSuit1[1] = redShirtVestMaterials[0];
					materialSuit2[1] = redShirtVestMaterials[0];
					materialSuit3[2] = redShirtVestMaterials[0];
					materialSuit4[1] = redShirtVestMaterials[0];
					materialSuit5[1] = redShirtVestMaterials[0];
					materialSuit6[1] = redShirtVestMaterials[0];
					materialSuit7[1] = redShirtVestMaterials[0];
					materialSuit8[1] = redShirtVestMaterials[0];
					break;
				case 1:
					materialSuit1[1] = redShirtVestMaterials[1];
					materialSuit2[1] = redShirtVestMaterials[1];
					materialSuit3[2] = redShirtVestMaterials[1];
					materialSuit4[1] = redShirtVestMaterials[1];
					materialSuit5[1] = redShirtVestMaterials[1];
					materialSuit6[1] = redShirtVestMaterials[1];
					materialSuit7[1] = redShirtVestMaterials[1];
					materialSuit8[1] = redShirtVestMaterials[1];
					break;
				case 2:
					materialSuit1[1] = redShirtVestMaterials[2];
					materialSuit2[1] = redShirtVestMaterials[2];
					materialSuit3[2] = redShirtVestMaterials[2];
					materialSuit4[1] = redShirtVestMaterials[2];
					materialSuit5[1] = redShirtVestMaterials[2];
					materialSuit6[1] = redShirtVestMaterials[2];
					materialSuit7[1] = redShirtVestMaterials[2];
					materialSuit8[1] = redShirtVestMaterials[2];
					break;
				case 3:
					materialSuit1[1] = redShirtVestMaterials[3];
					materialSuit2[1] = redShirtVestMaterials[3];
					materialSuit3[2] = redShirtVestMaterials[3];
					materialSuit4[1] = redShirtVestMaterials[3];
					materialSuit5[1] = redShirtVestMaterials[3];
					materialSuit6[1] = redShirtVestMaterials[3];
					materialSuit7[1] = redShirtVestMaterials[3];
					materialSuit8[1] = redShirtVestMaterials[3];
					break;
				case 4:
					materialSuit1[1] = redShirtVestMaterials[4];
					materialSuit2[1] = redShirtVestMaterials[4];
					materialSuit3[2] = redShirtVestMaterials[4];
					materialSuit4[1] = redShirtVestMaterials[4];
					materialSuit5[1] = redShirtVestMaterials[4];
					materialSuit6[1] = redShirtVestMaterials[4];
					materialSuit7[1] = redShirtVestMaterials[4];
					materialSuit8[1] = redShirtVestMaterials[4];
					break;
				case 5:
					materialSuit1[1] = redShirtVestMaterials[5];
					materialSuit2[1] = redShirtVestMaterials[5];
					materialSuit3[2] = redShirtVestMaterials[5];
					materialSuit4[1] = redShirtVestMaterials[5];
					materialSuit5[1] = redShirtVestMaterials[5];
					materialSuit6[1] = redShirtVestMaterials[5];
					materialSuit7[1] = redShirtVestMaterials[5];
					materialSuit8[1] = redShirtVestMaterials[5];
					break;
				case 6:
					materialSuit1[1] = redShirtVestMaterials[6];
					materialSuit2[1] = redShirtVestMaterials[6];
					materialSuit3[2] = redShirtVestMaterials[6];
					materialSuit4[1] = redShirtVestMaterials[6];
					materialSuit5[1] = redShirtVestMaterials[6];
					materialSuit6[1] = redShirtVestMaterials[6];
					materialSuit7[1] = redShirtVestMaterials[6];
					materialSuit8[1] = redShirtVestMaterials[6];
					break;
				case 7:
					materialSuit1[1] = redShirtVestMaterials[7];
					materialSuit2[1] = redShirtVestMaterials[7];
					materialSuit3[2] = redShirtVestMaterials[7];
					materialSuit4[1] = redShirtVestMaterials[7];
					materialSuit5[1] = redShirtVestMaterials[7];
					materialSuit6[1] = redShirtVestMaterials[7];
					materialSuit7[1] = redShirtVestMaterials[7];
					materialSuit8[1] = redShirtVestMaterials[7];
					break;
				case 8:
					materialSuit1[1] = redShirtVestMaterials[8];
					materialSuit2[1] = redShirtVestMaterials[8];
					materialSuit3[2] = redShirtVestMaterials[8];
					materialSuit4[1] = redShirtVestMaterials[8];
					materialSuit5[1] = redShirtVestMaterials[8];
					materialSuit6[1] = redShirtVestMaterials[8];
					materialSuit7[1] = redShirtVestMaterials[8];
					materialSuit8[1] = redShirtVestMaterials[8];
					break;
				case 9:
					materialSuit1[1] = redShirtVestMaterials[9];
					materialSuit2[1] = redShirtVestMaterials[9];
					materialSuit3[2] = redShirtVestMaterials[9];
					materialSuit4[1] = redShirtVestMaterials[9];
					materialSuit5[1] = redShirtVestMaterials[9];
					materialSuit6[1] = redShirtVestMaterials[9];
					materialSuit7[1] = redShirtVestMaterials[9];
					materialSuit8[1] = redShirtVestMaterials[9];
					break;
			}
		}else if(myShirtColor == ShirtColor.White){
			switch (value){
				case 0:
					materialSuit1[1] = whiteShirtVestMaterials[0];
					materialSuit2[1] = whiteShirtVestMaterials[0];
					materialSuit3[2] = whiteShirtVestMaterials[0];
					materialSuit4[1] = whiteShirtVestMaterials[0];
					materialSuit5[1] = whiteShirtVestMaterials[0];
					materialSuit6[1] = whiteShirtVestMaterials[0];
					materialSuit7[1] = whiteShirtVestMaterials[0];
					materialSuit8[1] = whiteShirtVestMaterials[0];
					break;
				case 1:
					materialSuit1[1] = whiteShirtVestMaterials[1];
					materialSuit2[1] = whiteShirtVestMaterials[1];
					materialSuit3[2] = whiteShirtVestMaterials[1];
					materialSuit4[1] = whiteShirtVestMaterials[1];
					materialSuit5[1] = whiteShirtVestMaterials[1];
					materialSuit6[1] = whiteShirtVestMaterials[1];
					materialSuit7[1] = whiteShirtVestMaterials[1];
					materialSuit8[1] = whiteShirtVestMaterials[1];
					break;
				case 2:
					materialSuit1[1] = whiteShirtVestMaterials[2];
					materialSuit2[1] = whiteShirtVestMaterials[2];
					materialSuit3[2] = whiteShirtVestMaterials[2];
					materialSuit4[1] = whiteShirtVestMaterials[2];
					materialSuit5[1] = whiteShirtVestMaterials[2];
					materialSuit6[1] = whiteShirtVestMaterials[2];
					materialSuit7[1] = whiteShirtVestMaterials[2];
					materialSuit8[1] = whiteShirtVestMaterials[2];
					break;
				case 3:
					materialSuit1[1] = whiteShirtVestMaterials[3];
					materialSuit2[1] = whiteShirtVestMaterials[3];
					materialSuit3[2] = whiteShirtVestMaterials[3];
					materialSuit4[1] = whiteShirtVestMaterials[3];
					materialSuit5[1] = whiteShirtVestMaterials[3];
					materialSuit6[1] = whiteShirtVestMaterials[3];
					materialSuit7[1] = whiteShirtVestMaterials[3];
					materialSuit8[1] = whiteShirtVestMaterials[3];
					break;
				case 4:
					materialSuit1[1] = whiteShirtVestMaterials[4];
					materialSuit2[1] = whiteShirtVestMaterials[4];
					materialSuit3[2] = whiteShirtVestMaterials[4];
					materialSuit4[1] = whiteShirtVestMaterials[4];
					materialSuit5[1] = whiteShirtVestMaterials[4];
					materialSuit6[1] = whiteShirtVestMaterials[4];
					materialSuit7[1] = whiteShirtVestMaterials[4];
					materialSuit8[1] = whiteShirtVestMaterials[4];
					break;
				case 5:
					materialSuit1[1] = whiteShirtVestMaterials[5];
					materialSuit2[1] = whiteShirtVestMaterials[5];
					materialSuit3[2] = whiteShirtVestMaterials[5];
					materialSuit4[1] = whiteShirtVestMaterials[5];
					materialSuit5[1] = whiteShirtVestMaterials[5];
					materialSuit6[1] = whiteShirtVestMaterials[5];
					materialSuit7[1] = whiteShirtVestMaterials[5];
					materialSuit8[1] = whiteShirtVestMaterials[5];
					break;
				case 6:
					materialSuit1[1] = whiteShirtVestMaterials[6];
					materialSuit2[1] = whiteShirtVestMaterials[6];
					materialSuit3[2] = whiteShirtVestMaterials[6];
					materialSuit4[1] = whiteShirtVestMaterials[6];
					materialSuit5[1] = whiteShirtVestMaterials[6];
					materialSuit6[1] = whiteShirtVestMaterials[6];
					materialSuit7[1] = whiteShirtVestMaterials[6];
					materialSuit8[1] = whiteShirtVestMaterials[6];
					break;
				case 7:
					materialSuit1[1] = whiteShirtVestMaterials[7];
					materialSuit2[1] = whiteShirtVestMaterials[7];
					materialSuit3[2] = whiteShirtVestMaterials[7];
					materialSuit4[1] = whiteShirtVestMaterials[7];
					materialSuit5[1] = whiteShirtVestMaterials[7];
					materialSuit6[1] = whiteShirtVestMaterials[7];
					materialSuit7[1] = whiteShirtVestMaterials[7];
					materialSuit8[1] = whiteShirtVestMaterials[7];
					break;
				case 8:
					materialSuit1[1] = whiteShirtVestMaterials[8];
					materialSuit2[1] = whiteShirtVestMaterials[8];
					materialSuit3[2] = whiteShirtVestMaterials[8];
					materialSuit4[1] = whiteShirtVestMaterials[8];
					materialSuit5[1] = whiteShirtVestMaterials[8];
					materialSuit6[1] = whiteShirtVestMaterials[8];
					materialSuit7[1] = whiteShirtVestMaterials[8];
					materialSuit8[1] = whiteShirtVestMaterials[8];
					break;
				case 9:
					materialSuit1[1] = whiteShirtVestMaterials[9];
					materialSuit2[1] = whiteShirtVestMaterials[9];
					materialSuit3[2] = whiteShirtVestMaterials[9];
					materialSuit4[1] = whiteShirtVestMaterials[9];
					materialSuit5[1] = whiteShirtVestMaterials[9];
					materialSuit6[1] = whiteShirtVestMaterials[9];
					materialSuit7[1] = whiteShirtVestMaterials[9];
					materialSuit8[1] = whiteShirtVestMaterials[9];
					break;
			}
		}else if(myShirtColor == ShirtColor.Yellow){
			switch (value){
				case 0:
					materialSuit1[1] = yellowShirtVestMaterials[0];
					materialSuit2[1] = yellowShirtVestMaterials[0];
					materialSuit3[2] = yellowShirtVestMaterials[0];
					materialSuit4[1] = yellowShirtVestMaterials[0];
					materialSuit5[1] = yellowShirtVestMaterials[0];
					materialSuit6[1] = yellowShirtVestMaterials[0];
					materialSuit7[1] = yellowShirtVestMaterials[0];
					materialSuit8[1] = yellowShirtVestMaterials[0];
					break;
				case 1:
					materialSuit1[1] = yellowShirtVestMaterials[1];
					materialSuit2[1] = yellowShirtVestMaterials[1];
					materialSuit3[2] = yellowShirtVestMaterials[1];
					materialSuit4[1] = yellowShirtVestMaterials[1];
					materialSuit5[1] = yellowShirtVestMaterials[1];
					materialSuit6[1] = yellowShirtVestMaterials[1];
					materialSuit7[1] = yellowShirtVestMaterials[1];
					materialSuit8[1] = yellowShirtVestMaterials[1];
					break;
				case 2:
					materialSuit1[1] = yellowShirtVestMaterials[2];
					materialSuit2[1] = yellowShirtVestMaterials[2];
					materialSuit3[2] = yellowShirtVestMaterials[2];
					materialSuit4[1] = yellowShirtVestMaterials[2];
					materialSuit5[1] = yellowShirtVestMaterials[2];
					materialSuit6[1] = yellowShirtVestMaterials[2];
					materialSuit7[1] = yellowShirtVestMaterials[2];
					materialSuit8[1] = yellowShirtVestMaterials[2];
					break;
				case 3:
					materialSuit1[1] = yellowShirtVestMaterials[3];
					materialSuit2[1] = yellowShirtVestMaterials[3];
					materialSuit3[2] = yellowShirtVestMaterials[3];
					materialSuit4[1] = yellowShirtVestMaterials[3];
					materialSuit5[1] = yellowShirtVestMaterials[3];
					materialSuit6[1] = yellowShirtVestMaterials[3];
					materialSuit7[1] = yellowShirtVestMaterials[3];
					materialSuit8[1] = yellowShirtVestMaterials[3];
					break;
				case 4:
					materialSuit1[1] = yellowShirtVestMaterials[4];
					materialSuit2[1] = yellowShirtVestMaterials[4];
					materialSuit3[2] = yellowShirtVestMaterials[4];
					materialSuit4[1] = yellowShirtVestMaterials[4];
					materialSuit5[1] = yellowShirtVestMaterials[4];
					materialSuit6[1] = yellowShirtVestMaterials[4];
					materialSuit7[1] = yellowShirtVestMaterials[4];
					materialSuit8[1] = yellowShirtVestMaterials[4];
					break;
				case 5:
					materialSuit1[1] = yellowShirtVestMaterials[5];
					materialSuit2[1] = yellowShirtVestMaterials[5];
					materialSuit3[2] = yellowShirtVestMaterials[5];
					materialSuit4[1] = yellowShirtVestMaterials[5];
					materialSuit5[1] = yellowShirtVestMaterials[5];
					materialSuit6[1] = yellowShirtVestMaterials[5];
					materialSuit7[1] = yellowShirtVestMaterials[5];
					materialSuit8[1] = yellowShirtVestMaterials[5];
					break;
				case 6:
					materialSuit1[1] = yellowShirtVestMaterials[6];
					materialSuit2[1] = yellowShirtVestMaterials[6];
					materialSuit3[2] = yellowShirtVestMaterials[6];
					materialSuit4[1] = yellowShirtVestMaterials[6];
					materialSuit5[1] = yellowShirtVestMaterials[6];
					materialSuit6[1] = yellowShirtVestMaterials[6];
					materialSuit7[1] = yellowShirtVestMaterials[6];
					materialSuit8[1] = yellowShirtVestMaterials[6];
					break;
				case 7:
					materialSuit1[1] = yellowShirtVestMaterials[7];
					materialSuit2[1] = yellowShirtVestMaterials[7];
					materialSuit3[2] = yellowShirtVestMaterials[7];
					materialSuit4[1] = yellowShirtVestMaterials[7];
					materialSuit5[1] = yellowShirtVestMaterials[7];
					materialSuit6[1] = yellowShirtVestMaterials[7];
					materialSuit7[1] = yellowShirtVestMaterials[7];
					materialSuit8[1] = yellowShirtVestMaterials[7];
					break;
				case 8:
					materialSuit1[1] = yellowShirtVestMaterials[8];
					materialSuit2[1] = yellowShirtVestMaterials[8];
					materialSuit3[2] = yellowShirtVestMaterials[8];
					materialSuit4[1] = yellowShirtVestMaterials[8];
					materialSuit5[1] = yellowShirtVestMaterials[8];
					materialSuit6[1] = yellowShirtVestMaterials[8];
					materialSuit7[1] = yellowShirtVestMaterials[8];
					materialSuit8[1] = yellowShirtVestMaterials[8];
					break;
				case 9:
					materialSuit1[1] = yellowShirtVestMaterials[9];
					materialSuit2[1] = yellowShirtVestMaterials[9];
					materialSuit3[2] = yellowShirtVestMaterials[9];
					materialSuit4[1] = yellowShirtVestMaterials[9];
					materialSuit5[1] = yellowShirtVestMaterials[9];
					materialSuit6[1] = yellowShirtVestMaterials[9];
					materialSuit7[1] = yellowShirtVestMaterials[9];
					materialSuit8[1] = yellowShirtVestMaterials[9];
					break;
			}
		}
		
		suitStyle1SkinnedMeshRenderer.materials = materialSuit1;
		suitStyle2SkinnedMeshRenderer.materials = materialSuit2;
		suitStyle3SkinnedMeshRenderer.materials = materialSuit3;
		suitStyle4SkinnedMeshRenderer.materials = materialSuit4;
		suitStyle5SkinnedMeshRenderer.materials = materialSuit5;
		suitStyle6SkinnedMeshRenderer.materials = materialSuit6;
		suitStyle7SkinnedMeshRenderer.materials = materialSuit7;
		suitStyle8SkinnedMeshRenderer.materials = materialSuit8;
	}
	#endregion
	
	#region Shoes
	public void ChangeShoesStyle(int value){
		foreach (GameObject model in shoesStyles){
			model.SetActive(false);
		}
		if(value == 0){
			shoesStyles[0].SetActive(true);
			casualShoesDropDown.SetActive(false);
			businessShoesDropDown.SetActive(true);
		}else if(value == 1){
			shoesStyles[1].SetActive(true);
			casualShoesDropDown.SetActive(true);
			businessShoesDropDown.SetActive(false);
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
	
	#region Suits
	public void ChangeSuitStyle(int value){
		//Disable normal shirt and pants
		foreach (GameObject model in shirtAndPantStyles){
			model.SetActive(false);
		}
		//Disable every suit style
		foreach (GameObject model in suitStyles){
			model.SetActive(false);
		}
		//Select suit style
		if(value == 0){
			suitStyles[0].SetActive(true);
		}else if(value == 1){
			suitStyles[1].SetActive(true);
		}else if(value == 2){
			suitStyles[2].SetActive(true);
		}else if(value == 3){
			suitStyles[3].SetActive(true);
		}else if(value == 4){
			suitStyles[4].SetActive(true);
		}else if(value == 5){
			suitStyles[5].SetActive(true);
		}else if(value == 6){
			suitStyles[6].SetActive(true);
		}else if(value == 7){
			suitStyles[7].SetActive(true);
		}
	}
	#endregion
}