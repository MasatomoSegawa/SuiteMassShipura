
using UnityEngine;
using System.Collections;

public class StaminaGauge : Singleton<StaminaGauge> {

	public Transform GaugePosition;

	/// ゲージのスプライト群
	public SpriteRenderer spriteRenderer;
	public Sprite NormalGauge;
	public Sprite RecoveryGauge;
	public Sprite Warn1Gauge;
	public Sprite Warn2Gauge;

	// えとりちゃんのスタミナ最大値
	public float StaminaMaxValue = 100;

	private float CurrentStaminaValue = 1;
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

			//Debug.Log (CurrentStaminaValue);
		}

	}

	public void GetMusi(){

		GaugeChange (-DeclineMusiDamage);

	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	void Init(){

		this.GaugeMaxNum = this.GaugePosition.localScale.x;

		this.CurrentStaminaValue = this.StaminaMaxValue * this.InitValue / 100.0f;

		this.SetGauge (this.CurrentStaminaValue);

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

		float xScale = (this.CurrentStaminaValue / StaminaMaxValue) * (float)this.GaugeMaxNum;

		this.GaugePosition.localScale = new Vector3 (xScale, this.GaugePosition.localScale.y,this.GaugePosition.localScale.z);

		if (this.CurrentStaminaValue >=  this.StaminaMaxValue * 70.0f / 100.0f) {
			this.spriteRenderer.sprite = NormalGauge;
		} else if (this.StaminaMaxValue * 40.0f / 100.0f <= this.CurrentStaminaValue && this.CurrentStaminaValue <  this.StaminaMaxValue * 70.0f / 100.0f) {
			this.spriteRenderer.sprite = Warn1Gauge;
		} else if (0.0f <= this.CurrentStaminaValue && this.CurrentStaminaValue <  this.StaminaMaxValue * this.InitValue / 40.0f) {
			this.spriteRenderer.sprite = Warn2Gauge;
		}
	
		if (_isStaminaHeal == true) {
			this.spriteRenderer.sprite = RecoveryGauge;
		}

	}

	/// <summary>
	/// ゲージをセットするメソッド
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetGauge(float value){

		this.CurrentStaminaValue = Mathf.Clamp (value, 0.0f, StaminaMaxValue);

		this.ScaleChange ();

	}

	/// <summary>
	/// ゲージの値が変化した時に呼ばれるメソッド
	/// </summary>
	public void GaugeChange(float value){

		float newValue = CurrentStaminaValue + value;

		this.CurrentStaminaValue = Mathf.Clamp (newValue, 0.0f, StaminaMaxValue);

		this.ScaleChange ();

		//疲労回復中じゃない　かつ　ゲージが０になったら
		if (this._isStaminaHeal == false && this.CurrentStaminaValue <= 0.0f) {
			this.GaugeZero ();
		}

		//疲労回復中　かつ　ゲージがMAXになったら
		if (this._isStaminaHeal == true && this.CurrentStaminaValue >= 100.0f) {
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
