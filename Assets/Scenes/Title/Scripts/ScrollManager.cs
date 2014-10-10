using UnityEngine;
using System.Collections;

public class ScrollManager : MonoBehaviour {

	public delegate void ChangeValue(int newxtIndex,int oldIndex);
	public event ChangeValue ChangeValueEvent;

	public GameObject[] Tabs;

	private int CurrentTabIndex = 0;
	private int oldIndex = 0;

	public Camera TargetCamera;
	private Vector3 MidPoint;
	private float width;

	private bool isAnimationNow = false;
	public bool AnimationFlag = true;
	public float damping = 2.0f;

	private Vector3 TargetPosition;

	void Start(){

		Init ();
			
	}

	void Init(){

		Vector3 leftTop = TargetCamera.ViewportToWorldPoint (new Vector3 (1, 0, 0));
		Vector3 rightTop = TargetCamera.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		width = Vector3.Distance (leftTop, rightTop); 

		for (int i = 0; i < Tabs.Length; i++) {

			Tabs [i].transform.position = new Vector3(transform.position.x + width * i, transform.position.y, 0.0f);
			Tabs [i].GetComponent<ButtonManager> ().enabled = false;

		}

		Tabs [0].GetComponent<ButtonManager> ().enabled = true;
		Tabs [0].GetComponent<ButtonManager> ().init ();

		this.CameraPositionCange ();
	}

	void Update(){

		if (isAnimationNow == true) {

			float tmpZ = TargetCamera.transform.position.z;
			Vector3 tmp = TargetCamera.transform.position;
			tmp = Vector3.Lerp (TargetCamera.transform.position, TargetPosition, Time.deltaTime * damping);
			tmp.z = tmpZ;
			TargetCamera.transform.position = tmp;
		}

	}

	void StartAnimation(){

		this.isAnimationNow = true;
		this.TargetPosition = Tabs [CurrentTabIndex].transform.position;

	}

	void EndAnimation(){

	}

	/// <summary>
	/// カメラのポジションを変えるメソッド
	/// </summary>
	void CameraPositionCange(){

		Vector3 tmp = Tabs [CurrentTabIndex].transform.position;
		tmp.z = TargetCamera.transform.position.z;
		this.TargetCamera.transform.position = tmp;

	}

	/// <summary>
	/// 前のコントロールに戻る
	/// </summary>
	public void OldBack(){

		if (this.oldIndex != this.CurrentTabIndex && this.oldIndex < this.CurrentTabIndex) {
			ChangeControl (this.oldIndex);
		}

	}

	/// <summary>
	/// タブの値が変わった時に呼ばれるメソッド
	/// </summary>
	/// <param name="nextIndex">Next index.</param>
	public void ChangeControl(int nextIndex){

		this.oldIndex = this.CurrentTabIndex;

		this.CurrentTabIndex = Mathf.Clamp (nextIndex, 0, Tabs.Length);

		//イベント発生
		this.ChangeValueEvent (nextIndex,oldIndex);

		//移動にアニメーションを用いるかどうか
		if (AnimationFlag == false) {
			this.CameraPositionCange ();
		} else {
			this.StartAnimation ();
		}
	}

}
