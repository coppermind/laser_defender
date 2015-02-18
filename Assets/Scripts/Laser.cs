using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public float damagePoints = 5f;

	private float screenTopEdge;
	
	void Start () {
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		
		screenTopEdge = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).y;
	}
	
	void Update () {
		float laserBottomEdge = transform.position.y;
		if (laserBottomEdge >= screenTopEdge) {
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Enemy enemy = collider.gameObject.GetComponent<Enemy>();
		if (enemy != null) {
			Destroy(gameObject);
		}
	}
}
