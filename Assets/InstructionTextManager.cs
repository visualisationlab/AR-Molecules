using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionTextManager : MonoBehaviour {

	public static InstructionTextManager instance = null;
	public Text instructionTextComponent;

	void Awake(){
		// check if we have an instance already.
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	public void setText(string text){
		instructionTextComponent.text = text;
	}

}
