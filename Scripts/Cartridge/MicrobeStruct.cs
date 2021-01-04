using System.Collections.Generic;

public struct MicrobeStruct
{
    public Microbes microbeName;
    public string microbeInfo;
    public UnityEngine.Sprite microbeSprite;
    public int minTemp;
    public int maxTemp;
    public int minTime;
    public int maxTime;
    public int minFuel;
    public int maxFuel;

    public MicrobeStruct(Microbes microbeName, string microbeInfo, UnityEngine.Sprite microbeSprite, int minTemp, int maxTemp,
        int minTime, int maxTime, int minFuel, int maxFuel)
    {
        this.microbeName = microbeName;
        this.microbeInfo = microbeInfo;
        this.microbeSprite = microbeSprite;
        this.minTemp = minTemp;
        this.maxTemp = maxTemp;
        this.minTime = minTime;
        this.maxTime = maxTime;
        this.minFuel = minFuel;
        this.maxFuel = maxFuel;
    }
};

public enum Microbes
{
    Penicillium,
    Diastase,
    AspergillusNiger,
    EscherichiaColi,
    Actinobacteria,
    Lactobacillus
}

public enum Microbes_Kr
{
    푸른곰팡이,
    다이아스테이스,
    누룩균,
    대장균,
    방선균,
    유산균
}
