using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class getdevicetype : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            text.text = "Andriod - " + SystemInfo.deviceName;
        }
        else
        {
            text.text = "deviceName - " + SystemInfo.deviceName + "/n deviceModel - " + SystemInfo.deviceModel + "/n devicetypr - " + SystemInfo.deviceType;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
