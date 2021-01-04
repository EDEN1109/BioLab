using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CultureWaterSceneMgr : SceneMgr
{
    [SerializeField] private GameObject nanobot;
    [SerializeField] private GameObject[] DNAPrefabs = new GameObject[3];
    private const int DNALimit = 60;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        playerCarrier.InitAmount();
        SpawnDNAs();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckSceneMove();
    }

    private void SpawnDNAs()
    {
        Vector3 spawnPosition;
        Vector3 spawnRotation;
        int spawnNum;

        for (int i = 0; i < DNALimit; i++)
        {
            spawnPosition.x = Random.Range(-50f, 50f);
            spawnPosition.y = Random.Range(-3f, 3f);
            spawnPosition.z = Random.Range(-50f, 50f);
            spawnRotation.x = Random.Range(0f, 360f);
            spawnRotation.y = Random.Range(0f, 360f);
            spawnRotation.z = Random.Range(0f, 360f);

            spawnNum = Random.Range(0, DNAPrefabs.Length);

            Instantiate(DNAPrefabs[spawnNum], spawnPosition, Quaternion.Euler(spawnRotation));
        }
    }

    private void CheckSceneMove()
    {
        if(playerData.LeftGripPress() && playerData.LeftTriggerPress() && playerData.RightGripPress() && playerData.RightTriggerPress())
        {
            if (nanobot != null)
            {
                Destroy(nanobot);
                playerCarrier.SetNanobotLeftAmountNull();
                playerCarrier.SaveCarrier();
                SceneLoader.Instance.LoadScene("Lab");
                DontDestroyOnLoad(playerDataMgr);
            }
        }
    }
}
