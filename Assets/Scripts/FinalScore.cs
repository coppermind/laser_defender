using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScore : MonoBehaviour {

	void Start () {
		GetComponent<Text>().text = ScoreController.totalScore.ToString();
	}

}
