using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP_Player : MonoBehaviour {
	
	[SerializeField] int myTeamNumber = 1;
	[SerializeField] string myControl = "1";
	[SerializeField] Rigidbody2D myRigidbody2D;

	private Vector2 myDirection;
	private Vector2 myMoveAxis;
	[SerializeField] float mySpeed = 1;
	[SerializeField] float moveGravity;
	[SerializeField] float moveSensitivity;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMove ();
	}

	private void UpdateMove () {
		float t_inputHorizontal = Input.GetAxis ("Horizontal");
		float t_inputVertical = Input.GetAxis ("Vertical");
		myDirection = (Vector3.up * t_inputVertical + Vector3.right * t_inputHorizontal).normalized;

		myMoveAxis += myDirection * moveSensitivity;
		if (myMoveAxis.magnitude > 1)
			myMoveAxis.Normalize ();


		myRigidbody2D.velocity = myMoveAxis * mySpeed;
	
/********************************************************
-----------CREATES RESISTANCE AND INERTIA--------------
*********************************************************/

		float t_moveAxisReduce = Time.deltaTime * moveGravity;
		if (myMoveAxis.magnitude < t_moveAxisReduce)
			myMoveAxis = Vector2.zero;
		else
			myMoveAxis *= (myMoveAxis.magnitude - t_moveAxisReduce);

		//Debug.Log ("ControlMove" + myDirection + " : " +myMoveAxis);
	}

}
