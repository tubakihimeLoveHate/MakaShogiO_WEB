using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchController : MonoBehaviour {

	public GameObject Bench1;
	public GameObject Bench2;

	Animator animator1;

	Animator animator2;

	bool Player = false;
	bool Enemy = false;

	// Use this for initialization
	void Start () {
		animator1 = Bench1.GetComponent<Animator>();
		animator2 = Bench2.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPlayer(){
		this.Player = !this.Player;
		animator1.SetBool("parametor",this.Player);
	}

	public void OnEnemy(){
		this.Enemy = !this.Enemy;
		animator2.SetBool("parametor",this.Enemy);
	}
}
