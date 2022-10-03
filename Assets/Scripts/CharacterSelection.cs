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
	//The above enum should be used in similar fashion like the myShirtColor enum in ChangeShirtColor(); function
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
		hairStyle1SkinnedMeshRenderer.material = hairStyle1Materials[value];
		hairStyle2SkinnedMeshRenderer.material = hairStyle2Materials[value];
		hairStyle3SkinnedMeshRenderer.material = hairStyle3Materials[value];
	}
	#endregion
	
	#region Face, Glasses and Eyes
	public void ChangeFaceStyle(int value){
		faceSkinnedMeshRenderer.material = faceMaterials[value];
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
		glasses1SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[value];
		glasses2SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[value];
		glasses3SkinnedMeshRenderer.material = tieBowtieGlassesMaterials[value];
	}
	
	public void ChangeEyesColor(int value){
		eyesSkinnedMeshRenderer.material = eyesMaterials[value];
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
		
		materials1[0] = shirtMaterials[value];
		materials2[0] = shirtMaterials[value];
		materials3[0] = shirtMaterials[value];
		materials4[0] = shirtMaterials[value];
		
		materialSuit1[1] = shirtMaterials[value];
		materialSuit2[1] = shirtMaterials[value];
		materialSuit3[2] = shirtMaterials[value];
		materialSuit4[1] = shirtMaterials[value];
		materialSuit5[1] = shirtMaterials[value];
		materialSuit6[1] = shirtMaterials[value];
		materialSuit7[1] = shirtMaterials[value];
		materialSuit8[1] = shirtMaterials[value];
		
		if(value == 0){
			myShirtColor = ShirtColor.Black;
		}else if(value == 1){
			myShirtColor = ShirtColor.Blue;
		}else if(value == 2){
			myShirtColor = ShirtColor.Gray;
		}else if(value == 3){
			myShirtColor = ShirtColor.Green;
		}else if(value == 4){
			myShirtColor = ShirtColor.Pink;
		}else if(value == 5){
			myShirtColor = ShirtColor.Red;
		}else if(value == 6){
			myShirtColor = ShirtColor.White;
		}else if(value == 7){
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
		
		materials1[1] = pantsMaterials[value];
		materials2[1] = pantsMaterials[value];
		materials3[1] = pantsMaterials[value];
		materials4[1] = pantsMaterials[value];
		
		materialSuit1[0] = pantsMaterials[value];
		materialSuit2[0] = pantsMaterials[value];
		materialSuit3[0] = pantsMaterials[value];
		materialSuit4[0] = pantsMaterials[value];
		materialSuit5[2] = pantsMaterials[value];
		materialSuit6[2] = pantsMaterials[value];
		materialSuit7[2] = pantsMaterials[value];
		materialSuit8[2] = pantsMaterials[value];
		
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
		
		materials2[2] = tieBowtieGlassesMaterials[value];
		materials3[2] = tieBowtieGlassesMaterials[value];
		materials4[2] = tieBowtieGlassesMaterials[value];
		
		materialSuit1[2] = tieBowtieGlassesMaterials[value];
		materialSuit3[3] = tieBowtieGlassesMaterials[value];
		materialSuit4[3] = tieBowtieGlassesMaterials[value];
		materialSuit5[3] = tieBowtieGlassesMaterials[value];
		materialSuit7[3] = tieBowtieGlassesMaterials[value];
		materialSuit8[3] = tieBowtieGlassesMaterials[value];
		
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
		
		materialSuit1[3] = closedJacketMaterials[value];
		materialSuit2[2] = closedJacketMaterials[value];
		materialSuit3[1] = closedJacketMaterials[value];
		materialSuit4[2] = closedJacketMaterials[value];
		materialSuit5[0] = openJacketMaterials[value];
		materialSuit6[0] = openJacketMaterials[value];
		materialSuit7[0] = openJacketMaterials[value];
		materialSuit8[0] = openJacketMaterials[value];
		
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
			materialSuit1[1] = blackShirtVestMaterials[value];
			materialSuit2[1] = blackShirtVestMaterials[value];
			materialSuit3[2] = blackShirtVestMaterials[value];
			materialSuit4[1] = blackShirtVestMaterials[value];
			materialSuit5[1] = blackShirtVestMaterials[value];
			materialSuit6[1] = blackShirtVestMaterials[value];
			materialSuit7[1] = blackShirtVestMaterials[value];
			materialSuit8[1] = blackShirtVestMaterials[value];
		}else if(myShirtColor == ShirtColor.Blue){
			materialSuit1[1] = blueShirtVestMaterials[value];
			materialSuit2[1] = blueShirtVestMaterials[value];
			materialSuit3[2] = blueShirtVestMaterials[value];
			materialSuit4[1] = blueShirtVestMaterials[value];
			materialSuit5[1] = blueShirtVestMaterials[value];
			materialSuit6[1] = blueShirtVestMaterials[value];
			materialSuit7[1] = blueShirtVestMaterials[value];
			materialSuit8[1] = blueShirtVestMaterials[value];
		}else if(myShirtColor == ShirtColor.Gray){
			materialSuit1[1] = grayShirtVestMaterials[value];
			materialSuit2[1] = grayShirtVestMaterials[value];
			materialSuit3[2] = grayShirtVestMaterials[value];
			materialSuit4[1] = grayShirtVestMaterials[value];
			materialSuit5[1] = grayShirtVestMaterials[value];
			materialSuit6[1] = grayShirtVestMaterials[value];
			materialSuit7[1] = grayShirtVestMaterials[value];
			materialSuit8[1] = grayShirtVestMaterials[value];
		}else if(myShirtColor == ShirtColor.Green){
			materialSuit1[1] = greenShirtVestMaterials[value];
			materialSuit2[1] = greenShirtVestMaterials[value];
			materialSuit3[2] = greenShirtVestMaterials[value];
			materialSuit4[1] = greenShirtVestMaterials[value];
			materialSuit5[1] = greenShirtVestMaterials[value];
			materialSuit6[1] = greenShirtVestMaterials[value];
			materialSuit7[1] = greenShirtVestMaterials[value];
			materialSuit8[1] = greenShirtVestMaterials[value];
		}else if(myShirtColor == ShirtColor.Pink){
			materialSuit1[1] = pinkShirtVestMaterials[value];
			materialSuit2[1] = pinkShirtVestMaterials[value];
			materialSuit3[2] = pinkShirtVestMaterials[value];
			materialSuit4[1] = pinkShirtVestMaterials[value];
			materialSuit5[1] = pinkShirtVestMaterials[value];
			materialSuit6[1] = pinkShirtVestMaterials[value];
			materialSuit7[1] = pinkShirtVestMaterials[value];
			materialSuit8[1] = pinkShirtVestMaterials[value];
		}else if(myShirtColor == ShirtColor.Red){
			materialSuit1[1] = redShirtVestMaterials[value];
			materialSuit2[1] = redShirtVestMaterials[value];
			materialSuit3[2] = redShirtVestMaterials[value];
			materialSuit4[1] = redShirtVestMaterials[value];
			materialSuit5[1] = redShirtVestMaterials[value];
			materialSuit6[1] = redShirtVestMaterials[value];
			materialSuit7[1] = redShirtVestMaterials[value];
			materialSuit8[1] = redShirtVestMaterials[value];
		}else if(myShirtColor == ShirtColor.White){
			materialSuit1[1] = whiteShirtVestMaterials[value];
			materialSuit2[1] = whiteShirtVestMaterials[value];
			materialSuit3[2] = whiteShirtVestMaterials[value];
			materialSuit4[1] = whiteShirtVestMaterials[value];
			materialSuit5[1] = whiteShirtVestMaterials[value];
			materialSuit6[1] = whiteShirtVestMaterials[value];
			materialSuit7[1] = whiteShirtVestMaterials[value];
			materialSuit8[1] = whiteShirtVestMaterials[value];
		}else if(myShirtColor == ShirtColor.Yellow){
			materialSuit1[1] = yellowShirtVestMaterials[value];
			materialSuit2[1] = yellowShirtVestMaterials[value];
			materialSuit3[2] = yellowShirtVestMaterials[value];
			materialSuit4[1] = yellowShirtVestMaterials[value];
			materialSuit5[1] = yellowShirtVestMaterials[value];
			materialSuit6[1] = yellowShirtVestMaterials[value];
			materialSuit7[1] = yellowShirtVestMaterials[value];
			materialSuit8[1] = yellowShirtVestMaterials[value];
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
		businessStyleSkinnedMeshRenderer.material = buisnessShoesMaterials[value];
	}
	
	public void ChangeCasualShoesColor(int value){
		casualStyleSkinnedMeshRenderer.material = casualShoesMaterials[value];
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