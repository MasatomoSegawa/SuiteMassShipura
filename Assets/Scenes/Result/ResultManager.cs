using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultManager : MonoBehaviour {

	public Text ScoreText;

	// Use this for initialization
	void Start () {

		if(ScoreManager.Instance != null)
			ScoreText.text = ScoreManager.Instance.Score.ToString();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.anyKeyDown) {
			FadeManager.Instance.LoadLevel ("NewTitle", 1.0f);
		}

	}
}
