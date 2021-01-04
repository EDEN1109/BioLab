using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BioreactorVender : MonoBehaviour
{
    [SerializeField]
    private Text costText;
    [SerializeField]
    private GameObject bioreactor;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform printPoint;
    [SerializeField]
    private PandemicStatus pandemic;
    [SerializeField]
    private Animator animator;
    private Transform bioTrans;
    private const float moveSpeed = 0.75f;
    private const int costInc = 500;
    private int cost = 1000;

    // Start is called before the first frame update
    void Start()
    {
        costText.text = cost + " 원";
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))
        {
            MovingBioreactor();
        }
    }

    private void MovingBioreactor()
    {
        if (bioTrans.position.Equals(printPoint.position))
        {
            bioTrans = null;
        }
        else
        {
            bioTrans.position = Vector3.MoveTowards(bioTrans.position, printPoint.position, Time.smoothDeltaTime * moveSpeed);
        }
    }

    public void OnClickBuy()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        if(pandemic.DecreaseCoin(cost))
        {
            bioTrans = Instantiate(bioreactor, spawnPoint).transform;
            animator.SetTrigger("Rolling");
            cost += costInc;
            costText.text = cost + " 원";
        }
    }
}
