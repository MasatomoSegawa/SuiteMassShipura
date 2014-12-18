using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum WeatherState
{
	Sunny,
Rain,
Snow,
Storm,
Thunder,
	None,
}

public class WeatherManager : Singleton<WeatherManager>
{
	// 現在の天気.
	public GameObject CurrentWeather;
	public BackGroundWeather BackgroundWeatherScript;
	public Transform pos;
	// 現在の天気のスクリプト.
	[HideInInspector]
	public Weather CurrentWeatherScript;
	[HideInInspector]
	public WeatherState CurrentWeatherState;
	[HideInInspector]
	public WeatherState OldWeatherState;
	[HideInInspector]
	public WeatherState NextWeather;
	public WeatherState InitWeather;
	public GameObject Sunny;
	public GameObject Rain;
	public GameObject Snow;
	public GameObject Storm;
	public GameObject Thunder;
	[HideInInspector]
	public bool firstFlag = true;
	[HideInInspector]
	public Animator anim;

	void Start ()
	{

		this.anim = this.GetComponent<Animator> ();

		CurrentWeather = CreateWeatherObject (InitWeather);
		this.CurrentWeatherState = InitWeather;
		CurrentWeather.transform.position = new Vector3 (pos.position.x, pos.position.y, 10.0f);
		CurrentWeatherScript = CurrentWeather.GetComponent<Weather> ();
		CurrentWeatherScript.FadeOutEvent += CreateNewWeather;
	
	}

	/// <summary>
	/// 天候を変えるメソッド
	/// </summary>
	/// <param name="newWeather">New weather.</param>
	public void ChangeWeather (WeatherState newWeather)
	{
	
		switch (newWeather) {
		case WeatherState.Sunny:
			this.anim.SetTrigger ("doSunny");
			break;

		case WeatherState.Rain:
			this.anim.SetTrigger ("doRainy");
			break;

		case WeatherState.Snow:
			this.anim.SetTrigger ("doSnowy");
			break;

		case WeatherState.Thunder:
			this.anim.SetTrigger ("doThunder");
			break;

		case WeatherState.Storm:
			this.anim.SetTrigger ("doStormy");
			break;
		}

	}

	void AnimEnter_Weather(WeatherState state){

		if (firstFlag == false) {
			ChangeRequest (state);
		} else {
			firstFlag = false;
		}

	}

	void ChangeRequest (WeatherState state)
	{

		Debug.Log ("ChangeRequest: " + state);

		this.NextWeather = state;
		this.CurrentWeatherScript.WeatherFadeOut ();
		this.BackgroundWeatherScript.SetWeather (state);

	}

	void CreateNewWeather ()
	{

		Debug.Log ("Hi!" + this.NextWeather);

		GameObject Old = this.CurrentWeather;

		CurrentWeather = CreateWeatherObject (this.NextWeather);
		OldWeatherState = this.CurrentWeatherState;
		this.CurrentWeatherState = NextWeather;
		CurrentWeather.transform.position = new Vector3 (pos.position.x, pos.position.y, 10.0f);
		CurrentWeatherScript = CurrentWeather.GetComponent<Weather> ();
		CurrentWeatherScript.FadeOutEvent += CreateNewWeather;

		Destroy (Old);

		//BackgroundWeather.GetComponent<Animator> ().SetTrigger ("doFadeOut");

	}

	GameObject CreateWeatherObject (WeatherState state)
	{

		GameObject temp = null;

		switch (state) {
		case WeatherState.Sunny:
			temp = Instantiate (Sunny) as GameObject;
			break;

		case WeatherState.Rain:
			temp = Instantiate (Rain) as GameObject;
			break;

		case WeatherState.Snow:
			temp = Instantiate (Snow) as GameObject;
			break;

		case WeatherState.Thunder:
			temp = Instantiate (Thunder) as GameObject;			
			break;

		case WeatherState.Storm:
			temp = Instantiate (Storm) as GameObject;
			break;
		}

		return temp;

	}
}