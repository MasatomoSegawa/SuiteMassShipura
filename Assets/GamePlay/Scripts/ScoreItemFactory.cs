using UnityEngine;
using System.Collections;

public class ScoreItemFactory : Singleton<ScoreItemFactory> {

	public GameObject[] ScoreItem;
	public GameObject BonusItem;
	public GameObject Mushi;

	/// <summary>
	/// ランダムに虫とアイテムを生成する。
	/// </summary>
	/// <returns>The instantiate.</returns>
	/// <param name="position">Position.</param>
	public GameObject RandomInstantiate(Vector3 position){

		if (Random.Range (1, 10) >= 7) {
			GameObject MusiClone = Instantiate (Mushi, position, Quaternion.identity) as GameObject;
			MusiClone.GetComponent<Musi> ().Init ();

			return MusiClone;
		}

		GameObject clone = Instantiate(ScoreItem[Random.Range(0,ScoreItem.Length)],position,Quaternion.identity) as GameObject;
		clone.GetComponent<ScoreItem> ().Init ();

		clone.rigidbody2D.AddTorque (50.0f);

		return clone;
	}

	public GameObject BonusItemInstantiate(Vector3 position){

		GameObject bonusItemClone = Instantiate (BonusItem, position, Quaternion.identity) as GameObject;

		return bonusItemClone;

	}

}