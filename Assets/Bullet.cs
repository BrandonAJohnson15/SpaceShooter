using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

	[SerializeField]
	int damage = 1;
	[SerializeField]
	float speed = 10f;

	[SerializeField]
	float bulletLifeTime = 5f;

	[SerializeField]
	ParticleSystem explosionParticles;

	public float Speed
	{
		get
		{
			return speed;
		}
	}

	private void Start()
	{
		StartCoroutine(StartLifeCountdown());
	}

	IEnumerator StartLifeCountdown()
	{
		yield return new WaitForSeconds(bulletLifeTime);
		Destroy(this.gameObject);
	}
}
