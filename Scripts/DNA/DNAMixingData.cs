using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAMixingData : MonoBehaviour
{
    public DNAStruct[] DNAStructs = new DNAStruct[5];
    public DNAStruct medicine = new DNAStruct();
    private List<List<bool>> countUnknown = new List<List<bool>>();
    private int difficalty = 0;
    private int criticalAge = 0;
    private int unknownNum = 0;
    // effects에 모든 약물 정보를 받고 power가 1보다 큰 상위 몇개와 0보다 작은 하위 몇개만 medicine의 성분으로 들어감
    private List<DNAEffectStruct> effects = new List<DNAEffectStruct>();
    private int numOfDNA = 0;
    private List<int> effectPower = new List<int>();
    private bool[] isSphereEmpty = new bool[5];


    public bool[] IsSphereEmpty { get => isSphereEmpty; set => isSphereEmpty = value; }
    public int NumOfDNA { get => numOfDNA; set => numOfDNA = value; }

    private void Awake()
    {
        InitMixing();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitMixing()
    {
        effects.Clear();
        countUnknown.Clear();
        effectPower.Clear();
        difficalty = 0;
        criticalAge = 0;
        unknownNum = 0;
        NumOfDNA = 0;

        medicine.DNAsize = difficalty;
        medicine.criticalAge = criticalAge;
        medicine.unknownNum = unknownNum;
        medicine.positive = new List<DNAEffectStruct>();
        medicine.negative = new List<DNAEffectStruct>();

        effects = new List<DNAEffectStruct>();
        for (int i = 0; i < System.Enum.GetValues(typeof(Effect)).Length; i++)
        {
            DNAEffectStruct effect;
            effect.effect = (Effect)i;
            effect.isHide = false;
            effect.power = 0;
            effects.Add(effect);
        }
        for (int i = 0; i < isSphereEmpty.Length; i++)
        {
            DNAStructs[i] = new DNAStruct();
            isSphereEmpty[i] = true;
        }
        for (int i = 0; i < System.Enum.GetValues(typeof(Effect)).Length; i++)
        {
            countUnknown.Add(new List<bool>());
        }
        for (int i = 0; i < System.Enum.GetValues(typeof(Effect)).Length; i++)
        {
            effectPower.Add(0);
        }
    }

    private int SetEffectPower(int numOfSphere, int effectNum, int power)
    {
        if(numOfSphere == 0)
        {
            effectPower[effectNum] += power * 2;
        }
        else
        {
            effectPower[effectNum] += power;
        }

        return effectPower[effectNum];
    }
    private void ReplaceEffectList(List<DNAEffectStruct> effectStructs, DNAEffectStruct effectStruct)
    {
        int min = effectStructs[0].power;
        int minNum = 0;

        for (int i = 0; i < effectStructs.Count; i++)
        {
            if (Mathf.Abs(min) > Mathf.Abs(effectStructs[i].power))
            {
                minNum = i;
                min = effectStructs[i].power;
            }
        }

        if (Mathf.Abs(effectStruct.power) > Mathf.Abs(effectStructs[minNum].power))
        {
            effectStructs[minNum] = effectStruct;
        }
    }

    public bool MakeMedicine()
    {
        bool isSuccess = true;
        int negaPower = 0;

        for(int i = 0; i < medicine.negative.Count; i++)
        {
            negaPower += medicine.negative[i].power;
        }

        if(negaPower < -10 || medicine.DNAsize == 0)
        {
            isSuccess = false;
        }

        return isSuccess && !IsSphereEmpty[0];
    }

    public void SetMedicineData(int numOfSphere)
    {
        if(isSphereEmpty[numOfSphere])
        {
            difficalty -= DNAStructs[numOfSphere].DNAsize;

            for(int i = 0; i < DNAStructs[numOfSphere].positive.Count; i++)
            {
                for(int j = 0; j < effects.Count; j++)
                {
                    if (DNAStructs[numOfSphere].positive[i].effect == effects[j].effect)
                    {
                        DNAEffectStruct effect;
                        effect.effect = effects[j].effect;
                        countUnknown[j].Remove(DNAStructs[numOfSphere].positive[i].isHide);
                        effect.isHide = countUnknown[j].Find(x => x == true);
                        effect.power = SetEffectPower(numOfSphere, j, -DNAStructs[numOfSphere].positive[i].power);
                        effects[j] = effect;
                        break;
                    }
                }
            }

            for (int i = 0; i < DNAStructs[numOfSphere].negative.Count; i++)
            {
                for (int j = 0; j < effects.Count; j++)
                {
                    if (DNAStructs[numOfSphere].negative[i].effect == effects[j].effect)
                    {
                        DNAEffectStruct effect;
                        effect.effect = effects[j].effect;
                        countUnknown[j].Remove(DNAStructs[numOfSphere].negative[i].isHide);
                        effect.isHide = countUnknown[j].Find(x => x == true);
                        effect.power = SetEffectPower(numOfSphere, j, DNAStructs[numOfSphere].negative[i].power);
                        effects[j] = effect;
                        break;
                    }
                }
            }
        }
        else
        {
            difficalty += DNAStructs[numOfSphere].DNAsize;

            for (int i = 0; i < DNAStructs[numOfSphere].positive.Count; i++)
            {
                for (int j = 0; j < effects.Count; j++)
                {
                    if (DNAStructs[numOfSphere].positive[i].effect == effects[j].effect)
                    {
                        DNAEffectStruct effect;
                        effect.effect = effects[j].effect;
                        countUnknown[j].Add(DNAStructs[numOfSphere].positive[i].isHide);
                        effect.isHide = countUnknown[j].Find(x => x == true);
                        effect.power = SetEffectPower(numOfSphere, j, DNAStructs[numOfSphere].positive[i].power);
                        effects[j] = effect;
                        break;
                    }
                }
            }

            for (int i = 0; i < DNAStructs[numOfSphere].negative.Count; i++)
            {
                for (int j = 0; j < effects.Count; j++)
                {
                    if (DNAStructs[numOfSphere].negative[i].effect == effects[j].effect)
                    {
                        DNAEffectStruct effect;
                        effect.effect = effects[j].effect;
                        countUnknown[j].Add(DNAStructs[numOfSphere].negative[i].isHide);
                        effect.isHide = countUnknown[j].Find(x => x == true);
                        effect.power = SetEffectPower(numOfSphere, j, -DNAStructs[numOfSphere].negative[i].power);
                        effects[j] = effect;
                        break;
                    }
                }
            }
        }

        criticalAge = 0;

        for(int i = 0; i < isSphereEmpty.Length; i++)
        {
            if(!isSphereEmpty[i] && DNAStructs[i].criticalAge > 0)
            {
                criticalAge = DNAStructs[i].criticalAge;
                break;
            }
        }

        // medicine에 저장
        List<DNAEffectStruct> positive = new List<DNAEffectStruct>();
        List<DNAEffectStruct> negative = new List<DNAEffectStruct>();

        unknownNum = 0;
        for(int i = 0; i < effects.Count; i++)
        {
            if(effects[i].power >= 1)
            {
                if(positive.Count < 6)
                {
                    if(positive.Count >= 6)
                    {
                        ReplaceEffectList(positive, effects[i]);
                    }
                    else
                    {
                        positive.Add(effects[i]);
                    }
                    if(effects[i].isHide)
                    {
                        unknownNum++;
                    }
                }
            }
            else if(effects[i].power <= -1)
            {
                if (negative.Count < 3)
                {
                    if (negative.Count >= 3)
                    {
                        ReplaceEffectList(negative, effects[i]);
                    }
                    else
                    {
                        negative.Add(effects[i]);
                    }
                    if (effects[i].isHide)
                    {
                        unknownNum++;
                    }
                }
            }
        }

        medicine.DNAsize = difficalty;
        medicine.criticalAge = criticalAge;
        medicine.unknownNum = unknownNum;
        medicine.positive = positive;
        medicine.negative = negative;
    }
}
