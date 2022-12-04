using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;

    List<PlantObject> plantObj = new List<PlantObject>();

    private void Awake()
    {
        var loadPlants = Resources.LoadAll("Plants", typeof(PlantObject));
        foreach (var _plant in loadPlants)
        {
            plantObj.Add((PlantObject)_plant);
           

        }
        plantObj.Sort(SortByPrice);

        foreach (var _plant in plantObj)
        {
            PlantItem newPlant = Instantiate(plantItem, transform).GetComponent<PlantItem>();
            newPlant.plant = _plant;
        }
    }

    private int SortByPrice(PlantObject _plantObject1, PlantObject _plantObject2) => _plantObject1.buyPrice.CompareTo(_plantObject2.buyPrice);
    private int SortByTime(PlantObject _plantObject1, PlantObject _plantObject2) => _plantObject1.timeBtwStages.CompareTo(_plantObject2.timeBtwStages);
}
