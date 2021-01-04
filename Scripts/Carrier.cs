using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carrier : MonoBehaviour
{
    private PlayerData playerData;
    private DNASave DNASave;
    private List<DNAStruct> DNAs = new List<DNAStruct> ();
    private int initialAmount = 5;
    private int amount;
    private int nowInNum = 0;
    private Text nanobotLeftAmount;
    private bool isSetNanobotUI = false;

    // Start is called before the first frame update
    void Awake()
    {
        amount = initialAmount;
        DNASave = GetComponent<DNASave>();
        playerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerData.SceneName == "CultureWater")
        {
            CheckInputNum();
        }
    }
    
    private void CheckInputNum()
    {
        while(DNAs.Count > amount)
        {
            DNAs.RemoveAt(DNAs.Count - 1);
            nowInNum--;
        }
    }

    private void SetNanobotLeftAmount()
    {
        if(isSetNanobotUI)
        {
            nanobotLeftAmount.text = (amount - nowInNum).ToString();
        }
    }

    public void IncreaseInitialAmount()
    {
        initialAmount += 2;
    }

    public void DecreaseAmount()
    {
        if(amount > 0)
        {
            amount--;
            SetNanobotLeftAmount();
            if (amount < nowInNum)
            {
                nowInNum -= DNAs[DNAs.Count - 1].DNAsize;
                DNAs.RemoveAt(DNAs.Count - 1);
            }
        }
    }
    
    public bool InputDNA(DNAStruct DNAStruct)
    {
        if(playerData.SceneName != "CultureWater")
        {
            return false;
        }

        if(amount >= nowInNum + DNAStruct.DNAsize)
        {
            nowInNum += DNAStruct.DNAsize;
            SetNanobotLeftAmount();
            DNAs.Add(DNAStruct);
            return true;
        }
        return false;
    }

    public void InitAmount()
    {
        amount = initialAmount;
        nowInNum = 0;
        DNAs.Clear();
    }

    public void SaveCarrier()
    {
        DNASave.SaveDNAData(DNAs, "DNA");
    }

    public void SetNanobotLeftAmountNull()
    {
        isSetNanobotUI = false;
        nanobotLeftAmount = null;
    }

    public void SetNanobotUIPanel(NanobotUIPanel nanobotUI)
    {
        nanobotLeftAmount = nanobotUI.leftCountText;
        isSetNanobotUI = true;
        nanobotLeftAmount.text = (amount - nowInNum).ToString();
    }
}
