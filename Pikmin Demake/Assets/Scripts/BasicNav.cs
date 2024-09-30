using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GroundType { Grass, Fire, Water, Electric}
public enum MinionType { Fire, Water, Electric}


public class BasicNav : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public int currentMask;
    public GroundType groundType;
    public MinionType myElement;
    public CameraTargeter camTarget;
    public GameObject myRing;


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

        if (groundType == GroundType.Electric)
        {
            agent.speed = 5f;

        }
        else {agent.speed = 3.5f; }

        AtLocationCheck();

        
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
    }
    public void SelectMinion()
    {
        myRing.SetActive(true);
    }
    public void SelectMinionTarget(Vector3 targetPos, Transform targetLocation)
    {
        target = targetLocation;
        agent.SetDestination(targetPos);
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
