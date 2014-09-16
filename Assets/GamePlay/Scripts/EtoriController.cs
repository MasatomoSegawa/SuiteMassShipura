using UnityEngine;
using System.Collections;

public class EtoriController : Singleton<EtoriController> {

	//触手の座標
	public Transform[] TentaclePoints;

	//触手のプレハブ
	public GameObject Tentacle;

	//
	private GameObject[] Tentacles;

	//キーコード
	private KeyCode[] keycodes = { KeyCode.Z, KeyCode.X , KeyCode.C, 
								   KeyCode.Space, KeyCode.LeftArrow, 
						     	   KeyCode.DownArrow, KeyCode.RightArrow};

	private int _CurrentExistTentaclsNum = 0;

	public int CurrentExistTentaclsNum{
		get{ return _CurrentExistTentaclsNum; }
	}

	// Use this for initialization
	void Start () {

		Tentacles = new GameObject[keycodes.Length];

	}
	
	// Update is called once per frame
	void Update () {

		if (GameModel.isFreeze == false) {
			CheckKeyBoard ();
		}
	
	}

	/// <summary>
	/// キーボード検知するメソッド
	/// </summary>
	void CheckKeyBoard(){

		_CurrentExistTentaclsNum = 0;

		for (int i = 0; i < keycodes.Length; i++) {

			if (Input.GetKeyDown (keycodes [i]) == true){
				Tentacles [i] = Instantiate (Tentacle, TentaclePoints [i].position, Quaternion.identity)as GameObject;
			} 
			else if (Input.GetKeyUp (keycodes [i]) == true) {

				if (Tentacles [i] != null) {
					Destroy (Tentacles [i]);
				}

			}

			if (Tentacles [i] != null) {
				_CurrentExistTentaclsNum += 1;
			}


		}
	
	}

}