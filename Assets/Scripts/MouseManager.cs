using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public int max_in = 0;
    public int max_out = 200;
    public float pan_speed = 20;
    public float zoom_speed = 1000;
    public Vector2 area_limits_x = new Vector2(-100, 100);
    public Vector2 area_limits_y = new Vector2(-100, 100);
    public Vector2 area_limits_z = new Vector2(-100, 100);
    private Vector2 moused;
    public Camera cam;
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
        Debug.Log(pos);
        cam.transform.position = pos;

        cam.transform.Translate(Input.mouseScrollDelta.y * Vector3.forward * zoom_speed * Time.deltaTime);

    }
}
