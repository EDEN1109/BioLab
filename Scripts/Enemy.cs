using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private MeshRenderer mesh;
    private Transform nanobot;
    private float moveSpeed = 5f;
    private float health = 100f;
    private float healthDownSpeed = 40f;
    private bool isDie = false;

    public bool IsDie { set => isDie = value; }

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        nanobot = GameObject.Find("Nanobot").transform;
        mesh.material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNanobot();
        CheckDie();
    }

    private void CheckDie()
    {
        if(isDie)
        {
            Destroy(this.gameObject);
        }
    }

    private void MoveToNanobot()
    {
        transform.LookAt(nanobot);
        transform.position = Vector3.MoveTowards(transform.position, nanobot.position, Time.smoothDeltaTime * moveSpeed);
    }

    public void OnRayAttack()
    {
        health -= Time.deltaTime * healthDownSpeed;

        float colorLevel = health / 100f;
        mesh.material.color = new Color(colorLevel, colorLevel, colorLevel, 1f);

        if (health < 0)
        {
            isDie = true;
        }
    }
}
