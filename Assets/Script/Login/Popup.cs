
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Interior;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public static Popup Instance;
    public Canvas canvas;
    public Button btn_left;
    public Text btn_left_txt;
    public Button btn_right;
    public Text btn_right_txt;
    public Text Title;
    public Text Message;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public void Showpopup(PopupCodes code,string title,string msg,GameObject clientobject = null)
    {
       
        switch (code)
        {
            case PopupCodes.QuitApp:
                Quitapp(title, msg);
                break;
            case PopupCodes.Alert:
                Alert(title, msg);
                break;
            case PopupCodes.NoInternet:
                Alert(title, msg);
                break;
        }
    }
  
    void Alert(string title, string msg) {
        canvas.enabled = true;
        btn_left.onClick.RemoveAllListeners();
        btn_right.onClick.RemoveAllListeners();
        btn_left.gameObject.SetActive(false);
        btn_right.gameObject.SetActive(true);
        Title.text = title;
        Message.text = msg;
        btn_right_txt.text = "Close";
        btn_right.onClick.AddListener(Closepopup);
    }
    void Quitapp(string title, string msg)
    {
        canvas.enabled = true;
        btn_left.onClick.RemoveAllListeners();
        btn_right.onClick.RemoveAllListeners();
        btn_left.gameObject.SetActive(true);
        btn_right.gameObject.SetActive(true);
        Title.text = title;
        Message.text = msg;
        btn_left_txt.text = "Yes";
        btn_left.onClick.AddListener(Quit);
        btn_right_txt.text = "No";
        btn_right.onClick.AddListener(Closepopup);
    }

    void Nointernet(string title, string msg)
    {
        canvas.enabled = true;
        btn_left.onClick.RemoveAllListeners();
        btn_right.onClick.RemoveAllListeners();
        btn_left.gameObject.SetActive(true);
        btn_right.gameObject.SetActive(true);
        Title.text = title;
        Message.text = msg;
        btn_left_txt.text = "Refresh";
        btn_left.onClick.AddListener(Refresh);
        btn_right_txt.text = "Quit";
        btn_right.onClick.AddListener(Quit);
    }
    public void Closepopup()
    {
        canvas.enabled = false;
        Title.text = null;
        Message.text = null;
        btn_left.onClick.RemoveAllListeners();
        btn_right.onClick.RemoveAllListeners();
        btn_left.gameObject.SetActive(false);
        btn_right.gameObject.SetActive(false);
    }
    
    void Quit()
    {
        GS.Instance.SaveData();
        Application.Quit();
    }
    void Refresh()
    {
        if (GS.Instance.InternetAvailable())
        {
            Loading.Instance.Show_loading();
           // GS.Instance.GetData();
            StartCoroutine(loaduisceneagian());
        }
        else
        {
            Closepopup();
            Nointernet("No Internet", "Please turn on Internet");
          
        }     
    }
    IEnumerator loaduisceneagian()
    {
        yield return new WaitForSecondsRealtime(3f);
        GS.Instance.OnLoadScene("UIScene");
    }
    internal void Showpopup(object aPIError, string v1, string v2)
    {
        throw new NotImplementedException();
    }
}
