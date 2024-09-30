using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NewPlatform : MonoBehaviour
{
    public ObjectiveMarker thisObjective;
    public GameObject land;
    public bool activator = true;
    NavMeshSurface surface;
    

    private void Awake()
    {
        surface = FindObjectOfType<NavMeshSurface>();
    }

    void Update()
    {
        
       if(thisObjective.objectiveMet == true && activator == false)
        {
            land.SetActive(true);
            activator = true;
            surface.BuildNavMesh();
            
        }
        else if (thisObjective.objectiveMet == false && activator == true)
        {
            land.SetActive(false);
            activator = false;
            surface.BuildNavMesh();
            
        }

    }
}
