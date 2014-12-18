using UnityEngine;
using System.Collections;

public class Weather : MonoBehaviour {

	public WeatherState myWeather;
	protected float WeatherDuration = 30.0f;
	private float CurrentTime = 0.0f; 
	private float NextTime = 0.0f;

	public delegate void FadeOut();
	public event FadeOut FadeOutEvent;

	// Use this for initialization
	void Start () {
		WeatherIn ();

		FadeOutEvent += () => {
			Debug.Log("Weather" + myWeather + "FadeOutStart!");
		};
	}
	
	// Update is called once per frame
	void Update () {

		WeatherUpdate ();

	}

	void OnDestroy(){
		WeatherOut ();
	}
		
	// 天気コンストラクタ
	public virtual void WeatherIn(){

	}

	// 天気デストラクタ
	public virtual void WeatherOut(){

	}

	public virtual void WeatherFadeOut(){
	}

	public virtual void WeatherUpdate(){

	}

	public void EndFadeOut(){
		FadeOutEvent ();
	}
}