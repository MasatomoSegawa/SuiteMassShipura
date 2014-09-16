using UnityEngine;
using System.Collections;

public class RaneManager : MonoBehaviour {

	//お菓子を生成するレーン
	public Rane[] Ranes;

	//ボーナスアイテムを生成するレーン
	public BonusRane bonusRane;

	public float OkashiFallCoolTime = 0.5f;
	private float CurrentTime;

	void Start(){

		//テンションゲージがMAX時にボーナスアイテムを生成する様に登録する
		TensionGauge.Instance.MaxGaugeEvent += FallBonusItem;

	}
	
	// Update is called once per frame
	void Update () {

		//お菓子アイテムをランダムに落とす
		if (GameModel.isFreeze == false) {

			if (CurrentTime <= Time.time) {
				CurrentTime = Time.time + OkashiFallCoolTime;

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