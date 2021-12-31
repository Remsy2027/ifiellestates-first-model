using Interior;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Vilew_all_panal : MonoBehaviour
{
    ifeeStateVillacategory current_villas;
    public Image Main_image;
    public TextMeshProUGUI Beds;
    public TextMeshProUGUI Baths;
    public TextMeshProUGUI Squarefeet;
    public TextMeshProUGUI Name;
    public GameObject Download;
    public GameObject ThreeSixty_btn;
    public ImageLoader Image_obj;
    public Transform panal;
    // Start is called before the first frame update
    public static Vilew_all_panal Instance;
    ToFileDownloadHandler _Newdownloader;
    public GameObject Download_text;
    string modelpath;
    public void Start()
    {
        Instance = this;
    }

    public void Setdata(ifeeStateVillacategory currentvillas,Sprite image_villas)
    {
        _Newdownloader = new ToFileDownloadHandler();
        current_villas = currentvillas;
        Main_image.sprite = image_villas;
        Name.text = currentvillas.Name;
        Beds.text = currentvillas.Beds;
        Baths.text = currentvillas.Baths;
        Squarefeet.text = currentvillas.Squarefeet;
        if (Application.platform == RuntimePlatform.Android)
        {
            modelpath = currentvillas.modelpath;
        }
        else
        {
            modelpath = currentvillas.iosmodelpath;
        }
        string model_path = Path.Combine(Application.persistentDataPath, Path.GetFileName( modelpath));

        if (File.Exists(model_path))
        {
            ThreeSixty_btn.SetActive(true);
            Download.SetActive(false);
        }
        else
        {
            ThreeSixty_btn.SetActive(false);
            Download.SetActive(true);
        }

        if(panal.childCount > 0)
        {
            foreach(Transform t in panal)
            {
                Destroy(t.gameObject);
            }
        }
        if(currentvillas.imagepathlist.Count > 0)
        {
            foreach(var path in currentvillas.imagepathlist)
            {
                Instantiate(Image_obj, panal).setdata(path);
            }
        }
    }
    public void Download_btn_click()
    {   if (GS.Instance.InternetAvailable())
        {
            Download.SetActive(false);
            Download_text.SetActive(true);
            string image_download_path = Path.Combine(Application.persistentDataPath, Path.GetFileName(modelpath));

            if (File.Exists(image_download_path))
            {
                File.Delete(image_download_path);
            }
            // Loading.Instance.Show_loading("Please wait while downloading..");
            //StartCoroutine(Startlocaldownload("https://drive.google.com/uc?export=download&id=" + modelpath, modelpath));
            StartCoroutine(Startlocaldownload(modelpath, Path.GetFileName(modelpath)));
        }
        else
        {
            Popup.Instance.Showpopup(PopupCodes.Alert, "No Internet", "Please turn on Internet");
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

            //Percent = (m_CurrentValue * 1f).ToString("0.00");
            //   print("Percent " + m_CurrentValue.ToString());
            Download_text.GetComponent<TextMeshProUGUI>().text = "Downloading... " + m_CurrentValue.ToString() + "%";
            yield return new WaitForSecondsRealtime(0.5f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        if (_Newdownloader.isDownloadSuccess)
        {
            string image_download_path = Path.Combine(Application.persistentDataPath, foldername);
            if (File.Exists(image_download_path))
            {
                Loading.Instance.Hide_loading();
                Download.SetActive(false);
                ThreeSixty_btn.SetActive(true);

                Download_text.SetActive(false);
                //  loadimage(image_download_path);
            }
        }
        else
        {
            Loading.Instance.Hide_loading();
            Download.SetActive(true);
            ThreeSixty_btn.SetActive(false);

            Download_text.SetActive(false);
        }
    }

    public void Three_sixty_btn_click()
    {
        string image_download_path = Path.Combine(Application.persistentDataPath, Path.GetFileName(modelpath));
        if (File.Exists(image_download_path))
        {
            Debug.Log(image_download_path);
            StartCoroutine(loadscene(image_download_path));
        }
        else
        {
            Download.SetActive(true);
            ThreeSixty_btn.SetActive(false);

        }
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
                    Download.SetActive(true);
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
                Download.SetActive(true);
                ThreeSixty_btn.SetActive(false);
                if (File.Exists(defaultpath))
                {
                    File.Delete(defaultpath);
                }
            }
        }
    }
    public void Back_btn_click()
    {
        UIcontroller.Instance.Villase.enabled = true;
        UIcontroller.Instance.View_all_panal.enabled = false;
       
        if (panal.childCount > 0)
        {
            foreach (Transform t in panal)
            {
                Destroy(t.gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
