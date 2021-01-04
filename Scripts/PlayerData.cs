using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Brinicle;

[RequireComponent(typeof(Carrier))]
[RequireComponent(typeof(DNASave))]
public class PlayerData : MonoBehaviour
{
    private UXController leftHand;
    private UXController rightHand;
    private bool isSetHands;
    private Transform controllerLeftHand;
    private Transform controllerRightHand;
    private Transform mainCamera;

    // 플레이어의 역할이 없으면 N, 테오면 T, 써니면 S, 라스무스면 R이 된다.
    private char playerRoll = 'N';
    private string questTitle;
    private string questObject;
    private Vector3 spawnPos;

    private string sceneName;
    private GameObject lookObj;

    private float leftTime = 600f;
    private bool isStart = false;

    public char PlayerRoll { get => playerRoll; }
    public Vector3 SpawnPos { get => spawnPos; }
    public GameObject LookObj { get => lookObj; }
    public bool IsSetHands { get => isSetHands; }
    public string QuestTitle { get => questTitle; }
    public string QuestObject { get => questObject; }
    public string SceneName { get => sceneName; set => sceneName = value; }
    public float LeftTime { get => leftTime; set => leftTime = value; }
    public bool IsStart { get => isStart; set => isStart = value; }

    private void Awake()
    {
        gameObject.tag = "PlayerData";
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        isSetHands = SetHands();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            lookObj = PlayerRaycast();
            leftTime -= Time.deltaTime;
        }
    }
    private GameObject PlayerRaycast()
    {
        RaycastHit hit;
        Debug.DrawRay(mainCamera.position, mainCamera.forward * 100f, Color.red, 0.3f);
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit))
        {
            return hit.transform.gameObject;
        }
        return null;
    }

    public void SetPlayerRoll(char rollName)
    {
        playerRoll = rollName;
        if (rollName == 'T')
        {
            spawnPos = new Vector3(-3.8f, 0.5f, 0);
            questTitle = "신약 개발";
            questObject = "[유전자 분석/재조합 전문가 테오]\n신종 코로나로 인해 빠르게 죽어가는 인류를 살리기 위해 신종 코로나 백신을 개발하십시오.\n" +
                "◈ 나노봇을 타고 DNA에서 염기서열을 추출\n◈ 염기서열을 조합하여 증상에 맞는 신약을 개발\n";
        }
        else if (rollName == 'S')
        {
            spawnPos = new Vector3(3f, 0.5f, 2.8f);
            questTitle = "물질 생산";
            questObject = "[바이오리엑터 제어 전문가 써니]\n약을 조제하기 위해 필요한 물질을 생산하십시오.\n" +
                "◈ 약 조제에 필요한 물질을 생산\n◈ 물질에 들어가는 효소와 미생물을 확인\n◈ 효소와 미생물에 따라 알맞는 온도와 시간, 연료를 설정";
        }
        else if (rollName == 'R')
        {
            spawnPos = new Vector3(7.5f, 0.5f, 0);
            questTitle = "약 조제와 판매";
            questObject = "[생체 3D프린팅 전문가 라스무스]\n3D프린터로 약을 조제하여 대중에 판매하십시오.\n" +
                "◈ 손님의 상태에 맞는 약 선택\n◈ 손님의 특성과 약의 부작용을 고려하여 용제와 용량 선택\n◈ 프린팅한 약을 손님에게 판매";
        }
    }

    private bool SetHands()
    {
        if (UXController.controllers.Count == 2)
        {
            if (UXController.controllers[0].isLeftHand)
            {
                leftHand = UXController.controllers[0];
                rightHand = UXController.controllers[1];
            }
            else
            {
                leftHand = UXController.controllers[1];
                rightHand = UXController.controllers[0];
            }
            controllerLeftHand = GameObject.FindGameObjectWithTag("LeftHand").transform;
            controllerRightHand = GameObject.FindGameObjectWithTag("RightHand").transform;
            return true;
        }
        return false;
    }

    public void SetSpawnPosition(Vector3 newPos)
    {
        spawnPos = newPos;
    }

    public string GetLeftTimeString()
    {
        string min = "0" + Mathf.FloorToInt(leftTime / 60);
        string sec = (Mathf.FloorToInt(leftTime) % 60).ToString();

        if ((Mathf.FloorToInt(leftTime) % 60) < 10)
        {
            sec = "0" + (Mathf.FloorToInt(leftTime) % 60);
        }

        return min + ":" + sec;
    }

    public Vector3 GetLeftHandRayPoint()
    {
        if(leftHand.interactHitInfo.point != null)
        {
            return leftHand.interactHitInfo.point;
        }
        return controllerLeftHand.forward;
    }

    public Vector3 GetRightHandRayPoint()
    {
        if (rightHand.interactHitInfo.point != null)
        {
            return rightHand.interactHitInfo.point;
        }
        return controllerRightHand.forward;
    }

    public Quaternion GetLeftHandRotation()
    {
        return controllerLeftHand.rotation;
    }
    public Quaternion GetRightHandRotation()
    {
        return controllerRightHand.rotation;
    }
    public bool LeftTriggerPress()
    {
        return leftHand.TriggerButtonPress();
    }
    public bool RightTriggerPress()
    {
        return rightHand.TriggerButtonPress();
    }
    public bool LeftGripPress()
    {
        return leftHand.GripButtonPress();
    }
    public bool RightGripPress()
    {
        return rightHand.GripButtonPress();
    }
    public bool IsLeftHandGrabing()
    {
        return leftHand.isGrabing;
    }
    public bool IsRightHandGrabing()
    {
        return rightHand.isGrabing;
    }
    public GameObject LeftHandGrabObject()
    {
        return leftHand.grabingObject.gameObject;
    }
    public GameObject RightHandGrabObject()
    {
        return rightHand.grabingObject.gameObject;
    }
}
