using Interior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{
    public ifeeStateVillacategory Currentvillas;
    public static UIcontroller Instance;

    public Canvas Villase;
    public Canvas Community;
    public Canvas Contactus;
    public Canvas menu;
    public Canvas View_all_panal;
    public Canvas My_build;
    public Canvas Setting;
    public Canvas Resouces;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Screen.orientation = ScreenOrientation.Portrait;
        Villase.GetComponent<VIllas_panal>().Start_Villas();
    }
    public void comunity_click()
    {
        Community.GetComponent<Community_panal>().Start_Villas();
        Villase.enabled = false;
        Community.enabled = true;
        Contactus.enabled = false;
        menu.enabled = false;
        View_all_panal.enabled = false;
        My_build.enabled = false;
        Resouces.enabled = false;
        Setting.enabled = false;
    }
    public void Villas_click()
    {
        Villase.GetComponent<VIllas_panal>().Start_Villas();
        Villase.enabled = true;
        Community.enabled = false;
        Contactus.enabled = false;
        menu.enabled = false;
        View_all_panal.enabled = false;
        My_build.enabled = false;
        Resouces.enabled = false;
        Setting.enabled = false;
    }
    public void ContactUs_click()
    {
        Contactus.enabled = true;
    }
    public void View_all_click()
    {
        Villase.enabled = false;
        Community.enabled = false;
        Contactus.enabled = false;
        menu.enabled = false;
        View_all_panal.enabled = true;
        My_build.enabled = false;
        Resouces.enabled = false;
        Setting.enabled = false;
    }
    public void Resources_click()
    {
        Community.GetComponent<Community_panal>().Start_Villas();
        Villase.enabled = false;
        Community.enabled = false;
        Contactus.enabled = false;
        menu.enabled = false;
        View_all_panal.enabled = false;
        My_build.enabled = false;
        Resouces.enabled = true;
        Setting.enabled = false;
    }
    public void My_build_click()
    {
        Community.GetComponent<Community_panal>().Start_Villas();
        Villase.enabled = false;
        Community.enabled = false;
        Contactus.enabled = false;
        menu.enabled = false;
        View_all_panal.enabled = false;
        My_build.enabled = true;
        Resouces.enabled = false;
        Setting.enabled = false;
    }
    public void Setting_click()
    {
        Community.GetComponent<Community_panal>().Start_Villas();
        Villase.enabled = false;
        Community.enabled = false;
        Contactus.enabled = false;
        menu.enabled = false;
        View_all_panal.enabled = false;
        My_build.enabled = false;
        Resouces.enabled = false;
        Setting.enabled = true;
    }
    public void View_all_Back_click()
    {
        Villase.enabled = true;
        Community.enabled = false;
        Contactus.enabled = false;
        menu.enabled = false;
        View_all_panal.enabled = false;
        My_build.enabled = false;
        Resouces.enabled = false;
        Setting.enabled = false;
    }
    public void Menu_click()
    {     
        menu.enabled = true;
    }
    public void Backmenu_click()
    {
        menu.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
