using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DNASave : MonoBehaviour
{
    private string savedDataPath;
    private string dictionaryFullName;

    // Start is called before the first frame update
    void Start()
    {
        savedDataPath = Application.persistentDataPath + "/savedData";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<DNAStruct> LoadDNAData(string dictionaryName)
    {
        /* Save Format Example
         * -----------
         * DNAsize,1
         * CriticalAge,0
         * Positive
         * Chills,true,2
         * Fever,false,1
         * Negative
         * Vomit,false,1
         */
        List<DNAStruct> DNAStruct = new List<DNAStruct>();

        dictionaryFullName = savedDataPath + "/" + dictionaryName;
        Directory.CreateDirectory(savedDataPath);

        if (File.Exists(dictionaryFullName))
        {
            string[] fileContent = File.ReadAllLines(dictionaryFullName);
            bool isPositive = false;
            int DNASize = 0;
            int criticalAge = 0;
            int unknownNum = 0;
            List<DNAEffectStruct> positive = new List<DNAEffectStruct>();
            List<DNAEffectStruct> negative = new List<DNAEffectStruct>();

            foreach (string line in fileContent)
            {
                string[] buffer = line.Split(',');

                if(buffer[0] == "DNAsize")
                {
                    DNASize = int.Parse(buffer[1]);
                }
                else if(buffer[0] == "CriticalAge")
                {
                    criticalAge = int.Parse(buffer[1]);
                }
                else if(buffer[0] == "UnknownNum")
                { 
                    unknownNum = int.Parse(buffer[1]);
                }
                else if(buffer[0] == "Positive")
                {
                    isPositive = true;
                }
                else if(buffer[0] == "Negative")
                {
                    isPositive = false;
                }
                else if(buffer[0] == "END_ITEM")
                {
                    DNAStruct item;
                    item.DNAsize = DNASize;
                    item.criticalAge = criticalAge;
                    item.unknownNum = unknownNum;
                    item.positive = positive;
                    item.negative = negative;

                    DNAStruct.Add(item);

                    positive = new List<DNAEffectStruct>();
                    negative = new List<DNAEffectStruct>();
                }
                else
                {
                    DNAEffectStruct effectStruct;
                    effectStruct.effect = (Effect)Enum.Parse(typeof(Effect), buffer[0]);
                    effectStruct.isHide = bool.Parse(buffer[1]);
                    effectStruct.power = int.Parse(buffer[2]);

                    if(isPositive)
                    {
                        positive.Add(effectStruct);
                    }
                    else
                    {
                        negative.Add(effectStruct);
                    }
                }
            }
            Debug.Log("LOADED DATA from " + dictionaryFullName);
        }
        return DNAStruct;
    }

    public void SaveDNAData(List<DNAStruct> DNAStruct, string dictionaryName)
    {
        dictionaryFullName = savedDataPath + "/" + dictionaryName;
        Directory.CreateDirectory(savedDataPath);

        if (DNAStruct != null)
        {
            string fileContent = "";

            foreach (var item in DNAStruct)
            {
                fileContent += "DNAsize," + item.DNAsize + "\n";
                fileContent += "CriticalAge," + item.criticalAge + "\n";
                fileContent += "UnknownNum," + item.unknownNum + "\n";
                fileContent += "Positive\n";
                foreach (var effect in item.positive)
                {
                    fileContent += effect.effect + "," + effect.isHide + "," + effect.power + "\n";
                }
                fileContent += "Negative\n";
                foreach (var effect in item.negative)
                {
                    fileContent += effect.effect + "," + effect.isHide + "," + effect.power + "\n";
                }
                fileContent += "END_ITEM\n";
            }

            File.WriteAllText(dictionaryFullName, fileContent);

            Debug.Log("SAVED DATA in " + dictionaryFullName);
        }
    }

    public void DeleteDNAData(string dictionaryName)
    {
        dictionaryFullName = savedDataPath + "/" + dictionaryName;

        if (File.Exists(dictionaryFullName))
        {
            File.Delete(dictionaryFullName);
        }
    }
}