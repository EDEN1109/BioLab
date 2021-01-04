using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PandemicStatus : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text TimeText;
    [SerializeField] private Image infectedBar;
    [SerializeField] private Image curedBar;
    [SerializeField] private Image deadBar;
    [SerializeField] private GameObject worldMap;

    private PlayerData playerData;
    private List<int> covids = new List<int>();
    private Image worldColor;
    private const float deadInit = 40f;
    private const float deadAccel = 0.2f;
    private float deadValue = deadInit;
    private float deadTimer = deadInit;
    private float infected;
    private float cured;
    private float dead;
    private int coin;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
        worldColor = worldMap.GetComponent<Image>();
        InitPandemic();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerData.IsStart)
        {
            LeftTimer();
            ItsDeadTime();
        }
    }

    private void ItsDeadTime()
    {
        deadTimer -= Time.deltaTime;

        if(deadTimer < 0f)
        {
            IncreaseDead();
            deadValue -= deadValue * deadAccel;
            deadTimer = deadValue;
        }
    }

    private void InitPandemic()
    {
        infected = 100f;
        cured = 0f;
        dead = 0f;
        coin = 0;
        coinText.text = coin + " 원";

        for(int  i = 0; i < 100; i++)
        {
            covids.Add(i);
        }

        SetBars();
    }

    private void SetWorldMapColor()
    {
        float howGreen = cured / 100f;
        float howBlack = dead / 100f;
        worldColor.color = new Color(1 - howBlack, howGreen, howGreen);
    }

    private void DeleteWorldMapCOVID()
    {
        int rand = Random.Range(0, covids.Count);
        worldMap.transform.GetChild(covids[rand]).gameObject.SetActive(false);
        covids.RemoveAt(rand);
    }

    private void DeadWorldMapCOVID()
    {
        int rand = Random.Range(0, covids.Count);
        worldMap.transform.GetChild(covids[rand]).GetComponent<Image>().color = new Color(0.3f, 0, 0);
        covids.RemoveAt(rand);
    }

    private void SetBars()
    {
        infectedBar.fillAmount = infected / 100f;
        deadBar.fillAmount = dead / 100f;
        curedBar.fillAmount = deadBar.fillAmount + cured / 100f;
    }
    private void LeftTimer()
    {
        if(playerData.LeftTime > 0f)
        {
            TimeText.text = playerData.GetLeftTimeString();
        }
        else
        {
            IncreaseDead();
        }
    }

    public bool IncreaseCured()
    {
        if(infected > 0f)
        {
            cured += 1f;
            infected -= 1f;
            deadValue = deadInit;
            deadTimer = deadValue;

            SetBars();
            DeleteWorldMapCOVID();
            SetWorldMapColor();
            return true;
        }
        return false;
    }

    public bool IncreaseDead()
    {
        if(infected > 0f)
        {
            dead += 1f;
            infected -= 1f;

            SetBars();
            DeadWorldMapCOVID();
            SetWorldMapColor();
            return true;
        }
        return false;
    }

    public void IncreaseCoin()
    {
        coin += 1000;
        coinText.text = coin + " 원";
    }

    public bool DecreaseCoin(int cost)
    {
        if(coin - cost >= 0)
        {
            coin -= cost;
            coinText.text = coin + " 원";
            return true;
        }
        return false;
    }
}
