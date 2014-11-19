using UnityEngine;
using System.Collections;

public class Snow : Weather {

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Snow;
	}


	public override void WeatherOut ()
	{

	}
}
