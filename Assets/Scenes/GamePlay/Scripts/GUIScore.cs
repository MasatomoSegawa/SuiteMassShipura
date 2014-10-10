﻿using UnityEngine;
using System.Collections;

public class GUIScore : Singleton<GUIScore> {

	public TextMesh myScoreText;

	private int currentScore = 0;

	public int CurrentScore{
		get{ return currentScore; }
	}

	// Use this for initialization
	void Start () {
	
		Init ();

	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	void Init(){

		this.RewriteText ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RewriteText(){

		int k = this.currentScore / 1000;
		int s = this.currentScore % 1000;

		this.myScoreText.text = k.ToString() + "," + s.ToString("D3");

	}

	/// <summary>
	/// スコアポイント加算時に呼ばれるメソッド
	/// </summary>
	/// <param name="point">Point.</param>
	public void AddScore(int point){
	
		this.currentScore += point;

		this.RewriteText ();
	}

}