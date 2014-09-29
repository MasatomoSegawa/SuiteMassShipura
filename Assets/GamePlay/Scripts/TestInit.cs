using UnityEngine;
using System.Collections;

public class TestInit : State
{

	private GameObject StartEffect;

	public override void StateStart ()
	{
		Debug.Log (this.ToString () + " Start!");
		GameModel.isFreeze = true;

		this.StartEffect = this.ResourceManagerInstance.InstantiateResourceWithName ("StartEffect");

		//スタミナゲージ
		StaminaGauge staminaGauge = GameObject.FindGameObjectWithTag ("StaminaGauge").GetComponent<StaminaGauge> ();
		//えとりちゃん
		EtoriController etori = GameObject.FindGameObjectWithTag ("Player").GetComponent<EtoriController> ();

		///イベント登録
		//スタミナゲージが０になった時
		staminaGauge.GaugeZeroEvent += () => {
			//触手１本しか出せなくする
			etori.AllDeleateTentacls();
			etori.ConstraintTentacls(1);
		};

		//スタミナゲージが回復した時
		staminaGauge.HealEndEvent += () => {
			//触手を全部扱えるようにする
			etori.ConstraintTentacls(6);
		};

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
