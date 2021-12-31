using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(3f);
        string filePath = Path.Combine(Application.persistentDataPath, "fstate");
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            StartCoroutine(GS.Instance.OnLoadScene("UIScene"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
