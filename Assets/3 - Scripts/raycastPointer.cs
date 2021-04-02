using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class raycastPointer : MonoBehaviour
{
    public Material material;
    public Transform pointer;

    private LineRenderer lineRenderer;

    void Start() 
    {
        lineRenderer = pointer.gameObject.AddComponent<LineRenderer>();
        
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(material);
        lineRenderer.positionCount = 2;

        //pointerFinger = gameObject.transform.Find("ControllerButtonHints");
        //Debug.Log(pointerFinger.ToString());
    }

    void Update()
    {
        // if (pointerFinger == null && gameObject.transform.Find("RightRenderModel Slim(Clone)") != null)
        // {
        //     pointerFinger = getMyFinger(gameObject.transform.Find("RightRenderModel Slim(Clone)"));
        // }    

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            lineRenderer.enabled = true;

            var hit_obj = hit.collider.gameObject;
            var end = hit.point;

            if ((hit_obj.name).ToString().Contains("Btn"))
            {
                var button = hit_obj.GetComponent<Button>();
                button.Select();
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

            // var newBegin = new Vector3(pointerFinger.position.x, pointerFinger.position.y, pointerFinger.position.z);
            // var newEnd = new Vector3(end.x, pointerFinger.position.y, end.z);

            var newBegin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            var newEnd = new Vector3(end.x, end.y, end.z);

            lineRenderer.SetPosition(0, newBegin);
            lineRenderer.SetPosition(1, newEnd);
        }
        else
        {
            lineRenderer.enabled = false;
        }    
    }

    public Transform GetMyFinger(Transform hand)
    {
        return hand.Find("vr_glove_right_model_slim(Clone)/slim_r/Root/finger_index_r_aux");
    }
}
