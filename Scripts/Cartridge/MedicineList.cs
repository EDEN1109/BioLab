using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineList : MonoBehaviour
{
    public List<MedicineStruct> medicineList = new List<MedicineStruct>();
    private int givenNum = 0;
    private int medicineNum = System.Enum.GetValues(typeof(Medicines)).Length;

    [SerializeField]
    private Sprite[] sprites = new Sprite[System.Enum.GetValues(typeof(Medicines)).Length];
    [SerializeField]
    private GameObject[] medicinePrefab = new GameObject[System.Enum.GetValues(typeof(Medicines)).Length];
    [SerializeField]
    private RasmusPanel rasmusPanel;
    private List<bool> isGiven = new List<bool>();

    public int GivenNum { get => givenNum; }

    // 각 약의 Matters 설정, 싱글톤으로 prefab관련 클래스 만들고, mainMatter에 맞는 prefab 할당
    void Awake()
    {
        medicineList.Add(new MedicineStruct(Medicines.Tylenol, "대표적인 일반진통제/해열진통제로 1878년 유럽의 화학서에 그 존재가 처음 기록되었다.",
        sprites[(int)Medicines.Tylenol], medicinePrefab[(int)Medicines.Tylenol], new List<Matters> (new Matters[] { Matters.Acetaminophen }), 0,
        new List<DNAEffectStruct> ( new DNAEffectStruct[] { new DNAEffectStruct(Effect.Fever, false, 2), new DNAEffectStruct(Effect.Headache, false, 1) }), new List<DNAEffectStruct>(new DNAEffectStruct[] { new DNAEffectStruct(Effect.Fatigue, false, 1) })));
        isGiven.Add(false);

        medicineList.Add(new MedicineStruct(Medicines.Penicillin, "1928년, 영국의 생물학자 알렉산더 플레밍이 포도상구균을 배양하는 과정에서 실수하여 우연히 발견되었다.",
        sprites[(int)Medicines.Penicillin], medicinePrefab[(int)Medicines.Penicillin], new List<Matters>(new Matters[] { Matters.Penicillin, Matters.Penicillin }),
        0, new List<DNAEffectStruct>(), new List<DNAEffectStruct>()));
        isGiven.Add(false);

        medicineList.Add(new MedicineStruct(Medicines.Benachio, "2009년 발매된 소화제로 하루 세 번 복용한다.",
        sprites[(int)Medicines.Benachio], medicinePrefab[(int)Medicines.Benachio], new List<Matters>(new Matters[] { Matters.Acetaminophen }),
        0, new List<DNAEffectStruct>(), new List<DNAEffectStruct>()));
        isGiven.Add(false);

        medicineList.Add(new MedicineStruct(Medicines.Remdesivir, "코로나-19의 치료제로 주목받은 약으로 에볼라 치료제로 개발되었었다.",
        sprites[(int)Medicines.Remdesivir], medicinePrefab[(int)Medicines.Remdesivir], new List<Matters>(new Matters[] { Matters.Acetaminophen }),
        0, new List<DNAEffectStruct>(), new List<DNAEffectStruct>()));
        isGiven.Add(false);

        medicineList.Add(new MedicineStruct(Medicines.Festal, "밥과 국수 등 탄수화물로 인한 소화불량에 뛰어난 효과를 보인다.",
        sprites[(int)Medicines.Festal], medicinePrefab[(int)Medicines.Festal], new List<Matters>(new Matters[] { Matters.Acetaminophen }),
        0, new List<DNAEffectStruct>(), new List<DNAEffectStruct>()));
        isGiven.Add(false);

        medicineList.Add(new MedicineStruct(Medicines.Tamiflu, "1996년 미국 제약회사 길리어드 사이언스가 개발한 인플루엔자 치료제이다.",
        sprites[(int)Medicines.Tamiflu], medicinePrefab[(int)Medicines.Tamiflu], new List<Matters>(new Matters[] { Matters.Acetaminophen }),
        0, new List<DNAEffectStruct>(), new List<DNAEffectStruct>()));
        isGiven.Add(false);

        medicineList.Add(new MedicineStruct(Medicines.Zovirax, "다양한 용제로 출시된 HSV와 수두 바이러스에 탁월한 효과가 있는 항바이러스 약이다.",
        sprites[(int)Medicines.Zovirax], medicinePrefab[(int)Medicines.Zovirax], new List<Matters>(new Matters[] { Matters.Acetaminophen }),
        0, new List<DNAEffectStruct>(), new List<DNAEffectStruct>()));
        isGiven.Add(false);
    }

    void Start()
    {
        
    }

    // 복잡도 단계별, 효과별 medicineList 만들어서 dnaStruct의 조건에 따라 반환하기
    public MedicineStruct GetMedicineData(DNAStruct dnaStruct)
    {
        if(givenNum < medicineList.Count)
        {
            isGiven[givenNum] = true;

            MedicineStruct key = medicineList[givenNum];
            key.criticalAge = dnaStruct.criticalAge;
            key.positive = dnaStruct.positive;
            key.negative = dnaStruct.negative;

            medicineList[givenNum] = key;

            rasmusPanel.SetMedicineList(medicineList[givenNum]);
            givenNum++;

            return medicineList[givenNum - 1];
        }

        return new MedicineStruct();
    }

    public MedicineStruct GetFirstMedicineData()
    {
        isGiven[0] = true;
        rasmusPanel.SetMedicineList(medicineList[0]);
        givenNum++;

        return medicineList[0];
    }
}
