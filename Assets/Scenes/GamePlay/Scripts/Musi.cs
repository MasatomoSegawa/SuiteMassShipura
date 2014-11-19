using UnityEngine;
using System.Collections;

public class Musi : MonoBehaviour {

	[Range(0.0f,10.0f)]
	private float FallSpeed = 5.0f;

	[Range(0.0f,100.0f)]
	public int point;

	public delegate void GetMusi();
	public event GetMusi GetMusiEvent;

	public void Init(float FallSpeed){

		this.FallSpeed = FallSpeed;

		///イベント登録
		//this.GetMusiEvent += ComboManager.Instance.MissCombo;
		this.GetMusiEvent += () => {
			EtoriController.Instance.GetComponent<Animator>().SetTrigger("GetHae");
		};
		this.GetMusiEvent += StaminaGauge.Instance.GetMusi;
				
	}

	// Update is called once per frame
	void Update () {

		if(GameModel.isFreeze == false)
			this.transform.Translate (-Vector3.up * FallSpeed * Time.deltaTime);

	}

	/// <summary>
	/// 触手に虫が当たった時に呼ばれるメソッド
	/// </summary>
	public void End(){

		GetMusiEvent ();

		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){

		switch (other.tag) {
		case "DeathLine":
			Destroy (this.gameObject);
			break;

		case "Tentacls":
			End ();
			break;
		}

	}

}
