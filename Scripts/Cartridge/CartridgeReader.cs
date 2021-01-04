using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeReader : MonoBehaviour
{
    [SerializeField]
    private Transform[] cartridgePos = new Transform[2];
    [SerializeField]
    private CartridgeReaderPanel cartridgeReaderPanel;
    private Transform cartridge;
    private Cartridge cartridgeScript;
    private bool nowInsert = false;
    private bool isUpright = false;

    public bool NowInsert { get => nowInsert; }

    // Start is called before the first frame update
    void Start()
    {
        cartridgeReaderPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (nowInsert)
        {
            StuckObject();
        }
    }
    private void StuckObject()
    {
        if (!cartridgeScript.IsGrip)
        {
            if (!cartridge.GetComponent<Rigidbody>().isKinematic)
            {
                cartridge.GetComponent<Rigidbody>().isKinematic = true;
                cartridge.GetComponent<Rigidbody>().WakeUp();
            }

            if (!cartridge.rotation.Equals(cartridgePos[0].rotation))
            {
                cartridge.rotation = cartridgePos[0].rotation;
            }
            if (!isUpright)
            {
                cartridge.position = cartridgePos[0].position;
                isUpright = true;
                cartridgeReaderPanel.gameObject.SetActive(true);
                if (cartridgeScript.IsMicrobe)
                {
                    cartridgeReaderPanel.SetMicrobePanel(cartridge.GetComponent<MicrobeCartridge>());
                }
                else
                {
                    cartridgeReaderPanel.SetMedicinePanel(cartridge.GetComponent<MedicineCartridge>());
                }
            }
            else if (!cartridge.position.Equals(cartridgePos[1].position))
            {
                cartridge.position = Vector3.MoveTowards(cartridge.position, cartridgePos[1].position, Time.smoothDeltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (cartridge == null && other.tag == "Cartridge")
        {
            cartridgeScript = other.GetComponent<Cartridge>();
            if (!cartridgeScript.IsStuck)
            {
                cartridgeScript.IsStuck = true;
                cartridge = other.transform;
                nowInsert = true;
            }
            else
            {
                cartridgeScript = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cartridge != null && other.gameObject.Equals(cartridge.gameObject) && other.GetComponent<InteractionalObjects>().IsGrip)
        {
            print("Out");
            cartridgeReaderPanel.gameObject.SetActive(false);
            cartridge.GetComponent<Rigidbody>().isKinematic = false;
            cartridge.GetComponent<Rigidbody>().WakeUp();

            cartridgeScript.IsStuck = false;
            cartridgeScript = null;
            cartridge = null;
            nowInsert = false;
            isUpright = false;
        }
    }
}
