using UnityEngine;
using System.Collections;

public class CakeAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<SsSprite> ().PlayCount = 1;
		this.GetComponent<SsSprite> ().Play ();
		this.GetComponent<SsSprite> ().AnimationFinished = (SsSprite sprite) => {
			DestroyImmediate (this.gameObject);
		};

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
