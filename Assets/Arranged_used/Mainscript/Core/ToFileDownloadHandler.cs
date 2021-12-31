using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Interior;
using UnityEngine;
using UnityEngine.Networking;

public class ToFileDownloadHandler : DownloadHandlerScript
{
    public static ToFileDownloadHandler instace;
    int expected = -1;
    private int received = 0;
    private string filepath;
    private FileStream fileStream;
    private bool canceled = false;
    public UnityWebRequest webRequest;
 //   UnityWebRequest downloadrequest;
    public static float per;
    public bool _isDownloadSuccess;
    public bool _isDone;
    int filesize;
    public bool downloadfailed = false;
    public bool isDone
    {
        get
        {
            return _isDone;
        }
        
    }
    public ToFileDownloadHandler() {
        instace = this;
    }
    public bool isDownloadSuccess
    {
        get
        {
            return _isDownloadSuccess;
        }
    }
    public ToFileDownloadHandler(byte[] buffer, string filepath)
      : base(buffer)
    {
        this.filepath = filepath;
       // Debug.Log("Buffer");
        fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
    }

    protected override byte[] GetData() { return null; }
    int datasaved;
    protected override bool ReceiveData(byte[] data, int dataLength)
    {
        Debug.Log("Adddata");
        datastore(data);
        if (data == null || data.Length < 1)
        {
            return false;
        }
        received += dataLength;      
      
       // Debug.Log("ReceiveData datasaved " + datasaved);
        if (!canceled) fileStream.Write(data, 0, dataLength);
        return true;
    }
    void datastore(byte[] data) {

        Debug.Log("datastore  data.Length " +  data.Length);
        datasaved += data.Length;
    }

    protected override float GetProgress()
    {
        if (expected < 0)
        {
           return 0;
        }
        else
        {
            return (float)received / expected;
        }
    }
    bool lengthstore = false;

    UnityWebRequest downloadrequest;
    /* {
        get
        {
            return webRequest;
        }
       set {
            downloadrequest = webRequest;
        }
    }*/
    public float percentage()
    {
        /*if (downloadrequest == null)
        {
            return 0;
        }
        else
        {*/
        return GetProgress();
        //return webRequest.downloadProgress;
        // }
    }
    protected override void CompleteContent()
    {
        fileStream.Close();
    }

