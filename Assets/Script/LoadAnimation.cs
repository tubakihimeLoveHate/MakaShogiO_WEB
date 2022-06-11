using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAnimation : MonoBehaviour {

	public RawImage rightBack;
	public RawImage leftBack;

	public bool loadComplite=false;

	//public GameObject rightFront;
	//public GameObject leftFront;
	/* 

	private Transform defaultRightBack;
	private Transform defaultLeftBack;
	private Transform defaultRightFront;
	private Transform defaultLeftFront;
*/
	public bool start = false;
	public bool startFront = false;
	

	Animator animator;

	//public bool loadEnd = false;
	//mainからの制御よう
	//public bool loadSection = false;
	private Vector3 BackSpeed;
	private Vector3 FrontSpeed;
	
	public bool fadeStart　= false;

	[SerializeField]

	private float alfa = 0;


	// Use this for initialization
	void Start () {
		animator=GetComponent<Animator>();
		BackSpeed = new Vector3(2f,0,0);
		FrontSpeed = new Vector3(15f,0,0);
		/* 
		defaultRightBack = rightBack.transform;
		defaultRightFront = rightFront.transform;
		defaultLeftBack = leftBack.transform;
		defaultLeftFront = leftFront.transform;
		*/
		//LoadStart();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//背景が閉じる演出
		/* 
		if(start){
			fadeStart = true;
			rightBack.transform.localPosition -= BackSpeed;
			leftBack.transform.localPosition += BackSpeed;
		}

		//襖が閉じる演出
		if(startFront){
			rightFront.transform.localPosition -= FrontSpeed;
			leftFront.transform.localPosition += FrontSpeed;
		}

		//背景が中心に足した時の調整onetime
		if(rightBack.transform.localPosition.x <0){
			rightBack.transform.localPosition = new Vector3(0,0,0);
			leftBack.transform.localPosition = new Vector3(0,0,0);
			start = false;
			fadeStart = false;
			startFront = true;
			//Debug.Log("Load2");
		}

		//襖が中心に達した時の調整処理onetime
		if(rightFront.transform.localPosition.x<0){
			//Debug.Log("Load2.5");
			rightFront.transform.localPosition = new Vector3(0,0,0);
			leftFront.transform.localPosition = new Vector3(0,0,0);
			startFront = false;
			loadSection=true;
		}

		if(loadEnd){
			//アニメーションにした方が良い
			if(rightBack.transform.localPosition.x < 200){
				rightBack.transform.localPosition += FrontSpeed;
				leftBack.transform.localPosition -= FrontSpeed;
				rightFront.transform.localPosition += FrontSpeed;
				leftFront.transform.localPosition -= FrontSpeed;
				if(rightBack.transform.localPosition.x > 200){
					rightBack.transform.localPosition = defaultRightBack.localPosition;
					leftBack.transform.localPosition = defaultLeftBack.localPosition;
					rightFront.transform.localPosition = defaultRightFront.localPosition;
					rightFront.transform.localPosition = defaultRightFront.localPosition;
					loadEnd = false;
					//Debug.Log("LoadFINAL");
				}
			}
		}


	if(loadSection)loadEnd=true;
		*/
		
		

		
		//テストよう
		if(fadeStart){
			/* 
			rightBack.color = new Color(255,255,255,alfa);
			leftBack.color = new Color(255,255,255,alfa);
			alfa += 0.015f;
			if(alfa >1){
				fadeStart = false;
			}
*/
		
		}
	}

	public void LoadStart(){
		//fadeStart = true;
		//rightBack.CrossFadeAlpha(1,1,true);
		//leftBack.CrossFadeAlpha(1,1,true);
		animator.SetTrigger("close");
		
	}

	public void LoadEnd(){
		animator.SetTrigger("open");
	}

	
}
