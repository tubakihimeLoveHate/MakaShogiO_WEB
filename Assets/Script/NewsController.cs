using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//名前変えたい
public class NewsController : MonoBehaviour {

	//public GameObject contents;

	public GameObject settingImage;

	//public GameObject newsContents;
	
	public Transform topImage;

	//int maxTransform = 185;

	//float movetime = 3.0f;

	//float speed = 1;

	//public Image[] NewsContent;

	//int maxIndex ;

	public GameObject account,setting,mail,buy,news,app;

	public GameObject objectCaches;


	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	} 


	public void OnTap(int i){
		settingImage.transform.localPosition = topImage.localPosition;
		settingImage.SetActive(true);
		GameObject objects = null;
		

		switch(i){
			case 0:
				objects = account;
				break;
			//case 1:
				//objects = setting;
				//break;
			case 2:
				objects = mail;
				break;
			case 3:
				objects = buy;
				break;
			case 4:
				objects = news;
				break;
			case 5:
				objects = app;
				break;
			default:
				break;
		}

		objectCaches = objects;
		objects.gameObject.SetActive(true);


	}

	public void OnTapRreturn(){
		settingImage.transform.localPosition = new Vector3(-1000,0,0);
		settingImage.SetActive(false);
		//newsContents.SetActive(false);
		if(objectCaches != null)objectCaches.SetActive(false);
	}
}
