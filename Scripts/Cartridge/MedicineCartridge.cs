using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineCartridge : Cartridge
{
    private MedicineList medicineList;
    public MedicineStruct medicineStruct = new MedicineStruct();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        medicineList = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MedicineList>();
        IsMicrobe = false;

        if(medicineList.GivenNum == 0)
        {
            medicineStruct = medicineList.GetFirstMedicineData();
            cartridgeName = (int)medicineStruct.medicineName;
            cartridgeInfo = medicineStruct.medicineInfo;
            spriteImg = medicineStruct.medicineSprite;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void MakeMedicineDetail(DNAStruct dnaStruct)
    {
        if (medicineList == null)
        {
            medicineList = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MedicineList>();
        }
        medicineStruct = medicineList.GetMedicineData(dnaStruct);
        cartridgeName = (int)medicineStruct.medicineName;
        cartridgeInfo = medicineStruct.medicineInfo;
        spriteImg = medicineStruct.medicineSprite;
    }
}
