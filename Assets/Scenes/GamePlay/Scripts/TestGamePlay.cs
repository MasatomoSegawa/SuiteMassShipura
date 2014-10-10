using UnityEngine;
using System.Collections;

public class TestGamePlay : State
{

	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

		GUITimer.Instance.EndTimeEvent += EndTime;

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
	void EndTime(){

		Debug.Log ("EndTime in Play");

		this.EndState ();

		SoundManager.Instance.StopBGM ("Play");

	}

}
