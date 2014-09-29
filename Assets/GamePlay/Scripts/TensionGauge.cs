using UnityEngine;
using System.Collections;

public class TensionGauge : Singleton<TensionGauge>
{
	public Transform GaugePosition;
	[SerializeField]
	private float _CurrentValue = 1.0f;

	public delegate void _MaxGauge ();

	public event _MaxGauge MaxGaugeEvent;
	//ボーナスタイム開始時のイベント
	public delegate void BonusTimeStart ();

	public event BonusTimeStart BonusTimeStartEvent;
	//ボーナスタイム終了時のイベント
	public delegate void BonusTimeEnd ();

	public event BonusTimeEnd BonusTimeEndEvent;

	[Range (0, 100)]
	public int InitValue = 0;
	private bool isBonusTime = false;

	public bool _isBonusTime {
		get{ return isBonusTime; }
	}
	//ボーナスタイムの時間
	public float BonusTimeDuration = 5.0f;
	//ボーナスタイム時に少しずつ減るゲージの量
	private float BonusTimeGaugeDownNum = 1.0f / 100.0f;

	public float CurrentValue {
		get{ return _CurrentValue; }
	}

	float start = 0.0f;

	void Awake ()
	{

		Init ();

	}

	void Update ()
	{
	
		if (GameModel.isFreeze == false) {
			if (isBonusTime == true) {

				//ボーナスタイム中のゲージを減らす処理
				float t = 1.0f / (BonusTimeDuration * 60.0f);
				GaugeChange (-t);

			}
		}

	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	void Init ()
	{

		this.SetGauge (this.InitValue / 100.0f);

		this.GaugePosition.localScale = new Vector3 (_CurrentValue, 1.0f, 1.0f);

		this.MaxGaugeEvent += () => {
			//Debug.Log("MaxNow!");
		};
			
	}

	/// <summary>
	/// 触手に虫が当たった時に呼ばれるメソッド
	/// テンションゲージを下げる
	/// </summary>
	/// <param name="point">Point.</param>
	public void GetMusi (int point)
	{

		this.GaugeChange (-point / 100.0f);

	}

	/// <summary>
	/// ゲージがMAX時に呼ばれるイベント
	/// </summary>
	public void MaxGauge ()
	{

		this.SetGauge (0.0f);

		this.MaxGaugeEvent ();

	}

	/// <summary>
	/// ボーナスタイムスタート時に呼ばれるメソッド
	/// </summary>
	public void StartBonusTime ()
	{

		Debug.Log ("ボーナスタイムスタート!");

		this.isBonusTime = true;

		this.SetGauge (1.0f);
	
		this.BonusTimeGaugeDownNum = this.BonusTimeDuration / 100.0f;

		BonusTimeStartEvent ();
	}

	/// <summary>
	/// ボーナスタイムが終了時に呼ばれるメソッド
	/// </summary>
	void EndBonusTime ()
	{

		this.isBonusTime = false;

		this.BonusTimeEndEvent ();

	}

	/// <summary>
	/// ゲージをセットするメソッド
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetGauge (float value)
	{

		this._CurrentValue = Mathf.Clamp (value, 0.0f, 1.0f);

		Debug.Log ("Set:" + value + "current:" + this._CurrentValue);

		this.GaugePosition.localScale = new Vector3 (_CurrentValue, 1.0f, 1.0f);

		//ゲージが100％になったら
		if (this._CurrentValue >= 1.0f && isBonusTime == false) {
			MaxGauge ();
		}


	}

	/// <summary>
	/// ゲージの値が変化した時に呼ばれるメソッド
	/// </summary>
	public void GaugeChange (float value)
	{

		float newValue = _CurrentValue + value;

		this._CurrentValue = Mathf.Clamp (newValue, 0.0f, 1.0f);

		this.GaugePosition.localScale = new Vector3 (_CurrentValue, 1.0f, 1.0f);

		//ゲージが100％になったら
		if (this._CurrentValue >= 1.0f && isBonusTime == false) {
			MaxGauge ();
		}

		//ボーナスタイムのゲージが０%になったら
		if (this.CurrentValue <= 0.0f && isBonusTime == true) {
			EndBonusTime ();
		}

		

	}
}
