using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatterList : MonoBehaviour
{
    private MicrobeList microbeList;
    public List<MatterStruct> matterList = new List<MatterStruct>();

    // Start is called before the first frame update
    void Awake()
    {
        microbeList = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MicrobeList>();
        matterList.Add(new MatterStruct(Matters.Acetaminophen, new List<MicrobeStruct> { microbeList.microbeList[4] }));
        matterList.Add(new MatterStruct(Matters.Penicillin, new List<MicrobeStruct> { microbeList.microbeList[0], microbeList.microbeList[2] }));
        matterList.Add(new MatterStruct(Matters.Ibuprofen, new List<MicrobeStruct> { microbeList.microbeList[0], microbeList.microbeList[1] }));
        matterList.Add(new MatterStruct(Matters.DibutylSebacate, new List<MicrobeStruct> { microbeList.microbeList[3], microbeList.microbeList[4] }));
        matterList.Add(new MatterStruct(Matters.Pseudoephedrine, new List<MicrobeStruct> { microbeList.microbeList[0], microbeList.microbeList[5] }));
        matterList.Add(new MatterStruct(Matters.Pancreatin, new List<MicrobeStruct> { microbeList.microbeList[1], microbeList.microbeList[2], microbeList.microbeList[3] }));
        matterList.Add(new MatterStruct(Matters.Simethicone, new List<MicrobeStruct> { microbeList.microbeList[1], microbeList.microbeList[2], microbeList.microbeList[3], microbeList.microbeList[5] }));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
