using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	float diag = Mathf.Sqrt(2);
	bool movingX = false;
	bool movingY = false;
	Vector2 direction = new Vector2(0,0);
	int directionInt = 0;
	
	public GameObject arrow;
	public GameObject projectile;
	public int moveForce = 80;
	public int maxSpeed = 10;
	public int rotationSpeed = 15;
	public float friction = .9f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		faceDirection(directionInt);
	}
	
	void FixedUpdate() {
		bool moving = (movingX || movingY);
		
		movingX = false;
		movingY = false;
		Controls ();
		
		//controls max speed
		if (rigidbody2D.velocity.magnitude > maxSpeed) {
			rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);
		}

		//friction when not moving (ie stopping force)
		if (!moving) {
			this.gameObject.rigidbody2D.velocity *= friction; 

		}
		
	}
	
	void Controls() {
	
		#region movement
		//note: direction is dominated by up/down because they are later
		
		if (Input.GetKey (KeyCode.D)) {
			moveSide(1);
			directionInt = 0;
		}
		
		if (Input.GetKey (KeyCode.A)) {
			moveSide(-1);
			directionInt = 4;
		}
		
		if (Input.GetKey (KeyCode.W)) {
			moveVertical(1);
			directionInt = 2;
		}
		
		if (Input.GetKey (KeyCode.S)) {
			moveVertical(-1);
			directionInt = 6;
		}
		
		//diagonal direction keys
		
		if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) {
			directionInt = 1;
		}
		else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) {
			directionInt = 3;
		}
		else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) {
			directionInt = 7;
		}
		else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) {
			directionInt = 5;
		}
		#endregion
//		if (Input.GetKeyDown(KeyCode.Space)) {
//			shoot ();
//		}
	}
	
	void moveSide(float multiplier) {
	
		if (movingY) {
			rigidbody2D.AddForce (new Vector2 (multiplier * moveForce / diag, 0));
			direction = new Vector2(multiplier,direction.y);
			
		} else {
			rigidbody2D.AddForce (new Vector2 (multiplier * moveForce, 0));			
			direction = new Vector2(multiplier, 0);
			
			}		
		movingX = true;
	}
	
	void moveVertical(float multiplier) {
		
		if (movingX) {
			rigidbody2D.AddForce (new Vector2 (0, multiplier * moveForce / diag));
			direction = new Vector2(direction.x, multiplier);
			
			
		} else {
			rigidbody2D.AddForce (new Vector2 (0, multiplier * moveForce));			
			direction = new Vector2(0, multiplier);
			
			}
		movingY = true;
	}
	
	void faceDirection(int direction) {		
		//aim in direction of movement
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, direction * 45), rotationSpeed);
		
		//aim at mouse
		Vector3 moz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 hurr = new Vector3( moz.x, moz.y, 0);
		float angle = Mathf.Atan2(hurr.y - transform.position.y, hurr.x - transform.position.x) * Mathf.Rad2Deg;
		//print (hurr);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,angle), rotationSpeed);
			
	}
	
	void turnLeft() {

	}
	
	void turnRight() {
	
	}
	
	void turn180() {
	
	}
	
	void shoot() {
	}
	
}
