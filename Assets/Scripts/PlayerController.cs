using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController))]

public class PlayerController: NetworkBehaviour{
	//Player Controller variables & references
	[Header("References")]
	public float walkingSpeed = 7.5f;
	[SerializeField] float runningSpeed = 11.5f;
	//[SerializeField] float jumpHeight = 3.0f;
	[SerializeField] float gravity = 20.0f;
	[SerializeField] Camera FirstPersonCamera;
	[SerializeField] Camera ThirdPersonCamera;
	[SerializeField] Camera TopCamera;
	[SerializeField] Camera BotCamera;
	[SerializeField] float lookSpeed = 2.0f;
	float lookXLimit = 90.0f;
	
	//My variables & references
	[Space(10)]
	[SerializeField] GameObject groundCheck;
	Camera workingCamera;
	Animator animator;
	bool isGrounded;
	bool isFat;
	bool isLocked;
	
	public enum CameraState{
		FirstPersonCamera,
		ThirdPesonCamera,
		TopCamera,
		BotCamera
	};
	public CameraState myCamera;
	
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
		animator = GetComponent<Animator>();
		tempGravity = gravity;
		
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		isLocked = true;
		
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
		isGrounded = Physics.CheckSphere(groundCheck.transform.position, 0.2f, groundLayer);
		
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
			
			// Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
			// when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
			// as an acceleration (ms^-2)
			if (!characterController.isGrounded){
				moveDirection.y -= gravity * Time.deltaTime;
			}
			
			// Move the controller
			if(isRunning)
				characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
			else
				characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
		}
		
		// Player and Camera rotation
		if (canMove && isLocked){
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
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
			moveDirection.y = 100;
			Debug.Log("Jump");
		}
		
		if(Input.GetMouseButtonDown(0) && PlayerCanvasScript.canClick == false){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			isLocked = true;
		}
		
		if(Input.GetKeyDown(KeyCode.Escape) && isLocked){
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			isLocked = false;
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
			_goalPosition = transform.position; //Need to be revesed, so this is actualy the starting position
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
			GetComponent<Animator>().SetBool("IsFlying", true);
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
			isLocked = true;
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
			isLocked = true;
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
			isLocked = false;
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
			isLocked = false;
			canMove = false;
		}
	}
	
	void OnSizeChange(Vector3 Old, Vector3 New){
		transform.localScale = New;
	}
	
	[Command]
	void CmdTransformPosition(){
		RpcCastLine();
	}
	
	[TargetRpc]
	void RpcCastLine(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 20f, groundLayer)){
			_startPosition = hit.point /*+ new Vector3(0f, 1f, 0f)*/;
		}
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