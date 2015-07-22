using UnityEngine;
using System.Collections;

public class Storm : Weather {

	public Leaves leaves;

	public override void WeatherIn ()
	{
		this.myWeather = WeatherState.Storm;

		EtoriController.Instance.AnimEnter_Stormy ();

		this.leaves.EndFadeOutEvent += () => {
			this.EndFadeOut();
		};

	}

	public override void WeatherFadeOut ()
	{
		this.leaves.doFadeOut ();
	}


	public override void WeatherOut ()
	{

	}
}
