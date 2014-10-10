using UnityEngine;
using System.Collections;

public class KeyConfigManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.anyKeyDown) {
			FadeManager.Instance.LoadLevel ("NewTitle", 1.0f);
		}

	}
}
