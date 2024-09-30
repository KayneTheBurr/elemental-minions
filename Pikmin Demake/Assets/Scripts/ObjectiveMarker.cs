using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ObjectiveType { Fire, Water, Electric, Total}
public class ObjectiveMarker : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;
    public ObjectiveType objective;
    public bool objectiveMet = false, winCircle = false;
    public int fireNeeded, waterNeeded, elecNeeded, totalNeeded;
    public int fireMinionsHere, waterMinionsHere, elecMinionsHere, totalMinionsHere;


    
    void Start()
    {
        
    }

    
    void Update()
    {
        //update the objective text based on objective type and if final win circle
        if(winCircle)
        {
            switch (objective)
            {
                case ObjectiveType.Fire:
                    objectiveText.color = Color.red;
                    objectiveText.text = "WIN \n" + fireMinionsHere + " / " + fireNeeded + " Fire \n";
                    break;
                case ObjectiveType.Water:
                    objectiveText.color = Color.blue;
                    objectiveText.text = "WIN \n" + waterMinionsHere + " / " + waterNeeded + " Water \n";
                    break;
                case ObjectiveType.Electric:
                    objectiveText.color = Color.yellow;
                    objectiveText.text = "WIN \n" + elecMinionsHere + " / " + elecNeeded + " Electric \n";
                    break;
                case ObjectiveType.Total:
                    objectiveText.color = Color.black;
                    objectiveText.text = "WIN \n" + totalMinionsHere + " / " + totalNeeded + " Total \n";
                    break;
            }
        }
        else if(!winCircle)
        {
            switch (objective)
            {
                case ObjectiveType.Fire:
                    objectiveText.color = Color.red;
                    objectiveText.text = fireMinionsHere + " / " + fireNeeded + " Fire \n";
                    break;
                case ObjectiveType.Water:
                    objectiveText.color = Color.blue;
                    objectiveText.text = waterMinionsHere + " / " + waterNeeded + " Water \n";
                    break;
                case ObjectiveType.Electric:
                    objectiveText.color = Color.yellow;
                    objectiveText.text = elecMinionsHere + " / " + elecNeeded + " Electric \n";
                    break;
                case ObjectiveType.Total:
                    objectiveText.color = Color.black;
                    objectiveText.text = totalMinionsHere + " / " + totalNeeded + " Total \n";
                    break;
            }
        }
        
    }

    //determine if objective is met depending on the objective type 
    public void ObjectiveMet()
    {
        switch (objective)
        {
            case ObjectiveType.Fire:
                if(fireMinionsHere >= fireNeeded)
                {
                    objectiveMet = true;
                }
                else if(fireMinionsHere<fireNeeded)
                {
                    objectiveMet = false;
                }
                break;
            case ObjectiveType.Water:
                if(waterMinionsHere >= waterNeeded)
                {
                    objectiveMet = true;
                }
                else if(waterMinionsHere < waterNeeded)
                {
                    objectiveMet = false;
                }
                break;
            case ObjectiveType.Electric:
                if(elecMinionsHere >= elecNeeded)
                {
                    objectiveMet = true;
                }
                else if(elecMinionsHere < elecNeeded)
                {
                    objectiveMet = false;
                }
                break;
            case ObjectiveType.Total:
                if(totalMinionsHere >= totalNeeded)
                {
                    objectiveMet = true;
                }
                else if(totalMinionsHere < totalNeeded)
                {
                    objectiveMet = false;
                }
                break;
        }
    }
    //add to total and appropriate number when entering circle
    public void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.GetComponent<BasicNav>() == true)
        {
            totalMinionsHere++;
            MinionType element = col.gameObject.GetComponent<BasicNav>().myElement;
            switch (element)
            {
                case MinionType.Fire:
                    fireMinionsHere++;
                    break;
                case MinionType.Water:
                    waterMinionsHere++;
                    break;
                case MinionType.Electric:
                    elecMinionsHere++;
                    break;
            }
        }
        ObjectiveMet();
            
    }
    //take away from total and elemental when leaving circle
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<BasicNav>() == true)
        {
            totalMinionsHere--;
            MinionType element = col.gameObject.GetComponent<BasicNav>().myElement;
            switch (element)
            {
                case MinionType.Fire:
                    fireMinionsHere--;
                    break;
                case MinionType.Water:
                    waterMinionsHere--;
                    break;
                case MinionType.Electric:
                    elecMinionsHere--;
                    break;
            }
        }
        ObjectiveMet();
    }
}
