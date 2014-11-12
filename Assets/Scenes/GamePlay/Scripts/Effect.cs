using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	void EndAnimation(){

		if (this.transform.parent.gameObject != null) {
			DestroyImmediate (this.transform.parent.gameObject);
		} else {
			DestroyImmediate (this.gameObject);
		}

	}

}
