
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class SETVR : MonoBehaviour
{
    public GameObject Andriod_camera;
    public GameObject cardboard_camera;
    public GameObject canvas_andriod;
    public GameObject playerscript;
    public GameObject cardboard_camera_main;
    public GameObject Andriod_camera_main;
   
    public bool cardboard;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadDevice("cardboard"));
        //StartCoroutine(LoadDevice("None"));
        // Startvr();
    }

    void OnEnable()
    {
        //Startvr();
    }

    public void Startvr() {
       // StartCoroutine(startcardboard());
        cardboard = true;
        Andriod_camera.SetActive(false);
        // yield return new WaitForSecondsRealtime(2f);
        cardboard_camera.SetActive(true);
        canvas_andriod.SetActive(false);


        playerscript.GetComponent<fpsController>().enabled = false;
        //yield return new WaitForSecondsRealtime(1f);
        //
        //   playerscript.GetComponent<CharacterController>().enabled = true;
        //XRSettings.eyeTextureResolutionScale = 2.0f;
        StartCoroutine(LoadDevice("cardboard"));
    }
  
  
  

   

    public IEnumerator removecarboardwait()
    {
        yield return new WaitForSecondsRealtime(2f);
        playerscript.GetComponent<fpsController>().enabled = true;
        Andriod_camera.SetActive(true);
        cardboard_camera.SetActive(false);
        canvas_andriod.SetActive(true);
        cardboard = false;
        //  GameplayController.Instance.RemoveCardboardCam();
        
    }
   public IEnumerator LoadDevice(string newDevice)
    {
      
       /* string[] devices = new string[] { "Cardboard", "None" };
        XRSettings.LoadDeviceByName(devices);
        
        yield return null;*/
        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
         yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

           //  XRSettings.LoadDeviceByName(newDevice);
          // yield return null;
           //XRSettings.enabled = true;
           if (XRGeneralSettings.Instance.Manager.activeLoader == null)
           {
               Debug.Log("failed");
               //Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
           }
           else
           {
               Debug.Log("Starting XR...");
               XRGeneralSettings.Instance.Manager.StartSubsystems();
               Debug.Log("xracccccc " + XRGeneralSettings.Instance.Manager.activeLoader);
               // XRGeneralSettings.Instance.Manager.loaders.Add(XRLoader.)
           }
       /* string[] devices = new string[] { "Cardboard", "None" };
        XRSettings.LoadDeviceByName(devices);

        XRSettings.enabled = true;*/
        // StartCoroutine(StartXR());

    }


}
