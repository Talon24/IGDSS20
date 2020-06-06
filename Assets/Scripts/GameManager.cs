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

    private Tile[,] map;
    private float long_diameter = 10f;
    private float edge_length = 17.323232f;

    public Vector2 get_map_size(){
        float z = heightmap.width * 10f;
        float x = heightmap.height * 11.5470f;
        return new Vector2(x, z);
    }
    public Vector2 get_map_size_grid()
    {
        float z = heightmap.width;
        float x = heightmap.height;
        return new Vector2(x, z);
    }
    void Start()
    {
        // float edge_length = 17.323232f;
        // float long_diameter = 10f;
        this.map = new Tile[heightmap.height, heightmap.width];
        for (int y = 0; y < heightmap.height; y++){
            for (int x = 0; x < heightmap.width; x++)
            {
                UnityEngine.Color pixel = heightmap.GetPixel(x, y);
                float pixel_val = Mathf.Max(pixel.r, pixel.g, pixel.b);

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

                float rotation =  360f / 6f * Random.Range(0, 5);

                // map[x, y] = new Tile(tile, x, y, height, rotation);
                // map[x, y].place();
                Transform tileObject = Instantiate(tile, position_absolute(x, height, y), Quaternion.Euler(0, rotation, 0));
                // Debug.Log(tileObject.GetComponent<Tile>());
                Tile tile_ = tileObject.GetComponent<Tile>();
                tile_.position = new Vector3(x, height, y);
                tile_.rotation = rotation;
                map[x, y] = tile_;

            }
        }
    foreach (Tile tile in map) {
        tile.neighbors = FindNeighborsOfTile(tile);
    }
    }

    public Vector3 position_absolute(float x, float y, float z)
    {
        Vector3 abs_position;
        // Debug.Log((position.x, position.z));
        if (x % 2 == 0)
        {
            abs_position = new Vector3(x / 2 * edge_length, y, z * long_diameter);
        }
        else
        {
            abs_position = new Vector3(((x - 1) / 2 * edge_length) + 8.66f, y, (z * long_diameter) + 5);
        }
        return abs_position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Buildings
    public GameObject[] _buildingPrefabs; //References to the building prefabs
    public int _selectedBuildingPrefabIndex = 0; //The current index used for choosing a prefab to spawn from the _buildingPrefabs list
    #endregion


    #region Resources
    private Dictionary<ResourceTypes, float> _resourcesInWarehouse = new Dictionary<ResourceTypes, float>(); //Holds a number of stored resources for every ResourceType

    //A representation of _resourcesInWarehouse, broken into individual floats. Only for display in inspector, will be removed and replaced with UI later
    [SerializeField]
    private float _ResourcesInWarehouse_Fish;
    [SerializeField]
    private float _ResourcesInWarehouse_Wood;
    [SerializeField]
    private float _ResourcesInWarehouse_Planks;
    [SerializeField]
    private float _ResourcesInWarehouse_Wool;
    [SerializeField]
    private float _ResourcesInWarehouse_Clothes;
    [SerializeField]
    private float _ResourcesInWarehouse_Potato;
    [SerializeField]
    private float _ResourcesInWarehouse_Schnapps;
    #endregion

    #region Enumerations
    public enum ResourceTypes { None, Fish, Wood, Planks, Wool, Clothes, Potato, Schnapps }; //Enumeration of all available resource types. Can be addressed from other scripts by calling GameManager.ResourceTypes
    #endregion


    #region Methods
    //Makes the resource dictionary usable by populating the values and keys
    void PopulateResourceDictionary()
    {
        _resourcesInWarehouse.Add(ResourceTypes.None, 0);
        _resourcesInWarehouse.Add(ResourceTypes.Fish, 0);
        _resourcesInWarehouse.Add(ResourceTypes.Wood, 0);
        _resourcesInWarehouse.Add(ResourceTypes.Planks, 0);
        _resourcesInWarehouse.Add(ResourceTypes.Wool, 0);
        _resourcesInWarehouse.Add(ResourceTypes.Clothes, 0);
        _resourcesInWarehouse.Add(ResourceTypes.Potato, 0);
        _resourcesInWarehouse.Add(ResourceTypes.Schnapps, 0);
    }

    //Sets the index for the currently selected building prefab by checking key presses on the numbers 1 to 0
    void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _selectedBuildingPrefabIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _selectedBuildingPrefabIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _selectedBuildingPrefabIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _selectedBuildingPrefabIndex = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _selectedBuildingPrefabIndex = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _selectedBuildingPrefabIndex = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _selectedBuildingPrefabIndex = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _selectedBuildingPrefabIndex = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _selectedBuildingPrefabIndex = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _selectedBuildingPrefabIndex = 9;
        }
    }

    //Updates the visual representation of the resource dictionary in the inspector. Only for debugging
    void UpdateInspectorNumbersForResources()
    {
        _ResourcesInWarehouse_Fish = _resourcesInWarehouse[ResourceTypes.Fish];
        _ResourcesInWarehouse_Wood = _resourcesInWarehouse[ResourceTypes.Wood];
        _ResourcesInWarehouse_Planks = _resourcesInWarehouse[ResourceTypes.Planks];
        _ResourcesInWarehouse_Wool = _resourcesInWarehouse[ResourceTypes.Wool];
        _ResourcesInWarehouse_Clothes = _resourcesInWarehouse[ResourceTypes.Clothes];
        _ResourcesInWarehouse_Potato = _resourcesInWarehouse[ResourceTypes.Potato];
        _ResourcesInWarehouse_Schnapps = _resourcesInWarehouse[ResourceTypes.Schnapps];
    }

    //Checks if there is at least one material for the queried resource type in the warehouse
    public bool HasResourceInWarehoues(ResourceTypes resource)
    {
        return _resourcesInWarehouse[resource] >= 1;
    }

    //Is called by MouseManager when a tile was clicked
    //Forwards the tile to the method for spawning buildings
    public void TileClicked(int height, int width)
    {
        Tile t = map[height, width];

        PlaceBuildingOnTile(t);
    }

    //Checks if the currently selected building type can be placed on the given tile and then instantiates an instance of the prefab
    private void PlaceBuildingOnTile(Tile t)
    {
        //if there is building prefab for the number input
        if (_selectedBuildingPrefabIndex < _buildingPrefabs.Length)
        {
            //TODO: check if building can be placed and then istantiate it

        }
    }

    //Returns a list of all neighbors of a given tile
    public List<Tile> FindNeighborsOfTile(Tile t)
    {
        List<Tile> result = new List<Tile>();
        Vector3 position = t.position;
        Vector2 map_size = get_map_size_grid();
        // Debug.Log(string.Format("",));

        // Debug.Log(string.Format("x start: {0}, x end: {1}", (Mathf.Max(0, (int)position.z - 1), (Mathf.Min(map_size.x, (int)position.z + 1)))));
        // Debug.Log(string.Format("\nx start: {0}, x end: {1}\ty start: {2}, y end: {3}",
        //                         Mathf.Max(0, (int)position.z - 1), Mathf.Min(map_size.x -1, (int)position.z + 1),
        //                         Mathf.Max(0, (int)position.x - 1), Mathf.Min(map_size.x -1, (int)position.x + 1)));
        int xref = (int)position.z;
        int yref = (int)position.x;
        // Debug.Log(string.Format("Origin - x: {0}, y: {1}", xref, yref));

        /*
        o -> included, x -> excluded. self is always exluded. depending on the row, two gridsurroundings are not in the hexagrid-surroundings
        Odd: 
          o o     x o o
         o x o -> o x o
          o o     x o o
        Even: 
          o o     o o x
         o x o -> o x o
          o o     o o x
        */
        for (int x = Mathf.Max(0, xref - 1); x <= Mathf.Min(map_size.x - 1, xref + 1); x++){
            for (int y = Mathf.Max(0, yref - 1); y <= Mathf.Min(map_size.x - 1, yref + 1); y++){
                // Debug.Log(string.Format("x: {0}, y: {1}", x, y));
                if (
                    yref % 2 == 1 && (((x + y) % 2 != (xref + yref) % 2) || x > xref)  // if row is odd, upleft and downleft corner excluded
                    ||
                    yref % 2 == 0 && (((x + y) % 2 != (xref + yref) % 2) || x < xref)  // if row is even, upright and downright corner excluded
                    ){
                    // Debug.Log(string.Format("x: {0}, y: {1}", x, y));
                        result.Add(map[y, x]);
                    }
            }
        }

        // Debug.Log(string.Join(", ", result));
        return result;
    }
    #endregion
}
