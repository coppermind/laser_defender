﻿using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 1.5f;
	public float padding = 0.2f;
	public float spawnIntervalSeconds = 0.5f;
	public int shipCount = 0;
	
	private int direction = 1;
	private float screenRightEdge, screenLeftEdge;
	//private LevelManager levelManager;

	void Start () {
		//levelManager = GameObject.FindObjectOfType<LevelManager>();
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		
		screenLeftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
		screenRightEdge = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;
	
		SpawnUntilFull();
	}
	
	void Update () {
		float formationRightEdge = transform.position.x + padding * width;
		float formationLeftEdge = transform.position.x - padding * width;
		
		if (formationLeftEdge <= screenLeftEdge) {
			direction = 1;
		} else if (formationRightEdge >= screenRightEdge) {
			direction = -1;
		}
		transform.position += new Vector3(direction * speed * Time.deltaTime, 0 ,0);
		
		if (AllShipsAreDead()) {
			SpawnUntilFull();
		}
	}
	
	void OnDrawGizmos() {
		float xMin, xMax, yMin, yMax;
		xMin = transform.position.x - 0.5f * width;
		xMax = transform.position.x + 0.5f * width;
		yMin = transform.position.y - 0.5f * height;
		yMax = transform.position.y + 0.5f * height;
		Gizmos.DrawLine(new Vector3(xMin, yMin, 0f), new Vector3(xMax, yMin, 0f));
		Gizmos.DrawLine(new Vector3(xMin, yMax, 0f), new Vector3(xMax, yMax, 0f));
		Gizmos.DrawLine(new Vector3(xMin, yMin, 0f), new Vector3(xMin, yMax, 0f));
		Gizmos.DrawLine(new Vector3(xMax, yMin, 0f), new Vector3(xMax, yMax, 0f));
	}
	
	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition();
		if (null != freePosition) {
			SpawnEnemyShipAt(freePosition, freePosition.position);
			Invoke ("SpawnUntilFull", spawnIntervalSeconds);
		}
	}
	
	Transform NextFreePosition() {
		Position[] spawnPoints = GetComponentsInChildren<Position>();
		int points = spawnPoints.Length;
		for (int i = 0; i < points; i++) {
			int current = Random.Range(0, points-1);
			if (0 == spawnPoints[current].transform.childCount) {
				return spawnPoints[current].transform;
			}
		}
		return null;
	}
	
	void SpawnEnemyShipAt(Transform parentElement, Vector3 shipPosition) {
		GameObject enemy = Instantiate(enemyPrefab, new Vector3(10.64f, 8f, 0), Quaternion.identity) as GameObject;
		enemy.transform.parent = parentElement;
		enemy.transform.position = shipPosition;
		shipCount++;
	}
	
	bool AllShipsAreDead() {
		foreach (Transform spawnPoint in transform) {
			if (0 < spawnPoint.childCount) {
				return false;
			}
		}
		return true;
	}
	
	
}
