using System.Collections.Generic;

public struct MatterStruct
{
    public Matters matterName;
    public List<MicrobeStruct> ingredient;
    public int minTemp;
    public int maxTemp;
    public int minTime;
    public int maxTime;
    public int minFuel;
    public int maxFuel;

    public MatterStruct(Matters matterName, List<MicrobeStruct> ingredient)
    {
        this.matterName = matterName;
        this.ingredient = ingredient;
        this.minTemp = 0;
        this.maxTemp = 0;
        this.minTime = 0;
        this.maxTime = 0;
        this.minFuel = 0;
        this.maxFuel = 0;

        minTemp = FindMinValue(0);
        minTime = FindMinValue(1);
        minFuel = FindMinValue(2);
        maxTemp = FindMaxValue(0);
        maxTime = FindMaxValue(1);
        maxFuel = FindMaxValue(2);
    }

    // mode 0, Temp     mode 1, Time    mode 2, Fuel
    private int FindMinValue(int mode)
    {
        int min = int.MinValue;
        for (int i = 0; i < ingredient.Count; i++)
        {
            if (mode == 0 && ingredient[i].minTemp > min)
            {
                min = ingredient[i].minTemp;
            }
            else if(mode == 1 && ingredient[i].minTime > min)
            {
                min = ingredient[i].minTime;
            }
            else if(mode == 2 && ingredient[i].minFuel > min)
            {
                min = ingredient[i].minFuel;
            }
        }
        return min;
    }

    private int FindMaxValue(int mode)
    {
        int max = int.MaxValue;
        for (int i = 0; i < ingredient.Count; i++)
        {
            if (mode == 0 && ingredient[i].maxTemp < max)
            {
                max = ingredient[i].maxTemp;
            }
            else if (mode == 1 && ingredient[i].maxTime < max)
            {
                max = ingredient[i].maxTime;
            }
            else if (mode == 2 && ingredient[i].maxFuel < max)
            {
                max = ingredient[i].maxFuel;
            }
        }
        return max;
    }
}

public enum Matters
{
    Penicillin, // 항생제
    Acetaminophen, // 진통소염제
    Ibuprofen, // 진통소염제
    DibutylSebacate, // 가소제, 알약으로 만들기 위함
    Pseudoephedrine, // 코막힘
    Pancreatin, // 소화
    Simethicone, // 가스 제거제
    Fail = int.MaxValue
}

public enum Matters_Kr
{
    페니실린,
    아세트아미노펜,
    이부프로펜,
    디부칠세바케이트,
    슈도에페드린,
    판크레아틴,
    시메티콘,
    실패 = int.MaxValue
}