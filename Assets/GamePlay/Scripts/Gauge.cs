using UnityEngine;
using System.Collections;

public class Gauge : MonoBehaviour {

	public Transform GaugePosition;

	private float CurrentValue = 1.0f;

	void Start(){

		Init ();

	}

	void Update(){

		if (Input.GetKeyDown(KeyCode.A)) {
			this.GaugeChange (-1 / 100.0f);
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			this.GaugeChange (1 / 100.0f);
		}

	}

	/// <summary>
	/// 初期化メソッド
	/// </summary>
	void Init(){

		GaugePosition.localScale = new Vector3 (CurrentValue, 1.0f, 1.0f);

	}

	/// <summary>
	/// ゲージの値が変化した時に呼ばれるメソッド
	/// </summary>
	public void GaugeChange(float value){

		float newValue = CurrentValue + value;

		this.CurrentValue = Mathf.Clamp (newValue, 0.0f, 1.0f);

		GaugePosition.localScale = new Vector3 (CurrentValue, 1.0f, 1.0f);

	}


}