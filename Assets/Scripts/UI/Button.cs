using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    // public string BuildingName;
    public GameObject Building;
    public GameManager gameManager;
    // private int index;
    // Start is called before the first frame update
    void Start()
    {
        // GameObject building = Array.Find(gameManager._buildingPrefabs, x => x.name == BuildingName);
        // index = Array.IndexOf(gameManager._buildingPrefabs, building);

        // index = Array.IndexOf(gameManager._buildingPrefabs, Building);
    }

    // Update is called once per frame
    void OnClick() {
        Debug.Log(string.Format("Button is clicked!"));
        // gameManager._selectedBuildingPrefabIndex = index;
        gameManager.SetSelectedBuilding(Building);
    }
}
