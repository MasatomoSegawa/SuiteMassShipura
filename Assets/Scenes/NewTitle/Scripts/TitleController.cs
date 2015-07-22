using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	void Start(){

		SoundManager.Instance.PlayBGM ("Title");

	}

	public void OnConfirm(){

		SoundManager.Instance.PlaySE ("Confirm");

	}
		
	public void OnSelect(){

		SoundManager.Instance.PlaySE ("Select");

	}

	public void OnButtonClick(string myName){

		switch (myName) {
		case "StartButton":
			FadeManager.Instance.LoadLevel ("GamePlay", 1.0f);
			break;

		case "EndButton":
			Application.Quit ();
			break;
		}

	}

}
