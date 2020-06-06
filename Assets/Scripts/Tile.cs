using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{
    public Transform type;
    public Vector3 position;
    public float rotation;
    private float long_diameter = 10f;
    private float edge_length = 17.323232f;
    public List<Tile> neighbors;
    public Tile baseVersion;
    public Tile alternateVersion;

    public Building _building; //The building on this tile
    public List<Tile> _neighborTiles; //List of all surrounding tiles. Generated by GameManager

    #region Enumerations
    public enum TileTypes { Empty, Water, Sand, Grass, Forest, Stone, Mountain }; //Enumeration of all available tile types. Can be addressed from other scripts by calling Tile.Tiletypes
    #endregion

    public Tile(Transform type, float horizontal_pos, float vertical_pos, float height, float rotation){
        this.type = type;
        this.position = new Vector3(horizontal_pos, height, vertical_pos);
        this.rotation = rotation;
    }
    public Tile(Transform type, float horizontal_pos, float vertical_pos, float height)
    {
        this.type = type;
        this.position = new Vector3(horizontal_pos, height, vertical_pos);
        this.rotation = 0f;
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

    public void place() {
        Object.Instantiate(type, position_absolute(), Quaternion.Euler(0, rotation, 0));
        // TODO: save instanciated obect to keep reference to it for interactions
        
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
    public string test(){
        return "Hallo";
    }
}
