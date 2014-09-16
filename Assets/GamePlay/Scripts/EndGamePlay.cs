using UnityEngine;
using System.Collections;

public class EndGamePlay : State
{
	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");

	}

	public override void StateUpdate ()
	{

	
	}

	public override void StateDestroy ()
	{
		Debug.Log (this.ToString () + " Destroy!");

	}
}
