using Interior;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class object_prefab : MonoBehaviour
{
    public Image Main_image;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Beds;
    public TextMeshProUGUI Baths;
    public TextMeshProUGUI Squarefeet;
    public ifeeStateVillacategory Currentvillas;
    string modelpath;
    public GameObject Downloadbtn;
    public GameObject ThreeSixty_btn;
    public GameObject Download_text;
    ToFileDownloadHandler _Newdownloader;
    // Start is called before the first frame update
    public void Setdata(ifeeStateVillacategory villas)
    {
        Currentvillas = villas;
        Title.text = villas.Name;
        Beds.text = villas.Beds;
        Baths.text = villas.Baths;
        Squarefeet.text = villas.Squarefeet;
        Main_image.GetComponent<ImageLoader>().Imagepath = villas.imagepath;
        if (Application.platform == RuntimePlatform.Android)
        {
            modelpath = villas.modelpath;
        }
        else
        {
            modelpath = villas.iosmodelpath;
        }
        _Newdownloader = new ToFileDownloadHandler();
        string image_download_path = Path.Combine(Application.persistentDataPath, Path.GetFileName(modelpath));
        Debug.Log("image_download_path "+image_download_path);
        if (File.Exists(image_download_path))
        {
            Downloadbtn.SetActive(false);
            ThreeSixty_btn.SetActive(true);
            Download_text.SetActive(false);
        }
        else
        {
            Download_text.SetActive(false);
            Downloadbtn.SetActive(true);
            ThreeSixty_btn.SetActive(false);
        }
    }
    public void Download_btn_click() {

        if (GS.Instance.InternetAvailable())
        {
            _Newdownloader = new ToFileDownloadHandler();
            string image_download_path = Path.Combine(Application.persistentDataPath, Path.GetFileName(modelpath));
            Downloadbtn.SetActive(false);
            Download_text.SetActive(true);
            if (File.Exists(image_download_path))
            {
                File.Delete(image_download_path);
            }
            //Loading.Instance.Show_loading("Please wait while downloading..");
            StartCoroutine(Startlocaldownload(modelpath, Path.GetFileName(modelpath)));
        }
        else
        {
           Popup.Instance.Showpopup(PopupCodes.Alert,"No Internet", "Please turn on Internet");
        }
    }

    IEnumerator Startlocaldownload(string path, string foldername)
    {
        StartCoroutine(_Newdownloader.NewDownloadInLocal(path, foldername));
        yield return new WaitForSecondsRealtime(0.5f);
        while (!_Newdownloader.isDone)
        {
            //   print(_Newdownloader.percentage());
            double m_CurrentValue = System.Math.Round(_Newdownloader.webRequest.downloadProgress * 100, 2);
            string value = null;
            if (m_CurrentValue == 0)
            {
                value = "";
            }
            else
            {
                value = m_CurrentValue.ToString() + " %";
            }
            Debug.Log("_Newdownloader.webRequest.downloadProgress " + _Newdownloader.webRequest.downloadProgress);
            Debug.Log("Persentage "+m_CurrentValue.ToString());
            yield return new WaitForSecondsRealtime(0.2f);
            /*  Download_text.GetComponent<TextMeshProUGUI>().text = "Downloading. "+ m_CurrentValue.ToString()+"%";
              yield return new WaitForSecondsRealtime(0.2f);
              Download_text.GetComponent<TextMeshProUGUI>().text = "Downloading.. " + m_CurrentValue.ToString() + "%";
              yield return new WaitForSecondsRealtime(0.2f); */
            Download_text.GetComponent<TextMeshProUGUI>().text = "Downloading... " + m_CurrentValue.ToString() + "%";
        }
        yield return new WaitForSecondsRealtime(0.5f);
        if (_Newdownloader.isDownloadSuccess)
        {
            string image_download_path = Path.Combine(Application.persistentDataPath, foldername);
            if (File.Exists(image_download_path))
            {
                Loading.Instance.Hide_loading();
                Downloadbtn.SetActive(false);
                Download_text.SetActive(false);
                ThreeSixty_btn.SetActive(true);
            }
        }
        else
        {
           // Loading.Instance.Hide_loading();
            Downloadbtn.SetActive(true);
            ThreeSixty_btn.SetActive(false);

            Download_text.SetActive(false);
        }
    }
    public void ThreeSixty_btn_click()
    {
       string image_download_path = Path.Combine(Application.persistentDataPath, Path.GetFileName(modelpath));
         if (File.Exists(image_download_path))
         {
             Debug.Log(image_download_path);
             StartCoroutine(loadscene(image_download_path));
         }
         else
         {
             Downloadbtn.SetActive(true);
             ThreeSixty_btn.SetActive(false);
         }    
    /*  Loading.Instance.Show_loading("Please wait..");
     StartCoroutine(   GS.Instance.OnLoadScene("HouseUrp"));   */
    }
    public IEnumerator loadscene(string defaultpath)
    {
        // Debug.Log(defaultpath);
        if (File.Exists(defaultpath))
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
                   
                    GS.Instance.loadscene(_bundlenew);
                    /*   string[] scenePaths = _bundlenew.GetAllScenePaths();
                       string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
                       SceneManager.LoadSceneAsync(sceneName);  */
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
                      }*/
                }
                else
                {
                    Loading.Instance.Hide_loading();
                    Popup.Instance.Showpopup(PopupCodes.Alert, "Notice", "File not proper downloaded");
                    Debug.Log("hereee11111111111  not");
                    Downloadbtn.SetActive(true);
                    ThreeSixty_btn.SetActive(false);
                    if (File.Exists(defaultpath))
                    {
                        File.Delete(defaultpath);
                    }
                }
            }
            else
            {
                Loading.Instance.Hide_loading();
                Popup.Instance.Showpopup(PopupCodes.Alert, "Notice", "File not proper downloaded");
                Debug.Log("hereee11111111111  not");
                Downloadbtn.SetActive(true);
                ThreeSixty_btn.SetActive(false);
                if (File.Exists(defaultpath))
                {
                    File.Delete(defaultpath);
                }
            }
        }
    }
    public void View_all_btn()
    {
        UIcontroller.Instance.View_all_click();
        Vilew_all_panal.Instance.Setdata(Currentvillas, Main_image.sprite);
        //StartCoroutine(GS.Instance.OnLoadScene("HouseUrp"));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
