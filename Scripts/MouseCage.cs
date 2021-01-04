using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCage : MonoBehaviour
{
    [SerializeField]
    private Transform mouse;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform endPos;

    public Animator Animator { get => animator; set => animator = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Death_1") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            mouse.position = startPos.position;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump_fly"))
        {
            if (Vector3.Distance(mouse.position, endPos.position) < 0.001f)
            {
                animator.SetTrigger("Land");
            }
            else
            {
                mouse.position = Vector3.Lerp(mouse.position, endPos.position, 0.1f);
            }
        }
    }
}
