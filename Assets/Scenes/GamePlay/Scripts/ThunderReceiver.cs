using UnityEngine;
using System.Collections;

public class ThunderReceiver : MonoBehaviour {

	public GameObject[] Turus;

	void OnParticleCollision (GameObject obj)
	{
	
		int number = int.Parse(obj.transform.parent.name [obj.transform.parent.name.Length - 1].ToString());
		Turus [number-1].GetComponent<TuruController> ().OnHitThunder ();
	
	}
}
