using UnityEngine;
using System.Collections;

public class BonusRane : MonoBehaviour {

	public Transform SpawnPoint;

	/// <summary>
	/// テンションMAX時に生成されるケーキを落とすメソッド
	/// </summary>
	public void FallBonusItem(){

		ScoreItemFactory.Instance.BonusItemInstantiate (SpawnPoint.position);

	}

}