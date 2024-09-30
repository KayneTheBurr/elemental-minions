using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class ChangeFloorType : MonoBehaviour
{
    public ObjectiveMarker thisObjective;
    public GameObject land;
    public bool activator = true;
    NavMeshSurface surface;
    public GroundType myGround, newGround;
    public GroundTypeSet groundSetter;
    public NavMeshModifier floorType;
    //public int floorChangeTypeNum, floorNormalTypeNum;


    private void Awake()
    {
        surface = FindObjectOfType<NavMeshSurface>();
        floorType = this.GetComponent<NavMeshModifier>();
        groundSetter = this.GetComponent<GroundTypeSet>();
    }
    private void Start()
    {
        myGround = groundSetter.thisGround;
    }

    void Update()
    {
        if (thisObjective.objectiveMet == true && activator == false)
        {
            
            //floorType.area = floorChangeTypeNum;
            groundSetter.SetGroundType(newGround);
            activator = true;
            surface.BuildNavMesh();
            
        }
        else if (thisObjective.objectiveMet == false && activator == true)
        {
            //floorType.area = floorNormalTypeNum;
            groundSetter.SetGroundType(myGround);
            activator = false;
            surface.BuildNavMesh();
            
        }
    }
}
