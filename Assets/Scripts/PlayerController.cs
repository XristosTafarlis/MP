using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController))]

public class PlayerController: NetworkBehaviour{
	[Header("References")]
	public float walkingSpeed = 7.5f;	//Is public because is needed in CharacterSelection.cs
	[SerializeField] float runningSpeed = 11.5f;	//Default running speed
	[SerializeField] float gravity = 20.0f;	//Default gravity
	[SerializeField] float jumpSpeed = 8.0f;	//Default jump multiplayer
	[SerializeField] float lookSpeed = 2.0f;	//Default look sensitivity
	[HideInInspector] public bool canMove = true;	//Boolean that can be used to stop the player from moving
	
	[Header("Cameras")]
	[SerializeField] Camera FirstPersonCamera;	//Reference to the first person cammera
	[SerializeField] Camera ThirdPersonCamera;	//Reference to the third person cammera
	[SerializeField] Camera TopCamera;	//Reference to the camera that look the top part when in customization
	[SerializeField] Camera BotCamera;	//Reference to the camera that look the bottom part when in customization
	float lookXLimit = 90.0f;	//Maximum angle the player can look up and down
	
	CharacterController characterController;	//Character Controller component
	Animator animator;	//Animator component
	Camera workingCamera;	//Used to keep track of what camera is being used
	bool isFat;	//That was a temporary variable to test multiplayer behavior
	
	public enum CameraState{	//Camera state, controls which camera is being used
		FirstPersonCamera,
		ThirdPesonCamera,
		TopCamera,
		BotCamera
	};
	public CameraState myCamera;
	
	//Fly variables
	float currentPointBetween0and1;
	float targetPoint;
	float tempGravity;
	bool isFlying;
	Vector3 _goalPosition;
	Vector3 _startPosition;
	[SerializeField] LayerMask groundLayer;
	
	[SyncVar(hook = "OnSizeChange")]	//Temporary variable to check network functionality
	Vector3 SV_size;
	
	Vector3 moveDirection = Vector3.zero;
	float rotationX = 0;
	
	void Start(){
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		tempGravity = gravity;
		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		
		if(!isLocalPlayer){
			FirstPersonCamera.gameObject.SetActive(false);
			ThirdPersonCamera.gameObject.SetActive(false);
			TopCamera.gameObject.SetActive(false);
			BotCamera.gameObject.SetActive(false);
		}else{
			FirstPersonCamera.gameObject.SetActive(true);
			ThirdPersonCamera.gameObject.SetActive(false);
			TopCamera.gameObject.SetActive(false);
			BotCamera.gameObject.SetActive(false);
		}
		myCamera = CameraState.FirstPersonCamera;
		workingCamera = FirstPersonCamera;
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
			float movementDirectionY = moveDirection.y;
			
			// Press Left Shift to run
			bool isRunning = Input.GetKey(KeyCode.LeftShift);
			float axisX;
			float axisY;
			
			if (canMove){
				axisY = Input.GetAxisRaw("Horizontal");
				axisX = Input.GetAxisRaw("Vertical");
			}else{
				axisX = axisY = 0f;
			}
			
			moveDirection = (forward * axisX) + (right * axisY);
			moveDirection.Normalize();
			
			if (Input.GetButtonDown("Jump") && canMove && characterController.isGrounded){
				moveDirection.y = jumpSpeed;
				GetComponent<Animator>().SetTrigger("Jump");
			}
			else{
				moveDirection.y = movementDirectionY;
			}
			
			// Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
			// when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
			// as an acceleration (ms^-2)
			if (!characterController.isGrounded){
				moveDirection.y -= gravity * Time.deltaTime;
			}
			
			// Move the controller
			if(isRunning)
				characterController.Move(new Vector3(moveDirection.x * runningSpeed * Time.deltaTime, moveDirection.y * walkingSpeed * Time.deltaTime, moveDirection.z * runningSpeed * Time.deltaTime));
			else
				characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
		}
		
