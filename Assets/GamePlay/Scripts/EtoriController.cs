using UnityEngine;
using System.Collections;

public class EtoriController : Singleton<EtoriController> {

	public GameObject hantei;

	private Animator anim;

	//触手の座標
	public Transform[] TentaclePoints;

	//触手のプレハブ
	public GameObject Tentacle;

	//触手's
	private GameObject[] Tentacles;

	//キーコード
	private KeyCode[] keycodes = { KeyCode.Z, KeyCode.X , KeyCode.C, 
								   KeyCode.LeftArrow,  KeyCode.DownArrow, KeyCode.RightArrow
								};

	private int ConstraintTentaclsNum = 6;

	private int _CurrentExistTentaclsNum = 0;

	public int CurrentExistTentaclsNum{
		get{ return _CurrentExistTentaclsNum; }
	}

	// Use this for initialization
	void Start () {

		this.Tentacles = new GameObject[keycodes.Length];

		this.anim = this.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (GameModel.isFreeze == false) {
			CheckKeyBoard ();
		}
	
	}

	/// <summary>
	/// 触手の出せる本数を制限するメソッド
	/// </summary>
	/// <param name="num">Number.</param>
	public void ConstraintTentacls(int num){

		this.ConstraintTentaclsNum = Mathf.Clamp (num, 0, 6);

	}

	/// <summary>
	/// 触手全削除
	/// </summary>
	public void AllDeleateTentacls(){

		foreach(GameObject tentacl in Tentacles){

			if (tentacl != null) {
				Destroy (tentacl);
			}

		}

	}

	/// <summary>
	/// キーボード検知するメソッド
	/// </summary>
	void CheckKeyBoard(){

		_CurrentExistTentaclsNum = 0;
		for (int i = 0; i < keycodes.Length; i++) {
			if (Tentacles [i] != null) {
				_CurrentExistTentaclsNum += 1;
			}
		}
	
		for (int i = 0; i < keycodes.Length; i++) {
		
			//キーボード押した時
			if (Input.GetKeyDown (keycodes [i]) == true && Tentacles [i] == null && _CurrentExistTentaclsNum < ConstraintTentaclsNum) {
				Tentacles [i] = Instantiate (Tentacle, TentaclePoints [i].position, Quaternion.identity)as GameObject;
			} 

			//キーボードから離れた時
			if (Input.GetKeyUp (keycodes [i]) == true) {

				if (Tentacles [i] != null) {
					Destroy (Tentacles [i]);
				}
			}

		}

		//ケーキキャッチ
		//今は判定のみ出す
		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetTrigger ("catchCake");
		}

	}

	////////アニメーターイベント////////


	void Anim_CatchCake(){
		GameObject clone = Instantiate (hantei) as GameObject;
		Destroy (clone, 0.5f);

		Debug.Log ("Catch");
	}

}