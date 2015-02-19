using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public GameObject laserPrefab;
	public float shipSpeed = 0.2f;
	public float shipPadding = 0.5f;
	
	public float laserSpeed = 10f;
	public float laserRepeatRate = 0.2f;

	public float hitPoints = 10f;
	public int spawnCount = 3;
	
	public AudioClip laserSFX;
	
	private float minX, maxX, minY, maxY;
	
	private LevelManager levelManager;
	
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		minX = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + shipPadding;
		minY = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + shipPadding;
		
		maxX = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - shipPadding;
		maxY = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).y - shipPadding;
	}

	void Update () {
		Vector3 shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		
		float newXPosition = shipPosition.x;
		float newYPosition = shipPosition.y;
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
			newXPosition = transform.position.x - shipSpeed;
			UpdateShipPosition(shipPosition, newXPosition, newYPosition);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			newXPosition = transform.position.x + shipSpeed;
			UpdateShipPosition(shipPosition, newXPosition, newYPosition);
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			newYPosition = transform.position.y + shipSpeed;
			UpdateShipPosition(shipPosition, newXPosition, newYPosition);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			newYPosition = transform.position.y - shipSpeed;
			UpdateShipPosition(shipPosition, newXPosition, newYPosition);
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("FireLaser", 0.0001f, laserRepeatRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("FireLaser");
		}
	}
	
	void UpdateShipPosition (Vector3 ship, float x, float y) {
		ship.x = Mathf.Clamp(x, minX, maxX);
		ship.y = Mathf.Clamp(y, minY, maxY);
		transform.position = ship;
	}
	
	void FireLaser() {
		GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		laser.rigidbody2D.velocity = new Vector3(0f, laserSpeed, 0f);
		AudioSource.PlayClipAtPoint(laserSFX, transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyLaser enemyLaser = collider.gameObject.GetComponent<EnemyLaser>();
		Enemy enemyShip = collider.gameObject.GetComponent<Enemy>();
		
		if (enemyShip != null) {
			// Auto lose if collision with enemy ship
			Destroy(gameObject);
			levelManager.LoadLevel("Lose Screen");
		} else if (enemyLaser != null) {
			hitPoints -= enemyLaser.damagePoints;
			if (0 >= hitPoints) {
				Destroy(gameObject);
				// spawnCount--;
				levelManager.LoadLevel("Lose Screen");
			}
		} 
	}
}
