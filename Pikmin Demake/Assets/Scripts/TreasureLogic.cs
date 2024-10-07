using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using TMPro;

public enum TreasureFloor { Grass, Fire, Water, Electric}

public class TreasureLogic : MonoBehaviour
{
    public bool canMove, fireMove, waterMove, elecMove;
    public int minionsNeededToMove, minionsAttached;
    public GameObject redRing, blueRing, yellowRing, greenRing;
    public TreasureFloor myFloor;
    public BasicNav attachedChar;
    public NavMeshSurface surface;
    public Transform target;
    public NavMeshAgent agent;
    public TextMeshProUGUI minionNumText;

    // Start is called before the first frame update
    void Start()
    {
        surface = FindObjectOfType<NavMeshSurface>();
        agent = this.GetComponent<NavMeshAgent>();
        target = FindObjectOfType<TargetPoint>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        minionNumText.text = ("" + minionsNeededToMove);
        if(minionsAttached >= minionsNeededToMove)
        {
            canMove = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray targetRay = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.SphereCast(targetRay, 0.5f, out RaycastHit hitInfo, 100000f, 1, QueryTriggerInteraction.Ignore))
            {
                if(hitInfo.transform == this.transform)
                {
                    FreeMinions();
                }
            }
        }

    }

    public void FreeMinions()
    {
        fireMove = false;
        waterMove = false;
        elecMove = false;

        GameObject[] minionArray = new GameObject[transform.childCount];
        int i = 0;

        foreach (Transform minion in transform)
        {
            minionArray[i] = minion.gameObject;
            i += 1;
        }
        foreach (GameObject minion in minionArray)
        {
            if (minion.GetComponent<BasicNav>() != null)
            {
                BasicNav minionLogic = minion.GetComponent<BasicNav>();
                minionLogic.redRing.SetActive(false);
                minionLogic.greenRing.SetActive(false);
                minion.transform.SetParent(null);
                minionLogic.agent.enabled = true;
                minionLogic.DeselectMinion();
            }
        }
        DeselectTreasure();
        minionsAttached = 0;
        agent.enabled = false;
        canMove = false;
        Camera.main.GetComponent<CameraTargeter>().selectedTreasure = null;
        surface.BuildNavMesh();
    }

    public void SelectTreasure()
    {
        switch (myFloor)
        {
            case TreasureFloor.Grass:
                greenRing.SetActive(true);
                break;
            case TreasureFloor.Fire:
                redRing.SetActive(true);
                break;
            case TreasureFloor.Water:
                blueRing.SetActive(true);
                break;
            case TreasureFloor.Electric:
                yellowRing.SetActive(true);
                break;
        }

    }
    public void DeselectTreasure()
    {
        redRing.SetActive(false);
        blueRing.SetActive(false);
        yellowRing.SetActive(false);
        greenRing.SetActive(false);
    }

    public void SetMinionsToCarrying()
    {
        target.gameObject.SetActive(false);
        agent.enabled = true;
        agent.areaMask = 0;
        agent.areaMask |= 1 << NavMesh.GetAreaFromName("Walkable");
        GameObject[] minionArray = new GameObject[transform.childCount];
        int i = 0;

        foreach (Transform minion in transform)
        {
            minionArray[i] = minion.gameObject;
            i += 1;
        }
        foreach( GameObject minion in minionArray)
        {
            if(minion.GetComponent<BasicNav>() != null )
            {
                BasicNav minionLogic = minion.GetComponent<BasicNav>();
                TreasureFloorType(minionLogic.myElement);
                minionLogic.redRing.SetActive(false);
                minionLogic.greenRing.SetActive(true);
            }
        }
        SetTreasureFloorAllowed();
        Camera.main.GetComponent<CameraTargeter>().selectedTreasure = this;
        SelectTreasure();
    }
    public void SetTreasureFloorAllowed()
    {
        agent.areaMask = 0;
        agent.areaMask |= 1 << NavMesh.GetAreaFromName("Walkable");
        

        if (fireMove && !waterMove && !elecMove)
        {
            
            agent.areaMask |= 1 << NavMesh.GetAreaFromName("Fire");
            myFloor = TreasureFloor.Fire;
        }
        else if(waterMove && !fireMove && !elecMove)
        {

            agent.areaMask |= 1 << NavMesh.GetAreaFromName("Water");
            myFloor = TreasureFloor.Water;
        }
        else if(elecMove && !fireMove && !waterMove)
        {

            agent.areaMask |= 1 << NavMesh.GetAreaFromName("Electric");
            myFloor = TreasureFloor.Electric;
        }
        else
        {
            myFloor = TreasureFloor.Grass;
        }

    }
    public void TreasureFloorType(MinionType element)
    {
        switch (element)
        {
            case MinionType.Fire:
                fireMove = true;
                
                break;
            case MinionType.Water:
                waterMove = true;
                
                break;
            case MinionType.Electric:
                elecMove = true;
                
                break;
        }
    }

    public void AttachMinion(BasicNav minion)
    {
        Debug.Log(minion.myElement);
        minion.agent.enabled = false;
        
        minion.gameObject.transform.SetParent(this.gameObject.transform);
        surface.BuildNavMesh();
        minionsAttached++;

        if(minionsAttached >= minionsNeededToMove)
        {
            minion.Carrying();
            SetMinionsToCarrying();
        }
        else if(minionsAttached < minionsNeededToMove)
        {
            minion.TryToCarry();
        }
        
    }
    public void MoveTreasure(Vector3 targetPos, Transform targetLocation)
    {
        target = targetLocation;
        agent.SetDestination(targetPos);
    }
   

}
