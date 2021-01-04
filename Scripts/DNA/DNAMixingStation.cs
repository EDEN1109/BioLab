using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNAMixingStation : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoints;
    [SerializeField]
    private GameObject selectSphere;
    [SerializeField]
    private GameObject[] DNAPrefabs = new GameObject[3];
    [SerializeField]
    private DNAMixingPanel mixingPanel;
    [SerializeField]
    private CartridgeMaker cartridgeMaker;
    [SerializeField]
    private MouseCage mouseCage;
    
    private DNAMixingData mixingData;
    private List<GameObject> DNAObj = new List<GameObject>();
    private List<DNAStruct> DNAs = new List<DNAStruct>();
    private Carrier carrier;
    private DNASave DNALoad;

    // Start is called before the first frame update
    void Start()
    {
        DNALoad = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<DNASave>();
        carrier = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<Carrier>();
        DNAs = DNALoad.LoadDNAData("DNA");
        DNALoad.DeleteDNAData("DNA");
        mixingData = GetComponent<DNAMixingData>();

        SpawnDNAs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnDNAs()
    {
        int i = 0;

        foreach(var item in DNAs)
        {
            DNAObj.Add(Instantiate(DNAPrefabs[item.DNAsize - 1], spawnPoints.GetChild(i).transform));
            DNApart dna = DNAObj[DNAObj.Count - 1].GetComponent<DNApart>();
            DNAObj[DNAObj.Count - 1].transform.parent = spawnPoints.GetChild(i);
            dna.LoadDNA(item.DNAsize, item.criticalAge, item.unknownNum, item.positive, item.negative, mixingPanel);

            i++;
        }
    }

    private void DestroyAllDNA()
    {
        for(int i = DNAObj.Count - 1; i >= 0; i--)
        {
            Destroy(DNAObj[i]);
        }
        DNAObj.Clear();
    }

    public void OnClickMakeMedicine()
    {
        if(mixingData.MakeMedicine())
        {
            // 약만들기 성공
            cartridgeMaker.SpawnCartridge(mixingData.medicine);
            DestroyAllDNA();
            mixingData.InitMixing();
            carrier.IncreaseInitialAmount();

            mouseCage.Animator.SetTrigger("FeelGood");
        }
        else
        {
            // 실패
            mouseCage.Animator.SetTrigger("Death");
        }
        mixingPanel.SetMixingStation();
    }
}
