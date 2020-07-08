using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    public float pan_speed = 20;
    public float zoom_speed = 1000;
    public Vector2 area_limits_x = new Vector2(-100, 100);
    public Vector2 area_limits_y = new Vector2(-100, 100);
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
    

    // Start is called before the first frame update
    void Start()
    {
        area_limits2 = manager.get_map_size();
        #if !UNITY_EDITOR
        fingerID = 0; 
        #endif
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
        // if (Input.GetKey("+"))
        // {
        //     pos.y += pan_speed * Time.deltaTime;
        // }
        // if (Input.GetKey("-"))
        // {
        //     pos.y -= pan_speed * Time.deltaTime;
        // }

        pos.x = Mathf.Clamp(pos.x, 50, area_limits2[1]);
        pos.z = Mathf.Clamp(pos.z, 0, area_limits2[1]);
        cam.transform.position = pos;

        float wheel_delta = Input.mouseScrollDelta.y;
        float factor = zoom_speed * Time.deltaTime;
        if ((wheel_delta > 0 && pos.y - factor<= max_zoom_in) || (wheel_delta < 0 && pos.y + factor >= max_zoom_out)) {
            wheel_delta = 0;
        }
        cam.transform.Translate(wheel_delta * Vector3.forward * zoom_speed * Time.deltaTime);


        // click happened
        if (Input.GetMouseButtonDown(1)) {
            // Debug.Log("Button down!");
            RaycastHit hit__;
            Ray ray__ = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray__, out hit__, 2000.0f, 1 << tile_layer_mask))
                if (hit__.transform != null)
                {
                    previous_raycast_length = Vector3.Distance(hit__.transform.position, cam.transform.position);
                    previous_raycast_hit = hit__.point;
                    
                }
        } else {  // only if not first button
            if (Input.GetMouseButton(1)) {
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
                        // Debug.Log(string.Format("Previous: {0}\nCurrent:{1}", previous_raycast_hit, new_hit));

                        float vectorfactor = (cam.transform.position.y - previous_raycast_hit.y) / (cam.transform.position.y - new_hit.y);
                        Vector3 target_vector = cam_to_new_hit * vectorfactor;

                        // Debug.Log(string.Format("Vector: {0}", cam.transform.position + new Vector3(target_vector.x, -target_vector.y, target_vector.z)));
                        // Debug.Log(string.Format("Vector: {0}", target_vector));
                        // Debug.Log(string.Format("From: {0}\n                          To:{1}", (previous_raycast_hit), (cam.transform.position - target_vector)));

                        // Debug.Log(string.Format("From: {0}\n                          To:{1}", previous_raycast_hit, (cam.transform.position + target_vector)));
                        Vector3 movement = previous_raycast_hit - (cam.transform.position - target_vector);
                        // Debug.Log(string.Format("From: {0}\n                          To:{1}", previous_raycast_hit, movement));
                        // Debug.Log(string.Format("From: {0}\n                          To:{1}", previous_raycast_hit, ray_vector));


                        float pos_x = Mathf.Clamp(cam.transform.position.x + movement.x, 50, area_limits2[0]);
                        float pos_y = cam.transform.position.y;
                        float pos_z = Mathf.Clamp(cam.transform.position.z + movement.z, 0, area_limits2[1]);
                        cam.transform.position = new Vector3(pos_x, pos_y, pos_z);

                    }
            }






            // if (Input.GetMouseButton(1))
            // {
            //     RaycastHit hit_;
            //     Ray ray_ = cam.ScreenPointToRay(Input.mousePosition);

            //     if (Physics.Raycast(ray_, out hit_, 2000.0f, 1 << tile_layer_mask))
            //         if (hit_.transform != null)
            //         {
            //             distance = Vector3.Distance(hit_.transform.position, cam.transform.position);
            //             rayhit = true;
            //         }
            //     if (!rayhit) {
            //         float old_y = cam.transform.position.y;
            //         float pan_factor = Mathf.Max(((1.0f / (max_zoom_out - old_y) / max_zoom_out) * Time.deltaTime) * 500000.0f, 0.0f);
            //         // float pan_factor = ((((old_y) / max_zoom_out)) * Time.deltaTime * 100.0f);
            //         // Screen.currentResolution
            //         // Debug.Log(string.Format("X: {0}, Y: {1}", Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
            //         cam.transform.Translate(new Vector3(-Input.GetAxis("Mouse X") * pan_factor, -Input.GetAxis("Mouse Y") * pan_factor, 0));
            //         // Fix elevation of camera
            //         cam.transform.position = new Vector3(cam.transform.position.x, old_y, cam.transform.position.z);
            //     } else {
            //         float old_y = cam.transform.position.y;

            //         float pan_factor = Mathf.Sqrt(((distance * Time.deltaTime) * 0.35f));
            //         Debug.Log(((distance * Time.deltaTime) * 0.35f));
            //         cam.transform.Translate(new Vector3(-Input.GetAxis("Mouse X") * pan_factor, -Input.GetAxis("Mouse Y") * pan_factor, 0));
            //         // Fix elevation of camera
            //         cam.transform.position = new Vector3(cam.transform.position.x, old_y, cam.transform.position.z);
            //     }
            // }
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 200.0f, 1 << tile_layer_mask & 1 << ui_layer_mask) && !EventSystem.current.IsPointerOverGameObject(fingerID)) {
                if (hit.transform != null){
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
                    if (istile){
                        Tile tile = hit.collider.GetComponent<Tile>();
                        Debug.Log(string.Format("Clicked tile is {0}", tile.name));

                        if (manager.debugMode) {
                            if (tile.navigationPotentials.Count == 0) {
                                Debug.Log(string.Format("No potentials saved"));
                            }
                            foreach (var item in tile.navigationPotentials){
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

                        } else {
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
}
