using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net;
using Proyecto26;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Interior;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GS : MonoBehaviour
{
    public static GS Instance;
    public DataBase DB;
    public string FILENAME;
    public string websiteurl = "";

    public bool startautolive = false;
    public bool Stop_DebugLog = false;
   
    public bool Fakeintenet = false;

    void Awake()
    {
        Instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;
        DontDestroyOnLoad(this.gameObject);
        if (Stop_DebugLog)
        {
            Debug.Log("LOG STOPPED");
            Debug.unityLogger.logEnabled = false;
        }
    }

    // Start is called before the first frame update
   
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;
        StartCoroutine(LoadData());

    }
    public IEnumerator OnLoadScene(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            yield return null;
        }
        operation.allowSceneActivation = true;
    }
    public void SendData() {
        if (InternetAvailable())
        {
            RestClient.Put(websiteurl + "IfeeState" + ".json", DB.iffestatedb);
        }
    }
    public void GetData() {
        Debug.Log("getdata");
        RestClient.Get<ifeeState>(websiteurl + "IfeeState" + ".json").Then(response =>
        {
            Debug.Log("response " + response);
            GS.Instance.DB.iffestatedb = response;
        });
        GS.Instance.SaveData();
      
    }
    IEnumerator LoadData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FILENAME);
        Debug.Log(filePath);

        if (!File.Exists(filePath))
        {
            SaveData(true);
        }
        else
        {
           // yield return new WaitForSecondsRealtime(1);
           
          
            if (InternetAvailable())
            {
                GetData();
                yield return new WaitForSecondsRealtime(2);
            }
            else
            {
                try
                {
                    DB = JsonUtility.FromJson<DataBase>(File.ReadAllText(filePath));
                }
                catch (Exception e)
                {
                    Debug.Log("Failed to load " + e);
                }
            }
        }
    }
  public  IEnumerator clearscreen()
    {
      //  yield return new WaitForSecondsRealtime(5f);
        Resources.UnloadUnusedAssets(); // new line 
        AssetBundle.UnloadAllAssetBundles(true);
        yield return Resources.UnloadUnusedAssets();

       // Dispose();
        System.GC.Collect();
        System.GC.SuppressFinalize(true);
    }
    byte[] alldata;
    string allEncdata;
    string encryptdata;
    string Filedata;
    string Decryptdata;
    string originalkey = "c+MasEKX86kbnwFowjVAyA==";
    /*
    void Encryption()
    {
        // Debug.Log("Newdata.ttt111");
        string filepath = Path.Combine(Application.persistentDataPath, FILENAME);
        //  Debug.Log(filepath);
        if (File.Exists(filepath))
        {
            Filedata = File.ReadAllText(filepath);
            // Debug.Log(Filedata);
            encryptdata = tttEncryptiontest.AESEncryption(Filedata);
            //Debug.Log("encryptdata " + encryptdata);
            string Ecfilepath = Path.Combine(Application.persistentDataPath, "DE.int");
            File.Create(Ecfilepath).Close();
            File.WriteAllText(Ecfilepath, encryptdata);
            try
            {
                File.Copy(Ecfilepath, filepath, true);
            }
            catch (IOException iox)
            {
                Debug.Log(iox.Message);
            }
            if (File.Exists(Ecfilepath))
            {
                File.Delete(Ecfilepath);
            }
            // Debug.Log("encryptdata .ttt111 "+ File.ReadAllText(filepath));
        }
    }

    void Decryption()
    {
        //Debug.Log("Newdata.Decrpy111");
        string filepath = Path.Combine(Application.persistentDataPath, FILENAME);
        // Debug.Log(filepath);
        if (File.Exists(filepath))
        {
            encryptdata = File.ReadAllText(filepath);
            //    Debug.Log(encryptdata);
            Decryptdata = tttEncryptiontest.AESDecryption(encryptdata);
            // Debug.Log("encryptdata " + encryptdata);
            File.WriteAllText(filepath, Decryptdata);
            string Ecfilepath = Path.Combine(Application.persistentDataPath, "DE.int");
            File.Create(Ecfilepath).Close();
            File.WriteAllText(Ecfilepath, Decryptdata);
            try
            {
                File.Copy(Ecfilepath, filepath, true);
            }
            catch (IOException iox)
            {
                //Debug.Log(iox.Message);
            }
            if (File.Exists(Ecfilepath))
            {
                File.Delete(Ecfilepath);
            }
        }
    }
    */
    public void SaveData(bool NewStore = false)
    {      
        string filepath = Path.Combine(Application.persistentDataPath, FILENAME);
        try
        {
            //Debug.Log("SaveData try");
            string json = null;
            if (NewStore)
            {
                json = JsonUtility.ToJson(new DataBase());
                File.Create(filepath).Close();
            }
            else
            {
                json = JsonUtility.ToJson(DB);
            }
            if (File.Exists(filepath))
            {
                File.WriteAllText(filepath, json);
            }
        }
        catch (Exception)
        {
            Debug.Log("Failed to write data");
        }
        if (!NewStore)
        {
          
                if (InternetAvailable())
                {
                    // DB.playerinfo.LicenseDate = DateTime.Now.ToString();
                    // RestClient.Put(websiteurl + UnityEngine.SystemInfo.deviceUniqueIdentifier + ".json", DB.playerinfo);// 
                }
          
        }
        else
        {
            if (InternetAvailable())
            {
                GetData();
                StartCoroutine(Loaduiscene());
                // DB.playerinfo.LicenseDate = DateTime.Now.ToString();
                // RestClient.Put(websiteurl + UnityEngine.SystemInfo.deviceUniqueIdentifier + ".json", DB.playerinfo);// 
            }
            else
            {
                StartCoroutine(Loaduiscene());
            }
            
        }
       

    }
    IEnumerator Loaduiscene()
    {
        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(GS.Instance.OnLoadScene("UIScene"));
    }
    IEnumerator encryptd()
    {
        yield return new WaitForSecondsRealtime(2);
        //Encryption();
    }

    public void OnApplicationQuit()
    {
        SaveData();
    }
    public IEnumerator QuitAplication()
    {
        SaveData();
        yield return new WaitForSecondsRealtime(0.5f);        
        Application.Quit();
    }
    bool C;
    public bool InternetAvailable()
    {
        /* if (Application.internetReachability == NetworkReachability.NotReachable && !Fakeintenet)
         {
             using (var lWebClient = new WebClient())
             {
                 try
                 {
                     lWebClient.DownloadString("http://google.com/generate_204");
                     return true;
                 }
                 catch (Exception e)
                 {
                     return false;
                 }
             }
         }
         else {
             return true;
         }*/
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        request.timeout = 5;
        request.SendWebRequest();
        while (!request.isDone)
        {

        }
       // yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log("Error");
            return false;
        }
        else
        {
            Debug.Log("Success");
            return true;
        }




       /* StartCoroutine(CheckInternetConnection(isConnected =>
        {
            C = isConnected;
        }));
        return C;*/
    }
  

    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        request.timeout = 5;
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log("Error");
            action(false);
        }
        else
        {
            Debug.Log("Success");
            action(true);
        }
    }

    void OnApplicationFocus(bool focus)
    {
        Debug.Log("OnApplicationFocus-------------------------" + (focus ? "true" : "false"));
        if (!focus)
        {
            GS.Instance.SaveData();
        }
    }
    IEnumerator unloadassetbundle(AssetBundle Newscene)
    {
        yield return new WaitForSecondsRealtime(5f);
        Newscene.Unload(false);
    }
  public  void loadscene(AssetBundle  Newscene)
    {
        string[] scenePaths = Newscene.GetAllScenePaths();
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
     
        SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(unloadassetbundle(Newscene));
        // Debug.Log(defaultpath);
        /*  if (File.Exists(defaultpath))
          {
              Loading.Instance.Show_loading("Please wait...");
              yield return null;
               AssetBundleCreateRequest _assetbudlecreaterequest = AssetBundle.LoadFromFileAsync(defaultpath);
               Debug.Log("hereee000000");
               while (_assetbudlecreaterequest.isDone)
               {
                   yield return null;
               }

               yield return _assetbudlecreaterequest;
              Debug.Log("hereee");
              AssetBundle _bundlenew = _assetbudlecreaterequest.assetBundle;
              //yield return new WaitForSecondsRealtime(10f);
              Debug.Log("hereee11111111111");
              if (_bundlenew != null)
              {
                  Debug.Log("hereee22222222");
                  // tempory open sceneassetbundle whithout six step 
                  if (_bundlenew.isStreamedSceneAssetBundle)
                  {
                      Debug.Log("hereee33333333");

                      /*  AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
                        asyncOperation.allowSceneActivation = false;
                        Debug.Log("hereee5555555");
                        while (!asyncOperation.isDone)
                        {
                            Debug.Log("hereee6666666666");
                            if (asyncOperation.progress >= 0.9F)
                            {
                                asyncOperation.allowSceneActivation = true;
                            }
                        }
                  }
              }
              else
              {
                  Debug.Log("hereee11111111111  not");
              }
          }      */
    }
}



