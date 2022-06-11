using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckFront : MonoBehaviour {

	public Transform Change_form;
	public Transform Select_form;

	public FrickTest flicktest;

//	public Player player;
	public GameObject[] Komas = new GameObject[20];

	public CharPrefab[] Prefabs = new CharPrefab[20];

	// Use this for initialization
	void Start () {
		flicktest = new FrickTest();
		//player = new Player();
		/* 
		for(int p = 0;p<20;p++){
			
		}
		
		Prefabs[0] = player.mydeck[0];
		Prefabs[1] = player.mydeck[1];
		Prefabs[2] = player.mydeck[2];
		Prefabs[3] = player.mydeck[3];
		Prefabs[4] = player.mydeck[4];
		Prefabs[5] = player.mydeck[5];
		Prefabs[6] = player.mydeck[6];
		Prefabs[7] = player.mydeck[7];
		Prefabs[8] = player.mydeck[8];
		Prefabs[9] = player.mydeck[9];
		Prefabs[10] = player.mydeck[10];
		Prefabs[11] = player.mydeck[11];
		Prefabs[12] = player.mydeck[12];
		Prefabs[13] = player.mydeck[13];
		Prefabs[14] = player.mydeck[14];
		Prefabs[15] = player.mydeck[15];
		Prefabs[16] = player.mydeck[16];
		Prefabs[17] = player.mydeck[17];
		Prefabs[18] = player.mydeck[18];
		Prefabs[19] = player.mydeck[19];
		Prefabs[20] = player.mydeck[20];
		*/
	}
	
	// Update is called once per frame
	void Update () {
		flicktest.Flick();
	}

	public void Onmove_DeckScorpe(){

		//buttonの内容から分隊編成ページに反映する、その後に遷移
		Change_form.transform.localPosition = Select_form.transform.localPosition;
	}
}
