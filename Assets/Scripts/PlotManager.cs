using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;
    
    public Sprite drySpritePlot;
    public Sprite normalSpritePlot;
    public Sprite unavailableSprite;

    public bool isBought = true;

    private SpriteRenderer plot;

    private SpriteRenderer plant;
    private BoxCollider2D plantCollider;
   
    private bool isPlanted = false;
    private int stage = 0;
    private float timer;

    private PlantObject selectedPlant;
    private FarmManager fm;
    private bool isDry = true;

    private float speed =1f;

    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        fm = transform.GetComponentInParent<FarmManager>();
        plot = GetComponent<SpriteRenderer>();
        if (isBought)
        {
            plot.sprite = drySpritePlot;
        }
        else
        {
            plot.sprite = unavailableSprite;
        }
    }

    void Update()
    {
        if (isPlanted && !isDry)
        {
            timer -= speed*Time.deltaTime;
            if (timer < 0 && stage < selectedPlant.plantStages.Length-1)
            {
                timer = selectedPlant.timeBtwStages;
                stage++;
                UpdatePlant();
            }
        }
    }

    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if(stage == selectedPlant.plantStages.Length-1 && !fm.isPlanting && !fm.isSelecting)
                Harvest();
        }
        else if(fm.isPlanting && fm.selectedPlant.plant.buyPrice <=fm.money && isBought)
        {
            Plant(fm.selectedPlant.plant);
        }
        if (fm.isSelecting)
        {
            switch (fm.selectedTool)
            {
                case 1:
                    if (fm.money >= 5 && isBought && plot.sprite != normalSpritePlot)
                    {
                        fm.Transaction(-5);
                        isDry = false;
                        plot.sprite = normalSpritePlot;
                        if(isPlanted) 
                            UpdatePlant();
                    }
                    break;
                case 2:
                    if (fm.money>=70 && isBought)
                    {
                        if (speed < 2)
                        {
                            fm.Transaction(-70);
                            speed += .2f;
                        }

                    }
                    break;
                case 3:
                    if (fm.money >= 500 && !isBought)
                    {
                        fm.Transaction(-500);
                        isBought = true;
                        plot.sprite = drySpritePlot;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void OnMouseOver()
    {
        if (fm.isPlanting)
        {
            if (isPlanted || fm.selectedPlant.plant.buyPrice > fm.money || !isBought)
            {
                plot.color = unavailableColor;
            }
            else
            {
                plot.color = availableColor;
            }
        }
        if (fm.isSelecting)
        {
            switch (fm.selectedTool)
            {
                case 1:
                case 2:
                    if (isBought && fm.money>=(fm.selectedTool-1)*70)
                        plot.color = availableColor;

                    else
                        plot.color = unavailableColor;
                    break;
                case 3:
                    if (!isBought && fm.money>=500)
                        plot.color = availableColor;

                    else
                        plot.color = unavailableColor;
                    break;
                default:
                    plot.color = unavailableColor;
                    break;
            }
        }
    }

    private void OnMouseExit() => plot.color = Color.white;

    private void Harvest()
    {  
        isPlanted = false;
        plant.gameObject.SetActive(false);
        fm.Transaction(selectedPlant.sellPrice);
        isDry = true;
        plot.sprite = drySpritePlot;
        speed = 1f;
    }

    private void Plant(PlantObject newPlant)
    {
        selectedPlant = newPlant;
        isPlanted = true;

        fm.Transaction(-selectedPlant.buyPrice);

        stage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }

    private void UpdatePlant()
    {
        if (isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
        plant.sprite = selectedPlant.plantStages[stage];
        }
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y/2);
    }
}
