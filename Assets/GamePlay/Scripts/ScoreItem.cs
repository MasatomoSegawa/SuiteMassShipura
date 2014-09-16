using UnityEngine;
using System.Collections;

public enum ItemType{
	Score,TensionUp,
};

public class ScoreItem : MonoBehaviour {

	[Range(0.0f,10.0f)]
	public float FallSpeed = 5.0f;

	public int point;

	public ItemType myType;

	public delegate void GetScoreItem(int point);
	public event GetScoreItem GetScoreItemEvent;

	public void Init(){

		this.GetScoreItemEvent += GUIScore.Instance.AddScore;

	}
	
	// Update is called once per frame
	void Update () {

		this.transform.Translate (-Vector3.up * FallSpeed * Time.deltaTime);

	}

	/// <summary>
	/// 触手にアイテムが当たった時に呼ばれるメソッド
	/// </summary>
	void End(){

		int newPoint = (int)(TensionGauge.Instance.CurrentValue * point);

		GetScoreItemEvent (newPoint);

		TensionGauge.Instance.GaugeChange (20.0f / 100.0f);

		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){

		switch (other.tag) {
		case "DeathLine":
			Destroy (this.gameObject);
			break;

		case "Tentacls":
			End ();
			break;
		}

	}

}
