using UnityEngine;
using System.Collections;

public enum EtoriState
{
	Idle,
	GetHae,
	CatchCakeIn,
	CatchCakeIdle,
	CatchCakeOut,
	CakeEat,
	Bonus,
	StaminaZero,
}

public class EtoriController : Singleton<EtoriController>
{
	//えとりステート
	public EtoriState myState;
	//触手達
	public TuruController[] Turus;
	//判定
	public Transform Hantei;
	//メカニムのステート管理用アニメーター
	private Animator myMecanimAnimator;
	//SpriteStudio用のアニメーター
	SsSprite mySSAnimator;

	//Sunny用のアニメーション
	/// 0 - Idle
	/// 1 - getHae
	/// 2 - bonus
	/// 3 - CatchCakeIn
	/// 4 - CatchCakeOut
	/// 5 - EatCake
	/// 6 - CatchCakeIdle
	/// 7 - StaminaZero
	/// 8 - Clear
	public SsAnimation[] SunnySSAnimations;

	//Rainy用のアニメーション
	/// 0 - sunny to rainy
	/// 1 - Idle
	public SsAnimation[] RainySSAnimations;

	//Snow用のアニメーション
	/// 0 - sunny to rainy
	/// 1 - Idle
	public SsAnimation[] SnowySSAnimations;

	//Storm用のアニメーション
	/// 0 - sunny to rainy
	/// 1 - Idle
	public SsAnimation[] StormSSAnimations;

	//Thunder用のアニメーション
	/// 0 - sunny to rainy
	/// 1 - Idle
	public SsAnimation[] ThunderSSAnimations;

	//キーコード
	private KeyCode[] keycodes = { KeyCode.Z, KeyCode.X, KeyCode.C, 
		KeyCode.LeftArrow,  KeyCode.DownArrow, KeyCode.RightArrow
	};
	//現在の出せる触手の最大数
	private int ConstraintTentaclsNum = 6;
	//現在出している触手の数
	private int _CurrentExistTentaclsNum = 0;

	public int CurrentExistTentaclsNum {
		get{ return _CurrentExistTentaclsNum; }
	}
	// Use this for initialization
	void Start ()
	{

		this.myMecanimAnimator = this.GetComponent<Animator> ();

		this.myState = EtoriState.Idle;

		//SpriteStudio
		this.mySSAnimator = this.GetComponent<SsSprite> ();
		this.mySSAnimator.Animation = this.SunnySSAnimations [0];

	}
	// Update is called once per frame
	void Update ()
	{
	
		if (GameModel.isFreeze == false) {
			CheckKeyBoard ();
			CatchCake ();

			if (myState == EtoriState.Idle) {
				this.myMecanimAnimator.SetBool ("isIdle", true);
			} else {
				this.myMecanimAnimator.SetBool ("isIdle", false);
			}

		}
	
	}

	void CatchCake ()
	{

		if (myState == EtoriState.CatchCakeIn || myState == EtoriState.CatchCakeIdle) {

			Collider2D collision = Physics2D.OverlapCircle (Hantei.position, 0.5f);

			if (collision) {

				switch (collision.tag) {
				case "BonusItem":
					collision.gameObject.GetComponent<BonusItem> ().BonusTimeStart ();
					break;
				}

			}

		}

	}

	/// <summary>
	/// 触手の出せる本数を制限するメソッド
	/// </summary>
	/// <param name="num">Number.</param>
	public void ConstraintTentacls (int num)
	{

		this.ConstraintTentaclsNum = Mathf.Clamp (num, 0, 6);

	}

	/// <summary>
	/// 触手全削除
	/// </summary>
	public void AllDeleateTentacls ()
	{

		foreach (TuruController turu in Turus) {

			turu.DestroyTuru ();

		}

	}

	/// <summary>
	/// キーボード検知するメソッド
	/// </summary>
	void CheckKeyBoard ()
	{

		bool isInputSpace;
		//ケーキキャッチ
		//今は判定のみ出す
		if (Input.GetKey (KeyCode.Space)) {
			isInputSpace = true;
		} else {
			isInputSpace = false;
		}

		this.myMecanimAnimator.SetBool ("InputSpace", isInputSpace);

		_CurrentExistTentaclsNum = 0;
		foreach (TuruController turu in Turus) {

			if (turu.myState == TuruState.Open || turu.myState == TuruState.OpenIdle || turu.myState == TuruState.Eating) {
				_CurrentExistTentaclsNum += 1;
			}

		}

		int cnt = 0;
		for (int i = 0; i < Turus.Length; i++) {
		
			if (Input.GetKey (keycodes [i]) == true && Turus [i].myState == TuruState.Idle && ConstraintTentaclsNum > _CurrentExistTentaclsNum + cnt) {
				Turus [i].InputOpenKey ();
				cnt += 1;
			}

			if (Input.GetKeyUp (keycodes [i]) == true) {
				Turus [i].NotOpenKey ();
			}

		}

	}

	///////////////////////////////
	////////アニメーターイベント////////

