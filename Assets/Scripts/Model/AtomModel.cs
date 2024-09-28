using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomModel {

	/**
	 * 
 	 * Dictionary that holds the information for the atoms  
	 * [Name, Atomic Radius, Covalent Radius]
	 */
	public Dictionary<string, AtomData> data = new Dictionary<string, AtomData>
	{				
		{"H", new AtomData(  "H",   0.53f, 0.37f)},
		{"He", new AtomData( "He",  0.31f, 0.32f)},
		{"Li", new AtomData( "Li",  1.67f, 1.34f)},
		{"Be", new AtomData( "Be",  1.12f, 1.34f)},
		{"B", new AtomData(  "B",   0.87f, 0.82f)},
		{"C", new AtomData(  "C",   0.67f, 0.77f)},
		{"N", new AtomData(  "N",   0.56f, 0.75f)},
		{"O", new AtomData(  "O",   0.48f, 0.73f)},
		{"F", new AtomData(  "F",   0.42f,  0.71f)},
		{"Ne", new AtomData( "Ne",  0.38f, 0.69f)},
		{"Na", new AtomData(  "Na",   1.90f, 1.54f)},
		{"Mg", new AtomData(  "Mg",   1.45f, 1.30f)},
		{"Al", new AtomData(  "Al",   1.18f, 1.18f)},
		{"Si", new AtomData(  "Si",   1.11f, 1.11f)},
		{"P", new AtomData(  "P",   0.98f, 1.06f)},
		{"S", new AtomData(  "S",   0.88f, 1.02f)},
		{"Cl", new AtomData(  "Cl",   0.79f, 0.99f)},
		{"Ar", new AtomData(  "Ar",   0.71f, 0.97f)},
		{"K", new AtomData(  "K",   2.43f, 1.96f)},
		{"Ca", new AtomData(  "Ca",   1.94f, 1.74f)},
		{"Sc", new AtomData(  "Sc",   1.84f, 1.44f)},
		{"Ti", new AtomData(  "Ti",   1.76f, 1.36f)}
	};

	public AtomData getDataForAtom(string atom){
		
		//return data for the atom.
		if (data.ContainsKey(atom)){
			return data[atom];
		}

		//TODO improve this.
		//return hydrogen by default.
		return data["H"];
	}
}
