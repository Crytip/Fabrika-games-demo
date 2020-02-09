using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public static manager _manager;
    public GameObject randomempty;

    private void Awake()//create the singleton
    {
        if (manager._manager == null)
        {
            manager._manager = this;
        }
    }

    [SerializeField]
    private int stage;

    public GameObject player;
    private GameObject _player;

    public GameObject wheels;
    public GameObject trap1;
    public GameObject trap2;
    public GameObject speed_up_thingy;
    public GameObject chest;

    public GameObject road;
    private GameObject current_road;

    private bool first_time;

    private List<Vector3> drawn_points = new List<Vector3>();
    private List<Vector3> road_poins = new List<Vector3>();
    private List<GameObject> emptys = new List<GameObject>();
    private List<GameObject> roads = new List<GameObject>();
    public List<GameObject> wheels_list = new List<GameObject>();
    private List<GameObject> in_game_objs = new List<GameObject>();

    private PathCreator pth;
    public BezierPath BPath;

    //uı elements
    [SerializeField]
    private Text current_stage_txt;
    [SerializeField]
    private Text next_stage_txt;
    [SerializeField]
    private Slider stage_slider;
    [SerializeField]
    private GameObject menu_panel;
    [SerializeField]
    private GameObject draw_panel;
    [SerializeField]
    private GameObject exit_button;

    
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.SetQualityLevel(0);
        stage = 1;
        first_time = true;

        
        
        
    }

    public void start_game()//onclick event
    {
        StartCoroutine(start_stage());
        Camera.main.gameObject.GetComponent<camera_follow>().stage_slider = stage_slider.gameObject;
        draw_panel.SetActive(true);
        menu_panel.SetActive(false);
        stage_slider.gameObject.SetActive(true);
        exit_button.SetActive(true);
    }

    public Vector3 get_pos(Vector3 current_player_pos)//gives spawn points if the player falls of the road
    {
        int x = (int)(current_player_pos.x / 50);
        if (x <= 1)
        {
            return new Vector3(5, 5, 0);
        }
        else
        {
            return new Vector3(5+50*x-50, 5, 0);
        }
    }


    IEnumerator start_stage()
    {
        create_road(stage);
        yield return new WaitForSeconds(2);
        create_player(drawn_points);
    }

    void create_road(int _stage)
    {
        road_poins.Clear();

        

        road_poins.Add(new Vector3(0, 0, 0));
        road_poins.Add(new Vector3(5, 0, 0));
        road_poins.Add(new Vector3(10, 0, 0));


        if (stage == 1)
        {
            
            current_stage_txt.text = stage.ToString();
            next_stage_txt.text = (stage + 1).ToString();
            stage_slider.minValue = 0;
            stage_slider.maxValue = 220;

            //first road
            Debug.Log("we are creating the road");
            current_road = Instantiate(road);
            current_road.transform.position = new Vector3(0, 0, 0);
            pth = current_road.GetComponent<PathCreator>();
            roads.Add(current_road);

            road_poins.Add(new Vector3(15, 0, 0));
            road_poins.Add(new Vector3(20, 0, 0));
            road_poins.Add(new Vector3(35, 5, 0));
            road_poins.Add(new Vector3(50, -3, 0));
            road_poins.Add(new Vector3(65, 10, 0));
            road_poins.Add(new Vector3(80, 5, 0));

            BPath = new BezierPath(road_poins, false, PathSpace.xyz);
            BPath.GlobalNormalsAngle = 90;
            pth.bezierPath = BPath;

            //second road
            current_road = Instantiate(road);
            current_road.transform.position = new Vector3(0, 0, 0);
            pth = current_road.GetComponent<PathCreator>();
            roads.Add(current_road);

            road_poins.Clear();

            road_poins.Add(new Vector3(85, 0, 0));
            road_poins.Add(new Vector3(100, 0, 0));
            road_poins.Add(new Vector3(115, 5, 0));
            road_poins.Add(new Vector3(130, -5, 0));
            road_poins.Add(new Vector3(145, 10, 0));
            road_poins.Add(new Vector3(160, 0, 0));
            road_poins.Add(new Vector3(220,0,0));
            BPath = new BezierPath(road_poins, false, PathSpace.xyz);
            BPath.GlobalNormalsAngle = 90;
            pth.bezierPath = BPath;

            GameObject obj = Instantiate(chest);
            obj.transform.position = new Vector3(220,1,0);

        }
        create_traps();
        //BPath.AddSegmentToEnd(new Vector3(5,5,5));
        
    }
    private void create_traps()
    {
        GameObject obj = Instantiate(trap1);
        obj.transform.position = new Vector3(15,3,2.2f);

        in_game_objs.Add(obj);

        obj = Instantiate(trap2);
        obj.transform.position = new Vector3(178, 3.24f, 0);
        in_game_objs.Add(obj);

        obj = Instantiate(trap2);
        obj.transform.position = new Vector3(188, 3.24f, 0);
        in_game_objs.Add(obj);

        obj = Instantiate(trap2);
        obj.transform.position = new Vector3(201, 4.5f, 0);
        in_game_objs.Add(obj);

        obj = Instantiate(speed_up_thingy);
        obj.transform.position = new Vector3(57,6,0);
        obj.transform.eulerAngles = new Vector3(-40,90,0);

        in_game_objs.Add(obj);

        obj = Instantiate(speed_up_thingy);
        obj.transform.position = new Vector3(95, 1, 0);
        obj.transform.eulerAngles = new Vector3(-10, 90, 0);

        in_game_objs.Add(obj);


    }

    public void create_player(List<Vector3> points)
    {
        
        if (points.Count== 0)
        {
            Debug.Log("do nothing");
        }
        else
        {
            
            emptys.Clear();
            GameObject obj;
            for (int i = 0; i < points.Count; i++)
            {
                obj = Instantiate(randomempty);
                obj.transform.position = gameObject.GetComponent<draw_lines>().draw_cam.ScreenToWorldPoint(points[i]);
                emptys.Add(obj);
            }
            obj = new GameObject();
            obj.transform.position = gameObject.GetComponent<draw_lines>().draw_cam.ScreenToWorldPoint(points[0]);
            for (int i = 0; i < emptys.Count; i++)
            {
                emptys[i].transform.parent = obj.transform;
            }
            obj.transform.position = new Vector3(0, 0, 0);
            for (int i = 0; i < emptys.Count; i++)
            {
                emptys[i].transform.parent = null;
            }

            if (_player != null)
            {
                obj = Instantiate(player);
                obj.GetComponent<player_essentialcontrols>().manager = gameObject;
                obj.transform.position = _player.transform.position;
                Destroy(_player.gameObject);
                _player = obj;
            }
            else
            {
                _player = Instantiate(player);
                _player.GetComponent<player_essentialcontrols>().manager = gameObject;
                
                
                
            }

            for (int i = 0; i < emptys.Count; i++)
            {
                Debug.Log("how many times this fires");
                if (_player.transform.GetComponent<Spline>().nodes.Count > i)
                {

                    _player.transform.GetComponent<Spline>().nodes[i].Position = emptys[i].transform.position;
                    
                }
                else
                {
                    SplineNode node = new SplineNode(emptys[i].transform.position, emptys[i].transform.position);
                    _player.transform.GetComponent<Spline>().AddNode(node);
                }

            }
            if (first_time)
            {
                _player.transform.position = new Vector3(5, 2, 0);
                first_time = false;
            }
            

            create_wheels();
            Camera.main.gameObject.GetComponent<camera_follow>().player = _player;
            for (int i = 0; i < emptys.Count; i++)
            {
                Destroy(emptys[i]);
            }
            emptys.Clear();
        }
        
    }

    void create_wheels()
    {
        if (wheels_list.Count == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                Destroy(wheels_list[i]);

            }
        }
        
        wheels_list.Clear();
        GameObject obj = Instantiate(wheels);
        obj.transform.position = _player.transform.GetComponent<Spline>().nodes[0].Position+ _player.transform.position;
        obj.transform.parent = _player.transform;
        wheels_list.Add(obj);

        

        obj = Instantiate(wheels);
        obj.transform.position = _player.transform.GetComponent<Spline>().nodes[_player.transform.GetComponent<Spline>().nodes.Count-1].Position + _player.transform.position;
        obj.transform.parent = _player.transform;
        wheels_list.Add(obj);
        WheelCollider WC = obj.transform.GetComponent<WheelCollider>();
        WC.mass = 20;
    }

    public void exit_to(int where)
    {
        if (where ==1)//to the main menu
        {
            for (int i = 0; i < roads.Count; i++)
            {
                Destroy(roads[i]);
            }
            roads.Clear();
            Destroy(_player);
            menu_panel.SetActive(true);
            draw_panel.SetActive(false);
            stage_slider.gameObject.SetActive(false);
            exit_button.SetActive(false);

            for (int i = 0; i < in_game_objs.Count; i++)
            {
                Destroy(in_game_objs[i]);
            }
            in_game_objs.Clear();
        }
        else if (where == 2)//from the game
        {
            Application.Quit();
        }
    }


}
