using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Community_panal : MonoBehaviour
{

    public ImageLoader Main_Object_prefab;
    public Transform Panal;
    public GameObject Comming_soon;
    // Start is called before the first frame update
  public void Start_Villas()
    {
        if (GS.Instance.DB.iffestatedb.allcomunity != null)
        {

            if (GS.Instance.DB.iffestatedb.allcomunity.Count != 0)
            {
                Comming_soon.SetActive(false);
                foreach (var communitylist in GS.Instance.DB.iffestatedb.allcomunity)
                {
                    Instantiate(Main_Object_prefab, Panal).setdata(communitylist.imagepath);
                }
            }
            else
            {
                Comming_soon.SetActive(true);
            }
        }
        else
        {
            Comming_soon.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
