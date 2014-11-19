using UnityEngine;
using System.Collections;

public class Snow : Weather {

	GameObject[] Turus;

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Snow;
		Turus = GameObject.FindGameObjectsWithTag ("Turu");

		foreach (GameObject turu in Turus) {
			turu.GetComponent<TuruController> ().OpenSpeed = 0.25f;
		}

	}


	public override void WeatherOut ()
	{

		foreach (GameObject turu in Turus) {
			turu.GetComponent<TuruController> ().OpenSpeed = 1.0f;
		}

	}
}
