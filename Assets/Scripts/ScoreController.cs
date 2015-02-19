using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public static int totalScore = 0;
	private Text scoreText;

	void Start() {
		scoreText = GetComponent<Text>();
		Reset();
	}

	public void Reset() {
		totalScore = 0;
		UpdateText();
	}
	
	public void Score(int points) {
		totalScore += points;
		UpdateText();
	}
	
	void UpdateText() {
		scoreText.text = totalScore.ToString();
	}

}
