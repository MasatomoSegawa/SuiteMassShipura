using UnityEngine;
using System.Collections;

public class ScoreItemFactory : Singleton<ScoreItemFactory> {

	public GameObject[] ScoreItem;
	public GameObject BonusItem;
	public GameObject Mushi;
	public GameObject SnowItem;
	public GameObject BonusRaneObject;

	// アイテムが落ちる速度
	public float ItemSpeed;

	// ランダムに速度を付ける
	[Range(0.0f, 10.0f)]
	public float MaxRandomizeSpeed;

	[Header("ボーナスアイテムが落ちてくる時間")]
	public float BonusTimeDuration = 20.0f;

	// 
	private float BonusTimer = 0.0f;

	public float MusiSpeed = 1.0f;

	public void SetLevel(float itemSpeed, float maxRandomizeSpeed, float MusiSpeed){

		this.ItemSpeed = itemSpeed;
		this.MaxRandomizeSpeed = maxRandomizeSpeed;
		this.MusiSpeed = MusiSpeed;

	}

	/// <summary>
	/// ランダムに虫とアイテムを生成する。
	/// </summary>
	/// <returns>The instantiate.</returns>
	/// <param name="position">Position.</param>
	public GameObject OkashiInstantiate(Vector3 position){

		GameObject clone = Instantiate(ScoreItem[Random.Range(0,ScoreItem.Length)],position,Quaternion.identity) as GameObject;
		clone.GetComponent<ScoreItem> ().Init (ItemSpeed + Random.Range(0.0f,MaxRandomizeSpeed));

		return clone;
	}

	public GameObject MusiInstantiate(Vector3 position){

		if (WeatherManager.Instance.CurrentWeatherState != WeatherState.Snow) {

			GameObject MusiClone = Instantiate (Mushi, position, Quaternion.identity) as GameObject;
			MusiClone.GetComponent<Musi> ().Init (MusiSpeed + Random.Range (0.0f, MaxRandomizeSpeed));

			return MusiClone;
		}else {

			GameObject SnowItemClone = Instantiate (SnowItem, position, Quaternion.identity) as GameObject;
			SnowItemClone.GetComponent<Musi>().Init(MusiSpeed + Random.Range (0.0f, MaxRandomizeSpeed));

			return SnowItemClone;
		}

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