    protected override void ReceiveContentLength(int contentLength)
    {
        expected = contentLength;
    }
    public void Cancel()
    {
        canceled = true;
        webRequest.Abort();
        if (fileStream != null)
        {

            fileStream.Close();

            File.Delete(filepath);
        }
    }
    public string getMD5Hash(string path)
    {
        string md5hash = null;
        using (var md5 = MD5.Create())
        {
            try
            {
                using (var stream = File.OpenRead(path))
                {
                    var hash = md5.ComputeHash(stream);
                    md5hash = System.BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    Debug.Log("LocalFile" + md5hash);
                }
            }
            catch (Exception e) {

            }
        }
        return md5hash;
    }
    string downloadfiletag;
    public IEnumerator DownloadInLocalImage(string url, int version, string TAG = null)
    {
      string  savePath = Path.Combine(Application.persistentDataPath,TAG);
        _isDone = false;
        bool hasTodownload = false;
        if (!File.Exists(savePath))
        {
            hasTodownload = true;
        }
        else
        {
            hasTodownload = false;

        }
        if (hasTodownload)
        {
           
                webRequest = new UnityWebRequest(url);              
                Debug.Log("herere User-Agent "+ url);              
                webRequest.downloadHandler = new ToFileDownloadHandler(new byte[64 * 1024], savePath);
                downloadrequest = webRequest;
                yield return webRequest.SendWebRequest();
                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log("Download Error");
                    Debug.Log(webRequest.error);
                    downloadfailed = true;
                    //Popup.Instance.Showpopup(PopupCodes.Alert, "Error " + webRequest.responseCode, "Download Not successfully ");
                }
                else
                {
                    //set old version = new version in binary and save data
                    _isDownloadSuccess = true;
                }
               
            
        }
        else
        {
            _isDownloadSuccess = true;
        }
        _isDone = true;
        yield return null;
    }
    public IEnumerator NewDownloadInLocal(string url,  string TAG = null)
    {
        lengthstore = false;
        string savePath;
        downloadfiletag = TAG;
        // url = "https://kachhua-vr.s3.ap-south-1.amazonaws.com/encypted_zipFiles/01a2906ae17a0c019b023ab74ca2b05f";

        savePath = Path.Combine(Application.persistentDataPath, TAG);
        _isDone = false;
        Debug.Log(savePath);
        bool hasTodownload = false;
        if (!File.Exists(savePath))
        {
            //Create and download new asset bundle and set the info in binary
            hasTodownload = true;

        }
        else
        {
            hasTodownload = true;
        }
        if (hasTodownload)
        {
            webRequest = new UnityWebRequest(url);
            webRequest.downloadHandler = new ToFileDownloadHandler(new byte[64 * 1024], savePath);
            downloadrequest = webRequest;
            yield return new WaitForSecondsRealtime(0.5f);
            yield return webRequest.SendWebRequest();
            _isDownloadSuccess = true;
            expected = -1;

        }
        else
        {
            _isDownloadSuccess = true;
        }
        _isDone = true;
        yield return null;
    }

    public IEnumerator oNewDownloadInLocal(string url, int version, string TAG = null, string LocalMD5Hash = null, string SubjectIndex = null, string TopicIndex = null, string ContentIndex = null, string Receive_MD5Hash = null, string filesizes = null)
    {
        lengthstore = false;
        string savePath;
        downloadfiletag = TAG;
        // url = "https://kachhua-vr.s3.ap-south-1.amazonaws.com/encypted_zipFiles/01a2906ae17a0c019b023ab74ca2b05f";
     
        savePath = Path.Combine(Application.persistentDataPath, TAG );
        _isDone = false;
        //Unload bundle if any loaded before load new bundle 
     
        bool hasTodownload = false;
        if (!File.Exists(savePath))
        {
            //Create and download new asset bundle and set the info in binary
            hasTodownload = true;
           
        }
        else
        {
            hasTodownload = true;
           /* string downloaded_file_hash = getMD5Hash(savePath);
            Debug.Log("downloaded_file_hash MD5 - " + downloaded_file_hash);
            //////////////////////////////////////////////////////////////
            Debug.Log("Receive_MD5Hash  " + Receive_MD5Hash);
            string Received_MD5Hash = Receive_MD5Hash;
            if (LocalMD5Hash != null)
            {
                Received_MD5Hash = LocalMD5Hash;
                Debug.Log("MD5 Checksum Received by LOCAL SERVER : " + Received_MD5Hash);
            }
            Debug.Log(downloaded_file_hash + "==" + Received_MD5Hash);
            //  Received_MD5Hash = null;
            if (Received_MD5Hash == null)
            {
                Debug.Log("Herere");
                Received_MD5Hash = downloaded_file_hash;
                //check if donwloaded file is empty
                if (File.Exists(savePath))
                {
                    FileInfo FileVol = new FileInfo(savePath);
                    int SizeinKB = (int)(FileVol).Length / 1024;
                    Debug.Log("Herere " + SizeinKB);

                    if (SizeinKB < 5)
                    {
                        File.Delete(savePath);
                        Received_MD5Hash = null;
                    }
                }
            }

            if ((downloaded_file_hash != null && Received_MD5Hash != null) && downloaded_file_hash == Received_MD5Hash)
            {
                _isDownloadSuccess = true;
                expected = -1;
            }
            else
            {
                hasTodownload = true;
                Debug.Log("HASH NOT Matched, Removing File! Try Again");
            }*/
           
        }
        if (hasTodownload)
        {
          

                webRequest = new UnityWebRequest(url);
                //downloadrequest = webRequest;
               
                webRequest.downloadHandler = new ToFileDownloadHandler(new byte[64 * 1024], savePath);
                downloadrequest = webRequest;
                yield return new WaitForSecondsRealtime(0.5f);
                yield return webRequest.SendWebRequest();

              //  string downloaded_file_hash = getMD5Hash(savePath);
                //Debug.Log("downloaded_file_hash MD5 - " + downloaded_file_hash);
                //////////////////////////////////////////////////////////////
             //   Debug.Log("Receive_MD5Hash  " + Receive_MD5Hash);
             //   string Received_MD5Hash = Receive_MD5Hash;
             //   if (LocalMD5Hash != null)
             //   {
             //       Received_MD5Hash = LocalMD5Hash;
            //        Debug.Log("MD5 Checksum Received by LOCAL SERVER : " + Received_MD5Hash);
            //    }
            //    Debug.Log(downloaded_file_hash + "==" + Received_MD5Hash);
              //  Received_MD5Hash = null;
              /*  if (Received_MD5Hash == null)
                {
                    Debug.Log("Herere");
                    Received_MD5Hash = downloaded_file_hash;
                    //check if donwloaded file is empty
                   /*if (File.Exists(savePath))
                    {
                        FileInfo FileVol = new FileInfo(savePath);
                        int SizeinKB = (int)(FileVol).Length / 1024;
                        Debug.Log("Herere " + SizeinKB);

                        if (SizeinKB < 5)
                        {
                            File.Delete(savePath);
                            Received_MD5Hash = null;
                        }
                    }/////////
                }*/

              //  if ((downloaded_file_hash != null && Received_MD5Hash != null) && downloaded_file_hash == Received_MD5Hash)
              //  {
                //    Debug.Log("HASH Matched, Keeping File" + Received_MD5Hash);
                    //set old version = new version in binary and save data
                   _isDownloadSuccess = true;
                    expected = -1;
             //   }
              //  else
              //  {
              //      Debug.Log("HASH NOT Matched, Removing File! Try Again");
              //  }
            
        }
        else
        {
            _isDownloadSuccess = true;
        }
        _isDone = true;
        yield return null;
    }
}
