using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	void EndAnimation(){

		if (this.transform.parent.gameObject != null) {
			Destroy (this.transform.parent.gameObject);
		} else {
			Destroy (this.gameObject);
		}

	}

}
