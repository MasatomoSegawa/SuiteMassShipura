using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class StaminaGauge : Singleton<StaminaGauge> {

	//public Transform GaugePosition;

	public Slider StaminaBar;

	/// ゲージのスプライト群
	public Image image;
	public Sprite NormalGauge;
	public Sprite RecoveryGauge;
	public Sprite Warn1Gauge;
	public Sprite Warn2Gauge;

	// えとりちゃんのスタミナ最大値
	public float StaminaMaxValue = 100;

	private float GaugeMaxNum;

	public float HealTimeDuration = 5.0f;

	//スタミナゲージ回復中かどうか
	private bool _isStaminaHeal = false;

	[Header("触手の出してる本数分のダメージ")]
	public float DeclineTuruDamage = 0.0001f;
	[Header("時間経過によるダメージ")]
	public float DeclineTimeDamage = 0.001f;
	[Header("虫を取ったことによるダメージ")]
	public float DeclineMusiDamage = 0.1f;

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
			float newValue = 0.0f;

			//時間経過でゲージ減る
			newValue += -DeclineTimeDamage;

			//触手の本数分ゲージ減る
			newValue += -DeclineTuruDamage * EtoriController.Instance.CurrentExistTentaclsNum;
			GaugeChange (newValue);

			//Debug.Log (this.StaminaBar.value);
		}

	}

	public void GetMusi(){

		GaugeChange (-DeclineMusiDamage);

	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	void Init(){

		this.StaminaBar.maxValue = this.StaminaMaxValue;
		this.StaminaBar.value = InitValue;

		this.GaugeZeroEvent += () => {
			Debug.Log("Gauge Zero");
		};

		this.HealEndEvent += () => {
			Debug.Log("Heal End");
		};
			
	}

	/// <summary>
	/// ゲージのスケール調整
	/// </summary>
	void ScaleChange(){
	
		if (this.StaminaBar.value >=  this.StaminaMaxValue * 70.0f / 100.0f) {
			image.sprite = NormalGauge;
		} else if (this.StaminaMaxValue * 40.0f / 100.0f <= this.StaminaBar.value && this.StaminaBar.value <  this.StaminaMaxValue * 70.0f / 100.0f) {
			image.sprite = Warn1Gauge;
		} else if (0.0f <= this.StaminaBar.value && this.StaminaBar.value <  this.StaminaMaxValue * this.InitValue / 40.0f) {
			image.sprite = Warn2Gauge;
		}
	
		if (_isStaminaHeal == true) {
			image.sprite = RecoveryGauge;
		}

	}

	/// <summary>
	/// ゲージをセットするメソッド
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetGauge(float value){

		float temp = Mathf.Clamp (value, 0.0f, StaminaMaxValue);

		this.StaminaBar.value = temp;

		this.ScaleChange ();

	}

	/// <summary>
	/// ゲージの値が変化した時に呼ばれるメソッド
	/// </summary>
	public void GaugeChange(float value){

		float newValue = this.StaminaBar.value + value;

		newValue = Mathf.Clamp (newValue, 0.0f, StaminaMaxValue);

		this.StaminaBar.value = newValue;

		this.ScaleChange ();

		//疲労回復中じゃない　かつ　ゲージが０になったら
		if (this._isStaminaHeal == false && this.StaminaBar.value <= 0.0f) {
			this.GaugeZero ();
		}

		//疲労回復中　かつ　ゲージがMAXになったら
		if (this._isStaminaHeal == true && this.StaminaBar.value >= 100.0f) {
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

		this.SetGauge (50.0f);
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
