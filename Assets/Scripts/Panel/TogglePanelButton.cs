// source : Unity Tutorial.

using UnityEngine;
using System.Collections;

public class TogglePanelButton : MonoBehaviour {

	public void TogglePanel (GameObject panel) {
		print ("Toggle panel");
		panel.SetActive (!panel.activeSelf);
	}

}