using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailInfo : MonoBehaviour
{
    public static DetailInfo instance;

    [SerializeField]
    private Button detailViewInfo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayViewSwitch()
    {
        detailViewInfo.gameObject.SetActive(!detailViewInfo.gameObject.activeSelf);
    }
}
