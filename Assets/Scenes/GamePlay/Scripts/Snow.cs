using UnityEngine;
using System.Collections;

public class Snow : Weather {

	GameObject[] Turus;

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Snow;
		Turus = GameObject.FindGameObjectsWithTag ("Turu");

		foreach (GameObject turu in Turus) {
			turu.GetComponent<TuruController> ().OpenSpeed = 0.5f;
		}

		EtoriController.Instance.AnimEnter_Snowy ();

		SoundManager.Instance.StopBGM ("Play");
		SoundManager.Instance.PlayBGM ("Snowy");

	}

	public override void WeatherFadeOut ()
	{
		Debug.Log ("Snow FadeOut");
		this.EndFadeOut ();
	}

	public override void WeatherOut ()
	{

		this.myWeather = WeatherState.Snow;
		Turus = GameObject.FindGameObjectsWithTag ("Turu");

		foreach (GameObject turu in Turus) {
			turu.GetComponent<TuruController> ().OpenSpeed = 1.0f;
		}

		if (SoundManager.Instance != null) {
			SoundManager.Instance.StopBGM ("Snowy");
			SoundManager.Instance.PlayBGM ("Play");
		}

	}
}
