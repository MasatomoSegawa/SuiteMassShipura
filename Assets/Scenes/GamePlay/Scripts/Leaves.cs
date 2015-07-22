using UnityEngine;
using System.Collections;

public class Leaves : MonoBehaviour {

	public delegate void EndFadeOut();
	public event EndFadeOut EndFadeOutEvent;

	public GameObject[] leaves;

	public void doFadeOut(){

		this.GetComponent<Animator> ().SetTrigger ("doFadeOut");

		foreach (GameObject leaf in leaves) {
			leaf.GetComponent<Animator> ().SetTrigger ("doFadeOut");
		}

	}

	void End(){
		Debug.Log ("Hi!");
		EndFadeOutEvent ();
	}

}
