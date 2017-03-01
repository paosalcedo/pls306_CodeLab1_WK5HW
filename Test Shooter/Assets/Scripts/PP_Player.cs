using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP_Player : MonoBehaviour {
	
	[SerializeField] int myTeamNumber = 1;
	[SerializeField] string myControl = "1";
	[SerializeField] Rigidbody2D myRigidbody2D;
	[SerializeField] GameObject trapBullet;
	float cooldown;
	public Vector2 myDirection;
	public Vector2 myMoveAxis;
	[SerializeField] float mySpeed = 1;
	[SerializeField] float stickPrecision;
	[SerializeField] float moveSensitivity;

	// Use this for initialization
	void Start () {
 	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateMove();
		UpdateRotation();
		TrapperAttack();
	}

	private void UpdateMove ()
	{
		float t_inputHorizontal = Input.GetAxis ("Horizontal");
		float t_inputVertical = Input.GetAxis ("Vertical");
//		myDirection = (Vector3.up * t_inputVertical + Vector3.right * t_inputHorizontal).normalized;
		myDirection = (Vector3.up * t_inputVertical + Vector3.right * t_inputHorizontal);

		myMoveAxis += myDirection * moveSensitivity;
		if (myMoveAxis.magnitude > 1)
			myMoveAxis.Normalize ();

		myRigidbody2D.velocity = myMoveAxis * mySpeed;
 	
/********************************************************
-----------CREATES RESISTANCE AND INERTIA--------------
*********************************************************/
		//Debug.Log (myMoveAxis.magnitude);
		float t_moveAxisReduce = Time.deltaTime * stickPrecision;
//		Debug.Log("move axis reduce: " + t_moveAxisReduce);
//		Debug.Log("myDirection: " + myDirection.magnitude);
//		Debug.Log("myMoveAxis: " + myMoveAxis.magnitude);
//		Debug.Log("myRigidbody2D.velocity: " + myRigidbody2D.velocity.magnitude);

		if (myMoveAxis.magnitude < t_moveAxisReduce) { //this is when left stick is not pressed.
			myMoveAxis = Vector2.zero;
		} 
//			"UN-COMMENT" ELSE CONDITION BELOW FOR BETTER KEYBOARD SUPPORT
//			else {		
// 			myMoveAxis *= (myMoveAxis.magnitude - t_moveAxisReduce); 
//			//adding Sutphin's deadzone fix
//			myMoveAxis = myMoveAxis.normalized * 
//						((myMoveAxis.magnitude - t_moveAxisReduce) / 
//						(1 - t_moveAxisReduce)); 
//		}

//		float deadzone = 0.2f;		
//		if (myMoveAxis.magnitude < deadzone)
//			myMoveAxis = Vector2.zero;
//		else
//			myMoveAxis = myMoveAxis.normalized * ((myMoveAxis.magnitude - deadzone)/(1-deadzone));

//		if(stickInput.magnitude < deadzone)
//    stickInput = Vector2.zero;
//else
//    stickInput = stickInput.normalized * ((stickInput.magnitude - deadzone) / (1 - deadzone));
	}

	private void UpdateRotation ()
	{
		
//		ASSIGN AIMING TO THE RIGHT STICK
			float deadzone = 0.2f;
			float inputRightStickX = Input.GetAxis ("RightStickX");
			float inputRightStickY = Input.GetAxis ("RightStickY");
			
			Vector2 inputRightStick;
			inputRightStick = new Vector2 (inputRightStickX, inputRightStickY);
			
//Stores the last rotation angle.
//			Vector3 lastRotation;
//			lastRotation = transform.eulerAngles; 
//			
//			transform.eulerAngles = new Vector3 (
//				transform.eulerAngles.x, 
//				transform.eulerAngles.y,  
//				Mathf.Atan2 (inputRightStick.x, inputRightStick.y) * Mathf.Rad2Deg); //binds z rotation to the right stick		
//	

//Prevents snapping back to transform.eulerAngles.z = 270f when letting go of right analog stick.
//			if (inputRightStick.magnitude < deadzone) {
//				transform.eulerAngles = lastRotation;
//			}		

//Rigidbody version of rightstick control. 
		
		float lastRot = transform.eulerAngles.z; 
		myRigidbody2D.MoveRotation (Mathf.Atan2 (inputRightStick.x, inputRightStick.y) * Mathf.Rad2Deg);

		if (inputRightStick.magnitude < deadzone) {
			myRigidbody2D.MoveRotation (lastRot);
		}		
	}

	private void TrapperAttack ()
	{

		float inputAttack = Input.GetAxis ("RightTrigger");
		if (inputAttack == 1) {
				trapBullet = Instantiate (Resources.Load ("Prefabs/TrapBullet") as GameObject); 
				trapBullet.transform.position = transform.position;
				trapBullet.transform.rotation = transform.rotation;	
		}
	}

}
