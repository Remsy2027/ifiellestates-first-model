using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialandColorChangescript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool colorchange;
    public List<Color> List_color;
    public int count;
	public bool Singlematerial;
	public Material Single_Main_material_list;
	public List<Material> Main_material_list;
	public bool texturechange;
    public List<Texture2D> List_texture;
    public bool matiralchange;
    public List<Material> newmatrial;
    Material old;
    void Start()
    {
        count = 0;
       // old = GetComponent<MeshRenderer>().material;
    }
    public void change_material_color()
    {
       
        if (colorchange)
        {
            if (count < List_color.Count)
            {
            }
            else
            {
                count = 0;
            }
			
			if(Singlematerial)
			{		
				if (count < List_color.Count)
				{
				}
				else
				{
					count = 0;
				}		
				Single_Main_material_list.color = List_color[count];
			}else{
				if (count < List_color.Count)
				{
				}
				else
				{
					count = 0;
				}
				foreach(var allmaterial in Main_material_list){
			    allmaterial.color = List_color[count];
			}	
			}
          

        }
		else if(Singlematerial && texturechange){
			
			if (count < List_texture.Count)
            {
            }
            else
            {
                count = 0;
            }
			Single_Main_material_list.SetTexture("_BaseMap", List_texture[count]);

        }
		else if(!Singlematerial && texturechange){
			if (count < List_texture.Count)
            {
            }
            else
            {
                count = 0;
            }
			foreach(var allmaterial in Main_material_list){
			
					allmaterial.SetTexture("_BaseMap", List_texture[count]);
			}
		} else if (matiralchange)
        {
            /*  Material[] newmateriallist = new Material[] { newmatrial[count] };
              int i = 0;
              foreach(Material m in newmatrial)
              {
                  newmateriallist[i] = m;
              } */

            if (count < newmatrial.Count)
            {
            }
            else
            {
                count = 0;
            }
            Material[] newmaterial = new Material[] { newmatrial[count] };

            GetComponent<MeshRenderer>().materials = newmaterial;
            /////transform.GetComponent<MeshRenderer>().materials = newmateriallist;
        }
		 count++;
        /*else
        {
            if (count < List_material.Count)
            {
            }
            else
            {
                count = 0;
            }
         //   Debug.Log("here");
            Material[] newmaterial = new Material[] { List_material[count] };
            
            GetComponent<MeshRenderer>().materials = newmaterial;
        }*/

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
