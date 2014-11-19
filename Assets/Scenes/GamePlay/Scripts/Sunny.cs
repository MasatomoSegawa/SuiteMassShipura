using UnityEngine;
using System.Collections;

public class Sunny : Weather {

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Sunny;
	}


	public override void WeatherOut ()
	{

	}

}
