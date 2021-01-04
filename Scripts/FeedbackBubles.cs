using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBubles : MonoBehaviour
{
    private Buble[] bubles;
    private int size;
    private bool isCut = false;

    public bool IsCut { get => isCut; }

    // Start is called before the first frame update
    void Start()
    {
        size = transform.childCount;
        bubles = new Buble[size];

        for(int i = 0; i < size; i++)
        {
            bubles[i] = transform.GetChild(i).GetComponent<Buble>();
        }
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        isCut = CheckBubles();
        if(isCut)
        {
            gameObject.SetActive(false);
        }
    }

    private bool CheckBubles()
    {
        for(int i = 0; i < size; i++)
        {
            if(!bubles[i].IsDone)
            {
                return false;
            }
        }
        return true;
    }
}
