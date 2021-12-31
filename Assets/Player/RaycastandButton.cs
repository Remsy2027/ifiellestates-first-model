using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastandButton : MonoBehaviour
{
    public GameObject change_button;
    public GameObject setvr;
    private RaycastHit hitInfo;
    public float LineAndRaycastMaxDistance = 50f;
    public MaterialandColorChangescript Material_color_objects;
    public GameObject andriodcam;
    // Start is called before the first frame update
    void Start()
    {
        andriodcam = transform.GetChild(0).gameObject;
    }
    public void changematerial()
    {
        if(Material_color_objects != null)
        {
            Material_color_objects.change_material_color();
        }

    }
    KeyCode akey = KeyCode.A;
    // Update is called once per frame
    void Update()
    {
        
        if (setvr != null)
        {
            if (setvr.GetComponent<SETVR>().cardboard)
            {
                if (Input.GetKeyDown(akey) || Input.GetKeyDown(KeyCode.A)) // gets forward
                {
                    changematerial();
                }
                var inst = Camera.main;
                if (inst != null)
                {


                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, LineAndRaycastMaxDistance))
                    {
                        if (hitInfo.collider != null)
                        {
                            var materialandcolor = hitInfo.collider.gameObject.GetComponent<MaterialandColorChangescript>();
                            if (materialandcolor != null)
                            {
                                Material_color_objects = materialandcolor;
                                change_button.SetActive(true);
                            }
                            else
                            {
                                Material_color_objects = null;
                                change_button.SetActive(false);
                            }

                            //  Debug.Log("hit obj " + hitInfo.collider.gameObject.name);
                        }
                        else
                        {
                            Material_color_objects = null;
                            change_button.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                if (andriodcam != null)
                {
                    if (Physics.Raycast(andriodcam.transform.position, andriodcam.transform.forward, out hitInfo, LineAndRaycastMaxDistance))
                    {
                        if (hitInfo.collider != null)
                        {
                            var materialandcolor = hitInfo.collider.gameObject.GetComponent<MaterialandColorChangescript>();
                            if (materialandcolor != null)
                            {
                                Material_color_objects = materialandcolor;
                                change_button.SetActive(true);
                            }
                            else
                            {
                                Material_color_objects = null;
                                change_button.SetActive(false);
                            }

                            //  Debug.Log("hit obj " + hitInfo.collider.gameObject.name);
                        }
                        else
                        {
                            Material_color_objects = null;
                            change_button.SetActive(false);
                        }
                    }
                }
            }
       
        }
    }
}
