using UnityEngine;
using System.Collections;

public class TestInit : State
{
	private GameObject StartEffect;
	private LevelLoader levelLoader;
	private GameLevelInfo gameLevelInfo;

	public override void StateStart ()
	{
	
		this.gameLevelInfo = new GameLevelInfo ();
		this.levelLoader = GameObject.FindGameObjectWithTag ("LevelLoader").GetComponent<LevelLoader> ();

		GameModel.isFreeze = true;

		this.StartEffect = this.ResourceManagerInstance.InstantiateResourceWithName ("StartEffect");
		Transform Scale = this.StartEffect.transform;

		GameObject canvas = GameObject.Find ("GUICanvas");
		this.StartEffect.transform.SetParent (canvas.transform, false);

		gameLevelInfo = this.levelLoader.ReadLine ();
		LevelWeather ();
		LevelItem ();

	}

	public override void StateUpdate ()
	{

		if (Input.GetKeyDown (KeyCode.A)) {
			this.EndState ();
		}

			
	}

	void LevelWeather ()
	{
		WeatherManager.Instance.ChangeWeather (gameLevelInfo.Weather);
	}

	void LevelItem ()
	{

		RaneManager.Instance.SetLevel (gameLevelInfo.OkashiFallDuration,gameLevelInfo.MusiFallDuration);
		ScoreItemFactory.Instance.SetLevel (gameLevelInfo.ItemSpeed, gameLevelInfo.MaxRandamizeSpeed, gameLevelInfo.MusiSpeed);
	
	}

	public override void StateDestroy ()
	{
		//Debug.Log (this.ToString () + " Destroy!");
		GameModel.isFreeze = false;

		Destroy (this.StartEffect);
	}
}
