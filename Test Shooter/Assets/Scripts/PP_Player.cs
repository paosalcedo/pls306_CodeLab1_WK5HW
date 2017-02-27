using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP_Player : MonoBehaviour {
	
	[SerializeField] int myTeamNumber = 1;
	[SerializeField] string myControl = "1";
	[SerializeField] Rigidbody2D myRigidbody2D;

	private Vector2 myDirection;
	private Vector2 myMoveAxis;
	private Vector3 myRotation;
	[SerializeField] float mySpeed = 1;
	[SerializeField] float moveGravity;
	[SerializeField] float moveSensitivity;
	[SerializeField] float myRotSpeed;	

	// Use this for initialization
	void Start () {
		myRotation = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (gameObject.tag == "Rotator") {
//			UpdateRotation ();
//		} else {
//			UpdateMove ();
//		}

		UpdateMove();
		UpdateRotation();
	}

	private void UpdateMove () 
	{
		float t_inputHorizontal = Input.GetAxis ("Horizontal");
		float t_inputVertical = Input.GetAxis ("Vertical");
		myDirection = (Vector3.up * t_inputVertical + Vector3.right * t_inputHorizontal).normalized;

		myMoveAxis += myDirection * moveSensitivity;
		if (myMoveAxis.magnitude > 1)
			myMoveAxis.Normalize ();


		myRigidbody2D.velocity = myMoveAxis * mySpeed;
		//myRigidbody2D.AddForce (myMoveAxis * mySpeed);
	
/********************************************************
-----------CREATES RESISTANCE AND INERTIA--------------
*********************************************************/
		
		float t_moveAxisReduce = Time.deltaTime * moveGravity;
		Debug.Log(t_moveAxisReduce);
		if (myMoveAxis.magnitude < t_moveAxisReduce)
			myMoveAxis = Vector2.zero;
		else
			myMoveAxis *= (myMoveAxis.magnitude - t_moveAxisReduce);
	}

	private void UpdateRotation ()
	{
//		float inputRotate = Input.GetAxis("RightStickX");
//		transform.Rotate(Vector3.forward * myRotSpeed * inputRotate);

//		ASSIGNING CONTROL TO THE RIGHT STICK
		float deadzone = 0.2f;
		float inputRightStickX = Input.GetAxis ("RightStickX");
		float inputRightStickY = Input.GetAxis ("RightStickY");
		
		Vector2 inputRightStick;
		inputRightStick = new Vector2 (inputRightStickX, inputRightStickY);
		
		Vector3 lastRotation;
		lastRotation = transform.eulerAngles; 
		
		transform.eulerAngles = new Vector3(
			transform.eulerAngles.x, 
			transform.eulerAngles.y,  
			Mathf.Atan2(inputRightStick.x, inputRightStick.y) * Mathf.Rad2Deg);		

//Attempt to disable the rotation reset due to Unity's controller deadzone
		if (inputRightStick.magnitude < deadzone) {
			transform.eulerAngles = lastRotation;
		}		

//		float t_inputHorizontal = Input.GetAxis ("RightStickX");
//		float t_inputVertical = Input.GetAxis ("RightStickY");
//		myDirection = (Vector3.up * t_inputVertical + Vector3.right * t_inputHorizontal).normalized;
//
//		myMoveAxis += myDirection * moveSensitivity;
//		if (myMoveAxis.magnitude > 1)
//			myMoveAxis.Normalize ();
//
//
//		myRigidbody2D.velocity = myMoveAxis * mySpeed;
//		//myRigidbody2D.AddForce (myMoveAxis * mySpeed);
//	
///********************************************************
//-----------CREATES RESISTANCE AND INERTIA--------------
//*********************************************************/
//
//		float t_moveAxisReduce = Time.deltaTime * moveGravity;
//		if (myMoveAxis.magnitude < t_moveAxisReduce)
//			myMoveAxis = Vector2.zero;
//		else
//			myMoveAxis *= (myMoveAxis.magnitude - t_moveAxisReduce);
	}

}
