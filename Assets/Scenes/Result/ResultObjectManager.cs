using UnityEngine;
using System.Collections;

public class ResultObjectManager : MonoBehaviour {

	public TextMesh ScoreNumberLabel;
	public TextMesh MaximumChainNumebrLabel;
	public TextMesh DiffuicultyLabel;
	public TextMesh RankLabel;

	void Start(){

		int score = PlayerPrefs.GetInt ("Current_Score");
		int chain = PlayerPrefs.GetInt ("Current_Chain");
		string str = PlayerPrefs.GetString ("Current_Difficulty");
		Difficulty difficulty = Difficulty.Easy;

		switch (str) {
		case "Easy":
			difficulty = Difficulty.Easy;
			break;

		case "Normal":
			difficulty = Difficulty.Normal;
			break;

		case "hard":
			difficulty = Difficulty.Hard;
			break;

		case "VeryHard":
			difficulty = Difficulty.VeryHard;
			break;
		}

		Init (score, chain, difficulty);

	}

	public void Init(int ScoreNumber, int MaximumChainNumber, Difficulty difficulty){

		ScoreNumberLabel.text = System.String.Format ("{0:D9}", ScoreNumber);
		MaximumChainNumebrLabel.text = MaximumChainNumber.ToString();
		DiffuicultyLabel.text = difficulty.ToString ();
		RankLabel.text = "AA";

	}

}
