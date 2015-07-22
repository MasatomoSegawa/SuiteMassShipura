using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackGroundWeather : MonoBehaviour {

	Image myImage;

	WeatherState next;

	public Sprite Sunny;
	public Sprite Rainy;
	public Sprite Snowy;
	public Sprite Thunder;
	public Sprite Stormy;

	public delegate void EndFadeOut();
	public event EndFadeOut EndFadeOutEvent;

	void Start(){
		myImage = this.GetComponent<Image> ();
		ChangeBackgroundWeather (WeatherManager.Instance.InitWeather);
	}

	void AnimEnd_FadeOut(){
		ChangeBackgroundWeather (this.next);
	}

	public void SetWeather(WeatherState state){
		this.next = state;
		this.GetComponent<Animator>().SetTrigger("doFadeOut");
	}

	void ChangeBackgroundWeather(WeatherState weather){

		Debug.Log ("ChangeBackground:" + weather);

		switch (weather) {
		case WeatherState.Sunny:
			this.myImage.sprite = Sunny;
			break;

		case WeatherState.Rain:
			this.myImage.sprite = Rainy;
			break;

		case WeatherState.Storm:
			this.myImage.sprite = Stormy;
			break;

		case WeatherState.Thunder:
			this.myImage.sprite = Thunder;
			break;

		case WeatherState.Snow:
			this.myImage.sprite = Snowy;
			break;
		}

	}

}
