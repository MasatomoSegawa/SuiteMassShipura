using UnityEngine;
using System.Collections;

public class FXRain : MonoBehaviour {

	public delegate void EndFadeOut();
	public event EndFadeOut EndFadeOutEvent;

	public void doFadeOut(){
		this.GetComponent<Animator> ().SetTrigger ("doFadeOut");
	}

	public void End(){
		EndFadeOutEvent ();
	}

}
