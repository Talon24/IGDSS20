using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D heightmap;
    public Transform water_tile;
    public Transform sand_tile;
    public Transform grass_tile;
    public Transform forest_tile;
    public Transform stone_tile;
    public Transform mountain_tile;
    void Start()
    {
        float edge_length = 17.323232f;
        float long_diameter = 10f;
        for (int y = 0; y < heightmap.height; y++){
            for (int x = 0; x < heightmap.width; x++)
            {
                UnityEngine.Color pixel = heightmap.GetPixel(x, y);
                float pixel_val = Mathf.Max(pixel.r, pixel.g, pixel.b);
                // Debug.Log((x, y, pixel_val));
                // Debug.Log((x, y, pixel_val, pixel));
                Vector3 position = new Vector3();


                float height = 0f;

                Transform tile = null;
                // Fixed height
                // if (0.0f == (float) pixel_val) {tile = water_tile;}
                // else if (0.0 < pixel_val && pixel_val <= 0.2f) { tile = sand_tile; height = 1f;}
                // else if (0.2 < pixel_val && pixel_val <= 0.4f) { tile = grass_tile; height = 3f; }
                // else if (0.4 < pixel_val && pixel_val <= 0.6f) { tile = forest_tile; height = 4f; }
                // else if (0.6 < pixel_val && pixel_val <= 0.8f) { tile = stone_tile; height = 6f; }
                // else if (0.8 < pixel_val && pixel_val <= 1.0f) { tile = mountain_tile; height = 10f; }

                // Heightmap height
                float multiplicator = 30f;
                if (0.0f == (float)pixel_val) { tile = water_tile; }
                else if (0.0 < pixel_val && pixel_val <= 0.2f) { tile = sand_tile; height = pixel_val * multiplicator; }
                else if (0.2 < pixel_val && pixel_val <= 0.4f) { tile = grass_tile; height = pixel_val * multiplicator; }
                else if (0.4 < pixel_val && pixel_val <= 0.6f) { tile = forest_tile; height = pixel_val * multiplicator; }
                else if (0.6 < pixel_val && pixel_val <= 0.8f) { tile = stone_tile; height = pixel_val * multiplicator; }
                else if (0.8 < pixel_val && pixel_val <= 1.0f) { tile = mountain_tile; height = pixel_val * multiplicator; }

                if (x % 2 == 0)
                {
                    position = new Vector3(x / 2 * edge_length, height, y * long_diameter);
                }
                else
                {
                    position = new Vector3(((x - 1) / 2 * edge_length) + 8.66f, height, (y * long_diameter) + 5);
                }

                float rotation =  360f / 6f * Random.Range(0, 5);
                // Debug.Log(rotation);
                Object.Instantiate(tile, position, Quaternion.Euler(0, rotation, 0));
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
