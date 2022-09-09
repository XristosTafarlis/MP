using UnityEngine;
using System.Collections;
using Mirror;

[RequireComponent(typeof(CharacterController))]

public class PlayerController: NetworkBehaviour{
	//Player Controller variables & references
	[Header("References")]
	[SerializeField] float walkingSpeed = 7.5f;
	[SerializeField] float runningSpeed = 11.5f;
	[SerializeField] float jumpSpeed = 8.0f;
	[SerializeField] float gravity = 20.0f;
	[SerializeField] Camera FPCamera;
	[SerializeField] Camera TPCamera;
	[SerializeField] float lookSpeed = 2.0f;
	float lookXLimit = 90.0f;
	
	//My variables & references
	[Space(10)]
	Camera workingCamera;
	bool isFat;
	bool isLocked;
	bool tpcam;
	
	//Fly variables
	[Space(20)]
	float _current;
	float _target;
	float tempGravity;
	bool isFlying;
	Vector3 _goalPosition;
	Vector3 _startPosition;
	[SerializeField] LayerMask groundLayer;
	
	[SyncVar(hook = "OnSizeChange")]
	Vector3 SV_size;
	
	CharacterController characterController;
	Vector3 moveDirection = Vector3.zero;
	float rotationX = 0;
	
	[HideInInspector] public bool canMove = true;
	
	void Start(){
		characterController = GetComponent<CharacterController>();
		tempGravity = gravity;
		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		isLocked = true;
		
		if(!isLocalPlayer){
			FPCamera.gameObject.SetActive(false);
			TPCamera.gameObject.SetActive(false);
		}else{
			FPCamera.gameObject.SetActive(true);
			TPCamera.gameObject.SetActive(false);
		}
		
		workingCamera = FPCamera;
	}
	
	void Update(){
		if(!isLocalPlayer) return;
		
		KeyInputs();
		Fly();
		CmdTransformPosition();
		Movement();
	}
	
	void Movement(){
		if(characterController.enabled == true){
			// We are grounded, so recalculate move direction based on axes
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 right = transform.TransformDirection(Vector3.right);
			
			// Press Left Shift to run
			bool isRunning = Input.GetKey(KeyCode.LeftShift);
			float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
			float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
			float movementDirectionY = moveDirection.y;
			moveDirection = (forward * curSpeedX) + (right * curSpeedY);
			
			if (Input.GetButton("Jump") && canMove && characterController.isGrounded){
				moveDirection.y = jumpSpeed;
			}else{
				moveDirection.y = movementDirectionY;
			}
			
			// Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
			// when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
			// as an acceleration (ms^-2)
			if (!characterController.isGrounded){
				moveDirection.y -= gravity * Time.deltaTime;
			}
			
			// Move the controller
			characterController.Move(moveDirection * Time.deltaTime);
		}
		
		// Player and Camera rotation
		if (canMove){
			rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
			rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
			workingCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
			transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
		}
	}
	
	void KeyInputs(){
		if(Input.GetKeyDown(KeyCode.C)){
			CammeraChange();
		}
		if (Input.GetKeyDown(KeyCode.Q)){
			FlyInputs();
		}
		if(Input.GetKeyDown(KeyCode.F)){
			CmdResize();
		}
		
		//Temporary cursor Lock/Unlock for testing
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(isLocked){
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				isLocked = false;
			}else{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				isLocked = true;
			}
		}
	}
	
	void FlyInputs(){
		if(!isFlying && _current == 0){
			characterController.enabled = false;
			_startPosition = transform.position;
			_goalPosition = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
			_target = 1;
			isFlying = true;
			Invoke("SetFlyVariables", 1f);
		}else if (isFlying && _current == 1){
			characterController.enabled = false;
			//Raycast
			CmdTransformPosition();
			//_startPosition = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);
			_goalPosition = transform.position;
			_target = 0;
			moveDirection.y = 0;
			isFlying = false;
			Invoke("SetFlyVariables", 1f);
		}
	}
	
	void Fly(){
		_current = Mathf.MoveTowards(_current, _target, Time.deltaTime);
		transform.position = Vector3.Lerp(_startPosition, _goalPosition, _current);
	}
	
	void SetFlyVariables(){
		if(isFlying){
			gravity = 0f;
			moveDirection = Vector3.zero;
			characterController.enabled = true;
			GetComponent<Animator>().SetBool("isFlying", true);
		}else{
			gravity = tempGravity;
			characterController.enabled = true;
			GetComponent<Animator>().SetBool("isFlying", false);
		}
	}
	
	void CammeraChange(){
		if (tpcam){
			tpcam = false;
			workingCamera = FPCamera;
			FPCamera.gameObject.SetActive(true);
			TPCamera.gameObject.SetActive(false);
			lookXLimit = 90f;
		} else {
			tpcam = true;
			workingCamera = TPCamera;
			FPCamera.gameObject.SetActive(false);
			TPCamera.gameObject.SetActive(true);
			lookXLimit = 40f;
		}
	}
	
	void OnSizeChange(Vector3 Old, Vector3 New){
		transform.localScale = New;
	}
	
	[Command]
	void CmdTransformPosition(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 20f, groundLayer)){
			//We add half of the player's collider height because otherwise the center of our player will be on ground level
			_startPosition = hit.point + new Vector3(0f, characterController.height / 2, 0f);
		}
		RpcDrawLine();
	}
	
	[TargetRpc]
	void RpcDrawLine(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 20f, groundLayer)){
			_startPosition = hit.point + new Vector3(0f, 1f, 0f);
		}
	}
	
	[Command]
	void CmdResize(){
		if(!isFat){
			SV_size = new Vector3 (5, 5, 5);
			isFat = true;
		}else{
			SV_size = new Vector3 (1, 1, 1);
			isFat = false;
		}
		RpcChangeSize(SV_size);
	}
	
	[ClientRpc]
	void RpcChangeSize(Vector3 size){
		transform.localScale = size;
	}
}