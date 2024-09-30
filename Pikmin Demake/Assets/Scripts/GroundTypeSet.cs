using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class GroundTypeSet : MonoBehaviour
{
    public GroundType thisGround;
    public Material redFloor, greenFloor, blueFloor, yellowFloor;
    public GameObject land;
    public MeshRenderer myRend;
    public NavMeshModifier modifier;

    private void Awake()
    {
        myRend = land.GetComponent<MeshRenderer>();
        modifier = this.GetComponent<NavMeshModifier>();
        SetGroundType(thisGround);
    }

    public void SetGroundType(GroundType ground)
    {
        thisGround = ground;

        switch (thisGround)
        {
            case GroundType.Grass:
                myRend.material = greenFloor;
                modifier.area = 0;
                break;

            case GroundType.Fire:
                myRend.material = redFloor;
                modifier.area = 3;
                break;

            case GroundType.Water:
                myRend.material = blueFloor;
                modifier.area = 4;
                break;

            case GroundType.Electric:
                myRend.material = yellowFloor;
                modifier.area = 5;
                break;
        }
    }

}
