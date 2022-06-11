using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    [SerializeField, Header("PlayerInfo")]
    private Text playlerName;
    [SerializeField]
    private Text playClass;
    [SerializeField]
    private Text playerRate;

    // Start is called before the first frame update
    void Start()
    {
        //プロフィール
        playlerName.text = Player.playerName;
        playClass.text = "階級:" + Player.playerClass;
        playerRate.text = "レート:" + Player.rate.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
