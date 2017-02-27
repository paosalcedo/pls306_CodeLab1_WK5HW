﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour {
	[SerializeField] Rigidbody2D rb;
	[SerializeField] GameObject player;
	[SerializeField] float mySpeed; 
	private Vector2 myAimDir;
	
	// Update is called once per frame
	void Update () {
		MoveBullet();	
	}

	private void MoveBullet ()
	{
		rb.velocity = (Vector2.right * mySpeed);
	}
}