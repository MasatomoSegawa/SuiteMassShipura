using UnityEngine;
using System.Collections;

public class ScoreItemFactory : Singleton<ScoreItemFactory> {

	public GameObject[] ScoreItem;
	public GameObject BonusItem;
	public GameObject Mushi;
	public GameObject BonusRaneObject;

	// アイテムが落ちる速度
	public float ItemSpeed;

	// 虫が落ちる確率(1~10)
	public float MusiRandomize = 1;

	// ランダムに速度を付ける
	[Range(0.0f, 10.0f)]
	public float MaxRandomizeSpeed;

	[Header("ボーナスアイテムが落ちてくる時間")]
	public float BonusTimeDuration = 20.0f;

	// 
	private float BonusTimer = 0.0f;


	void Start(){

		/*
		string diffStr = PlayerPrefs.GetString ("Current_Difficulty");

		//debug
		diffStr = "Easy";

		Difficulty difficulty = Difficulty.Easy;
		float ItemsSpeed, MaxRandomize = 0.0f, OkashiCoolTime;

		switch (diffStr) {
		case "Easy":
			ItemsSpeed = 1.0f;
			MaxRandomize = 2.0f;
			OkashiCoolTime = 0.4f;
			break;

		case "Normal":
			ItemsSpeed = 1.5f;
			MaxRandomize = 3.0f;
			OkashiCoolTime = 0.35f;
			break;

		case "Hard":
			ItemsSpeed = 1.8f;
			MaxRandomize = 4.0f;
			OkashiCoolTime = 0.3f;
			break;

		case "VeryHard":
			ItemsSpeed = 2.0f;
			MaxRandomize = 5.0f;
			OkashiCoolTime = 0.25f;
			break;

		default:
			Debug.Log ("Error");
			break;
		}
		*/

	}

	void LevelChange(){

	}

	void Update(){

		/*
		if (GameModel.isFreeze == false) {
			if (BonusTimer >= BonusTimeDuration) {
				BonusTimer = 0.0f;

				BonusItemInstantiate (BonusRaneObject.transform.position);
			} else {
				BonusTimer += Time.deltaTime;
			}
		}
		*/

	}

	/// <summary>
	/// ランダムに虫とアイテムを生成する。
	/// </summary>
	/// <returns>The instantiate.</returns>
	/// <param name="position">Position.</param>
	public GameObject RandomInstantiate(Vector3 position){

		if (Random.Range (1.0f, 10.0f) <= MusiRandomize) {
			GameObject MusiClone = Instantiate (Mushi, position, Quaternion.identity) as GameObject;
			MusiClone.GetComponent<Musi> ().Init (ItemSpeed + Random.Range(0.0f,MaxRandomizeSpeed));

			return MusiClone;
		}

		GameObject clone = Instantiate(ScoreItem[Random.Range(0,ScoreItem.Length)],position,Quaternion.identity) as GameObject;
		clone.GetComponent<ScoreItem> ().Init (ItemSpeed + Random.Range(0.0f,MaxRandomizeSpeed));

		return clone;
	}

	/// <summary>
	/// ボーナスアイテムを生成する
	/// </summary>
	/// <returns>The item instantiate.</returns>
	/// <param name="position">Position.</param>
	public GameObject BonusItemInstantiate(Vector3 position){

		GameObject bonusItemClone = Instantiate (BonusItem, position, Quaternion.identity) as GameObject;
		bonusItemClone.GetComponent<BonusItem> ().Init (ItemSpeed);

		return bonusItemClone;

	}

}