using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TextAsset extension
 * 
 * This adds a method to convert a text asset to a list of strings.
 * 
 */


public static class TextAssetExtensionMethods {
	// return a list from the text asset contents so it is possible to read it line by line
	public static List<string> toList(this TextAsset ta) {
		return new List<string>(ta.text.Split('\n'));
	}

}