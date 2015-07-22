using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cloud : MonoBehaviour {

	enum CloudState{
		Move,Spark,Back,
	};

	public Transform FallPoint;

	CloudState myState = CloudState.Move;

	Vector3 destinationPoint;
	Vector3 firstPoint;

	float xVelocity = 0.0F;

	public GameObject Thunder;

	public float duration = 2.0f;

	Animator myMecanimAnimator;

	float nowTime = 0.0f;
	float nextTime = 0.0f;
	bool onceFlag = true;
	int myNumber = 0;
	string myName = "Thunder";

	public void Init(int myNumber){
	
		GameObject[] Turu = GameObject.FindGameObjectsWithTag ("Turu");

		//myNumber = Random.Range (0, Turu.Length);
		this.myNumber = myNumber;
		Vector3 pos = Turu [myNumber].transform.position;
		pos.y = this.transform.position.y;

		firstPoint = this.transform.position;
		destinationPoint = pos;

		this.nowTime = Time.time;
		this.nextTime = Time.time + duration;

		this.myMecanimAnimator = this.GetComponent<Animator> ();

		myName += Turu[myNumber].name[Turu[myNumber].name.Length - 1];
	}

	void FallThunder(){

		GameObject temp = Instantiate (Thunder) as GameObject;
		//Vector3 pos = this.transform.position;
		temp.transform.position = FallPoint.position;

		SoundManager.Instance.PlaySE ("ThunderDrop");

		temp.name = myName;

	}

	void Update(){

		switch (myState) {
		case CloudState.Move:
			Move ();
			break;

		case CloudState.Spark:
			Spark ();
			break;

		case CloudState.Back:
			Back ();
			break;
		}

	}

	void StartBack(){
		this.myState = CloudState.Back;

		Destroy (this.gameObject, duration + 1.0f);
	}

	void Spark(){

	}

	void Back(){
	
		Vector3 newpos = new Vector3 (0.0f, this.transform.position.y, this.transform.position.z);

		newpos.x = Mathf.SmoothDamp (this.transform.position.x, this.firstPoint.x, ref xVelocity, duration);
		//newpos.x = Mathf.Lerp (this.transform.position.x, this.destinationPoint.x, 0.01f);

		this.transform.position = newpos;

	}

	void Move(){

		Vector3 newpos = new Vector3 (0.0f, this.transform.position.y, this.transform.position.z);

		newpos.x = Mathf.SmoothDamp (this.transform.position.x, this.destinationPoint.x, ref xVelocity, duration);
		//newpos.x = Mathf.Lerp (this.transform.position.x, this.destinationPoint.x, 0.01f);

		this.transform.position = newpos;

		nowTime += Time.deltaTime;

		if (xVelocity <= 0.5f && nowTime >= nextTime && onceFlag == true) {
			myState = CloudState.Spark;
			this.myMecanimAnimator.SetTrigger ("doSpark");

			onceFlag = false;
		}

	}

}
