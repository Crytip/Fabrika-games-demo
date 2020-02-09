using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_essentialcontrols : MonoBehaviour
{
    public GameObject manager;

    private ParticleSystem ps;
    private WheelCollider WC1;
    private WheelCollider WC2;
    private WheelFrictionCurve WFC1;
    private WheelFrictionCurve WFC2;

    void Start()
    {
        StartCoroutine(get_childs());
        ps = gameObject.GetComponent<ParticleSystem>();
        
        ps.Stop();

    }

    IEnumerator get_childs()
    {
        yield return new WaitForSeconds(0.2f);
        WC1 = manager.GetComponent<manager>().wheels_list[0].gameObject.GetComponent<WheelCollider>();
        WC2 = manager.GetComponent<manager>().wheels_list[1].gameObject.GetComponent<WheelCollider>();
        WFC1 = WC1.forwardFriction;
        WFC2 = WC2.forwardFriction;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<chestscp>().open_chest();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8)
        {
            StartCoroutine(gofastforawhile());
        }
    }

    IEnumerator gofastforawhile()
    {
        ps.Play();
        WFC1.stiffness = 3;
        WFC2.stiffness = 3;
        WC1.forwardFriction = WFC1;
        WC2.forwardFriction = WFC2;
        yield return new WaitForSeconds(3);
        WFC1.stiffness = 1.5f;
        WFC2.stiffness = 1.5f;
        WC1.forwardFriction = WFC1;
        WC2.forwardFriction = WFC2;
        ps.Stop();
    }
}
