using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireType { SINGLESHOT, AUTOMATIC };

public class Gun : MonoBehaviour {

	[SerializeField]
	Bullet bulletPrefab;

	[SerializeField]
	GameObject bulletStartPos;

	[SerializeField]
	FireType fireType = FireType.SINGLESHOT;

	public void Fire()
	{
		Bullet bullet = GameObject.Instantiate(bulletPrefab, bulletStartPos.transform.position, bulletPrefab.transform.rotation);
		bullet.GetComponent<Rigidbody>().AddForce(bullet.Speed * Vector3.up,ForceMode.Impulse);
	}
}
