using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject laserPrefab;
	public float hitPoints = 10f;
	public float laserSpeed = 10f;
	public float laserRepeatRate = 2.5f;
	
	public GameObject kaboomFX;
	public AudioClip laserSFX;
	public AudioClip shipBoomSFX;
	
	public int score = 10;
	
	private ScoreController scoreController;
	private GameObject puff;
	
	void Update() {
		scoreController = GameObject.FindObjectOfType<ScoreController>();
		
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
				AudioSource.PlayClipAtPoint(shipBoomSFX, transform.position);
				scoreController.Score(score);
				Destroy(gameObject);
				CallDragon();
			}
		} else if (ship != null) {
			Destroy(gameObject);
		}
	}

	void FireLaser() {
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.rigidbody2D.velocity = new Vector3(0f, laserSpeed, 0f);
		AudioSource.PlayClipAtPoint(laserSFX, transform.position);
	}
	
	void CallDragon() {
		puff = Instantiate(kaboomFX, transform.position, Quaternion.identity) as GameObject;
		Invoke ("ShooDragon", 0.3f);
	}
	
	void ShooDragon(GameObject puff) {
		Destroy(puff);
	}
}
