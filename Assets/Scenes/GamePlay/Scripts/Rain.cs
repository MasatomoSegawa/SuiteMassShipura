using UnityEngine;
using System.Collections;

public class Rain : Weather {

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Rain;
		SoundManager.Instance.PlaySEWithSoundVolume ("rainDrop", 0.05f);
	}


	public override void WeatherOut ()
	{
		SoundManager.Instance.StopSE ("rainDrop");
	}

}
