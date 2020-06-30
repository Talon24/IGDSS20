using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{
    public Transform myTransform;
    public Vector3 position;
    public float rotation;
    private float long_diameter = 10f;
    private float edge_length = 17.323232f;
    public List<Tile> neighbors;
    public Tile baseVersion;
    public Transform decorationPrefab;
    private Transform decoration;

    public Dictionary<Tile, Tile> navigationDirections = new Dictionary<Tile, Tile>();
    public int navigationWeight;

    public Building _building; //The building on this tile
    public List<Tile> _neighborTiles; //List of all surrounding tiles. Generated by GameManager

    #region Enumerations
    public enum TileTypes {Empty = 0, Water = 1, Sand = 2, Grass = 3, Forest = 4, Stone = 5, Mountain = 6}; //Enumeration of all available tile types. Can be addressed from other scripts by calling Tile.Tiletypes
    #endregion
    public int TileType;

    public void Start(){
        decoration = Instantiate(decorationPrefab, transform.position, transform.rotation, transform);
        // if (TileType == (int)TileTypes.Mountain)
        // {
        //     // Debug.Log(string.Format("Is a mountain tile {0}", position));
        // }
    }

    public Vector3 position_absolute() {
        Vector3 abs_position;
        // Debug.Log((position.x, position.z));
        if (position.x % 2 == 0)
        {
            abs_position = new Vector3(position.x / 2 * edge_length, position.y, position.z * long_diameter);
        }
        else
        {
            abs_position = new Vector3(((position.x - 1) / 2 * edge_length) + 8.66f, position.y, (position.z * long_diameter) + 5);
        }
        return abs_position;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
    public string test()
    {
        return "Test";
    }

    public bool fitsRequirement(Transform[] required)
    {
        if (required == null || required.Length == 0)
        {
            return true;
        }
        foreach (Transform trafo in required)
        {
            if (trafo.GetComponent<Tile>().TileType == TileType)
            {
                return true;
            }
        }
        return false;
    }

    public void placeBuilding(GameObject building, RessourceManager ressourceManager, JobManager jobManager, Transform buildingParent){
        if (_building == null) {
            GameObject newBuild = Instantiate(building, position_absolute(), Quaternion.Euler(0, this.rotation, 0), buildingParent.transform);
            _building = newBuild.GetComponent<Building>();
            if (fitsRequirement(_building.build_requirement) && ressourceManager.canAfford(_building))
            {
                decoration.gameObject.SetActive(false);
                _building.tile = this;
                ressourceManager.buyBuilding(_building);
                _building.setEfficiency();
                _building.ressourceManager = ressourceManager;
                _building.jobManager = jobManager;
                _building.launch();
            } 
            else 
            { // Revert changes
                Destroy(_building.gameObject);
                _building = null;
                decoration.gameObject.SetActive(true);
            }
        }
        else {
            Destroy(_building.gameObject);
            _building = null;
            decoration.gameObject.SetActive(true);
        }
    }
}
