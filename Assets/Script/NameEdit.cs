using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameEdit : MonoBehaviour {


	public Text Playername;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnValueChange(string values){
		this.Playername.text = values;
		//この値を保存するように
	}
}
