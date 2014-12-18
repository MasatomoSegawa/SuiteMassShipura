using UnityEngine;
using System.Collections;

public class TestGamePlay : State
{
	private LevelLoader levelLoader;

	private float NextTime = 0.0f;
	private float CoolTime = 0.0f;

	private WeatherState NextWeather;

	public override void StateStart ()
	{
		//Debug.Log (this.ToString () + " Start!");

		///イベント登録
		// スタミナゲージが０になった時のイベント
		StaminaGauge.Instance.GaugeZeroEvent += EndGame;

		SoundManager.Instance.PlayBGM ("Play");

		this.levelLoader = GameObject.FindGameObjectWithTag ("LevelLoader").GetComponent<LevelLoader>();
	
		NextWeather = WeatherManager.Instance.InitWeather;
		NextTime = CoolTime + Time.time;
	}

	public override void StateUpdate ()
	{
	
		if (Time.time >= NextTime) {
			this.levelLoader.ReadLine ();

			CoolTime = this.levelLoader.NextLoadTime;

			if (NextWeather != this.levelLoader.Weather) {
				NextWeather = this.levelLoader.Weather;
				WeatherManager.Instance.ChangeWeather (this.NextWeather);
			}

			NextTime = CoolTime + Time.time;
		}

	
	}

	public override void StateDestroy ()
	{

		Debug.Log (this.ToString () + " Destroy!");

		GameModel.isFreeze = true;


	}

	/// <summary>
	/// 時間が０秒になった時に呼ばれるメソッド
	/// </summary>
	void EndGame(){

		Debug.Log ("EndTime in Play");

		this.EndState ();

		SoundManager.Instance.StopBGM ("Play");

	}

}
