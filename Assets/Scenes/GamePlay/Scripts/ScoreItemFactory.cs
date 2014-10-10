using UnityEngine;
using System.Collections;

public class ScoreItemFactory : Singleton<ScoreItemFactory> {

	public GameObject[] ScoreItem;
	public GameObject BonusItem;
	public GameObject Mushi;

	// アイテムが落ちる速度
	public float ItemSpeed;

	/// <summary>
	/// ランダムに虫とアイテムを生成する。
	/// </summary>
	/// <returns>The instantiate.</returns>
	/// <param name="position">Position.</param>
	public GameObject RandomInstantiate(Vector3 position){

		if (Random.Range (1, 10) >= 7 && TensionGauge.Instance._isBonusTime == false) {
			GameObject MusiClone = Instantiate (Mushi, position, Quaternion.identity) as GameObject;
			MusiClone.GetComponent<Musi> ().Init (ItemSpeed);

			return MusiClone;
		}

		GameObject clone = Instantiate(ScoreItem[Random.Range(0,ScoreItem.Length)],position,Quaternion.identity) as GameObject;
		clone.GetComponent<ScoreItem> ().Init (ItemSpeed);

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