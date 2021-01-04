using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningMessageLoop : MonoBehaviour
{
    [SerializeField]
    private Transform[] messageText = new Transform[2];
    [SerializeField]
    private float messageSpeed = 0.15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LoopText();
    }

    private void LoopText()
    {
        messageText[0].Translate(Vector2.left * messageSpeed * Time.smoothDeltaTime);
        messageText[1].Translate(Vector2.left * messageSpeed * Time.smoothDeltaTime);

        if(messageText[0].localPosition.x < -3025f)
        {
            messageText[0].localPosition = new Vector2(messageText[1].localPosition.x + 4200f, messageText[0].localPosition.y);
        }
        if (messageText[1].localPosition.x < -3025f)
        {
            messageText[1].localPosition = new Vector2(messageText[0].localPosition.x + 4200f, messageText[1].localPosition.y);
        }
    }
}
