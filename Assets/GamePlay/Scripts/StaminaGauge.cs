using UnityEngine;
using System.Collections;

public class StaminaGauge : Singleton<StaminaGauge> {

	public Transform GaugePosition;

	private float CurrentValue = 1.0f;

	//スタミナゲージ回復中かどうか
	private bool _isStaminaHeal = false;

	public float HealSpeed = 0.1f;

	public delegate void _GaugeZero ();
	public event _GaugeZero GaugeZeroEvent;

	void Start(){

		Init ();

	}

	void Update(){

		if (GameModel.isFreeze == false) {


			//スタミナ切れによる回復中でなければ
			if (_isStaminaHeal == false) {
				//触手の本数分ゲージが減っていく
				float newValue = (0.1f - 0.1f * EtoriController.Instance.CurrentExistTentaclsNum) / 100.0f;
				GaugeChange (newValue);
			} else {



			}

		}

	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	void Init(){

		this.GaugeZeroEvent += () => {
			Debug.Log("Gauge Zero");
		};

		GaugePosition.localScale = new Vector3 (CurrentValue, 1.0f, 1.0f);

	}

	/// <summary>
	/// ゲージの値が変化した時に呼ばれるメソッド
	/// </summary>
	public void GaugeChange(float value){

		float newValue = CurrentValue + value;

		this.CurrentValue = Mathf.Clamp (newValue, 0.0f, 1.0f);

		GaugePosition.localScale = new Vector3 (CurrentValue, 1.0f, 1.0f);

		if (this.CurrentValue <= 0.0f) {
			this.GaugeZero ();
		}

	}

	/// <summary>
	/// ゲージが０になった時に呼ばれるメソッド
	/// </summary>
	private void GaugeZero(){

		this._isStaminaHeal = true;

		//イベント発生
		this.GaugeZeroEvent ();

	}

}
