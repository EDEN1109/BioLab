using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNASphere : MonoBehaviour
{
    [SerializeField]
    private DNAMixingData mixingData;
    private DNApart DNAScript;
    private Transform DNA;
    private int myNum;
    private bool nowInsert = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myNum = transform.GetSiblingIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (nowInsert)
        {
            StuckObjects();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (DNA == null && other.tag == "DNA")
        {
            DNAScript = other.GetComponent<DNApart>();
            if (!DNAScript.IsStuck)
            {
                DNAScript.IsStuck = true;
                DNAScript.IsInSphere = true;
                mixingData.DNAStructs[myNum] = DNAScript.DNAStruct;
                mixingData.IsSphereEmpty[myNum] = false;
                mixingData.NumOfDNA++;
                mixingData.SetMedicineData(myNum);
                DNA = other.transform;
                nowInsert = true;
            }
            else
            {
                DNAScript = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (DNA != null && other.gameObject.Equals(DNA.gameObject))
        {
            DNAScript.IsStuck = false;
            DNAScript.IsInSphere = false;
            mixingData.IsSphereEmpty[myNum] = true;
            mixingData.NumOfDNA--;
            mixingData.SetMedicineData(myNum);
            DNAScript = null;
            DNA = null;
            nowInsert = false;
        }
    }

    private void StuckObjects()
    {
        if (nowInsert && !DNAScript.IsGrip)
        {
            if (!DNA.GetComponent<Rigidbody>().isKinematic)
            {
                DNA.GetComponent<Rigidbody>().isKinematic = true;
                DNA.GetComponent<Rigidbody>().WakeUp();
            }

            if (!DNA.rotation.Equals(transform.rotation))
            {
                DNA.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, transform.position, Time.smoothDeltaTime, 0));
            }
            if (!DNA.position.Equals(transform.position))
            {
                DNA.position = Vector3.MoveTowards(DNA.position, transform.position, Time.smoothDeltaTime);
            }

            if(Mathf.Abs(DNA.lossyScale.x - (this.transform.lossyScale.x * 1.2f)) > 0.01f)
            {
                if (DNA.lossyScale.x > (this.transform.lossyScale.x * 1.2f))
                {
                    DNA.localScale -= new Vector3(Time.smoothDeltaTime, Time.smoothDeltaTime, Time.smoothDeltaTime);
                }
                else if (DNA.lossyScale.x < (this.transform.lossyScale.x * 1.2f))
                {
                    DNA.localScale += new Vector3(Time.smoothDeltaTime, Time.smoothDeltaTime, Time.smoothDeltaTime);
                }
            }
        }
    }
}
