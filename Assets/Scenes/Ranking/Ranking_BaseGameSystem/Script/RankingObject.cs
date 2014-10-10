using UnityEngine;
using System.Collections;


public class RankingObject : MonoBehaviour {

	public TextMesh NumberLabel;
	public TextMesh ScoreLabel;
	public TextMesh DifficultyLabel;

	void Start(){
		Init (1, 1000, Difficulty.Easy);
	}

			public void Init(int number, int score, Difficulty difficulty){

		NumberLabel.text = number + ".";
		ScoreLabel.text = System.String.Format ("{0:D8}", score);
		DifficultyLabel.text = difficulty.ToString ();

	}

}
