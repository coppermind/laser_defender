using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject laserPrefab;
	public float hitPoints = 10f;
	public float laserSpeed = 10f;
	public float laserRepeatRate = 2.5f;
	
	void Update() {
		float probability = laserRepeatRate * Time.deltaTime;
		if (Random.value < probability) {
			FireLaser();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Laser laser = collider.gameObject.GetComponent<Laser>();
		Ship ship = collider.gameObject.GetComponent<Ship>();
		if (laser != null) {
			hitPoints -= laser.damagePoints;
			if (0 >= hitPoints) {
				Destroy(gameObject);
			}
		} else if (ship != null) {
			Destroy(gameObject);
		}
	}

	void FireLaser() {
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.rigidbody2D.velocity = new Vector3(0f, laserSpeed, 0f);
	}
}
