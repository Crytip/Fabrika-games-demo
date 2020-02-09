using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotating_trap : MonoBehaviour
{
    bool rotating = false;
    Vector3 angle;
    public int rotation_speed;

    void Start()
    {
        StartCoroutine(start_rotating());
    }

    IEnumerator start_rotating()
    {
        yield return new WaitForSeconds(2);
        rotating = true;
        angle = transform.eulerAngles;
    }

    
    void Update()
    {
        if (rotating)
        {
            transform.Rotate(Vector3.right*Time.deltaTime*rotation_speed);
            if (transform.eulerAngles.x >= angle.x + 90 && gameObject.transform.name == "rotating trap(Clone)")
            {

                rotating = false;
                transform.eulerAngles = new Vector3(angle.x + 90, transform.eulerAngles.y, transform.eulerAngles.z);
                StartCoroutine(start_rotating());
            }
        }
    }
}
