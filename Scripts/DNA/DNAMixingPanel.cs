using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNAMixingPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject DNAInfo;
    [SerializeField]
    private Text infoCriticalAgeText;
    [SerializeField]
    private Text infoUnknownText;
    [SerializeField]
    private Transform infoPositivePosition;
    [SerializeField]
    private Transform infoNegativePosition;
    [SerializeField]
    private GameObject mixingStation;
    [SerializeField]
    private Text mixingCriticalAgeText;
    [SerializeField]
    private Text mixingUnknownText;
    [SerializeField]
    private Transform mixingPositivePosition;
    [SerializeField]
    private Transform mixingNegativePosition;
    [SerializeField]
    private Text[] sphereText = new Text[5];
    [SerializeField]
    private Transform DNAModels;
    [SerializeField]
    private Transform sphereDNA;
    [SerializeField]
    private Sprite[] iconSprite = new Sprite[10];
    [SerializeField]
    private DNAMixingData mixingData;


    // Start is called before the first frame update
    void Start()
    {
        SetMixingStation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDNAInfo(DNAStruct DNAStruct)
    {
        DNAInfo.SetActive(true);
        DNAModels.gameObject.SetActive(true);
        mixingStation.SetActive(false);
        sphereDNA.gameObject.SetActive(false);

        for(int i = 0; i < DNAModels.childCount; i++)
        {
            if(i == DNAStruct.DNAsize - 1)
            {
                DNAModels.GetChild(DNAStruct.DNAsize - 1).gameObject.SetActive(true);
            }
            else
            {
                DNAModels.GetChild(i).gameObject.SetActive(false);
            }
        }

        if(DNAStruct.criticalAge == 0)
        {
            infoCriticalAgeText.text = "주의사항 없음";
        }
        else
        {
            infoCriticalAgeText.text = DNAStruct.criticalAge + "대에 치명적";
        }

        infoUnknownText.text = "미확인된 효과 " + DNAStruct.unknownNum + "개";

        int imageNum = 0;
        for (int i = 0; i < DNAStruct.positive.Count; i++)
        {
            if(!DNAStruct.positive[i].isHide)
            {
                infoPositivePosition.GetChild(imageNum).gameObject.SetActive(true);
                infoPositivePosition.GetChild(imageNum).GetComponent<Image>().sprite = iconSprite[(int)DNAStruct.positive[i].effect];

                if(DNAStruct.positive[i].power == 3)
                {
                    infoPositivePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(true);
                    infoPositivePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
                }
                else if(DNAStruct.positive[i].power == 2)
                {
                    infoPositivePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                    infoPositivePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    infoPositivePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                    infoPositivePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(false);
                }

                imageNum++;
            }
        }

        for (int i = imageNum; i < infoPositivePosition.childCount; i++)
        {
            infoPositivePosition.GetChild(i).gameObject.SetActive(false);
        }

        imageNum = 0;
        for (int i = 0; i < DNAStruct.negative.Count; i++)
        {
            if (!DNAStruct.negative[i].isHide)
            {
                infoNegativePosition.GetChild(imageNum).gameObject.SetActive(true);
                infoNegativePosition.GetChild(imageNum).GetComponent<Image>().sprite = iconSprite[(int)DNAStruct.negative[i].effect];

                if (DNAStruct.negative[i].power == 3)
                {
                    infoNegativePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(true);
                    infoNegativePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
                }
                else if (DNAStruct.negative[i].power == 2)
                {
                    infoNegativePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                    infoNegativePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    infoNegativePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                    infoNegativePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(false);
                }

                imageNum++;
            }
        }

        for (int i = imageNum; i < infoNegativePosition.childCount; i++)
        {
            infoNegativePosition.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SetMixingStation()
    {
        DNAInfo.SetActive(false);
        DNAModels.gameObject.SetActive(false);
        mixingStation.SetActive(true);
        sphereDNA.gameObject.SetActive(true);

        for(int i = 0; i < sphereText.Length; i++)
        {
            if(i == 0)
            {
                sphereText[i].text = "Main";
            }
            else if(i == 1)
            {
                sphereText[i].text = "Sub 1st";
            }
            else if (i == 2)
            {
                sphereText[i].text = "Sub 2nd";
            }
            else if (i == 3)
            {
                sphereText[i].text = "Sub 3rd";
            }
            else if (i == 4)
            {
                sphereText[i].text = "Sub 4th";
            }

            if (mixingData.IsSphereEmpty[i])
            {
                sphereText[i].text += "\n[Empty]";
                for (int j = 0; j < sphereDNA.GetChild(i).childCount; j++)
                {
                    sphereDNA.GetChild(i).GetChild(j).gameObject.SetActive(false);
                }
            }
            else
            {
                for (int j = 0; j < sphereDNA.GetChild(i).childCount; j++)
                {
                    if (j == mixingData.DNAStructs[i].DNAsize - 1)
                    {
                        sphereDNA.GetChild(i).GetChild(j).gameObject.SetActive(true);
                    }
                    else
                    {
                        sphereDNA.GetChild(i).GetChild(j).gameObject.SetActive(false);
                    }
                }
            }
        }

        if (mixingData.medicine.criticalAge == 0)
        {
            mixingCriticalAgeText.text = "주의사항 없음";
        }
        else
        {
            mixingCriticalAgeText.text = mixingData.medicine.criticalAge + "대에 치명적";
        }

        mixingUnknownText.text = "미확인된 효과 " + mixingData.medicine.unknownNum + "개";

        int imageNum = 0;
        for (int i = 0; i < mixingData.medicine.positive.Count; i++)
        {
            if (!mixingData.medicine.positive[i].isHide)
            {
                mixingPositivePosition.GetChild(imageNum).gameObject.SetActive(true);
                mixingPositivePosition.GetChild(imageNum).GetComponent<Image>().sprite = iconSprite[(int)mixingData.medicine.positive[i].effect];

                if (mixingData.medicine.positive[i].power >= 6)
                {
                    mixingPositivePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(true);
                    mixingPositivePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
                }
                else if (mixingData.medicine.positive[i].power >= 4)
                {
                    mixingPositivePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                    mixingPositivePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    mixingPositivePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                    mixingPositivePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(false);
                }

                imageNum++;
            }
        }

        for(int i = imageNum; i < mixingPositivePosition.childCount; i++)
        {
            mixingPositivePosition.GetChild(i).gameObject.SetActive(false);
        }

        imageNum = 0;
        for (int i = 0; i < mixingData.medicine.negative.Count; i++)
        {
            if (!mixingData.medicine.negative[i].isHide)
            {
                mixingNegativePosition.GetChild(imageNum).gameObject.SetActive(true);
                mixingNegativePosition.GetChild(imageNum).GetComponent<Image>().sprite = iconSprite[(int)mixingData.medicine.negative[i].effect];

                if (mixingData.medicine.negative[i].power <= -6)
                {
                    mixingNegativePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(true);
                    mixingNegativePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
                }
                else if (mixingData.medicine.negative[i].power <= -4)
                {
                    mixingNegativePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                    mixingNegativePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    mixingNegativePosition.GetChild(imageNum).GetChild(0).gameObject.SetActive(false);
                    mixingNegativePosition.GetChild(imageNum).GetChild(1).gameObject.SetActive(false);
                }

                imageNum++;
            }
        }
        for (int i = imageNum; i < mixingNegativePosition.childCount; i++)
        {
            mixingNegativePosition.GetChild(i).gameObject.SetActive(false);
        }
    }
}
