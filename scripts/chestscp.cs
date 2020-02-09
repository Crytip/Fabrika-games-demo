using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestscp : MonoBehaviour
{
    private bool open;
    private bool called_once;
    private GameObject kapak_holder;
    private ParticleSystem ps;
    void Start()
    {
        ps = gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
        ps.Stop();
        open = false;
        called_once = false;
        kapak_holder = gameObject.transform.GetChild(1).gameObject;
    }

    
    public void open_chest()
    {
        if (called_once == false)
        {
            called_once = true;
            open = true;
        }
    }

    void Update()
    {
        if (open)
        {
            kapak_holder.transform.Rotate(new Vector3(0,0,-1)*5);
            if (kapak_holder.transform.localEulerAngles.z < 270)
            {
                open = false;
                ps.Play();
            }
        }   
    }
}
