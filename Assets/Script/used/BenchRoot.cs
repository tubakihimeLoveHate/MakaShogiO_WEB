using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BenchRoot : MonoBehaviour
{
    private Animator animator;
    private bool isShown = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPushButton()
    {
        if (isShown)
        {
            animator.SetTrigger("");
        }
        else
        {
            animator.SetTrigger("");
        }
    }
}
