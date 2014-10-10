using UnityEngine;
using System.Collections;

public class ComboManager : Singleton<ComboManager> {

	public TextMesh myTextMesh;

	private int CurrentCombo;
	public int MaximumChain = 0;

	void Start(){
		Init ();
	}

	void Init(){



	}

	public void AddCombo(int point){
		CurrentCombo += 1;

		if (MaximumChain <= CurrentCombo) {
			MaximumChain = CurrentCombo;
		}

		ReWriteText ();
	}

	public void MissCombo(int point){
		CurrentCombo = 0;
		ReWriteText ();
	}

	void ReWriteText(){

		myTextMesh.text = CurrentCombo.ToString ();

	}

}
