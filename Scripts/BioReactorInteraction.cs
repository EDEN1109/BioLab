using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BioReactorInteraction : MonoBehaviour
{
    [SerializeField] private Transform[] barrelPos = new Transform[2];
    [SerializeField] private Transform topPos;
    [SerializeField] private Transform status;
    [SerializeField] private Text leftTime;
    [SerializeField] private Text matterText;
    [SerializeField] private Text tempText;
    [SerializeField] private Text timeSetText;
    [SerializeField] private Text fuelText;
    [SerializeField] private Image[] microbeImage = new Image[4];
    private MatterList matterList;
    private Transform barrel;
    private Transform bioTop;
    private Barrel barrelScript;
    private BioTop bioTopScript;
    private bool nowInsert = false;
    private bool isTopOn = false;
    private bool isSelectMatter = false;
    private bool isSet = false;
    private bool nowReaction = false;
    private float timer = 0;
    private int matterNum = 0;
    private int nowStatus = 0;
    private int totalStatus;
    private int temp = 0;
    private int reactionTime = 0;
    private int fuel = 0;

    // Start is called before the first frame update
    void Awake()
    {
        totalStatus = status.childCount;
    }

    void Start()
    {
        matterList = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MatterList>();
    }

    // Update is called once per frame
    void Update()
    {
        StuckObjects(nowInsert, isTopOn);

        CheckStatus();
        if (isSet)
        {
            Reactioning();
        }
        /*
        if (nowStatus == totalStatus - 1 && !isTopOn)
        {
            SpitBarrel();
        }
        */
        
        ChangeText();
    }
    private void SpitBarrel()
    {
        if (nowInsert && !barrelScript.IsGrip)
        {
            if (!barrel.rotation.Equals(barrelPos[0].rotation))
            {
                barrel.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, barrelPos[0].position, Time.smoothDeltaTime, 0));
            }
            if (!barrel.position.Equals(barrelPos[0].position))
            {
                barrel.position = Vector3.MoveTowards(barrel.position, barrelPos[0].position, Time.smoothDeltaTime);
            }
        }
    }
    private void Reactioning()
    {
        timer += Time.deltaTime;
        float nowTime = reactionTime - timer;
        if (nowTime >= 0)
        {
            leftTime.text = "남은 시간\n" + (Mathf.Ceil(nowTime * 100) / 100).ToString() + " 초";
        }
        else
        {
            leftTime.text = "남은 시간\n0 초";
        }
        if (nowTime <= 0 && nowReaction)
        {
            // Barrel에 선택한 Matter과 온도, 시간, 연료 값을 넘겨줌
            barrelScript.BioReaction(matterList.matterList[matterNum], temp, reactionTime, fuel);
            timer = 0;
            temp = 0;
            fuel = 0;
            nowReaction = false;
            nowStatus++;
        }
    }

    // 지금 진행 상황에 맞게 상태를 조절해주는 함수
    private void CheckStatus()
    {
        if (!nowInsert && nowStatus < totalStatus - 1)
        {
            isSelectMatter = false;
            isSet = false;
            nowReaction = false;

            nowStatus = 0;
            timer = 0;
        }
        else if (!isTopOn && nowStatus < totalStatus - 2)
        {
            isSelectMatter = false;
            isSet = false;
            nowReaction = false;

            nowStatus = 1;
            timer = 0;
        }

        if(nowStatus == 0 && nowInsert)
        {
            nowStatus++;
        }
        else if(nowStatus == 1 && isTopOn)
        {
            nowStatus++;
            SetSelectMatter();
        }
        else if (nowStatus == totalStatus - 2 && !isTopOn)
        {
            nowStatus++;
        }
        else if (nowStatus == totalStatus - 1 && !nowInsert)
        {
            InitAll();
        }
    }

    private void InitAll()
    {
        nowStatus = 0;
        matterNum = 0;
        timer = 0;
        temp = 0;
        reactionTime = 0;
        fuel = 0;
        nowInsert = false;
        isTopOn = false;
        isSelectMatter = false;
        isSet = false;
        nowReaction = false;
        tempText.text = temp.ToString() + " 도";
        timeSetText.text = reactionTime.ToString() + " 초";
        fuelText.text = fuel.ToString() + " mL";
    }

    private void SetSelectMatter()
    {
        matterText.text = ((Matters_Kr)matterList.matterList[matterNum].matterName).ToString();

        for(int i = 0; i < microbeImage.Length; i++)
        {
            if(i < matterList.matterList[matterNum].ingredient.Count)
            {
                microbeImage[i].color = new Color(1f, 1f, 1f, 1f);
                microbeImage[i].sprite = matterList.matterList[matterNum].ingredient[i].microbeSprite;
            }
            else
            {
                microbeImage[i].color = new Color(1f, 1f, 1f, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (barrel == null && other.tag == "Barrel")
        {
            barrelScript = other.GetComponent<Barrel>();
            if(!barrelScript.IsStuck && !barrelScript.IsBroken)
            {
                barrelScript.IsStuck = true;
                barrel = other.transform;
                nowInsert = true;
            }
            else
            {
                barrelScript = null;
            }
        }

        if (bioTop == null && other.tag == "BioTop")
        {
            bioTopScript = other.GetComponent<BioTop>();
            if (!bioTopScript.IsStuck)
            {
                bioTopScript.IsStuck = true;
                bioTop = other.transform;
                isTopOn = true;
            }
            else
            {
                bioTopScript = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (barrel != null && other.gameObject.Equals(barrel.gameObject) && other.GetComponent<InteractionalObjects>().IsGrip)
        {
            barrel.GetComponent<Rigidbody>().isKinematic = false;
            barrel.GetComponent<Rigidbody>().WakeUp();

            barrelScript.IsStuck = false;
            barrelScript = null;
            barrel = null;
            nowInsert = false;
        }

        if (bioTop != null && other.gameObject.Equals(bioTop.gameObject) && other.GetComponent<InteractionalObjects>().IsGrip)
        {
            bioTop.GetComponent<Rigidbody>().isKinematic = false;
            bioTop.GetComponent<Rigidbody>().WakeUp();

            bioTopScript.IsStuck = false;
            bioTopScript = null;
            bioTop = null;
            isTopOn = false;
        }
    }

    private void ChangeText()
    {
        for(int i = 0; i < totalStatus; i++)
        {
            if(i == nowStatus)
            {
                status.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                status.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    // 뚜껑 닫혀있는데 배럴 들어갈 수 있는 버그 고치기
    private void StuckObjects(bool nowInsert, bool isTopOn)
    {
        if(nowInsert && !barrelScript.IsGrip)
        {
            if (!barrel.GetComponent<Rigidbody>().isKinematic)
            {
                barrel.GetComponent<Rigidbody>().isKinematic = true;
                barrel.GetComponent<Rigidbody>().WakeUp();
            }

            if (!barrel.rotation.Equals(barrelPos[1].rotation))
            {
                barrel.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, barrelPos[1].position, Time.smoothDeltaTime, 0));
            }
            if (!barrel.position.Equals(barrelPos[1].position))
            {
                barrel.position = Vector3.MoveTowards(barrel.position, barrelPos[1].position, Time.smoothDeltaTime);
            }
        }
        if(isTopOn && !bioTopScript.IsGrip)
        {
            if(!bioTop.GetComponent<Rigidbody>().isKinematic)
            {
                bioTop.GetComponent<Rigidbody>().isKinematic = true;
                bioTop.GetComponent<Rigidbody>().WakeUp();
            }
            if (!bioTop.rotation.Equals(topPos.rotation))
            {
                bioTop.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, topPos.position, Time.smoothDeltaTime, 0));
            }
            if (!bioTop.position.Equals(topPos.position))
            { 
                bioTop.position = Vector3.MoveTowards(bioTop.position, topPos.position, Time.smoothDeltaTime);
            }
        }
    }

    public void OnClickMatter()
    {
        isSelectMatter = true;
        nowStatus++;
    }
    public void OnClickPrevMatter()
    {
        if (matterNum - 1 >= 0)
        {
            matterNum--;
        }
        else
        {
            matterNum = matterList.matterList.Count - 1;
        }
        SetSelectMatter();
    }
    public void OnClickNextMatter()
    {
        if (matterNum + 1 < matterList.matterList.Count)
        {
            matterNum++;
        }
        else
        {
            matterNum = 0;
        }
        SetSelectMatter();
    }
    public void OnClickSet()
    {
        if(reactionTime != 0)
        {
            isSet = true;
            nowReaction = true;
            nowStatus++;
        }
    }
    public void OnClickTempInc()
    {
        temp += 5;
        tempText.text = temp.ToString() + " 도";
    }
    public void OnClickTempDec()
    {
        temp -= 5;
        tempText.text = temp.ToString() + " 도";
    }
    public void OnClickTimeInc()
    {
        reactionTime++;
        timeSetText.text = reactionTime.ToString() + " 초";
    }
    public void OnClickTimeDec()
    {
        if(reactionTime > 0)
        {
            reactionTime--;
            timeSetText.text = reactionTime.ToString() + " 초";
        }
    }
    public void OnClickFuelInc()
    {
        fuel += 100;
        fuelText.text = fuel.ToString() + " mL";
    }
    public void OnClickFuelDec()
    {
        if(fuel > 0)
        {
            fuel -= 100;
            fuelText.text = fuel.ToString() + " mL";
        }
    }
}
