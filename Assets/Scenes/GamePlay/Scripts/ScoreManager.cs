using UnityEngine;
using System.Collections;

public class ScoreManager : Singleton<ScoreManager> {

	public int Score;

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}

	public void Init(int Score){
		this.Score = Score;
	}

}
