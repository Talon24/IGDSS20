using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
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

        pos.x = Mathf.Clamp(pos.x, area_limits_x[0], area_limits_x[1]);
        pos.z = Mathf.Clamp(pos.z, area_limits_y[0], area_limits_y[1]);
        cam.transform.position = pos;

        float wheel_delta = Input.mouseScrollDelta.y;
        float factor = zoom_speed * Time.deltaTime;
        if ((wheel_delta > 0 && pos.y - factor<= max_zoom_in) || (wheel_delta < 0 && pos.y + factor >= max_zoom_out)) {
            wheel_delta = 0;
        }
        cam.transform.Translate(wheel_delta * Vector3.forward * zoom_speed * Time.deltaTime);



        if (Input.GetMouseButton(1)) {
            float old_y = cam.transform.position.y;
            float pan_factor = Mathf.Max(((1.0f / (max_zoom_out - old_y) / max_zoom_out) * Time.deltaTime) * 500000.0f, 0.0f);
            // Debug.Log(pan_factor);
            cam.transform.Translate(new Vector3(-Input.GetAxis("Mouse X") * pan_factor, -Input.GetAxis("Mouse Y") * pan_factor, 0));
            cam.transform.position = new Vector3(cam.transform.position.x, old_y, cam.transform.position.z);
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 200.0f, 1 << tile_layer_mask)) {
                if (hit.transform != null){
                    Debug.Log(string.Format("Clicked tile is {0}", hit.transform.gameObject.name));
                }
            }

        }



    }
}
