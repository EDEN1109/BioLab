using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNApart : InteractionalObjects
{
    [HideInInspector] public DNAStruct DNAStruct;
    private int effectSize = System.Enum.GetValues(typeof(Effect)).Length;
    private List<DNAEffectStruct> positive = new List<DNAEffectStruct>();
    private List<DNAEffectStruct> negative = new List<DNAEffectStruct>();
    private Carrier carrier;
    private PlayerData playerData;
    private Nanobot nanobot;
    private DNAMixingPanel mixingPanel;
    private float criticalAgePercent = 10f;
    private float hidePercent = 15f;
    private bool isUsable = false;
    private bool isLoaded = false;
    private bool isInSphere = false;
    private bool isSelected = false;
    private bool hideEffect = false;
    private int dnaSize;
    private int criticalAge = 0;
    private int unknownNum = 0;

    public bool IsUsable { get => isUsable; set => isUsable = value; }
    public bool IsInSphere { get => isInSphere; set => isInSphere = value; }

    // Start is called before the first frame update
    void Start()
    { 
        GetComponent<BoxCollider>().enabled = false;
        carrier = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<Carrier>();
        playerData = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
        nanobot = GameObject.FindGameObjectWithTag("Nanobot").GetComponent<Nanobot>();
        dnaSize = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsable)
        {
            if(!isLoaded)
            {
                if (!GetComponent<BoxCollider>().enabled)
                {
                    SetEffects();
                    SetHide();
                    SetCriticalAge();
                    SetDNAStruct();
                }
                
                if (CheckDisplay() && (nanobot.nanobotUI.DNApart == null || !nanobot.nanobotUI.DNApart.Equals(this.gameObject)))
                {
                    nanobot.nanobotUI.DNApart = this.gameObject;
                    nanobot.nanobotUI.SetPanel(criticalAge, unknownNum, positive, negative);
                }
                else if (!CheckDisplay() && (nanobot.nanobotUI.DNApart != null && nanobot.nanobotUI.DNApart.Equals(this.gameObject)))
                {
                    nanobot.nanobotUI.DNApart = null;
                }
            }

            if(isLoaded && !isInSphere && !IsGrip)
            {
                PositionReset();
            }

            if(!GetComponent<BoxCollider>().enabled)
            {
                GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    private bool CheckDisplay()
    {
        if (GameObject.Equals(this.gameObject, playerData.LookObj))
        {
            if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 10f)
            {
                return true;
            }
        }
        return false;
    }

    private void SetDNAStruct()
    {
        DNAStruct.DNAsize = dnaSize;
        DNAStruct.criticalAge = criticalAge;
        DNAStruct.unknownNum = unknownNum;
        DNAStruct.positive = positive;
        DNAStruct.negative = negative;
    }

    private void SetEffects()
    { 
        int positiveSize = Random.Range(1 + (dnaSize - 1), dnaSize * 2 + 1);
        int negativeSize = Random.Range(1, dnaSize + 1);

        List<int> effectNum = new List<int>();

        for (int i = 0; i < positiveSize + negativeSize; i++)
        {
            int num = Random.Range(0, effectSize);

            if (!effectNum.Contains(num))
            {
                effectNum.Add(num);
            }
            else
            {
                i--;
            }
        }

        for(int i = 0; i < positiveSize + negativeSize; i++)
        {
            DNAEffectStruct effectStruct = new DNAEffectStruct();
            effectStruct.effect = (Effect)effectNum[i];
            effectStruct.isHide = false;

            int min = 1;
            int max = 4;

            if(dnaSize == 3)
            {
                min = 2;
            }
            else if(dnaSize == 1)
            {
                max = 3;
            }

            effectStruct.power = Random.Range(min, max);

            if (i < positiveSize)
            {
                positive.Add(effectStruct);
            }
            else
            {
                negative.Add(effectStruct);
            }
        }
    }

    private void SetHide()
    {
        for(int i = 0; i < positive.Count + negative.Count; i++)
        {
            int hideNum = Random.Range(0, 101);

            if(hideNum < hidePercent)
            {
                if(i < positive.Count)
                {
                    DNAEffectStruct key = positive[i];
                    key.isHide = true;
                    positive[i] = key;
                }
                else
                {
                    DNAEffectStruct key = negative[i - positive.Count];
                    key.isHide = true;
                    negative[i - positive.Count] = key;
                }
                unknownNum++;
            }
        }
    }

    private void SetCriticalAge()
    {
        int criticalNum = Random.Range(1, 101);

        if (criticalNum < criticalAgePercent)
        {
            criticalAge = Random.Range(1, 9) * 10;
        }
    }

    private void PrintAllEffect()
    {
        for (int i = 0; i < positive.Count + negative.Count; i++)
        {
            if (i < positive.Count)
            {
                DNAEffectStruct key = positive[i];
                print("Effect = " + key.effect + ", isHide = " + key.isHide + ", Power = " + key.power);
            }
            else
            {
                DNAEffectStruct key = negative[i];
                print("Effect = " + key.effect + ", isHide = " + key.isHide + ", Power = " + key.power);
            }
        }
    }

    private void PositionReset()
    {
        transform.position = transform.parent.position;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void LoadDNA(int dnaSize, int criticalAge, int unknownNum, List<DNAEffectStruct> positive, List<DNAEffectStruct> negative, DNAMixingPanel mixingPanel)
    {
        this.dnaSize = dnaSize;
        this.criticalAge = criticalAge;
        this.unknownNum = unknownNum;
        this.positive = positive;
        this.negative = negative;
        this.mixingPanel = mixingPanel;

        isLoaded = true;
        isUsable = true;
        SetDNAStruct();
    }

    public override void OnGripStart()
    {
        base.OnGripStart();
        if(isLoaded)
        {
            mixingPanel.SetDNAInfo(DNAStruct);
        }
    }

    public override void OnGripStaying()
    {
        base.OnGripStaying();
        if((playerData.LeftHandGrabObject() == gameObject && playerData.LeftTriggerPress()) || (playerData.RightHandGrabObject() == gameObject && playerData.RightTriggerPress()))
        {
            if(!isLoaded && carrier.InputDNA(DNAStruct))
            {
                Destroy(this.gameObject);
            }
        }
    }

    public override void OnGripEnd()
    {
        base.OnGripEnd();
        if(isLoaded)
        {
            mixingPanel.SetMixingStation();
        }
    }
}
