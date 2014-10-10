using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	//ボタン押された時のイベント
	public delegate void onInputDown();
	public event onInputDown onInputDownEvent;

	public Transform LeftPosition;
	public Transform RightPosition;

	/// <summary>
	/// ボタンが押された時に呼ばれるメソッド
	/// </summary>
	public void onEvent(){

		onInputDownEvent ();

	}

}
