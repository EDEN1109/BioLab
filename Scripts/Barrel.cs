using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : InteractionalObjects
{
    [SerializeField]
    private Mesh brokenGlassMesh;
    [SerializeField]
    private Material[] brokenMaterial = new Material[2];
    private bool isBioreaction = false;
    private bool isBroken = false;
    private MatterStruct matter;

    public bool IsBioreaction { get => isBioreaction; set => isBioreaction = value; }
    public bool IsBroken { get => isBroken; }
    public MatterStruct Matter { get => matter; set => matter = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 2f)
        {
            Broken();
        }
    }

    private void Broken()
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = brokenMaterial[0];
        transform.GetChild(1).GetComponent<MeshRenderer>().material = brokenMaterial[1];
        transform.GetChild(1).GetComponent<MeshFilter>().mesh = brokenGlassMesh;
        isBroken = true;

        matter.matterName = Matters.Fail;
    }

    public void BioReaction(MatterStruct matter, int temp, int reactionTime, int fuel)
    {
        this.matter = matter;
        isBioreaction = true;
        if (matter.minTemp > temp || matter.maxTemp < temp || matter.minTime > reactionTime || matter.maxTime < reactionTime
            || matter.minFuel > fuel || matter.maxFuel < fuel)
        {
            Broken();
        }
    }
}
