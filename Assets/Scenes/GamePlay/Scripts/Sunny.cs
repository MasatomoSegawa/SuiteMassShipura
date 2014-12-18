using UnityEngine;
using System.Collections;

public class Sunny : Weather {

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Sunny;

		EtoriController.Instance.AnimEnter_Sunny (WeatherManager.Instance.OldWeatherState);

		//SoundManager.Instance.PlayBGM ("Snowy");

	}

	public override void WeatherFadeOut ()
	{
		Debug.Log ("Sunny FadeOut");
		this.EndFadeOut ();
	}


	public override void WeatherOut ()
	{

	}

}
