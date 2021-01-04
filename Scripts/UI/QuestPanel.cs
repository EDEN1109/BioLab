using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] private Text questTitle;
    [SerializeField] private Text questObject;
    private PlayerData playerData;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        questTitle.text = playerData.QuestTitle;
        questObject.text = playerData.QuestObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.rotation = player.rotation;
    }

    public void DestroyPanelSelf()
    {
        playerData.IsStart = true;
        Destroy(this.gameObject, 0.1f);
    }
}
