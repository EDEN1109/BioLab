using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buble : MonoBehaviour
{
    private const float cuttingTime = 0.1f;
    private MeshRenderer mesh;
    private float timer = 0;
    private bool isDone = false;

    public bool IsDone { get => isDone; }

    // Start is called before the first frame update
    void Awake()
    {
        mesh = transform.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnRayCutting()
    {
        if (!isDone)
        {
            timer += Time.deltaTime;

            float howRed = timer / cuttingTime;
            mesh.material.color = new Color(1 - howRed, howRed, 0, 0.5f);

            if (timer >= cuttingTime)
            {
                isDone = true;
            }
        }
    }
}
