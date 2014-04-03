using UnityEngine;
using System.Collections;
using Pathfinding;

public class zombie : MonoBehaviour {

	public GameObject target;
	public float speed = 1f;
	float detectableRange = 5;

	Path path;
	Seeker seeker;
	int currentWayPoint;
	float rotationSpeed = 1f;
	float maxSpeed = 5;
	
	bool moving = false;
	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		seeker.StartPath(transform.position, target.transform.position, OnPathComplete); //set path to target

		if (Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < .3f) {
			currentWayPoint++;
		}
		faceDirection(target);
		rigidbody2D.AddForce((path.vectorPath[currentWayPoint] - transform.position).normalized * 500);
		*/
		
		//transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		//print (rigidbody2D.velocity.magnitude);
		
		moving = false;
		if (rigidbody2D.velocity.magnitude > maxSpeed) {
			rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);
		}
		
//		if (Vector2.Distance(target.transform.position, transform.position) <= 1) {
//			rigidbody2D.velocity = Vector2.zero;
//		}
		
		if (inDetectableRange(target)) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position + (target.transform.position - transform.position).normalized, target.transform.position - transform.position, 5);
			//print (hit.collider.gameObject);
			Debug.DrawRay(transform.position, target.transform.position - transform.position);
			//print (target.transform.rotation.eulerAngles.z - transform.rotation.eulerAngles.z);
			Vector2 dir = (new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)).normalized);
			if ((Vector2.Angle(target.transform.position - transform.position , dir)) <= 90 ) {
				if (hit.collider.gameObject.CompareTag("Player")) {
				//print ("chasing");
				attackTarget(target);
				}
			}
			
		}
		if (!moving) {
			rigidbody2D.velocity *= .9f;
		}
		print (moving);

	}
	
	void FixedUpdate() {
		
	
	}
	
	void OnPathComplete(Path p) {
		if (!p.error) {
			path = p;
			currentWayPoint = 0;
			
		}
		else {
			Debug.Log(p.error);
		}
		
	}
	
	void faceDirection(GameObject target) {		
		//aim in direction of movement
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, direction * 45), rotationSpeed);
		
		//aim at mouse
		Vector3  yes = target.transform.position;
		Vector3 hurr = new Vector3( yes.x, yes.y, 0);
		float angle = Mathf.Atan2(hurr.y - transform.position.y, hurr.x - transform.position.x) * Mathf.Rad2Deg;
		//print (hurr);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,angle), rotationSpeed);
		
		
	}
	
	bool inDetectableRange(GameObject target) {
		if (Vector3.Distance(target.transform.position, transform.position) <= detectableRange) {
			return true;		
		}
		return false;
	}
	
	void attackTarget(GameObject target) {
		moving = true;
		faceDirection(target);
		if (Vector2.Distance(target.transform.position, transform.position) > 1.2f) {
			rigidbody2D.AddForce((target.transform.position - transform.position).normalized * 100);
		}
	}
}
