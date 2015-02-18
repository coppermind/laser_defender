using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {

	public float damagePoints = 5f;
	
	private float screenBottomEdge;

	void Start () {
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		
		screenBottomEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;
	}
	
	void Update () {
		float laserBottomEdge = transform.position.y;
		if (laserBottomEdge <= screenBottomEdge) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Ship ship = collider.gameObject.GetComponent<Ship>();
		if (ship != null) {
			Destroy(gameObject);
		}
	}
}
