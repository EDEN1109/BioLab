using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartridgeReaderPanel : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text InfoText;
    [SerializeField]
    private Text tempText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text fuelText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMicrobePanel(MicrobeCartridge microbeCartridge)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);

        image.sprite = microbeCartridge.SpriteImg;
        nameText.text = ((Microbes_Kr)microbeCartridge.CartridgeName).ToString();
        InfoText.text = microbeCartridge.CartridgeInfo;
        tempText.text = microbeCartridge.MinTemp + " 도 ~ " + microbeCartridge.MaxTemp + " 도";
        timeText.text = microbeCartridge.MinTime + " 초 ~ " + microbeCartridge.MaxTime + " 초";
        fuelText.text = microbeCartridge.MinFuel + " mL ~ " + microbeCartridge.MaxFuel + " mL";
    }
    
    public void SetMedicinePanel(MedicineCartridge medicineCartridge)
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        image.sprite = medicineCartridge.SpriteImg;
        nameText.text = ((Medicines_Kr)medicineCartridge.CartridgeName).ToString();
        InfoText.text = medicineCartridge.CartridgeInfo;
    }
}
