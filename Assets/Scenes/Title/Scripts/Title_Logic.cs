using UnityEngine;
using System.Collections;

public class Title_Logic : State
{
	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

		FadeButtonManager FBM = GameObject.Find ("FadeButtonManager").GetComponent<FadeButtonManager> ();

		//ボタンのイベントを登録する
		GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>().onInputDownEvent += () => {
			FBM.ChangeControl(1);
			Debug.Log("Change Control");
		};
		GameObject.FindGameObjectWithTag("RankingButton").GetComponent<Button>().onInputDownEvent += () => {
			FadeManager.Instance.LoadLevel("Ranking",1.0f);
		};
		GameObject.FindGameObjectWithTag("KeyConfigButton").GetComponent<Button>().onInputDownEvent += () => {
			FadeManager.Instance.LoadLevel("KeyConfig",1.0f);
		};
		GameObject.FindGameObjectWithTag("EndButton").GetComponent<Button>().onInputDownEvent += () => {
			Application.Quit();
		};
		GameObject.FindGameObjectWithTag("EasyButton").GetComponent<Button>().onInputDownEvent += () => {
			FadeManager.Instance.LoadLevel("GamePlay",1.0f);
			PlayerPrefs.SetString("Current_Difficulty","Easy");
		};
		GameObject.FindGameObjectWithTag("NormalButton").GetComponent<Button>().onInputDownEvent += () => {
			FadeManager.Instance.LoadLevel("GamePlay",1.0f);
			PlayerPrefs.SetString("Current_Difficulty","Normal");
		};
		GameObject.FindGameObjectWithTag("DifficultButton").GetComponent<Button>().onInputDownEvent += () => {
			FadeManager.Instance.LoadLevel("GamePlay",1.0f);
			PlayerPrefs.SetString("Current_Difficulty","Hard");
		};
		GameObject.FindGameObjectWithTag("VeryHardButton").GetComponent<Button>().onInputDownEvent += () => {
			FadeManager.Instance.LoadLevel("GamePlay",1.0f);
			PlayerPrefs.SetString("Current_Difficulty","VeryHard");
		};


		//データの初期化
		PlayerPrefs.SetInt ("Current_Score", 0);
		PlayerPrefs.SetInt ("Current_Chain", 0);
		PlayerPrefs.SetString ("Current_Difficulty", "Easy");


		SoundManager.Instance.PlayBGM ("Title");
	}

	public override void StateUpdate ()
	{

		if (Input.GetKeyDown (KeyCode.A)) {
			this.EndState ();
		}
	
	}

	public override void StateDestroy ()
	{
		Debug.Log (this.ToString () + " Destroy!");
	}
}
