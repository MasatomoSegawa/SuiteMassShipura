using UnityEngine;
using System.Collections;

public class OkashiSelector : MonoBehaviour {

	public GameObject Okashi;
	private GameObject LeftSelector;
	private GameObject RightSelector;

	public void SetSelector(Vector3 Left,Vector3 Right){

		if (LeftSelector == null) {
			LeftSelector = Instantiate (Okashi, Left, Quaternion.identity) as GameObject;
		} else {
			LeftSelector.transform.position = Left;
		}

		if (RightSelector == null) {
			RightSelector = Instantiate (Okashi, Right, Quaternion.identity) as GameObject;
		} else {
			RightSelector.transform.position = Right;
		}
	}

}
