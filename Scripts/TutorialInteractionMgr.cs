using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialInteractionMgr : MonoBehaviour
{
    [SerializeField] private Transform[] elevators = new Transform[3];
    [SerializeField] private GameObject playerDataMgr;
    private PlayerData playerData;
    private TutorialSceneMgr tutorialMgr;
    private const float elevatorSpeed = 5f;
    private const float elevatorOpenSpeed = 40f;
    private int selectedRollNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        tutorialMgr = transform.GetComponent<TutorialSceneMgr>();
        playerData = playerDataMgr.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorialMgr.IsCall)
        {
            if(elevators[0].position.y < 0)
            {
                ElevatorCall();
            }
            else if(elevators[0].GetChild(1).transform.localRotation.y > 0)
            {
                ElevatorOpen();
            }
        }
    }

    public void OnStartBoxGrip()
    {
        if(tutorialMgr.StepNum == 1)
        {
            tutorialMgr.IsGrip = true;
        }
    }
    public void OnStartBoxTrigger()
    {
        if (tutorialMgr.StepNum == 1)
        {
            tutorialMgr.IsTrigger = true;
        }
    }
    public void OnClickElevatorCall()
    {
        if (tutorialMgr.StepNum == 3)
        {
            tutorialMgr.IsCall = true;
        }
    }

    public void OnClickSkip()
    {
        tutorialMgr.StepNum = 4;
        tutorialMgr.IsCall = true;
        tutorialMgr.IsSkip = true;
    }
    public void OnClickRollBtn()
    {
        if(EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable)
        {
            selectedRollNum++;
            string characterName = EventSystem.current.currentSelectedGameObject.name;
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            if(characterName == "Teo")
            {
                playerData.SetPlayerRoll('T');
            }
            else if(characterName == "Sunny")
            {
                playerData.SetPlayerRoll('S');
            }
            else if (characterName == "Rasmus")
            {
                playerData.SetPlayerRoll('R');
            }

            if(selectedRollNum >= 1)
            {
                SceneLoader.Instance.LoadScene("Lab");
                DontDestroyOnLoad(playerDataMgr);
            }
        }
    }
    private void ElevatorOpen()
    {
        elevators[0].GetChild(1).Rotate(Vector3.up * Time.smoothDeltaTime * elevatorOpenSpeed);
        elevators[1].GetChild(1).Rotate(Vector3.up * Time.smoothDeltaTime * elevatorOpenSpeed);
        elevators[2].GetChild(1).Rotate(Vector3.up * Time.smoothDeltaTime * elevatorOpenSpeed);
    }

    private void ElevatorCall()
    {
        elevators[0].Translate(Vector3.up * elevatorSpeed * Time.deltaTime);
        elevators[1].Translate(Vector3.up * elevatorSpeed * Time.deltaTime);
        elevators[2].Translate(Vector3.up * elevatorSpeed * Time.deltaTime);
    }
}
