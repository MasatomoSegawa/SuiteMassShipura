using UnityEngine;
using System.Collections;


public enum TuruState{
	Idle,Eating,Close,OpenIdle,Open,
}

public class TuruController : MonoBehaviour {

	//SpriteStudio用のアニメーター
	/// 0 - Idle
	/// 1 - Eating
	/// 2 - Close
	/// 3 - IdleOpen
	/// 4 - Open
	public SsAnimation[] SSAnimations;
	SsSprite mySSAnimator;

	//Mecanim用のアニメーター
	Animator myMecanimAnimator;

	public GameObject ItemGetEffect;

	public TuruState myState;

	public Transform Hantei;

	// 開閉速度.
	public float OpenSpeed = 1.0f;

	void Start(){

		Init ();

	}

	void Update(){

		//口が開いてたなら
		if (myState == TuruState.Open || myState == TuruState.OpenIdle) {

			Collider2D collision = Physics2D.OverlapCircle (Hantei.position,0.5f);

			if (collision) {

				switch (collision.tag) {
				case "Musi":
					collision.gameObject.GetComponent<Musi> ().End ();
					this.myMecanimAnimator.SetTrigger ("StartEating");
					SoundManager.Instance.PlaySE ("mogumogu");
					break;

				case "Okashi":
					collision.gameObject.GetComponent<ScoreItem> ().End ();
					this.myMecanimAnimator.SetTrigger ("StartEating");
					SoundManager.Instance.PlaySE ("mogumogu");
					GameObject clone = Instantiate (ItemGetEffect) as GameObject;
					//clone.transform.parent = this.transform;
					Vector3 vec = this.transform.position;
					vec.y += 2.5f;
					clone.transform.position = vec;
					break;

				}

			}

		}


	}

	public void Init(){

		this.myMecanimAnimator = this.GetComponent<Animator> ();

		this.mySSAnimator = this.GetComponent<SsSprite> ();

		this.myState = TuruState.Idle;

		this.mySSAnimator.Animation = this.SSAnimations [0];
	}

	/// <summary>
	/// 触手を強制的に引っ込ませる
	/// </summary>
	public void DestroyTuru(){

		this.myMecanimAnimator.SetBool ("isOpen",false);
		this.myMecanimAnimator.SetTrigger ("DestoryTuru");

	}

	/// <summary>
	/// Openキーが押された時
	/// </summary>
	public void InputOpenKey(){
	
		this.myMecanimAnimator.SetBool ("isOpen",true);

	}

	/// <summary>
	/// Openキーが押されていない時
	/// </summary>
	public void NotOpenKey(){

		this.myMecanimAnimator.SetBool ("isOpen",false);

	}

	///SpriteStudioのアニメーターEvent///

	/// <summary>
	/// Idle状態に戻る時のイベント
	/// </summary>
	public void AnimEnter_Idle(){

		this.myState = TuruState.Idle;

		this.mySSAnimator.Animation = this.SSAnimations [0];
		this.mySSAnimator.PlayCount = 0;
		this.mySSAnimator.Play ();

	}

	/// <summary>
	/// 触手の口が開く時のイベント
	/// </summary>
	public void AnimEnter_Open(){

		this.myState = TuruState.Open;

		this.mySSAnimator.Animation = this.SSAnimations [4];
		this.mySSAnimator.Speed = this.OpenSpeed;
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.Play ();

		this.mySSAnimator.AnimationFinished = (SsSprite sprite) => {
			this.myMecanimAnimator.SetTrigger("EndOpenAnimation");
		};

	}

	/// <summary>
	/// OpenIdle状態スタート時のイベント
	/// </summary>
	public void AnimEnter_OpenIdle(){

		this.myState = TuruState.OpenIdle;

		this.mySSAnimator.Animation = this.SSAnimations [3];
		this.mySSAnimator.Speed = 1.0f;
		this.mySSAnimator.PlayCount = 0;
		this.mySSAnimator.Play ();

	}

	/// <summary>
	/// 触手の口が閉じる時のイベント
	/// </summary>
	public void AnimEnter_Close(){

		this.myState = TuruState.Close;

		this.mySSAnimator.Animation = this.SSAnimations [2];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.Play ();

		this.mySSAnimator.AnimationFinished = (SsSprite sprite) => {
			this.myMecanimAnimator.SetTrigger("EndCloseAnimation");
		};

	}

	/// <summary>
	/// お食事中のイベント
	/// </summary>
	public void AnimEnter_Eating(){
	
		this.myState = TuruState.Eating;

		this.mySSAnimator.Animation = this.SSAnimations [1];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.Speed = 1.0f;
		this.mySSAnimator.Play ();

		this.mySSAnimator.AnimationFinished = (SsSprite sprite) => {
			this.myMecanimAnimator.SetTrigger("EndEatingAnimation");
		};

	}
		
}