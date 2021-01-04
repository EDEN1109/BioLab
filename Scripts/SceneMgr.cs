using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Brinicle;

public class SceneMgr : MonoBehaviour
{
    protected GameObject playerDataMgr;
    protected Transform player;
    protected PlayerData playerData;
    protected Carrier playerCarrier;

    protected virtual void Awake()
    {
        this.gameObject.tag = "SceneManager";
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetPlayerHeightLimit();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerDataMgr = GameObject.FindGameObjectWithTag("PlayerData");
        playerData = playerDataMgr.GetComponent<PlayerData>();
        playerCarrier = playerDataMgr.GetComponent<Carrier>();
        playerData.SceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void SetPlayerHeightLimit(float height = 2.5f)
    {
        BrinicleBase.instance.playerHeightLimit = height;
    }
}
