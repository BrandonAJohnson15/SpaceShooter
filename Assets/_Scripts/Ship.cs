using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ship : MonoBehaviour {

	[SerializeField]
	float speed = 100f;
	[SerializeField]
	float velocityLimit = 500f;
	[SerializeField]
	float smoothingDivisor = 2f;
	[SerializeField]
	int health = 5;
	[SerializeField]
	float rotationLimit = 45f;
	[SerializeField]
	float cameraBoundLimit = 15f;
	[SerializeField]
	List<Gun> guns;

	Rigidbody rb;
	Vector3 CurRotation;
	float minCameraX, maxCameraX, minCameraY, maxCameraY;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		CurRotation = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);

		float camDistance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
		Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
		Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));
		minCameraX = bottomCorner.x;
		maxCameraX = topCorner.x;
		minCameraY = bottomCorner.y;
		maxCameraY = topCorner.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if (horizontal != 0)
		{
			CurRotation.y -= horizontal * rotationLimit;
			rb.AddForce(Vector3.right * horizontal * speed);
		}
		else
		{
			if (CurRotation.y < 0)
			{
				CurRotation.y += rotationLimit;
			}
			else if (CurRotation.y > 0)
			{
				CurRotation.y -= rotationLimit;
			}
			rb.AddRelativeForce(0, -rb.velocity.y / smoothingDivisor, 0);
		}

		if (vertical != 0)
		{
			CurRotation.x += vertical * rotationLimit;
			rb.AddForce(Vector3.up * vertical * speed);
		}
		else
		{
			if (CurRotation.x < 0)
			{
				CurRotation.x += rotationLimit;
			}
			else if (CurRotation.x > 0)
			{
				CurRotation.x -= rotationLimit;
			}
			rb.AddRelativeForce(-rb.velocity.x / smoothingDivisor, 0, 0);
		}

		Vector3 position = this.transform.position;
		// camera constraints
		if (position.x < minCameraX + cameraBoundLimit)
		{
			position.x = minCameraX + cameraBoundLimit;
			rb.AddForce(Vector3.right * speed);
		}
		if (position.x > maxCameraX - cameraBoundLimit)
		{
			position.x = maxCameraX - cameraBoundLimit;
			rb.AddForce(Vector3.left * speed);
		}
		if (position.y < minCameraY + cameraBoundLimit)
		{
			position.y = minCameraY + cameraBoundLimit;
			rb.AddForce(Vector3.up * speed);
		}
		if (position.y > maxCameraY - cameraBoundLimit)
		{
			position.y = maxCameraY - cameraBoundLimit;
			rb.AddForce(Vector3.down * speed);
		}
		this.transform.position = position;

		CurRotation.y = Mathf.Clamp(CurRotation.y, -rotationLimit, rotationLimit);
		CurRotation.x = Mathf.Clamp(CurRotation.x, -rotationLimit, rotationLimit);
		CurRotation.z = Mathf.Clamp(CurRotation.z, -rotationLimit, rotationLimit);
		rb.transform.eulerAngles = CurRotation;


		if (Input.GetKeyDown(KeyCode.Space))
		{
			foreach (Gun gun in guns)
			{
				if (gun.gameObject.activeSelf)
				{
					gun.Fire();
				}
			}
		}
	}
}
