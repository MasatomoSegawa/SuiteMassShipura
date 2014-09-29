
using UnityEngine;
using System.Collections;

public class StaminaGauge : Singleton<StaminaGauge> {

	public Transform GaugePosition;

	private float CurrentValue = 1.0f;

	//スタミナゲージ回復中かどうか
	private bool _isStaminaHeal = false;

	[Range(0.0f,1.0f)]
	public float HealSpeed = 0.1f;

	[Range(0,100)]
	public int InitValue = 0;

	//スタミナゲージが切れた時に発生するイベント
	public delegate void _GaugeZero ();
	public event _GaugeZero GaugeZeroEvent;

	//疲労から回復した際に発生するイベント
	public delegate void _HealEnd();
	public event _HealEnd HealEndEvent;

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

				//スタミナ回復中
				this.GaugeChange (HealSpeed / 100.0f);

			}

		}

	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	void Init(){

		this.SetGauge (this.InitValue / 100.0f);

		this.GaugeZeroEvent += () => {
			Debug.Log("Gauge Zero");
		};

		this.HealEndEvent += () => {
			Debug.Log("Heal End");
		};
			
	}

	/// <summary>
	/// ゲージをセットするメソッド
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetGauge(float value){

		this.CurrentValue = Mathf.Clamp (value, 0.0f, 1.0f);

		this.GaugePosition.localScale = new Vector3 (CurrentValue, 1.0f, 1.0f);

	}

	/// <summary>
	/// ゲージの値が変化した時に呼ばれるメソッド
	/// </summary>
	public void GaugeChange(float value){

		float newValue = CurrentValue + value;

		this.CurrentValue = Mathf.Clamp (newValue, 0.0f, 1.0f);

		GaugePosition.localScale = new Vector3 (CurrentValue, 1.0f, 1.0f);

		//疲労回復中じゃない　かつ　ゲージが０になったら
		if (this._isStaminaHeal == false && this.CurrentValue <= 0.0f) {
			this.GaugeZero ();
		}

		//疲労回復中　かつ　ゲージがMAXになったら
		if (this._isStaminaHeal == true && this.CurrentValue >= 1.0f) {
			this.HealEnd ();
		}

	}

	/// <summary>
	/// 疲労回復完了時に呼ばれるメソッド
	/// </summary>
	private void HealEnd(){

		this._isStaminaHeal = false;

		//イベント発生
		this.HealEndEvent ();

		this.SetGauge (50.0f / 100.0f);
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
