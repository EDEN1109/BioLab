using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionalObjects : MyObjects
{
    private bool isRayOver = false;
    private bool isRayTrigger = false;
    private bool isCtrlColl = false;
    private bool isGrip = false;
    private bool isStuck = false;

    public bool IsRayOver { get => isRayOver; }
    public bool IsRayTrigger { get => isRayTrigger; }
    public bool IsCtrlColl { get => isCtrlColl; }
    public bool IsGrip { get => isGrip; }
    public bool IsStuck { get => isStuck; set => isStuck = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnRayOverStart()
    {
        isRayOver = true;
    }
    public virtual void OnRayOverEnd()
    {
        isRayOver = false;    
    }
    public virtual void OnRayTriggerStart()
    {
        isRayTrigger = true;
    }
    public virtual void OnRayTriggerStaying()
    {
        isRayTrigger = true;
    }
    public virtual void OnRayTriggerEnd()
    {
        isRayTrigger = false;
    }
    public virtual void OnCtrlCollStart()
    {
        isCtrlColl = true;
    }

    public virtual void OnCtrlCollEnd()
    {
        isCtrlColl = false;
    }

    public virtual void OnGripStart()
    {
        isGrip = true;
    }

    public virtual void OnGripStaying()
    {
        
    }

    public virtual void OnGripEnd()
    {
        isGrip = false;
    }
}
