using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInfo : MonoBehaviour {

	//プレイヤーとデッキマネージャーの仲介

	Animator animator;
	//bool isOpen = false;

	public GameObject scroll;
	// Use this for initialization
	void Start () {
		animator = scroll.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnOpenScroll(){
		animator.SetBool("Opener",true);
	}
}
