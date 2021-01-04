using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrobeList : MonoBehaviour
{
    public List<MicrobeStruct> microbeList = new List<MicrobeStruct>();
    private int microbesNum = System.Enum.GetValues(typeof(Microbes)).Length;

    [SerializeField]
    private Sprite[] sprites = new Sprite[System.Enum.GetValues(typeof(Microbes)).Length];
    private List<bool> isGiven = new List<bool>();

    void Awake()
    {
        microbeList.Add(new MicrobeStruct(Microbes.Penicillium, "노타툼, 크리소게늄 등의 종은 항생물질인 페니실린을 생성할 수 있어 의약품의 재료가 된다.",
        sprites[(int)Microbes.Penicillium], 10, 30, 2, 5, 150, 480));
        isGiven.Add(false);
        microbeList.Add(new MicrobeStruct(Microbes.Diastase, "디아스테이스는 탄수화물을 분해하는 알파(α)-아밀레이스, 베타(β)-아밀레이스, 감마(γ)-아밀레이스를 통칭한다.",
        sprites[(int)Microbes.Diastase], 25, 35, 4, 10, 230, 500));
        isGiven.Add(false);
        microbeList.Add(new MicrobeStruct(Microbes.AspergillusNiger, "여러 가지 물질로부터 영양분을 섭취하여 성장할 수 있으며, 이 중에는 공업에 이용되는 것이 많다.",
        sprites[(int)Microbes.AspergillusNiger], -10, 50, 1, 15, 100, 1000));
        isGiven.Add(false);
        microbeList.Add(new MicrobeStruct(Microbes.EscherichiaColi, "이로운 대장균은 비타민 K2등을 생산하고, 병의 원인이 되는 박테리아의 번식을 막기도 한다.",
        sprites[(int)Microbes.EscherichiaColi], 20, 40, 2, 8, 40, 400));
        isGiven.Add(false);
        microbeList.Add(new MicrobeStruct(Microbes.Actinobacteria, "몸은 실 모양이고 가지를 내기도 하며, 다른 세균류와 달리 외생 포자를 만들기도 한다.",
        sprites[(int)Microbes.Actinobacteria], -10, 45, 1, 15, 80, 700));
        isGiven.Add(false);
        microbeList.Add(new MicrobeStruct(Microbes.Lactobacillus, "일부 유산균은 소화기관이나 질 안에서 다른 병원미생물로부터 몸을 지키고, 항상성 유지를 돕는다.",
        sprites[(int)Microbes.Lactobacillus], 30, 36, 5, 10, 180, 300));
        isGiven.Add(false);
    }

    public MicrobeStruct GetMicrobeData()
    {
        for (int i = 0; i < microbeList.Count; i++)
        {
            if(!isGiven[i])
            {
                isGiven[i] = true;
                return microbeList[i];
            }
        }
        return new MicrobeStruct();
    }
}