using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneMgr : SceneMgr
{
    [SerializeField] private Transform step1;
    [SerializeField] private Transform step2;
    [SerializeField] private GameObject praise;
    [SerializeField] private GameObject warning;
    [SerializeField] private Transform front;
    private int stepNum = 0;
    private int step1Size;
    private int step2Size;
    private bool isSkip = false;
    private bool isWarningDone = false;
    private bool isGrip = false;
    private bool isTrigger = false;
    private bool isCall = false;
    private float timer = 0;
    private const float praiseTime = 3f;
    private const float warningTime = 5f;
    private const float tutorialEndTime = 0; // 튜토리얼 종료 후 일정시간 이상 캐릭터를 선택하지 않을 때 외곽 하이라이트
    private Vector3 playerPosition;

    public bool IsGrip { get => isGrip; set => isGrip = value; }
    public bool IsTrigger { get => isTrigger; set => isTrigger = value; }
    public bool IsCall { get => isCall; set => isCall = value; }
    public int StepNum { get => stepNum; set => stepNum = value; }
    public bool IsSkip { get => isSkip; set => isSkip = value; }

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        step1Size = step1.childCount;
        step2Size = step2.childCount;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(isSkip)
        {
            SkipTutorial();
        }
        else if(isWarningDone && (IsPlayerMove() || IsPick() || IsFront() || IsClick()))
        {
            if(GivePraise())
            {
                NextStep();
                SetStepScene();
                timer = 0;
            }
        }
        else if(!isWarningDone)
        {
            Warning();
        }
    }

    private void SkipTutorial()
    {
        warning.SetActive(false);
        praise.SetActive(false);
        step1.gameObject.SetActive(false);
        step2.gameObject.SetActive(true);
        for(int i = 0; i < step2Size; i++)
        {
            if(i != stepNum - step1Size)
            {
                step2.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                step2.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    private void Warning()
    {
        timer += Time.deltaTime;

        if (timer >= warningTime)
        {
            warning.SetActive(false);
            isWarningDone = true;
            playerPosition = player.position;
            timer = 0;
            step1.gameObject.SetActive(true);
        }
    }
    private bool GivePraise()
    {
        timer += Time.deltaTime;
        step1.gameObject.SetActive(false);
        step2.gameObject.SetActive(false);
        praise.SetActive(true);
        if (timer >= praiseTime)
        {
            praise.SetActive(false);
            return true;
        }
        return false;
    }
    private void SetStepScene()
    {
        if(StepNum < step1Size)
        {
            step1.gameObject.SetActive(true);
            for(int i = 0; i < step1Size; i++)
            {
                if(i == StepNum)
                {
                    step1.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    step1.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        else if(StepNum >= step1Size && StepNum < step1Size + step2Size)
        {
            step2.gameObject.SetActive(true);
            for (int i = 0; i < step2Size; i++)
            {
                if (i == StepNum - step1Size)
                {
                    step2.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    step2.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    private void NextStep()
    {
        stepNum++;
    }

    private bool IsPlayerMove()
    {
        if(StepNum == 0 && Vector3.Distance(player.position, playerPosition) > 3f)
        {
            return true;
        }
        return false;
    }
    private bool IsPick()
    {
        if(StepNum == 1 && isGrip && isTrigger)
        {
            return true;
        }
        return false;
    }
    private bool IsFront()
    {
        if (StepNum == 2 && Vector3.Distance(front.position, player.position) < 5f)
        {
            return true;
        }
        return false;
    }
    private bool IsClick()
    {
        if (StepNum == 3 && isCall)
        {
            return true;
        }
        return false;
    }
}
