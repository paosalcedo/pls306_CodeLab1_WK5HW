using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour {
	[SerializeField] Rigidbody2D rb;
//	[SerializeField] GameObject player;
	[SerializeField] float mySpeed; 
	//private Vector2 myAimDir;
	//[SerializeField] Rigidbody2D playerRigidBody;

	
	// Update is called once per frame
	void Update () {
		MoveBullet();	
	}

	private void MoveBullet (){ 
		rb.velocity = transform.up * mySpeed * Time.deltaTime; 
	}

	void OnCollisionEnter2D(Collision2D collide){
		if (collide.gameObject.tag == "Enemy") {
			Destroy (gameObject);
			Destroy (collide.gameObject);
		} 

		if (collide.gameObject.tag == "Level") {
			Destroy (gameObject);
		} 

		if (collide.gameObject.tag == "EnemyWeapons") {
			Destroy (collide.gameObject);
			Destroy (gameObject);
		}
	}
}
