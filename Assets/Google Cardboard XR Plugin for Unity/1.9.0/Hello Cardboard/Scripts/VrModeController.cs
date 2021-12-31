//-----------------------------------------------------------------------
// <copyright file="VrModeController.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;

/// <summary>
/// Turns VR mode on and off.
/// </summary>
public class VrModeController : MonoBehaviour
{
    /// <summary>
    /// Gets a value indicating whether the screen has been touched this frame.
    /// </summary>
    public SETVR setvr;
    private bool _isScreenTouched
    {
        get
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }
    public void closescene_backto_ui() {
        StartCoroutine(GS.Instance.clearscreen());
        SceneManager.LoadSceneAsync("UIScene");

    }
    /// <summary>
    /// Gets a value indicating whether the VR mode is enabled.
    /// </summary>
    private bool _isVrModeEnabled
    {
        get
        {
            return XRGeneralSettings.Instance.Manager.isInitializationComplete;
        }
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        // Configures the app to not shut down the screen and sets the brightness to maximum.
        // Brightness control is expected to work only in iOS, see:
        // https://docs.unity3d.com/ScriptReference/Screen-brightness.html.
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;

        // Checks if the device parameters are stored and scans them if not.
        // This is only required if the XR plugin is initialized on startup,
        // otherwise these API calls can be removed and just be used when the XR
        // plugin is started.
     /*  if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }*/
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitVR();
        }
        if (_isVrModeEnabled)
        {
            if (Api.IsCloseButtonPressed)
            {
               
                ExitVR();
            }
         
          
            if (Api.IsGearButtonPressed)
            {
                Api.ScanDeviceParams();
            }

            Api.UpdateScreenParams();
        }
        else
        {
            // TODO(b/171727815): Add a button to switch to VR mode.
            if (_isScreenTouched)
            {
               // EnterVR();
            }
        }
    }

    public void startcardboard() {
      setvr.  cardboard = true;
        setvr.Andriod_camera.SetActive(false);
        // yield return new WaitForSecondsRealtime(2f);
        setvr.cardboard_camera.SetActive(true);
        setvr.canvas_andriod.SetActive(false);


        setvr.playerscript.GetComponent<fpsController>().enabled = false;
        EnterVR();
    }
    public void stopcardboard()
    {
        setvr.playerscript.GetComponent<CharacterController>().enabled = false;

        setvr.playerscript.transform.eulerAngles = new Vector3( setvr.cardboard_camera_main.transform.eulerAngles.x, setvr.cardboard_camera_main.transform.eulerAngles.y,0f);
        setvr.Andriod_camera_main.transform.localRotation = Quaternion.identity;
        setvr.playerscript.GetComponent<fpsController>().enabled = true;
        setvr.Andriod_camera.SetActive(true);
        setvr.cardboard_camera.SetActive(false);
        setvr.canvas_andriod.SetActive(true);
        //  ExitVR();
    }
    /// <summary>
    /// Enters VR mode.
    /// </summary>
    private void EnterVR()
    {
        StartCoroutine(StartXR());
        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
    }

    /// <summary>
    /// Exits VR mode.
    /// </summary>
    private void ExitVR()
    {
        StopXR();
    }

    /// <summary>
    /// Initializes and starts the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    ///
    /// <returns>
    /// Returns result value of <c>InitializeLoader</c> method from the XR General Settings Manager.
    /// </returns>
    private IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed.");
        }
        else
        {
            Debug.Log("XR initialized.");

            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("XR started.");
        }
    }

    /// <summary>
    /// Stops and deinitializes the Cardboard XR plugin.
    /// See https://docs.unity3d.com/Packages/com.unity.xr.management@3.2/manual/index.html.
    /// </summary>
    private void StopXR()
    {
        Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        Debug.Log("XR stopped.");

        Debug.Log("Deinitializing XR...");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR deinitialized.");
        stopcardboard();
    }
}