	//スタミナ０状態スタート時イベント
	public void AnimEnter_StaminaZero ()
	{
		this.myMecanimAnimator.SetBool ("isStaminaTime", true);

		this.myState = EtoriState.StaminaZero;

		//素材が無いのでスタブ
		this.mySSAnimator.Animation = this.SunnySSAnimations [7];
		this.mySSAnimator.PlayCount = 0;
		this.mySSAnimator.Play ();

	}
	//スタミナ０状態が終了時のイベント
	public void AnimEnd_StaminaZero ()
	{

		this.myMecanimAnimator.SetBool ("isStaminaTime", false);
		this.myMecanimAnimator.SetTrigger ("EndStaminaZero");

	}
	//Idle状態のイベント
	void AnimEnter_Idle ()
	{

		this.myState = EtoriState.Idle;

		switch (WeatherManager.Instance.CurrentWeatherState) {
		case WeatherState.Sunny:
			this.mySSAnimator.Animation = this.SunnySSAnimations [0];
			break;

		case WeatherState.Rain:
			this.mySSAnimator.Animation = this.RainySSAnimations [1];
			break;

		case WeatherState.Snow:
			this.mySSAnimator.Animation = this.SnowySSAnimations [1];
			break;

		case WeatherState.Storm:
			this.mySSAnimator.Animation = this.StormSSAnimations [1];
			break;

		case WeatherState.Thunder:
			this.mySSAnimator.Animation = this.ThunderSSAnimations [1];
			break;
		}
			
		this.mySSAnimator.PlayCount = 0;
		this.mySSAnimator.Play ();

	}
	//ハエを食べた時のイベント
	public void AnimEnter_GetHae (int point)
	{

		this.myMecanimAnimator.SetBool ("isGetHaeAnimationNow", true);
		this.myState = EtoriState.GetHae;

		this.mySSAnimator.Animation = this.SunnySSAnimations [1];
		this.mySSAnimator.AnimationFinished = (SsSprite sprite) => {
			this.myMecanimAnimator.SetTrigger ("EndGetHaeAnimation");
			this.myMecanimAnimator.SetBool ("isGetHaeAnimationNow", false);
		};
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.Play ();

	}

	//ケーキをゲットした時のイベント
	public void AnimEnter_GetCake ()
	{

		this.myMecanimAnimator.SetTrigger ("GetCake");

	}

	//ケーキを食べ始めた時のイベント
	void AnimEnter_EatCake ()
	{

		GameObject clone = ResourceManager.Instance.InstantiateResourceWithName ("ShotCakeAnimation");

		SoundManager.Instance.StopBGM ("Play");
		SoundManager.Instance.PlayBGM ("BonusTime");

		this.myState = EtoriState.CakeEat;

		this.mySSAnimator.Animation = this.SunnySSAnimations [5];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.AnimationFinished = (SsSprite sprite) => {
			this.myMecanimAnimator.SetTrigger ("EndEatCakeAnimation");
		};
		this.mySSAnimator.Play ();

	}

	//ケーキをキャッチしようとする時のイベント
	void AnimEnter_CatchCakeIn ()
	{

		this.myState = EtoriState.CatchCakeIn;
	
		this.mySSAnimator.Animation = this.SunnySSAnimations [3];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.AnimationFinished = (SsSprite sprite) => {
			this.myMecanimAnimator.SetTrigger ("EndCatchCakeInAnimation");
		};
		this.mySSAnimator.Play ();

	}

	//ケーキキャッチ待機状態イベント
	void AnimEnter_CatchCakeIdle ()
	{

		this.myState = EtoriState.CatchCakeIdle;

		this.mySSAnimator.Animation = this.SunnySSAnimations [6];
		this.mySSAnimator.PlayCount = 0;
		this.mySSAnimator.Play ();

	}

	//ケーキキャッチ解除イベント
	void AnimEnter_CatchCakeOut ()
	{

		this.myState = EtoriState.CatchCakeOut;

		this.mySSAnimator.Animation = this.SunnySSAnimations [4];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.AnimationFinished = (SsSprite sprite) => {
			this.myMecanimAnimator.SetTrigger ("EndCatchCakeOutAnimation");
		};
		this.mySSAnimator.Play ();

	}

	//ボーナスタイム開始時のイベント
	public void AnimEnter_BonusTime ()
	{

		this.myMecanimAnimator.SetBool ("isBonusTime", true);

		this.myState = EtoriState.Bonus;

		this.mySSAnimator.Animation = this.SunnySSAnimations [2];
		this.mySSAnimator.PlayCount = 0;
		this.mySSAnimator.Play ();

	}

	//ボーナスタイム終了時のイベント
	void AnimEnd_BonusTime ()
	{

		SoundManager.Instance.StopBGM ("BonusTime");
		SoundManager.Instance.PlayBGM ("Play");
		this.myMecanimAnimator.SetBool ("isBonusTime", false);
		this.myMecanimAnimator.SetTrigger ("EndBonusTime");

	}

