using UnityEngine;
using System.Collections;

public class collisionTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject obj){
		Debug.Log ("name " + obj.name);
	}
}
