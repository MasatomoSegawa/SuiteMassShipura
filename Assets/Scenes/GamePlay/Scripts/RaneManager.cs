using UnityEngine;
using System.Collections;

public class RaneManager : Singleton<RaneManager> {

	//お菓子を生成するレーン
	public Rane[] Ranes;

	//ボーナスアイテムを生成するレーン
	public BonusRane bonusRane;

	//雷
	public GameObject Thunder;

	public float BonusOkashiFallCoolTime = 0.3f;
	public float OkashiFallCoolTime = 0.5f;
	private float CurrentTime;
	private float CurrentCoolTime;

	void Start(){

		this.CurrentCoolTime = OkashiFallCoolTime;

		Level ();

	}

	void Level(){

		/*
		string diffStr = PlayerPrefs.GetString ("Current_Difficulty");
		Difficulty difficulty;
		float ItemsSpeed = 0.0f, MaxRandomizeSpeed = 0.0f, OkashiCoolTime = 0.0f;

		diffStr = "Easy";

		switch (diffStr) {
		case "Easy":
			ItemsSpeed = 1.0f;
			MaxRandomizeSpeed = 2.0f;
			OkashiCoolTime = 0.4f;
			break;

		case "Normal":
			ItemsSpeed = 1.5f;
			MaxRandomizeSpeed = 3.0f;
			OkashiCoolTime = 0.35f;
			break;

		case "Hard":
			ItemsSpeed = 1.8f;
			MaxRandomizeSpeed = 4.0f;
			OkashiCoolTime = 0.3f;
			break;

		case "VeryHard":
			ItemsSpeed = 2.0f;
			MaxRandomizeSpeed = 5.0f;
			OkashiCoolTime = 0.25f;
			break;

		default:
			Debug.Log ("Error");
			break;
		}

		this.OkashiFallCoolTime = OkashiCoolTime;
	*/

	}

	void LevelChange(){
	

	}

	// Update is called once per frame
	void Update () {
	
		//お菓子アイテムをランダムに落とす
		if (GameModel.isFreeze == false) {

			if (CurrentTime <= Time.time) {
				CurrentTime = Time.time + CurrentCoolTime;
				this.FallRandomOkasi ();
			}

		}

	}

	void FallRandomOkasi(){

		Ranes [Random.Range (0, Ranes.Length)].FallOkashi ();

	}

	void FallBonusItem(){

		this.bonusRane.SendMessage ("FallBonusItem");

	}

}