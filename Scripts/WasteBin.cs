using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteBin : MonoBehaviour
{
    private List<GameObject> barrel = new List<GameObject>();

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
        if(other.tag == "Barrel")
        {
            barrel.Add(other.gameObject);
            Invoke("DestroyBarrel", 5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        int barrelIndex = barrel.IndexOf(other.gameObject);

        if (barrelIndex >= 0)
        {
            CancelInvoke("DestroyBarrel");
            barrel.RemoveAt(barrelIndex);
        }
    }

    private void DestroyBarrel()
    {
        Destroy(barrel[0]);
        barrel.RemoveAt(0);
    }
}
