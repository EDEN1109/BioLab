using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject cartridgePrefab;
    private GameObject cartridge;
    private Transform spawnPos;
    private float printSpeed = 0.5f;
    private bool isPrint = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPrint)
        {
            PrintCartridge();
        }
    }

    private void PrintCartridge()
    {
        if(Vector3.Distance(cartridge.transform.position, this.transform.position) > 0.375f)
        {
            isPrint = false;
            cartridge.GetComponent<Rigidbody>().isKinematic = false;
            cartridge.GetComponent<Rigidbody>().WakeUp();
        }
        else
        {
            cartridge.transform.Translate(Vector3.down * Time.smoothDeltaTime * printSpeed);
        }
    }

    public void SpawnCartridge(DNAStruct medicine)
    {
        cartridge = Instantiate(cartridgePrefab, spawnPos);
        cartridge.GetComponent<Rigidbody>().isKinematic = true;
        cartridge.GetComponent<Rigidbody>().WakeUp();
        isPrint = true;
        MedicineCartridge medicineCartridge = cartridge.GetComponent<MedicineCartridge>();
        medicineCartridge.MakeMedicineDetail(medicine);
    }
}
