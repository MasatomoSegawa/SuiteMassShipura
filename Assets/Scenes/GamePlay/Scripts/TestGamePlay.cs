using UnityEngine;
using System.Collections;

public class TestGamePlay : State
{
	private LevelLoader levelLoader;

	private float NextTime = 0.0f;
	private float CoolTime = 0.0f;

	private WeatherState OldWeather;

	GameLevelInfo gameLevelInfo;

	public override void StateStart ()
	{
		//Debug.Log (this.ToString () + " Start!");

		///イベント登録
		// スタミナゲージが０になった時のイベント
		StaminaGauge.Instance.GaugeZeroEvent += EndGame;

		SoundManager.Instance.PlayBGM ("Play");

		this.levelLoader = GameObject.FindGameObjectWithTag ("LevelLoader").GetComponent<LevelLoader>();
	
		NextTime = CoolTime + Time.time;
	}

	public override void StateUpdate ()
	{
	
		if (Time.time >= NextTime) {
			gameLevelInfo = this.levelLoader.ReadLine ();

			if (gameLevelInfo != null) {
		
				LevelWeather ();
				LevelItem ();

				NextTime = gameLevelInfo.NextLoadTime + Time.time;
			}
		}

	
	}

	void LevelWeather(){

		if (OldWeather != gameLevelInfo.Weather) {
			OldWeather = this.gameLevelInfo.Weather;
			WeatherManager.Instance.ChangeWeather (gameLevelInfo.Weather);
		}

	}

	void LevelItem(){

		RaneManager.Instance.SetLevel (gameLevelInfo.OkashiFallDuration, gameLevelInfo.MusiFallDuration);
		ScoreItemFactory.Instance.SetLevel (gameLevelInfo.ItemSpeed, gameLevelInfo.MaxRandamizeSpeed,gameLevelInfo.MusiSpeed);

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
