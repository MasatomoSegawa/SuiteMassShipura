using UnityEngine;
using System.Collections;

public class BonusTimeCutInEffect : MonoBehaviour {

	void Start(){

		GameModel.isFreeze = true;

	}

	void End(){
		GameModel.isFreeze = false;
		Destroy (this.gameObject);
	}

}
