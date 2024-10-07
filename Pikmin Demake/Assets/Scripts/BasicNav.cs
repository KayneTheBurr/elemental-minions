using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GroundType { Grass, Fire, Water, Electric}
public enum MinionType { Fire, Water, Electric}
public enum MinionState { Idle, Active, Carrying, TryingToCarry, NavToTreasure}


public class BasicNav : MonoBehaviour
{
    
    public Transform target;
    public NavMeshAgent agent;
    public int currentMask;
    public MinionState myState;
    public GroundType groundType;
    public MinionType myElement;
    public CameraTargeter camTarget;
    public GameObject myRing, redRing, greenRing;


    // Start is called before the first frame update
    void Start()
    {
        camTarget = FindObjectOfType<CameraTargeter>();
        agent = this.GetComponent<NavMeshAgent>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckFloorType();
        AtLocationCheck();

        if (groundType == GroundType.Electric)
        {
            agent.speed = 15f;
            agent.acceleration = 20;
        }
        else { agent.speed = 8f;  agent.acceleration = 12f; }

        if(myState == MinionState.NavToTreasure)
        {
            if(agent.isStopped || agent.isPathStale)
            {
                myState = MinionState.Idle;
            }
        }

    }
    public void AtLocationCheck()
    {
        if(target!=null)
        {
            if (target.position.x == agent.transform.position.x
            && target.position.z == agent.transform.position.z)
            {
                target.gameObject.SetActive(false);
            }
        }
    }
    public void DeselectMinion()
    {
        myRing.SetActive(false);
        if(myState != MinionState.NavToTreasure)
        {
            myState = MinionState.Idle;
        }
        
    }
    public void SelectMinion()
    {
        
        myRing.SetActive(true);
        myState = MinionState.Active;
    }
    public void SelectMinionTarget(Vector3 targetPos, Transform targetLocation)
    {
        target = targetLocation;
        target.gameObject.SetActive(true);
        agent.SetDestination(targetPos);
    }
    public void TryToCarry()
    {
        target.gameObject.SetActive(false);
        myRing.SetActive(false);
        redRing.SetActive(true);
        myState = MinionState.TryingToCarry;
        if(camTarget.selectedMinion == this)
        {
            camTarget.selectedMinion = null;
        }
    }
    public void Carrying()
    {
        target.gameObject.SetActive(false);
        myRing.SetActive(false);
        redRing.SetActive(false);
        greenRing.SetActive(true);
        camTarget.selectedMinion = null;
        myState = MinionState.Carrying;

    }
    

    public void CheckFloorType()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(agent.nextPosition, out hit, 10f, NavMesh.AllAreas);
        currentMask = hit.mask;
        switch (currentMask)
        {
            case 1:
                groundType = GroundType.Grass;
                break;
            case 8:
                groundType = GroundType.Fire;
                break;
            case 16:
                groundType = GroundType.Water;
                break;
            case 32:
                groundType = GroundType.Electric;
                break;
        }
            
    }
}
