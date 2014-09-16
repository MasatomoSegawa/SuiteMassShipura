using UnityEngine;
using System.Collections;

public class TestGamePlay : State
{

	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

		GUITimer.Instance.EndTimeEvent += EndTime;

	}

	public override void StateUpdate ()
	{

		if (Input.GetKeyDown (KeyCode.T)) {
			this.EndState ();
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
	void EndTime(){

		Debug.Log ("EndTime in Play");

	}

}
