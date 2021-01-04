using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InfoPanel : MonoBehaviour
{
    private Transform mainCamera;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.LookAt(mainCamera);
    }

    public abstract void SetPanel(List<string> words);
    public abstract void SetPanel(int criticalAge, int unknownNum, List<DNAEffectStruct> positiveWords, List<DNAEffectStruct> negativeWords);
}
