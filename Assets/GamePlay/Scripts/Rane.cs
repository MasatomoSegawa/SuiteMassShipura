using UnityEngine;
using System.Collections;

public class Rane : MonoBehaviour {

	public Transform SpawnPoint;
			
	public void FallOkashi(){

		ScoreItemFactory.Instance.RandomInstantiate (SpawnPoint.position);

	}

}