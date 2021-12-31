using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public Image photos;
    public string Imagepath;
    public bool potrate;
    public string imagename;
    ToFileDownloadHandler _Newdownloader;
    // Start is called before the first frame update


    public void setdata(string Image_path)
    {
        Imagepath = Image_path;
       // Start();
    }
 
    public IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (Imagepath != "" && Imagepath != null && Imagepath != " ")
        {
            _Newdownloader = new ToFileDownloadHandler();
            string path = Path.Combine(Application.persistentDataPath, Path.GetFileName(Imagepath));
            if (File.Exists(path))
            {
                loadimage(path);
            }
            else
            {
                if (GS.Instance.InternetAvailable())
                {
                    StartCoroutine(Startlocaldownload(Imagepath, Path.GetFileName(Imagepath)));
                }
                // download code
            }
        }
        StopCoroutine("Start");
    }
   
    IEnumerator Startlocaldownload(string path, string foldername)
    {
        StartCoroutine(_Newdownloader.NewDownloadInLocal(path, foldername));
        yield return new WaitForSecondsRealtime(0.5f);
        while (!_Newdownloader.isDone)
        {
         //   print(_Newdownloader.percentage());
            double m_CurrentValue = System.Math.Round(_Newdownloader.percentage() * 100, 2);
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
            yield return new WaitForSecondsRealtime(0.5f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        if (_Newdownloader.isDownloadSuccess)
        {
            string image_download_path = Path.Combine(Application.persistentDataPath, foldername);
            if (File.Exists(image_download_path))
            {
                loadimage(image_download_path);
            }
        }
        else
        {

        }
    }
    // Update is called once per frame
    void loadimage(string path)
    {
       
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                FileStream fsIn = new FileStream(path, FileMode.Open);
                int read;
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                Texture2D texture = new Texture2D(100, 100, TextureFormat.ETC2_RGBA8, false);
                texture.LoadImage(ms.ToArray());
                texture.Compress(true);
                photos.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            }
       
     

    }
}
