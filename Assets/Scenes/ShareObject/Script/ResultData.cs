using UnityEngine;
using System.Collections;

public enum Difficulty{
	Easy,Normal,Hard,VeryHard,
}

public enum Rank{
	A,AA,AAA,
}

public class ResultData : MonoBehaviour {

	int ScoreNumber;
	int ChainNumber;
	Difficulty difficulty;
	Rank rank;

	public void Init(int score,int chain, Difficulty difficulty){

	}

}
