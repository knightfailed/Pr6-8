using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI priceTxt;
    public Image icon;

    public Image btnImage;
    public TextMeshProUGUI btnText;

    private FarmManager fm;

    void Start()
    {
        fm = FindObjectOfType<FarmManager>();

        InititializeUI();
    }

    void Update()
    {
        
    }

    public void BuyPlant()
    {
        Debug.Log($"Bought {plant.plantName}");
        fm.SelectedPlant(this);
    }

    void InititializeUI()
    {
        nameTxt.text = plant.plantName;
        priceTxt.text = "$" + plant.buyPrice;
        icon.sprite = plant.icon;
    }
}
