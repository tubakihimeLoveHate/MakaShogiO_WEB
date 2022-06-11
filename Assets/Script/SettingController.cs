using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour {

	public Transform topImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnReturnButton(){
		transform.localPosition = new Vector2(-1000,0);
		gameObject.SetActive(false);
	}
}
