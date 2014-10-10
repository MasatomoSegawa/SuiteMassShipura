using UnityEngine;
using System.Collections;

public class GUITimer : Singleton<GUITimer>
{
	public TextMesh myDurationTextMesh;
	private float _nowDuration;
	public float InitTime = 30.0f;
	public bool isStop = false;
	private bool _isEnd = false;

	public float NowDuration{ get { return _nowDuration; } }

	public bool isEnd{ get { return _isEnd; } }

	public delegate void EndTime ();

	public event EndTime EndTimeEvent;
	// Use this for initialization
	void Awake ()
	{
		Init ();
	}

	void Update ()
	{
		if (GameModel.isFreeze == false) {
			if (isStop == false && _isEnd == false) {
				this.Clock ();
			}

		}
	}

	/// <summary>
	/// 時を刻むメソッド
	/// </summary>
	void Clock ()
	{
		this._nowDuration -= Time.deltaTime;

		if (this._nowDuration >= 0.0f) {
			ReWriteText ();
		} else
			this.EndTimer ();

	}

	/// <summary>
	/// textMeshの文字列を書き換えるメソッド
	/// </summary>
	void ReWriteText ()
	{

		int m = (int)this._nowDuration / 60;
		int s = (int)this._nowDuration % 60;

		this.myDurationTextMesh.text = m.ToString("D2") + ":" + s.ToString("D2");


	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	public void Init ()
	{
		this._nowDuration = this.InitTime;
		this.myDurationTextMesh.richText = true;
		this.ReWriteText ();
		this.isStop = false;
		this._isEnd = false;
		this.EndTimeEvent += () => {
			Debug.Log ("End Time");
		};
	}

	/// <summary>
	/// timeが0になった時に呼ばれるメソッド
	/// </summary>
	void EndTimer ()
	{
		this._isEnd = true;
		this.isStop = true;
		this._nowDuration = 0.0f;
		ReWriteText ();
		EndTimeEvent ();
	}
}
