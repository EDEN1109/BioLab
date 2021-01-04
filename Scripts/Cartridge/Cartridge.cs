using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge : InteractionalObjects
{
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    protected int cartridgeName;
    protected string cartridgeInfo;
    protected Sprite spriteImg;
    private bool isMicrobe;

    public bool IsMicrobe { get => isMicrobe; set => isMicrobe = value; }
    public int CartridgeName { get => cartridgeName; }
    public string CartridgeInfo { get => cartridgeInfo; }
    public Sprite SpriteImg { get => spriteImg; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spriteRenderer.sprite = spriteImg;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
