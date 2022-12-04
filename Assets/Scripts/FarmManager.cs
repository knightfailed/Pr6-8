using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectedPlant;
    public bool isPlanting = false;
    public int money = 0;
    public TextMeshProUGUI moneyTxt;

    public bool isSelecting = false;
    //1-water 2-Fertilizer 3-buy_cell
    public int selectedTool = 0;

    public Image[] buttonsImg;
    public Sprite normalButton;
    public Sprite selectedButton;

    public Color buyColor = Color.green;
    public Color cancelColor = Color.red;

    void Start() =>  moneyTxt.text = ("$" + money);



    public void SelectedPlant(PlantItem _newPlant)
    {
        
        if (selectedPlant == _newPlant)
        {
            CheckSelectionTool();

        }
        else
        {
            CheckSelectionTool();
            selectedPlant = _newPlant;
            selectedPlant.btnImage.color = cancelColor;
            selectedPlant.btnText.text = "Cancle";
            isPlanting = true;
        }
        
        
        
    }

    public void SelectTool(int toolNumber)
    {
        if (toolNumber == selectedTool)
        {
            //deselect
            CheckSelectionTool();

        }
        else
        {
            //selected
            CheckSelectionTool();
            isSelecting = true;
            selectedTool = toolNumber;
            buttonsImg[toolNumber - 1].sprite = selectedButton;
        }
    }

    private void CheckSelectionTool()
    {
        if (isPlanting)
        {
            isPlanting = false;
            if (selectedPlant != null)
            {
                selectedPlant.btnImage.color = buyColor;
                selectedPlant.btnText.text = "Buy";
                selectedPlant=null;
            }
        }
        if (isSelecting)
        {
            if (selectedTool>0)
            {
                buttonsImg[selectedTool - 1].sprite = normalButton;
            }
            isSelecting= false;
            selectedTool = 0;
        }
    }

    public void Transaction(int _value)
    {
        money += _value;
        moneyTxt.text = ("$" + money);
    }
}
