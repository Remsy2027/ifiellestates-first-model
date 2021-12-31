using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIllas_panal : MonoBehaviour
{

    public object_prefab Main_Object_prefab;
    public Transform Panal;
    // Start is called before the first frame update
   public void Start_Villas()
    {
        if(Panal.childCount > 0)
        {
            foreach(Transform t in Panal)
            {
                Destroy(t.gameObject);
            }
        }

        foreach (var villaslist in GS.Instance.DB.iffestatedb.allvilla)
        {
            Instantiate(Main_Object_prefab, Panal).Setdata(villaslist);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
