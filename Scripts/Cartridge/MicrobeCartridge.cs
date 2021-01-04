using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrobeCartridge : Cartridge
{
    private MicrobeList microbeList;
    private MicrobeStruct microbeStruct;
    private float minTemp;
    private float maxTemp;
    private float maxTime;
    private float minTime;
    private float maxFuel;
    private float minFuel;

    public float MinTemp { get => minTemp; }
    public float MaxTemp { get => maxTemp; }
    public float MinTime { get => minTime; }
    public float MaxTime { get => maxTime; }
    public float MinFuel { get => minFuel; }
    public float MaxFuel { get => maxFuel; }

    // Start is called before the first frame update
    protected override void Start()
    {
        microbeList = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MicrobeList>();
        SetMicrobe();
        IsMicrobe = true;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void SetMicrobe()
    {
        microbeStruct = microbeList.GetMicrobeData();
        cartridgeName = (int)microbeStruct.microbeName;
        cartridgeInfo = microbeStruct.microbeInfo;
        spriteImg = microbeStruct.microbeSprite;
        minTemp = microbeStruct.minTemp;
        maxTemp = microbeStruct.maxTemp;
        minTime = microbeStruct.minTime;
        maxTime = microbeStruct.maxTime;
        minFuel = microbeStruct.minFuel;
        maxFuel = microbeStruct.maxFuel;
    }
}