		// Player and Camera rotation
		if (canMove && Cursor.lockState == CursorLockMode.Locked){
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
		
		if(Input.GetMouseButtonDown(0) && PlayerCanvasScript.canClick == false){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		
		if(Input.GetKeyDown(KeyCode.Escape) && Cursor.lockState == CursorLockMode.Locked){
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
	
	void FlyInputs(){
		if(!isFlying && currentPointBetween0and1 == 0){
			characterController.enabled = false;
			_startPosition = transform.position;
			_goalPosition = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
			targetPoint = 1;
			isFlying = true;
			GetComponent<Animator>().SetBool("IsFlying", true);
			Invoke("SetFlyVariables", 1f);
		}else if (isFlying && currentPointBetween0and1 == 1){
			characterController.enabled = false;
			//Raycast
			CmdTransformPosition();
			_goalPosition = transform.position; //Need to be revesed, so this is actualy the starting position
			targetPoint = 0;
			moveDirection.y = 0;
			isFlying = false;
			Invoke("SetFlyVariables", 1f);
		}
	}
	
	void Fly(){
		currentPointBetween0and1 = Mathf.MoveTowards(currentPointBetween0and1, targetPoint, Time.deltaTime);
		transform.position = Vector3.Lerp(_startPosition, _goalPosition, currentPointBetween0and1);
	}
	
	void SetFlyVariables(){
		if(isFlying){
			gravity = 0f;
			moveDirection = Vector3.zero;
			characterController.enabled = true;
		}else{
			gravity = tempGravity;
			characterController.enabled = true;
			GetComponent<Animator>().SetBool("IsFlying", false);
		}
	}
	
	public void CammeraChange(){
		if (myCamera == CameraState.FirstPersonCamera){
			myCamera = CameraState.ThirdPesonCamera;
			workingCamera = ThirdPersonCamera;
			FirstPersonCamera.gameObject.SetActive(false);
			ThirdPersonCamera.gameObject.SetActive(true);
			TopCamera.gameObject.SetActive(false);
			BotCamera.gameObject.SetActive(false);
			lookXLimit = 40f;
			
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			canMove = true;
		}
		else if (myCamera == CameraState.ThirdPesonCamera){
			myCamera = CameraState.FirstPersonCamera;
			workingCamera = FirstPersonCamera;
			FirstPersonCamera.gameObject.SetActive(true);
			ThirdPersonCamera.gameObject.SetActive(false);
			TopCamera.gameObject.SetActive(false);
			BotCamera.gameObject.SetActive(false);
			lookXLimit = 90f;
			
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			canMove = true;
		}
	}
	
	public void CustomizationCamera(){
		if (myCamera == CameraState.TopCamera){
			myCamera = CameraState.BotCamera;
			workingCamera = FirstPersonCamera;
			FirstPersonCamera.gameObject.SetActive(false);
			ThirdPersonCamera.gameObject.SetActive(false);
			TopCamera.gameObject.SetActive(false);
			BotCamera.gameObject.SetActive(true);
			
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			canMove = false;
		}else if (myCamera == CameraState.BotCamera){
			myCamera = CameraState.TopCamera;
			workingCamera = FirstPersonCamera;
			FirstPersonCamera.gameObject.SetActive(false);
			ThirdPersonCamera.gameObject.SetActive(false);
			TopCamera.gameObject.SetActive(true);
			BotCamera.gameObject.SetActive(false);
			
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			canMove = false;
		}
	}
	
	[Command]
	void CmdTransformPosition(){
		RpcCastLine();
	}
	
	[TargetRpc]
	void RpcCastLine(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 20f, groundLayer)){
			_startPosition = hit.point;
		}
	}
	
	void OnSizeChange(Vector3 Old, Vector3 New){
		transform.localScale = New;
	}
	
	[Command]
	void CmdResize(){
		if(!isFat){
			SV_size = new Vector3 (5, 5, 5);
			isFat = true;
		}else{
			SV_size = new Vector3 (2, 2, 2);
			isFat = false;
		}
		RpcChangeSize(SV_size);
	}
	
	[ClientRpc]
	void RpcChangeSize(Vector3 size){
		transform.localScale = size;
	}
}