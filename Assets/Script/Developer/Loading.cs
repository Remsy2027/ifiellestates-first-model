using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public static Loading Instance;
    public GameObject Loading_gif;
    public Text Loadingtext;
    public Canvas Loading_canvas;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    public void Show_loading(string loadingtext = "") {
        Loading_canvas.enabled = true;
        if (!Loadingtext.gameObject.activeSelf)
        {
            Loadingtext.gameObject.SetActive(true);
        }
        Loadingtext.text = loadingtext;
        if (!Loading_gif.activeSelf)
        {
            Loading_gif.SetActive(true);
        }
    }
    public void Hide_loading()
    {
        Loading_canvas.enabled = false;
        Loadingtext.text = "";
        Loadingtext.gameObject.SetActive(false);
        Loading_gif.SetActive(false);
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
