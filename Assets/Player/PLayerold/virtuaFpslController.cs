using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class virtuaFpslController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler//, IPointerDownHandler, IPointerUpHandler
{
    public fpsController fpsController; 
    public float lookVer, lookHor;
    public RectTransform Handle;
    float time;

    void Start()
    {
        //StartCoroutine(Count()); 

    }
    void Update()
    {
        //Debug.Log(Handle.localPosition); 
        //Debug.Log(time);

    }

    public void OnBeginDrag(PointerEventData eventData)
    { 
        Handle.transform.localPosition = transform.InverseTransformPoint(eventData.pressPosition);
    }
  
    public void OnDrag(PointerEventData eventData)
    {
        Handle.localPosition += (Vector3)eventData.delta;
        

        if (eventData.dragging)
        {
            lookHor = Input.GetAxisRaw("Mouse X");
            lookVer = Input.GetAxisRaw("Mouse Y");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lookHor = 0;
        lookVer = 0;
    }
   /* void Update()
    {
        //Debug.Log(Handle.localPosition); 
        //Debug.Log(time);
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;

            }
            lookHor = TouchDist.x;
            lookVer = TouchDist.y;
        }
        else
        {
            TouchDist = new Vector2();
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        lookHor = 0;
        lookVer = 0;
    }*/
}

