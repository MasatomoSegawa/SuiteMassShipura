using UnityEngine;
using System.Collections;

public enum ItemType{
	Score,TensionUp,BonusItem,
};

public class ScoreItem : MonoBehaviour {

	[Range(0.0f,10.0f)]
	private float FallSpeed = 5.0f;

	public int point;

	public ItemType myType;

	public int GaugeAddNum = 1;
	//private float CalolyMultiply = 0.01f;

	//ポイント追加イベント
	public delegate void GetScoreItem(int point);
	public event GetScoreItem GetScoreItemEvent;

	//デストロイゾーンに入った時のイベント
	public delegate void DestroyMe ();
	public event DestroyMe DestroyEvent;

	public void Init(float FallSpeed){

		this.FallSpeed = FallSpeed;

		///イベント登録
		this.GetScoreItemEvent += GUIScore.Instance.AddScore;
		//this.GetScoreItemEvent += ComboManager.Instance.AddCombo;
		//this.DestroyEvent += ComboManager.Instance.MissCombo;

		GetScoreItemEvent += (int point) => {
			//Debug.Log(point + "Get!");
		};

		DestroyEvent += () => {
			//Debug.Log(point + "Destroy");
		};

	}
	
	// Update is called once per frame
	void Update () {

		if(GameModel.isFreeze == false)
			this.transform.Translate (-Vector3.up * FallSpeed * Time.deltaTime);

	}

	/// <summary>
	/// 触手にアイテムが当たった時に呼ばれるメソッド
	/// </summary>
	public void End(){

		int newPoint = point;

		GetScoreItemEvent (newPoint);

		/*
		if (TensionGauge.Instance._isBonusTime == false) {
			TensionGauge.Instance.GaugeChange (point * CalolyMultiply);
		}
		*/

		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){

		switch (other.tag) {
		case "DeathLine":
			Destroy (this.gameObject,0.1f);
			DestroyEvent ();
			break;

		case "Tentacls":
			End ();
			break;
		}

	}

}