	//クリア時のイベント
	public void AnimEnter_Clear ()
	{

		this.myState = EtoriState.Bonus;

		this.mySSAnimator.Animation = this.SunnySSAnimations [8];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.AnimationFinished = (SsSprite sprite) => {
			FadeManager.Instance.LoadLevel ("Result", 1.0f);
		};
		this.mySSAnimator.Play ();

	}

	// Sunnyに移行時のイベント.
	public void AnimEnter_Sunny (WeatherState weatherState)
	{

		this.myMecanimAnimator.SetTrigger ("ChangeWeather");
		Debug.Log ("AnimEnter_Sunny: OldWeather->:" + weatherState);

		switch (weatherState) {
		/*
		case WeatherState.Sunny:
			this.mySSAnimator.Animation = this.SunnySSAnimations [0];
			this.myState = EtoriState.Idle;
			this.mySSAnimator.PlayCount = 1;
			this.mySSAnimator.AnimationFinished = (SsSprite Sprite) => {
				this.myMecanimAnimator.SetTrigger("ChangeWeatherEnd");
			};
			break;
		*/

		case WeatherState.Storm:
			this.mySSAnimator.Animation = this.RainySSAnimations [0];
			this.myState = EtoriState.Idle;
			this.mySSAnimator.PlayCount = 1;
			this.mySSAnimator.PlayDirection = SsAnimePlayDirection.Reverse;
			this.mySSAnimator.AnimationFinished = (SsSprite Sprite) => {
				this.myMecanimAnimator.SetTrigger("ChangeWeatherEnd");
				this.mySSAnimator.PlayDirection = SsAnimePlayDirection.Forward;
			};
			this.mySSAnimator.Play ();
			break;

		case WeatherState.Thunder:
			this.mySSAnimator.Animation = this.ThunderSSAnimations [0];
			this.myState = EtoriState.Idle;
			this.mySSAnimator.PlayCount = 1;
			this.mySSAnimator.PlayDirection = SsAnimePlayDirection.Reverse;
			this.mySSAnimator.AnimationFinished = (SsSprite Sprite) => {
				this.myMecanimAnimator.SetTrigger ("ChangeWeatherEnd");
				this.mySSAnimator.PlayDirection = SsAnimePlayDirection.Forward;
			};
			this.mySSAnimator.Play ();
			break;

		case WeatherState.Snow:
			this.mySSAnimator.Animation = this.SnowySSAnimations [0];
			this.myState = EtoriState.Idle;
			this.mySSAnimator.PlayCount = 1;
			this.mySSAnimator.PlayDirection = SsAnimePlayDirection.Reverse;
			this.mySSAnimator.AnimationFinished = (SsSprite Sprite) => {
				this.myMecanimAnimator.SetTrigger ("ChangeWeatherEnd");
				this.mySSAnimator.PlayDirection = SsAnimePlayDirection.Forward;
			};
			this.mySSAnimator.Play ();
			break;

		}

	}

	public void AnimEnter_Stormy(){

		this.myMecanimAnimator.SetTrigger ("ChangeWeather");
		this.myState = EtoriState.Idle;

		Debug.Log ("AnimEnter_Stormy");

		this.mySSAnimator.Animation = this.StormSSAnimations [0];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.AnimationFinished = (SsSprite Sprite) => {
			this.myMecanimAnimator.SetTrigger("ChangeWeatherEnd");
		};
		this.mySSAnimator.Play ();

	}

	public void AnimEnter_Thunder(){

		this.myMecanimAnimator.SetTrigger ("ChangeWeather");
		this.myState = EtoriState.Idle;

		Debug.Log ("AnimEnter_Thunder");

		this.mySSAnimator.Animation = this.ThunderSSAnimations [0];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.AnimationFinished = (SsSprite Sprite) => {
			this.myMecanimAnimator.SetTrigger("ChangeWeatherEnd");
		};
		this.mySSAnimator.Play ();

	}

	// Rainyに移行時のイベント
	public void AnimEnter_Rainy ()
	{
		this.myMecanimAnimator.SetTrigger ("ChangeWeather");

		this.myState = EtoriState.Idle;
		this.mySSAnimator.Animation = this.RainySSAnimations [0];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.AnimationFinished = (SsSprite Sprite) => {
			this.myMecanimAnimator.SetTrigger("ChangeWeatherEnd");
		};
		this.mySSAnimator.Play ();

	}

	// Rainyに移行時のイベント
	public void AnimEnter_Snowy ()
	{
		this.myMecanimAnimator.SetTrigger ("ChangeWeather");

		this.myState = EtoriState.Idle;
		this.mySSAnimator.Animation = this.SnowySSAnimations [0];
		this.mySSAnimator.PlayCount = 1;
		this.mySSAnimator.AnimationFinished = (SsSprite Sprite) => {
			this.myMecanimAnimator.SetTrigger("ChangeWeatherEnd");
		};
		this.mySSAnimator.Play ();

	}
}