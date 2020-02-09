using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class camera_follow : MonoBehaviour
{
    public GameObject player;
    public GameObject stage_slider;
    private Vector3 velocity;
    private Vector3 direction;
    

    
    void Update()
    {
        if (player != null)
        {
            velocity = player.GetComponent<Rigidbody>().velocity;
            stage_slider.GetComponent<Slider>().value = player.transform.position.x;
            
            transform.position = player.transform.position + new Vector3(-1.7f, 2.7f, -7.3f)*4;
            
            player.transform.eulerAngles = new Vector3(0, 0, player.transform.eulerAngles.z);
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
            if (player.transform.position.y < -5)
            {
                player.transform.position = manager._manager.get_pos(player.transform.position);
                //since the player essential control doesnt have any updates doing it here is more efficient even though it doesnt look clean
            }
        }
        else
        {
            transform.position = new Vector3(0, 0, -15.35f);
        }
        
        
    }
}
