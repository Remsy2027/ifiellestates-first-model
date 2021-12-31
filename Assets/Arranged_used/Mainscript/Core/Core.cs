using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Interior
{
    public interface IResponder
    {
        void OnResopnse(string en, string data);
    }
    public enum PopupCodes
    {
        None,
        QuitApp,
        Alert,
        NotCreateRoom,
        TerminateComputergame,
        TerminateGameinroom,
        Blocked,
        ForceUpdate,
        VersionUpdate,
        Logout,
        Removeclient,
        RemoveItem,
        Chooseui,
        NoInternet
    }
   
    [System.Serializable]
    public class ifeeStateVillacategory
    {
        public string Name;
        public string imagepath;
        public string Beds;
        public string Baths;
        public string modelpath;
        public string iosmodelpath;
        public string Squarefeet;
        public List<string> imagepathlist = new List<string>();

    }
    [System.Serializable]
    public class ifeeState
    {
        public List<ifeeStateVillacategory> allvilla;
        public List<ifeeStateCommunitycategory> allcomunity;

    }   
    [System.Serializable]
    public class ifeeStateCommunitycategory
    {
        public string Name;
        public string imagepath;
        public bool potrate;

    }  
}
