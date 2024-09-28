using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour {
	
//	public static HelpManager instance = null;

	void Awake(){
//		// check if we have an instance already.
//		if (instance == null) {
//			instance = this;
//		} else if (instance != this) {
//			Destroy (gameObject);
//		}
	}

	void toggleHelpPanel(){
		
	}

	public void showHelpPanel(string text){
		InstructionTextManager.instance.setText (text);
	}

}