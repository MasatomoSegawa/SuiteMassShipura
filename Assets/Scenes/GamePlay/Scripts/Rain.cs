using UnityEngine;
using System.Collections;

public class Rain : Weather {

	GameObject[] Turus;

	public FXRain RainModel;

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Rain;
		SoundManager.Instance.PlaySEWithSoundVolume ("rainDrop", 0.05f);

		Turus = GameObject.FindGameObjectsWithTag ("Turu");

		foreach (GameObject turu in Turus) {
			turu.GetComponent<TuruController> ().OpenSpeed = 2.0f;
		}

		EtoriController.Instance.AnimEnter_Rainy ();

		this.RainModel.EndFadeOutEvent += () => {
			this.EndFadeOut();
		};

	}

	public override void WeatherFadeOut ()
	{
		this.RainModel.doFadeOut ();
	}

	public override void WeatherOut ()
	{

		//if(SoundManager.Instance != null)
			//SoundManager.Instance.StopSE ("rainDrop");

		/*
		foreach (GameObject turu in Turus) {
			if(turu != null){
				turu.GetComponent<TuruController> ().OpenSpeed = 1.0f;
			}
		}
		*/
	}

}
