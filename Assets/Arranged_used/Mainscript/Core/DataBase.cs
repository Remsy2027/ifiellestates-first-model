using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Interior
{
    [System.Serializable]
    public class DataBase
    {
        // public ServerData Serverdata;
        public ifeeState iffestatedb;
      
        public DataBase()
        {
             iffestatedb = new ifeeState();
        
        }
    }
}
