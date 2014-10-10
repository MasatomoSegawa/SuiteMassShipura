using UnityEngine;
using System.Collections;

public class BonusItem : MonoBehaviour {

	[Range(0.0f,10.0f)]
	public float FallSpeed = 5.0f;

	public int point;

	public delegate void GetBonusItem();
	public event GetBonusItem GetBonusItemEvent;

	public GameObject BonusTimeCutInEffect;

	public void Init(float FallSpeed){

		this.FallSpeed = FallSpeed;

		this.GetBonusItemEvent += TensionGauge.Instance.StartBonusTime;
		this.GetBonusItemEvent += EtoriController.Instance.AnimEnter_GetCake;

	}

	// Update is called once per frame
	void Update () {

		if(GameModel.isFreeze == false)
			this.transform.Translate (-Vector3.up * FallSpeed * Time.deltaTime);

	}

	/// <summary>
	/// 触手にアイテムが当たった時に呼ばれるメソッド
	/// </summary>
	public void BonusTimeStart(){

		//TensionGauge.Instance.StartBonusTime ();

		Instantiate (BonusTimeCutInEffect);

		Destroy (this.gameObject);

		this.GetBonusItemEvent ();

	}

	void OnTriggerEnter2D(Collider2D other){

		switch (other.tag) {
		case "DeathLine":
			Destroy (this.gameObject);
			break;

		case "EtoriChan":
			Debug.Log ("GetCake!");
			BonusTimeStart ();
			break;
		}

	}

}
