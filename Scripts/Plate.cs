using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField]
    private PandemicStatus pandemicStatus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Medicine" && pandemicStatus.IncreaseCured())
        {
            pandemicStatus.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }
}
