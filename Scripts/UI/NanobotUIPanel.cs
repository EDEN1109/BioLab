using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NanobotUIPanel : MonoBehaviour
{
    [Header("DNA Info Panel")]
    [SerializeField] private Text warningText;
    [SerializeField] private Text unknownText;
    [SerializeField] private Transform positivePosition;
    [SerializeField] private Transform negativePosition;
    [SerializeField] private Sprite[] iconSprite = new Sprite[10];
    [SerializeField] private GameObject DNApartPanel;
    [HideInInspector] public GameObject DNApart;

    [Header("Default Info Panel")]
    [SerializeField] private GameObject defaultPanel;
    [SerializeField] private Text leftTimeText;
    public Text leftCountText;

    [Header("UI Moving")]
    [SerializeField] private Transform[] rotateUI;
    [SerializeField] private Transform[] floatingUI;
    private List<Vector2> floatingDir = new List<Vector2>();
    private List<bool> isRotateRight = new List<bool>();
    private List<bool> isMoveToParent = new List<bool>();
    private List<float> rotateSpeed = new List<float>();
    private List<float> rotateTimer = new List<float>();
    private List<float> floatingSpeed = new List<float>();
    private List<float> floatingTimer = new List<float>();

    [Header("Auto Scrolling")]
    [SerializeField] private Scrollbar scroll;
    private float scrollSpeed = 0.1f;
    private float timer = 0f;

    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
        InitRotateUI();
        InitFloatingUI();
    }

    // Update is called once per frame
    void Update()
    {
        RotateUI();
        FloatingUI();
        AutoScrolling();
        CheckActiveDNAPanel();
        leftTimeText.text = playerData.GetLeftTimeString();
    }
    
    private void InitRotateUI()
    {
        for (int i = 0; i < rotateUI.Length; i++)
        {
            int decision = Random.Range(0, 2);
            if (decision == 0)
            {
                isRotateRight.Add(false);
            }
            else
            {
                isRotateRight.Add(true);
            }

            rotateSpeed.Add(Random.Range(4, 8) * 5f);
            rotateTimer.Add(Random.Range(2, 6));
        }
    }

    private void InitFloatingUI()
    {
        for (int i = 0; i < floatingUI.Length; i++)
        {
            isMoveToParent.Add(false);
            floatingDir.Add(new Vector2(Random.value, Random.value));
            floatingSpeed.Add(Random.Range(0.003f, 0.005f));
            floatingTimer.Add(Random.Range(2, 6));
        }
    }
    
    private void RotateUI()
    {
        for (int i = 0; i < rotateUI.Length; i++)
        {
            rotateTimer[i] -= Time.deltaTime;

            if (rotateTimer[i] < 0)
            {
                rotateSpeed[i] = Random.Range(4, 8) * 5f;
                rotateTimer[i] = Random.Range(2, 6);

                isRotateRight[i] = !isRotateRight[i];
            }

            if (isRotateRight[i])
            {
                rotateUI[i].Rotate(Vector3.back * Time.smoothDeltaTime * rotateSpeed[i]);
            }
            else
            {
                rotateUI[i].Rotate(Vector3.forward * Time.smoothDeltaTime * rotateSpeed[i]);
            }
        }
    }

    private void FloatingUI()
    {
        for (int i = 0; i < floatingUI.Length; i++)
        {
            floatingTimer[i] -= Time.deltaTime;

            if (floatingTimer[i] < 0)
            {
                floatingSpeed[i] = Random.Range(0.003f, 0.005f);
                floatingTimer[i] = Random.Range(2, 6);
                floatingDir[i] = new Vector3(Random.value, Random.value, 0);
            }

            if (Vector3.Distance(floatingUI[i].position, floatingUI[i].parent.position) > 0.03f || isMoveToParent[i])
            {
                isMoveToParent[i] = true;
                floatingUI[i].position = Vector3.Lerp(floatingUI[i].position, floatingUI[i].parent.position, Time.smoothDeltaTime);
                if (Vector3.Distance(floatingUI[i].position, floatingUI[i].parent.position) < 0.01f)
                {
                    isMoveToParent[i] = false;
                }
            }
            else
            {
                floatingUI[i].Translate(floatingDir[i] * Time.smoothDeltaTime * floatingSpeed[i]);
            }
        }

    }

    private void AutoScrolling()
    {
        if(DNApartPanel.activeSelf)
        {
            timer += Time.smoothDeltaTime * scrollSpeed;

            if (timer > 1f)
            {
                timer = 0f;
            }
            scroll.value = 1f - timer;
        }
    }

    private void CheckActiveDNAPanel()
    {
        if (DNApart != null && !DNApartPanel.activeSelf)
        {
            DNApartPanel.SetActive(true);
            defaultPanel.SetActive(false);
        }
        else if (DNApart == null && DNApartPanel.activeSelf)
        {
            DNApartPanel.SetActive(false);
            defaultPanel.SetActive(true);
        }
    }

    public void SetPanel(int criticalAge, int unknownNum, List<DNAEffectStruct> positiveWords, List<DNAEffectStruct> negativeWords)
    {
        // Init scroll bar value
        timer = 0f;
        scroll.value = 1f;

        if (criticalAge != 0)
        {
            warningText.text = criticalAge + "대에 치명적";
        }
        else
        {
            warningText.text = "주의사항 없음";
        }

        unknownText.text = "미확인 효과 " + unknownNum + "개";

        int imgNum = 0;
        for (int i = 0; i < positiveWords.Count; i++)
        {
            if (!positiveWords[i].isHide)
            {
                Transform effectSet = positivePosition.GetChild(imgNum);
                Text effectText = effectSet.GetChild(1).GetComponent<Text>();
                effectSet.gameObject.SetActive(true);

                effectText.text = (Effect_Kr)((int)positiveWords[i].effect) + " 치료";
                effectSet.GetChild(0).GetComponent<Image>().sprite = iconSprite[(int)positiveWords[i].effect];

                if (positiveWords[i].power == 2)
                {
                    effectSet.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    effectSet.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    effectText.text += "+";
                }
                else if (positiveWords[i].power == 3)
                {
                    effectSet.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    effectSet.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    effectText.text += "++";
                }
                else
                {
                    effectSet.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    effectSet.GetChild(0).GetChild(1).gameObject.SetActive(false);
                }

                imgNum++;
            }
        }

        for (int i = imgNum; i < positivePosition.childCount; i++)
        {
            positivePosition.GetChild(i).gameObject.SetActive(false);
        }

        imgNum = 0;
        for (int i = 0; i < negativeWords.Count; i++)
        {
            if (!negativeWords[i].isHide)
            {
                Transform effectSet = negativePosition.GetChild(imgNum);
                Text effectText = effectSet.GetChild(1).GetComponent<Text>();
                effectSet.gameObject.SetActive(true);

                effectText.text = (Effect_Kr)((int)negativeWords[i].effect) + " 부작용";
                effectSet.GetChild(0).GetComponent<Image>().sprite = iconSprite[(int)negativeWords[i].effect];

                if (negativeWords[i].power == 2)
                {
                    effectSet.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    effectSet.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    effectText.text += "+";
                }
                else if (negativeWords[i].power == 3)
                {
                    effectSet.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    effectSet.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    effectText.text += "++";
                }
                else
                {
                    effectSet.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    effectSet.GetChild(0).GetChild(1).gameObject.SetActive(false);
                }

                imgNum++;
            }
        }

        for (int i = imgNum; i < negativePosition.childCount; i++)
        {
            negativePosition.GetChild(i).gameObject.SetActive(false);
        }
    }
}
