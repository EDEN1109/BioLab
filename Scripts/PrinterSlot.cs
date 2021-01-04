using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterSlot : MonoBehaviour
{
    [SerializeField]
    private Transform[] barrelPos = new Transform[2];
    private Transform barrel;
    private Barrel barrelScript;
    private Matters matter;
    private bool nowInsert = false;
    private bool isUpright = false;

    public bool NowInsert { get => nowInsert; }
    public Matters Matter { get => matter; set => matter = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(nowInsert)
        {
            StuckObject();
        }
    }

    private void StuckObject()
    {
        if (!barrelScript.IsGrip)
        {
            if (!barrel.GetComponent<Rigidbody>().isKinematic)
            {
                barrel.GetComponent<Rigidbody>().isKinematic = true;
                barrel.GetComponent<Rigidbody>().WakeUp();
            }

            if (!barrel.rotation.Equals(transform.rotation))
            {
                barrel.rotation = transform.rotation;
            }
            if (!isUpright)
            {
                barrel.position = barrelPos[0].position;
                isUpright = true;
            }
            else if (!barrel.position.Equals(barrelPos[1].position))
            {
                barrel.position = Vector3.MoveTowards(barrel.position, barrelPos[1].position, Time.smoothDeltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (barrel == null && other.tag == "Barrel")
        {
            barrelScript = other.GetComponent<Barrel>();
            if (!barrelScript.IsStuck && !barrelScript.IsBroken)
            {
                barrelScript.IsStuck = true;
                barrel = other.transform;
                nowInsert = true;

                if(barrelScript.IsBioreaction)
                {
                    matter = barrelScript.Matter.matterName;
                }
                else
                {
                    matter = Matters.Fail;
                }
            }
            else
            {
                barrelScript = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (barrel != null && other.gameObject.Equals(barrel.gameObject) && other.GetComponent<InteractionalObjects>().IsGrip)
        {
            barrel.GetComponent<Rigidbody>().isKinematic = false;
            barrel.GetComponent<Rigidbody>().WakeUp();

            barrelScript.IsStuck = false;
            barrelScript = null;
            barrel = null;
            matter = Matters.Fail;
            nowInsert = false;
            isUpright = false;
        }
    }

    public void EmptyBarrel()
    {
        if(barrelScript != null)
        {
            MatterStruct key = barrelScript.Matter;
            key.matterName = Matters.Fail;
            barrelScript.Matter = key;
            barrelScript.IsBioreaction = false;
        }
    }
}
