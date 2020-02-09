using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class draw_lines : MonoBehaviour
{
    [SerializeField]
    private GameObject line_obj;
    private LineRenderer linerenderer;
    private List<Vector3> drawn_line_points = new List<Vector3>();

    [SerializeField]
    private GameObject draw_area;
    [SerializeField]
    private GameObject too_smal_alert;


    public Camera draw_cam;

    private int touch_index;

    void Start()
    {
        linerenderer = line_obj.GetComponent<LineRenderer>();
    }

    public void pointer_enter()
    {
        foreach (Touch item in Input.touches)
        {
            if (item.phase == TouchPhase.Began)
            {
                //this is the touch which just started
                touch_index = item.fingerId;
                Vector3 sup = new Vector3(Input.touches[touch_index].position.x, Input.touches[touch_index].position.y, 100);
                linerenderer.SetPosition(0, draw_cam.ScreenToWorldPoint(sup));
                linerenderer.SetPosition(1, draw_cam.ScreenToWorldPoint(sup));
            }

        }
    }

    public void drawing()
    {
        Vector3 sup;
        Vector3 hey;
        if (drawn_line_points.Count == 0)
        {
            
            sup = new Vector3(Input.touches[touch_index].position.x, Input.touches[touch_index].position.y, 100);
            linerenderer.positionCount++;
            hey = draw_cam.ScreenToWorldPoint(sup);
            linerenderer.SetPosition(linerenderer.positionCount - 1, hey);
            drawn_line_points.Add(sup);
        }
        else if (Vector3.Distance(drawn_line_points[drawn_line_points.Count-1], Input.touches[touch_index].position) > 50)
        {
            
            Debug.Log(Input.touches[touch_index].position);
            sup = new Vector3(Input.touches[touch_index].position.x, Input.touches[touch_index].position.y, 100);
            linerenderer.positionCount++;
            hey = draw_cam.ScreenToWorldPoint(sup);
            linerenderer.SetPosition(linerenderer.positionCount - 1, hey);
            drawn_line_points.Add(sup);
        }
        
    }

    public void pointer_exit()
    {

        if (drawn_line_points.Count - 1 > 3)
        {
            
            linerenderer.positionCount = 2;
            gameObject.GetComponent<manager>().create_player(drawn_line_points);
            drawn_line_points.Clear();
        }
        else
        {
            linerenderer.positionCount = 2;
            drawn_line_points.Clear();
            StartCoroutine(alert(1));
        }

    }

    IEnumerator alert(int type)
    {
        if (type == 1)
        {
            too_smal_alert.SetActive(true);
        }
        
        yield return new WaitForSeconds(2);
        
        if (type == 1)
        {
            too_smal_alert.SetActive(false);
        }
    }
}
