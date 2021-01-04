using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LabSceneMgr : SceneMgr
{

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        if (playerData != null)
        {
            Brinicle.BrinicleBase.instance.BrinicleBaseReposition(new Vector3(0, 0, 0));
            Brinicle.BrinicleBase.instance.PlayerRepositioning(playerData.SpawnPos);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void OnClickEnter()
    {
        playerData.SetSpawnPosition(player.position);
        SceneLoader.Instance.LoadScene("CultureWater");
        DontDestroyOnLoad(playerDataMgr);
    }
}
