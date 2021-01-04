using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RasmusPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject setting;
    [SerializeField]
    private GameObject nowPrint;
    [SerializeField]
    private GameObject medicineObject;
    [SerializeField]
    private Transform positive;
    [SerializeField]
    private Transform negative;
    [SerializeField]
    private Sprite[] iconSprite = new Sprite[10];
    [SerializeField]
    private Image pillImage;
    [SerializeField]
    private Button pillBtn;
    [SerializeField]
    private Image capsuleImage;
    [SerializeField]
    private Button capsuleButton;
    [SerializeField]
    private Text medicineText;
    [SerializeField]
    private Text doseText;
    [SerializeField]
    private Text warnText;
    [SerializeField]
    private Printer printer;

    private List<MedicineStruct> medicine = new List<MedicineStruct>();
    private int medicineCount;
    private int medicineNum = 0;
    private int dose = 0;
    private const float turnSpeed = 6f;
    private bool isPill = false;
    private bool isCapsule = false;
    private bool isSet = false;

    public bool IsSet { get => isSet; }

    // Start is called before the first frame update
    void Start()
    {
        InitPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMedicineList(MedicineStruct medicine)
    {
        this.medicine.Add(medicine);
        medicineCount = this.medicine.Count;
    }

    private void SetMedicineInfo()
    {
        medicineText.text = ((Medicines_Kr)medicine[medicineNum].medicineName).ToString();

        if(medicine[medicineNum].criticalAge > 0)
        {
            warnText.text = medicine[medicineNum].criticalAge + "대에 치명적";
        }
        else
        {
            warnText.text = "없음";
        }

        int imageNum = 0;
        for (int i = 0; i < medicine[medicineNum].positive.Count; i++)
        {
            positive.GetChild(imageNum).gameObject.SetActive(true);
            positive.GetChild(imageNum).GetComponent<Image>().sprite = iconSprite[(int)medicine[medicineNum].positive[i].effect];

            if (medicine[medicineNum].positive[i].power == 3)
            {
                positive.GetChild(imageNum).GetChild(0).gameObject.SetActive(true);
                positive.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
            }
            else if (medicine[medicineNum].positive[i].power == 2)
            {
                positive.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                positive.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                positive.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                positive.GetChild(imageNum).GetChild(1).gameObject.SetActive(false);
            }

            imageNum++;
        }

        for (int i = imageNum; i < positive.childCount; i++)
        {
            positive.GetChild(i).gameObject.SetActive(false);
        }

        imageNum = 0;
        for (int i = 0; i < medicine[medicineNum].negative.Count; i++)
        {
            negative.GetChild(imageNum).gameObject.SetActive(true);
            negative.GetChild(imageNum).GetComponent<Image>().sprite = iconSprite[(int)medicine[medicineNum].negative[i].effect];

            if (medicine[medicineNum].negative[i].power == 3)
            {
                negative.GetChild(imageNum).GetChild(0).gameObject.SetActive(true);
                negative.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
            }
            else if (medicine[medicineNum].negative[i].power == 2)
            {
                negative.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                negative.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                negative.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                negative.GetChild(imageNum).GetChild(1).gameObject.SetActive(false);
            }

            imageNum++;
        }

        for (int i = imageNum; i < negative.childCount; i++)
        {
            negative.GetChild(i).gameObject.SetActive(false);
        }

        medicineObject = medicine[medicineNum].medicinePrefab;
    }

    public void InitPanel()
    {
        isPill = false;
        isCapsule = false;
        isSet = false;
        dose = 0;
        pillImage.sprite = pillBtn.spriteState.disabledSprite;
        capsuleImage.sprite = capsuleButton.spriteState.disabledSprite;
        medicineNum = 0;

        setting.SetActive(true);
        medicineObject.SetActive(true);
        nowPrint.SetActive(false);

        SetMedicineInfo();
        doseText.text = dose + " mg";
    }

    public void OnClickIncreaseDose()
    {
        dose += 100;
        doseText.text = dose + " mg";
    }
    public void OnClickDecreaseDose()
    {
        if(dose >= 100)
        {
            dose -= 100;
            doseText.text = dose + " mg";
        }
    }

    public void OnClickPill()
    {
        isPill = true;
        isCapsule = false;
        pillImage.sprite = pillBtn.spriteState.selectedSprite;
        capsuleImage.sprite = capsuleButton.spriteState.disabledSprite;
    }

    public void OnClickCapsule()
    {
        isCapsule = true;
        isPill = false;
        pillImage.sprite = pillBtn.spriteState.disabledSprite;
        capsuleImage.sprite = capsuleButton.spriteState.selectedSprite;
    }

    public void OnClickSet()
    {
        if((isPill || isCapsule) && dose > 0)
        {
            isSet = true;

            setting.SetActive(false);
            medicineObject.SetActive(false);
            nowPrint.SetActive(true);

            printer.SetPrinterMatters(medicine[medicineNum].ingredient, medicine[medicineNum].ingredientNum);
        }
    }

    public void OnClickPrev()
    {
        medicineNum--;
        if(medicineNum < 0)
        {
            medicineNum = medicineCount - 1;
        }

        SetMedicineInfo();
    }

    public void OnClickNext()
    {
        medicineNum++;
        if (medicineNum >= medicineCount)
        {
            medicineNum = 0;
        }

        SetMedicineInfo();
    }
}
