using UnityEngine;
using System.Collections;

public class TestInit : State
{
	private GameObject StartEffect;

	public override void StateStart ()
	{
	
		GameModel.isFreeze = true;

		this.StartEffect = this.ResourceManagerInstance.InstantiateResourceWithName ("StartEffect");

	}

	public override void StateUpdate ()
	{

		if (Input.GetKeyDown (KeyCode.A)) {
			this.EndState ();
		}
	
	}

	public override void StateDestroy ()
	{
		Debug.Log (this.ToString () + " Destroy!");
		GameModel.isFreeze = false;

		Destroy (this.StartEffect);
	}
}
