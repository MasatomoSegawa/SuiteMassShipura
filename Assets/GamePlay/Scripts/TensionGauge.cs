using UnityEngine;
using System.Collections;

public class TensionGauge : Singleton<TensionGauge> {

	public Transform GaugePosition;

	[SerializeField]
	private float _CurrentValue = 1.0f;

	public delegate void _MaxGauge();
	public event _MaxGauge MaxGaugeEvent;

	[Range(0,100)]
	public int InitValue = 0;

	public float CurrentValue {
		get{ return _CurrentValue; }
	}

	void Awake(){

		Init ();

	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	void Init(){

		this.SetGauge (this.InitValue / 100.0f);

		this.GaugePosition.localScale = new Vector3 (_CurrentValue, 1.0f, 1.0f);

		this.MaxGaugeEvent += () => {
			Debug.Log("MaxNow!");
		};


	}

	/// <summary>
	/// 触手に虫が当たった時に呼ばれるメソッド
	/// テンションゲージを下げる
	/// </summary>
	/// <param name="point">Point.</param>
	public void GetMusi(int point){

		this.GaugeChange (- point / 100.0f);

	}

	/// <summary>
	/// ゲージがMAX時に呼ばれるイベント
	/// </summary>
	public void MaxGauge(){

		this.MaxGaugeEvent ();

	}

	/// <summary>
	/// ゲージをセットするメソッド
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetGauge(float value){

		this._CurrentValue = Mathf.Clamp (value, 0.0f, 1.0f);

		this.GaugePosition.localScale = new Vector3 (_CurrentValue, 1.0f, 1.0f);

		//ゲージが100％になったら
		if (this._CurrentValue >= 100.0f) {
			MaxGauge ();
		}

	}

	/// <summary>
	/// ゲージの値が変化した時に呼ばれるメソッド
	/// </summary>
	public void GaugeChange(float value){

		float newValue = _CurrentValue + value;

		this._CurrentValue = Mathf.Clamp (newValue, 0.0f, 1.0f);

		this.GaugePosition.localScale = new Vector3 (_CurrentValue, 1.0f, 1.0f);

		//ゲージが100％になったら
		if (this._CurrentValue >= 1.0f) {
			MaxGauge ();
		}

	}

}
