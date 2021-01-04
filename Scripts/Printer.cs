using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField]
    private PrinterSlot[] slots = new PrinterSlot[4];
    [SerializeField]
    private RasmusPanel rasmus;
    [SerializeField]
    private GameObject pill;
    [SerializeField]
    private GameObject failStuff;
    [SerializeField]
    private Transform printPoint;
    private bool[] isSlotFull = new bool[4];
    private bool isSet = false;
    private List<Matters> matters;
    private List<int> matterNum;


    // Start is called before the first frame update
    void Start()
    {
        AllSlotEmpty();
    }

    // Update is called once per frame
    void Update()
    {
        AllSlotStatus();
    }

    private void AllSlotEmpty()
    {
        for (int i = 0; i < isSlotFull.Length; i++)
        {
            isSlotFull[i] = false;
        }
    }

    private void AllSlotStatus()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].NowInsert)
            {
                isSlotFull[i] = true;
            }
            else
            {
                isSlotFull[i] = false;
            }
        }
    }

    private bool CheckAnySlotInsert()
    {
        for (int i = 0; i < isSlotFull.Length; i++)
        {
            if(isSlotFull[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    private void AllSlotBarrelEmpty()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].EmptyBarrel();
        }
    }

    private bool CheckMattersMatch()
    {
        List<Matters> ingredient = new List<Matters>();
        List<int> ingredientNum = new List<int>();

        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].NowInsert)
            {
                ingredient.Add(slots[i].Matter);
            }
        }

        for (int i = 0; i < ingredient.Distinct().ToList().Count; i++)
        {
            ingredientNum.Add(ingredient.FindAll(x => x == ingredient.Distinct().ToList()[i]).Count);
        }
        ingredient = ingredient.Distinct().ToList();

        for (int i = 0; i < matters.Count; i++)
        {
            int index = ingredient.IndexOf(matters[i]);
            if (index < 0 || ingredientNum[index] != matterNum[i])
            {
                return false;
            }
        }

        matters.Clear();
        matterNum.Clear();

        return true;
    }

    public void SetPrinterMatters(List<Matters> ingredient, List<int> ingredientNum)
    {
        matters = ingredient;
        matterNum = ingredientNum;
        isSet = true;
    }

    public void OnClickPrint()
    {
        if (CheckAnySlotInsert() && isSet)
        {
            if(CheckMattersMatch())
            {
                Instantiate(pill, printPoint.position, printPoint.rotation);
            }
            else
            {
                Instantiate(failStuff, printPoint.position, printPoint.rotation);
            }
            isSet = false;
            rasmus.InitPanel();
            AllSlotBarrelEmpty();
        }
    }
}
