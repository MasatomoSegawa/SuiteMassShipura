using UnityEngine;
using System.Collections;

public class RaneManager : Singleton<RaneManager> {

	// お菓子を生成するレーン
	public Rane[] Ranes;

	// ボーナスアイテムを生成するレーン
	public BonusRane bonusRane;

	// 雷
	public GameObject Thunder;

	public float BonusOkashiFallCoolTime = 0.3f;
	public float OkashiFallCoolTime = 0.5f;

	private float CurrentTime = 0.0f;
	private float CurrentCoolTime;

	// 虫が落ちるクールタイム
	public float MusiCoolTime;
	public float CuruentMusiTime = 0.0f;

	void Start(){

		this.CurrentCoolTime = OkashiFallCoolTime;
		this.MusiCoolTime = 5.0f;

	}

	public void SetLevel(float OkashiFallCoolTime, float MusiCoolTime){

		this.CurrentCoolTime = OkashiFallCoolTime;
		this.MusiCoolTime = MusiCoolTime;

		this.CuruentMusiTime = Time.time + MusiCoolTime;

	}

	// Update is called once per frame
	void Update () {
	
		//お菓子アイテムをランダムに落とす
		if (GameModel.isFreeze == false) {

			//お菓子
			if (CurrentTime <= Time.time) {
				CurrentTime = Time.time + CurrentCoolTime;
				this.FallRandomOkasi ();
			}

			if (CuruentMusiTime <= Time.time) {
				CuruentMusiTime = Time.time + MusiCoolTime;
				this.FallRandomMusi ();
			}

		}

	}

	void FallRandomMusi(){

		Ranes [Random.Range (0, Ranes.Length)].FallMusi ();

	}

	void FallRandomOkasi(){

		Ranes [Random.Range (0, Ranes.Length)].FallOkashi ();

	}

	void FallBonusItem(){

		this.bonusRane.SendMessage ("FallBonusItem");

	}

}