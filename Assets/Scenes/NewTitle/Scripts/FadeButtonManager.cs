using UnityEngine;
using System.Collections;

public class FadeButtonManager : MonoBehaviour {

	public GameObject[] ButtonManagers;

	public delegate void ChangeValue(int newxtIndex,int oldIndex);
	public event ChangeValue ChangeValueEvent;

	private int CurrentIndex;
	private int OldIndex;


	void Start(){
		Init ();

		ChangeValueEvent += (int newxtIndex, int oldIndex) => {
			Debug.Log("Change:" + newxtIndex + " to " + oldIndex);
		};

	}

	public void Init(){

		foreach (GameObject buttonManager in ButtonManagers) {
			buttonManager.GetComponent<ButtonManager> ().isFocus = false;
		}

		ButtonManagers [1].GetComponent<ButtonManager> ().InitFocusFalse ();
		ButtonManagers [0].GetComponent<ButtonManager> ().isFocus = true;

	}

	public void OldChange(){

		if (this.OldIndex != this.CurrentIndex && this.OldIndex < this.CurrentIndex) {
			ChangeControl (OldIndex);
		}

	}

	public void ChangeControl(int nextIndex){

		this.OldIndex = this.CurrentIndex;

		this.CurrentIndex = Mathf.Clamp (nextIndex, 0, ButtonManagers.Length);

		this.ChangeValueEvent (nextIndex, OldIndex);

		this.ButtonManagers [OldIndex].GetComponent<ButtonManager> ().isFocus = false;
		this.ButtonManagers [CurrentIndex].GetComponent<ButtonManager> ().isFocus = true;
	}

}
