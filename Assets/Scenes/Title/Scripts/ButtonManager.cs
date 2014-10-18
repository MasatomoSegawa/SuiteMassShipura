using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

	enum Direction{
		UP,DOWN,
	};

	public OkashiSelector Selector;
	public GameObject[] Buttons;
	public int initControlIndex;

	private int ControlIndex;

	public float coolTime = 0.01f;
	private float NextTime = 0.05f;

	private bool _isFocus = false;
	public bool isFocus {
		get{ return _isFocus;}
		set{ 
			this._isFocus = value;
			if (this._isFocus == true) {
				this.init ();
			} else {
				this.enabled = false;
				this.Sleep ();
			}
		}
	}

	void Start(){

		foreach (GameObject button in Buttons) {
			button.GetComponent<Button> ().onInputDownEvent += () => {
				this.isFocus = false;
			};
		}

	}

	void Sleep(){

		foreach (GameObject button in Buttons) {
			button.GetComponent<Animator> ().SetBool ("isFocus", true);
		}

	}

	public void InitFocusFalse(){

		foreach (GameObject button in Buttons) {
			button.GetComponent<Animator> ().SetTrigger ("InitUnFocus");
		}

	}

	public void init(){

		this.ControlIndex = initControlIndex;

		foreach (GameObject button in Buttons) {
			button.GetComponent<Animator> ().SetBool ("isFocus", false);
		}

		//新しくボタンをフォーカスさせる
		Vector2 Left = this.Buttons [ControlIndex].GetComponent<Button> ().LeftPosition.position;
		Vector2 Right = this.Buttons [ControlIndex].GetComponent<Button> ().RightPosition.position;
		Selector.SetSelector (Left, Right);

		this._isFocus = true;
		this.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (isFocus == true) {
			this.ControlInput ();
		}

	}

	void ValueChanged(Direction direction){

		SoundManager.Instance.PlaySE ("Select");

		int newIndex, oldIndex = this.ControlIndex;

		if (direction == Direction.DOWN) {
			newIndex =  (ControlIndex + 1) % Buttons.Length;
		} else {
			newIndex = (ControlIndex + Buttons.Length - 1) % Buttons.Length;
		}

		ControlIndex = Mathf.Clamp(newIndex, 0, Buttons.Length);

		//セレクターを変える
		Vector2 Left = this.Buttons [ControlIndex].GetComponent<Button> ().LeftPosition.position;
		Vector2 Right = this.Buttons [ControlIndex].GetComponent<Button> ().RightPosition.position;
		Selector.SetSelector (Left, Right);


	}

	void ControlInput(){

		if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && NextTime < Time.time) {
			ValueChanged (Direction.UP);

			NextTime = Time.time + coolTime;
		}

		if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && NextTime < Time.time){
			ValueChanged (Direction.DOWN);

			NextTime = Time.time + coolTime;
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			FadeButtonManager fm = GameObject.FindGameObjectWithTag ("FadeButtonManager").GetComponent<FadeButtonManager> ();
			fm.OldChange ();
			SoundManager.Instance.PlaySE ("Cancel");
		}
			
		//ボタンイベント発生
		if(Input.GetKeyDown(KeyCode.Z)){
			this.Buttons [ControlIndex].GetComponent<Button> ().onEvent ();
			SoundManager.Instance.PlaySE ("Confirm");
		}
			
	}

}
