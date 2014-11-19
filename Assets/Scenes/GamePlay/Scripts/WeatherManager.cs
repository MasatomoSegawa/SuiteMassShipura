using UnityEngine;
using System.Collections;

public enum WeatherState{
	Sunny,Rain,Snow,
}

public class WeatherManager : Singleton<WeatherManager> {

	// 現在の天気.
	public GameObject CurrentWeather;
	// 現在の天気のスクリプト.
	public Weather CurrentWeatherScript;

	public WeatherState InitWeather;

	public Transform BackgroundPosition;

	public GameObject Sunny;
	public GameObject Rain;
	public GameObject Snow;

	void Start(){

		CurrentWeather = GetWeatherGameObject (InitWeather);
		CurrentWeather.transform.position = BackgroundPosition.position;
		CurrentWeatherScript = CurrentWeather.GetComponent<Weather> ();
		CurrentWeatherScript.WeatherIn ();

	}

	public GameObject GetWeatherGameObject(WeatherState weather){

		GameObject result = null;

		switch (weather) {
		case WeatherState.Sunny:
			result = Instantiate (Sunny) as GameObject;
			break;

		case WeatherState.Rain:
			result = Instantiate (Rain) as GameObject;
			break;

		case WeatherState.Snow:
			result = Instantiate (Snow) as GameObject;
			break;
		}

		return result;

	}

	/// <summary>
	/// 天候を変えるメソッド
	/// </summary>
	/// <param name="newWeather">New weather.</param>
	public void ChangeWeather(WeatherState newWeather){

		if (this.CurrentWeatherScript.myWeather == newWeather) {
			Debug.Log ("天気同じネ!");
			return;
		}

		GameObject Old = this.CurrentWeather;
	
		switch (newWeather) {
		case WeatherState.Sunny:
			CurrentWeather = Instantiate (Sunny) as GameObject;
			break;

		case WeatherState.Rain:
			CurrentWeather = Instantiate (Rain) as GameObject;
			break;

		case WeatherState.Snow:
			CurrentWeather = Instantiate (Snow) as GameObject;
			break;
		}

		CurrentWeather.transform.position = BackgroundPosition.position;
		CurrentWeatherScript = CurrentWeather.GetComponent<Weather> ();
		CurrentWeather.GetComponent<Animator> ().SetTrigger ("doFadeIn");
		CurrentWeatherScript.WeatherIn ();
		CurrentWeather.GetComponent<SpriteRenderer> ().material.color = Color.clear;

		Old.GetComponent<Animator> ().SetTrigger ("doFadeOut");
		Old.GetComponent<Weather> ().WeatherOut ();
	}

	/*
	時間経過で天候が入れ替わる.
	*/
	void Update(){

		if (Input.GetKeyDown (KeyCode.L)) {
			ChangeWeather (WeatherState.Snow);
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			ChangeWeather (WeatherState.Sunny);
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			ChangeWeather (WeatherState.Rain);
		}

	}


}