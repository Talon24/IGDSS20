using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public float pan_speed = 20;
    public float zoom_speed = 1000;
    public int max_zoom_in = 10;
    public int max_zoom_out = 100;
    public Camera cam;
    public int tile_layer_mask = 8;
    public int ui_layer_mask = 5;
    private Vector3 previous_raycast_hit;
    private float previous_raycast_length;
    public GameManager manager;

    private Vector2 area_limits2;
    public int fingerID = -1;
    public Canvas InfoField;
    

    // Start is called before the first frame update
    void Start()
    {
        area_limits2 = manager.get_map_size();
        // #if !UNITY_EDITOR
        // fingerID = 0; 
        // #endif
    }

    // Update is called once per frame

    void Update(){
        Vector3 pos = cam.transform.position;
        if (Input.GetKey("w")){
            pos.x -= pan_speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.x += pan_speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.z -= pan_speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.z += pan_speed * Time.deltaTime;
        }
        pos.x = Mathf.Clamp(pos.x, 0, area_limits2[1]);
        pos.z = Mathf.Clamp(pos.z, 0, area_limits2[1]);
        cam.transform.position = pos;
        Zoom();
        MouseMovement();
        CheckTileClick();
        ManageInfoWindow();
    }

    void Zoom(){
        Vector3 pos = cam.transform.position;

        float wheel_delta = Input.mouseScrollDelta.y;
        float factor = zoom_speed;
        if ((wheel_delta > 0 && pos.y <= max_zoom_in) || (wheel_delta < 0 && pos.y >= max_zoom_out))
        {
            wheel_delta = 0;
        }
        cam.transform.Translate(wheel_delta * Vector3.forward * zoom_speed);
    }
    void MouseMovement(){
        if (Input.GetMouseButtonDown(1))
        {
            // Debug.Log("Button down!");
            RaycastHit hit__;
            Ray ray__ = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray__, out hit__, 2000.0f, 1 << tile_layer_mask))
                if (hit__.transform != null)
                {
                    previous_raycast_length = Vector3.Distance(hit__.transform.position, cam.transform.position);
                    previous_raycast_hit = hit__.point;

                }
        }
        else
        {  // only if not first button
            if (Input.GetMouseButton(1))
            {
                float cam_height_rel = cam.transform.position.y - previous_raycast_hit.y;

                RaycastHit hit___;
                Ray ray___ = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray___, out hit___, 2000.0f, 1 << tile_layer_mask))
                    if (hit___.transform != null)
                    {
                        float new_distance = Vector3.Distance(hit___.transform.position, cam.transform.position);
                        Vector3 new_hit = hit___.point;
                        Vector3 ray_vector = ray___.direction;
                        Vector3 cam_to_new_hit = cam.transform.position - new_hit;

                        float vectorfactor = (cam.transform.position.y - previous_raycast_hit.y) / (cam.transform.position.y - new_hit.y);
                        Vector3 target_vector = cam_to_new_hit * vectorfactor;
                        Vector3 movement = previous_raycast_hit - (cam.transform.position - target_vector);


                        float pos_x = Mathf.Clamp(cam.transform.position.x + movement.x, 50, area_limits2[0]);
                        float pos_y = cam.transform.position.y;
                        float pos_z = Mathf.Clamp(cam.transform.position.z + movement.z, 0, area_limits2[1]);
                        cam.transform.position = new Vector3(pos_x, pos_y, pos_z);

                    }
            }
        }
    }
    void CheckTileClick(){
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 200.0f, 1 << tile_layer_mask & 1 << ui_layer_mask) && !EventSystem.current.IsPointerOverGameObject(fingerID))
            {
                if (hit.transform != null)
                {
                    // Debug.Log(string.Format("Clicked tile is {0}", hit.transform.gameObject.name));
                    // Debug.Log(string.Format("Clicked tile is {0}", hit.transform.gameObject.GetType()));
                    bool istile = false;
                    try
                    {
                        istile = hit.collider.GetComponent<Tile>() != null;
                        // Debug.Log(string.Format("Clicked tile is {0}", tile.name));
                        // Debug.Log(string.Format("Clicked tile is {0}", tile.position));
                    }
                    catch (System.NullReferenceException)
                    {
                        Debug.Log(string.Format("Nothing targetted."));
                    }
                    if (istile)
                    {
                        Tile tile = hit.collider.GetComponent<Tile>();
                        Debug.Log(string.Format("Clicked tile is {0}", tile.name));

                        if (manager.debugMode)
                        {
                            if (tile.navigationPotentials.Count == 0)
                            {
                                Debug.Log(string.Format("No potentials saved"));
                            }
                            foreach (var item in tile.navigationPotentials)
                            {
                                Debug.Log(string.Format("Potential to {0} is {1}", item.Key, item.Value));
                            }

                            foreach (var target in tile.navigationPotentials)
                            {
                                Tile targetTile = target.Key;
                                NavigationManager nav = GameObject.FindObjectOfType<NavigationManager>();
                                List<Tile> path = nav.getPath(tile, targetTile);
                                foreach (Tile pathTile in path)
                                {
                                    // pathTile.gameObject.SetActive(false);
                                    Debug.Log(string.Format("Step goes over {0}", pathTile.position));
                                }
                                break;
                            }

                        }
                        else
                        {
                            manager.PlaceBuildingOnTile(tile);
                        }
                        // tile.test();
                        // Debug.Log(string.Join(", ", tile.neighbors));
                        // foreach (Tile tile_ in tile.neighbors)
                        // {
                        //     Debug.Log(string.Format("{0}", tile_.type.name));
                        // }
                    }
                }
            }
        }
    }

    void ManageInfoWindow(){
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            InfoField.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            InfoField.gameObject.SetActive(false);
        }
        //Put info field next to cursor
        InfoField.transform.position = Input.mousePosition;
        
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 200.0f, 1 << tile_layer_mask & 1 << ui_layer_mask))
        {
            if (hit.transform != null)
            {
                try
                {
                    List<(string, string)> items;
                    Tile tile = hit.collider.GetComponent<Tile>();
                    items = tile.Properties();
                    string output1 = "";
                    string output2 = "";
                    foreach ((string, string) item in items)
                    {
                        output1 += item.Item1 + "\n";
                        output2 += item.Item2 + "\n";
                    }
                    foreach (Text t in InfoField.GetComponentsInChildren<Text>())
                    {
                        if (t.name == "Keys")
                        {
                            t.text = output1; // Force resizing the tooltip
                            t.text = output1.Remove(output1.Length - 1, 1);
                        } 
                        else if (t.name == "Values")
                        {
                            t.text = output2; // Force resizing the tooltip
                            t.text = output2.Remove(output2.Length - 1, 1);
                        }
                    }

                    // Text text = InfoField.GetComponent<Text>();

                    
                }
                catch (System.NullReferenceException)
                {
                    Debug.Log(string.Format("Null pointer reference thrown"));
                }
            } else {
                Debug.Log(string.Format("No transform was hit"));
            }
        }
    }
}

