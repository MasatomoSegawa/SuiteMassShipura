using UnityEngine;
using System.Collections;

public class TestGamePlay : State
{

	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

		///イベント登録
		// スタミナゲージが０になった時のイベント
		StaminaGauge.Instance.GaugeZeroEvent += EndGame;


		SoundManager.Instance.PlayBGM ("Play");
	}

	public override void StateUpdate ()
	{


	
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
