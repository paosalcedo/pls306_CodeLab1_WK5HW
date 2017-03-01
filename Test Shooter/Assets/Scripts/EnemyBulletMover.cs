using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMover : MonoBehaviour {

	GameObject player;
	GameObject[] players; 
	//	[SerializeField] Transform player;
	[SerializeField] Rigidbody2D rb;
	//[SerializeField] Rigidbody2D playerrb;
	[SerializeField] float fireballSpeed; 

	Vector2 vectorToPlayer;

	void Start (){
		players = GameObject.FindGameObjectsWithTag ("Player");
	}
	// Update is called once per frame
	void Update () {
		MoveFireball();	
		FindPlayer ();
 		//Debug.Log (vectorToPlayer);
	}

	private void MoveFireball (){ 
//		transform.position += transform.up * fireballSpeed * Time.deltaTime; 
		//rb.velocity = vectorToPlayer * fireballSpeed;
		rb.AddForce(vectorToPlayer * fireballSpeed);
	}

	private void FindPlayer(){
		foreach (GameObject player in players) {
			vectorToPlayer = player.transform.position -  transform.position; //get vector between player and enemy. 
//			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, .03f);
		}
	}
		
	void OnCollisionEnter2D(Collision2D collide){
		Destroy (gameObject);
	}
}