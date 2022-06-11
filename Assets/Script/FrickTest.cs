using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrickTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private Vector3 touchStartPos;
	private Vector3 touchEndPos;
	string Direction;

	public void Flick(){
	if (Input.GetKeyDown(KeyCode.Mouse0)){
		touchStartPos = new Vector3(Input.mousePosition.x,
									Input.mousePosition.y,
									Input.mousePosition.z);
		}

	if (Input.GetKeyUp(KeyCode.Mouse0)){
		touchEndPos = new Vector3(Input.mousePosition.x,
								Input.mousePosition.y,
								Input.mousePosition.z);
		GetDirection();
		}
	}

	void GetDirection(){
     float directionX = touchEndPos.x - touchStartPos.x;
     float directionY = touchEndPos.y - touchStartPos.y;
     

		if (Mathf.Abs(directionY) < Mathf.Abs(directionX)){
			if (30 < directionX){
					//右向きにフリック
					Direction = "right";
			}else if (-30 > directionX){
					//左向きにフリック
					Direction = "left";
			}
			
		}else if (Mathf.Abs(directionX)<Mathf.Abs(directionY)){
			if (30 < directionY)
				//上向きにフリック
				Direction = "up";
			else if (-30 > directionY)
				//下向きのフリック
				Direction = "down";
			
		}else{
					//タッチを検出
					Direction = "touch";
			}

		switch (Direction){
			case "up":
				//上フリックされた時の処理
				Debug.Log("上フリック");
			　　 break;

			case "down":
				//下フリックされた時の処理
				Debug.Log("下フリック");
			　　　 break;

			case "right":
		　　　　　　//右フリックされた時の処理
				Debug.Log("右フリック");
			　　　break;

			case "left":
		　　　　　　//左フリックされた時の処理
				Debug.Log("左フリック");
			　　　break;

			case "touch":
		　　　　　 //タッチされた時の処理
				Debug.Log("タッチ");
				break;
			}

		}

		
}
