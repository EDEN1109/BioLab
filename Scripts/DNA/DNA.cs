using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    private FeedbackBubles[] bubles;
    private DNApart[] dnas;
    private Transform player;
    private Vector3 randPos;
    private Vector3 randRot;
    private float rotSpeed = 2.5f;
    private float moveSpeed = 0.05f;
    private int bubleCount;
    private int dnaCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomPosAndRot();
        SetDnaAndBuble();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        CheckUsable();
    }

    private void SetRandomPosAndRot()
    {
        randPos.x = Random.Range(-1.0f, 1.0f);
        randPos.y = Random.Range(-1.0f, 1.0f);
        randPos.z = Random.Range(-1.0f, 1.0f);

        randRot.x = Random.Range(-1.0f, 1.0f);
        randRot.y = Random.Range(-1.0f, 1.0f);
        randRot.z = Random.Range(-1.0f, 1.0f);
    }

    private void SetDnaAndBuble()
    {
        dnaCount = transform.GetChild(0).childCount;
        bubleCount = transform.GetChild(1).childCount;

        dnas = new DNApart[dnaCount];
        bubles = new FeedbackBubles[bubleCount];

        for (int i = 0; i < dnaCount; i++)
        {
            dnas[i] = transform.GetChild(0).GetChild(i).GetComponent<DNApart>();
            dnas[i].IsUsable = false;
        }

        for (int i = 0; i < bubleCount; i++)
        {
            bubles[i] = transform.GetChild(1).GetChild(i).GetComponent<FeedbackBubles>();
        }
    }

    private void Moving()
    {
        float mulSpeed = 1f;
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance < 6f)
        {
            mulSpeed = distance / 6f;

            if(distance < 3.5f)
            {
                mulSpeed = 0;
            }
        }

        transform.Translate(randPos * moveSpeed * mulSpeed * Time.smoothDeltaTime);
        transform.Rotate(randRot * rotSpeed * mulSpeed * Time.smoothDeltaTime);
    }

    private void CheckUsable()
    {
        for(int i = 0; i < dnaCount; i++)
        {
            bool forwardCut = true;
            bool backCut = true;

            if(i > 0)
            {
                forwardCut = bubles[i - 1].IsCut;
            }

            if(i < dnaCount - 1)
            {
                backCut = bubles[i].IsCut;
            }

            if(forwardCut && backCut)
            {
                dnas[i].IsUsable = true;
            }
        }
    }
}
