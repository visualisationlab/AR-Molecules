using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomColors{

	/**
	 * Method that returns a color for a certain atom or grey by default 
	 */
	public static Color getColorForAtom(string atomName){
		if(colors.ContainsKey(atomName)){
			return colors[atomName];
		} else{
			return Color.grey;
		}
	}

	/**
	 * TODO green is used for a lot of molecules..
 	 * Dictionary that holds the colors for each atom. 
	 */
	private static Dictionary<string, Color> colors = new Dictionary<string, Color>
	{
		{"He", Color.cyan},
		{"H", Color.white},
		{"Li", Color.green},
		{"Be", Color.green},
		{"B", Color.green},
		{"C", Color.black},
		{"N", Color.blue},
		{"O", Color.red},
		{"F", Color.green},
		{"Ne", Color.green},
		{"Na", Color.green},
		{"Mg", Color.green},
		{"Al", Color.green},
		{"Si", Color.green},
		{"P", Color.cyan},
		{"S", Color.green},
		{"Cl", Color.green},
		{"Ar", Color.green},
		{"K", Color.green},
		{"Ca", Color.green},
		{"Sc", Color.green},
		{"Ti", Color.green}
	};
}
