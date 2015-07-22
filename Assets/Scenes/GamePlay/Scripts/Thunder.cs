using UnityEngine;
using System.Collections;

public class Thunder : Weather {

	public Transform CloudSpawnPoint;
	public GameObject Cloud;

	private ThunderLevelLoader myLevelLoader;
	private float NextLoadTime = 0.0f;
	private float CoolTime = 0.0f;
	private bool[] CurrentThunderFallPoint = new bool[6];

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Thunder;

		EtoriController.Instance.AnimEnter_Thunder();

		SoundManager.Instance.PlaySE ("Thunder");

		myLevelLoader = this.GetComponent<ThunderLevelLoader> ();


	}

	Cloud CreateCloud(){
		GameObject cloud = Instantiate (Cloud, CloudSpawnPoint.position, Quaternion.identity)as GameObject;
		return cloud.GetComponent<Cloud> ();
	}

	public override void WeatherUpdate ()
	{

		if (Time.time >= NextLoadTime) {
			this.myLevelLoader.LoadLine ();

			CoolTime = this.myLevelLoader.NextLoadTime;
			CurrentThunderFallPoint = this.myLevelLoader.CurrentThunderFallPoint;

			for (int i = 0; i < CurrentThunderFallPoint.Length; i++) {
				if (CurrentThunderFallPoint [i] == true) {
					CreateCloud ().Init (i);
				}
			}

			NextLoadTime = CoolTime + Time.time;
		}

	}

	public override void WeatherFadeOut ()
	{
		Debug.Log ("Thunder FadeOut");
		this.EndFadeOut ();
	}

	public override void WeatherOut ()
	{

		if(SoundManager.Instance != null)
			SoundManager.Instance.StopSE ("Thunder");

	}

}
