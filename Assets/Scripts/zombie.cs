using UnityEngine;
using System.Collections;
using Pathfinding;

public class zombie : MonoBehaviour {

	public GameObject target;
	public float maxSpeed = 1f;
	float detectableRange = 20;

	Path path;
	Seeker seeker;
	int currentWayPoint;
	float rotationSpeed = 1f;
	
	

	Vector3 targetLoc;
	
	bool moving = false;
	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
		//seeker.StartPath(transform.position, target.transform.position, OnPathComplete); //set path to target
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		//seeker.StartPath(transform.position, target.transform.position, OnPathComplete); //set path to target

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
		int layerMask = 1 << 10;
		layerMask = ~layerMask;
		if (inDetectableRange(target)) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position + (target.transform.position - transform.position).normalized, target.transform.position - transform.position, detectableRange, layerMask);
			//print (hit.collider.gameObject);
			Debug.DrawRay(transform.position, target.transform.position - transform.position);
			Debug.DrawRay(transform.position, targetLoc-transform.position, Color.green);
			//print (target.transform.rotation.eulerAngles.z - transform.rotation.eulerAngles.z);
			Vector2 dir = (new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)).normalized);
			if ((Vector2.Angle(target.transform.position - transform.position , dir)) <= 90 ) {
				if (hit.collider.gameObject.CompareTag("Player")) {
				
				targetLoc = target.transform.position;
;
				}
				else {

					//InvokeRepeating("findPath",10,10);
				}
			}
			attackTarget(targetLoc);
			
		}
		if (!moving) {
			rigidbody2D.velocity *= .9f;
		}
		//print (moving);

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
	
	void faceDirection(Vector3 target) {		
		//aim in direction of movement
		//transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, direction * 45), rotationSpeed);

		Vector3 hurr = new Vector3( target.x, target.y, 0);
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
	
	void attackTarget(Vector3 target) {
		moving = true;
		faceDirection(target);
		if (Vector2.Distance(target, transform.position) > 1.2f) {
			rigidbody2D.AddForce((target - transform.position).normalized * 300);
		}
	}
	
	void findPath() {
		seeker.StartPath(transform.position, target.transform.position, OnPathComplete); //set path to target
	}
}
