using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour {

	[SerializeField]
	float speed = 500f;
	[SerializeField]
	int health = 5;
	[SerializeField]
	float rotationLimit = 45f;

	Rigidbody rb;


	Vector3 CurRotation;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		CurRotation = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			CurRotation.y += speed;
			rb.AddForce(Vector3.left * speed);
			
		} 
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			CurRotation.y -= speed;
			rb.AddForce(Vector3.right * speed);
		}

		CurRotation.y = Mathf.Clamp(CurRotation.y, -rotationLimit, rotationLimit);
		CurRotation.x = Mathf.Clamp(CurRotation.x, -rotationLimit, rotationLimit);
		CurRotation.z = Mathf.Clamp(CurRotation.z, -rotationLimit, rotationLimit);

		rb.transform.eulerAngles = CurRotation;
	}
}
