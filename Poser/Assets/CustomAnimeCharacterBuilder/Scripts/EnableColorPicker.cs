using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableColorPicker : MonoBehaviour {

	public GameObject ColorPickerParent, MyColorPicker;
	// Use this for initialization
	void OnEnable()
	{
		ColorPickerParent.SetActive (true);
		MyColorPicker.SetActive (true);
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
