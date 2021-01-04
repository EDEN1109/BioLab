using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct MedicineStruct
{
    public Medicines medicineName;
    public string medicineInfo;
    public UnityEngine.Sprite medicineSprite;
    public UnityEngine.GameObject medicinePrefab;
    public List<Matters> ingredient;
    public List<int> ingredientNum;
    public Matters mainMatter;
    public int criticalAge;
    public List<DNAEffectStruct> positive;
    public List<DNAEffectStruct> negative;

    public MedicineStruct(Medicines medicineName, string medicineInfo, UnityEngine.Sprite medicineSprite, UnityEngine.GameObject medicinePrefab, 
        List<Matters> ingredient, int criticalAge, List<DNAEffectStruct> positive, List<DNAEffectStruct> negative)
    {
        this.medicineName = medicineName;
        this.medicineInfo = medicineInfo;
        this.medicineSprite = medicineSprite;
        this.medicinePrefab = medicinePrefab;
        this.criticalAge = criticalAge;
        this.positive = positive;
        this.negative = negative;

        for(int i = 0; i < positive.Count; i++)
        {
            DNAEffectStruct key = positive[i];
            key.isHide = false;
            positive[i] = key;
        }
        for (int i = 0; i < negative.Count; i++)
        {
            DNAEffectStruct key = negative[i];
            key.isHide = false;
            negative[i] = key;
        }

        // Matters의 중복 개수 계산
        List<Matters> list = ingredient;
        this.ingredient = list.Distinct().ToList();
        List<int> counts = new List<int>();

        for(int i = 0; i < list.Distinct().ToList().Count; i++)
        {
            counts.Add(list.FindAll(x => x == list.Distinct().ToList()[i]).Count);
        }
        this.ingredientNum = counts;

        // 가장 많이 들어가는 Matter 계산
        int maxIndex = -1;
        int max = int.MinValue;
        for(int i = 0; i < counts.Count; i++)
        {
            if(max < counts[i])
            {
                max = counts[i];
                maxIndex = i;
            }
        }

        this.mainMatter = list.Distinct().ToList()[maxIndex];
    }
}

public enum Medicines
{
    Tylenol, // 진통제
    Penicillin, // 항생제
    Benachio, // 소화약
    Remdesivir, // 렘데시비르, 항바이러스제
    Festal, // 소화제
    Tamiflu, // 항바이러스제(인플루엔자)
    Zovirax // 항바이러스제(HSV, 수두)
}

public enum Medicines_Kr
{
    타이레놀,
    페니실린,
    베나치오,
    렘데시비르,
    훼스탈,
    타미플루,
    조비락스
}
