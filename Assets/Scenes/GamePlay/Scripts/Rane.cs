using UnityEngine;
using System.Collections;

public class Rane : MonoBehaviour {

	public Transform SpawnPoint;
			
	public void FallOkashi(){

		ScoreItemFactory.Instance.OkashiInstantiate (SpawnPoint.position);

	}

	public void FallMusi(){
	
		ScoreItemFactory.Instance.MusiInstantiate (SpawnPoint.position);

	}
		
}