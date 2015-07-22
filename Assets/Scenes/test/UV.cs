using UnityEngine;
using System.Collections;

public class UV : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (- Time.time * 0.25f,0.0f);
	}
}
