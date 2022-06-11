using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour {


	public Image UIobj;
	public bool roop;
	public bool endfaids = false;
	public float countTime = 60.0f;
	//public Text count; 
	public float outtext;//表示用int

	public float deltacnt = 0;
	// Use this for initialization
	void Start () {
		roop = true;
		UIobj.fillAmount = 1;
		UIobj.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		if (roop) {
			endfaids = false;
            UIobj.fillAmount -= 1.0f / countTime * Time.deltaTime;
			deltacnt += Time.deltaTime;
			outtext = countTime - deltacnt;
			//count.text = Mathf.CeilToInt(outtext).ToString();

			if(UIobj.fillAmount<=0.3) UIobj.color = Color.red;
			if(UIobj.fillAmount == 0){ //リセット作業
				UIobj.fillAmount = 1;
				//count.text = "0";
				deltacnt = 0;
				UIobj.color = Color.green;
				endfaids = true;
			}
        }
	}

	public void ResetTimer(){
		UIobj.fillAmount = 1;
		UIobj.color = Color.green;
	}
}
