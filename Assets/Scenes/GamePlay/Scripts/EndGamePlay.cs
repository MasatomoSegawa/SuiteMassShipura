using UnityEngine;
using System.Collections;

public class EndGamePlay : State
{

	float RateTime = 1.0f;
	float NextTime = 0.0f;
	bool onlyFlag = true;

	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

		//ランキング集計
		int Score = GUIScore.Instance.CurrentScore;
		int Chain = ComboManager.Instance.MaximumChain;

		PlayerPrefs.SetInt ("Current_Score", Score);
		PlayerPrefs.SetInt ("Current_Chain", Chain);

		NextTime = Time.time + RateTime;

	}

	public override void StateUpdate ()
	{

		if (NextTime <= Time.time && onlyFlag) {
			onlyFlag = false;

			EtoriController.Instance.GetComponent<Animator> ().SetTrigger ("EndGame");

			SoundManager.Instance.PlayBGM ("Finish");
		}

		//FadeManager.Instance.LoadLevel ("Result", 1.0f);
	
	}

	public override void StateDestroy ()
	{
		Debug.Log (this.ToString () + " Destroy!");

	}
}
