using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargeter : MonoBehaviour
{
    public float sphereRadius, speed;
    public BasicNav selectedMinion = null;
    public Transform targetCenter, targetPoint;

    

    
    void Update()
    {
        //Camera movement with arrows or wasd
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        //No minion selected initially, and right click will clear any selected minion

        if (Input.GetMouseButtonDown(1))
        {
            //if a minion was selected, deselect it
            if(selectedMinion != null)
            {
                selectedMinion.DeselectMinion();
                selectedMinion = null;
            }
            else { selectedMinion = null; }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Ray targetRay = Camera.main.ScreenPointToRay(mousePosition);
            Debug.DrawRay(targetRay.origin, targetRay.direction, Color.red);
            int layer = 1;

            if (Physics.SphereCast(targetRay, sphereRadius, out RaycastHit hitInfo, 100000f, layer, QueryTriggerInteraction.Ignore))

            {
                BasicNav minion = hitInfo.transform.GetComponent<BasicNav>();

                //if a minion is selected
                if (minion != null)
                {
                    //click a minion to select as the one to move, turn on its ring

                    //if no minion was assigned, make this the selected minion
                    if(selectedMinion == null)
                    {
                        selectedMinion = minion;
                        selectedMinion.SelectMinion();
                    }
                    //if a minion was already active, make the previous minion inactive and activate this minion
                    else if (selectedMinion != null)
                    {
                        selectedMinion.DeselectMinion();
                        selectedMinion = minion;
                        selectedMinion.SelectMinion();
                    }
                    
                }
                //if NOT a minion is selected
                else if(minion == null)
                {
                    //if clicked is not a minion, move target to location and set current minion destination at target
                    targetPoint.gameObject.SetActive(true);
                    targetPoint.position = hitInfo.point;
                    if(selectedMinion == null)
                    {
                        targetPoint.gameObject.SetActive(false);
                    }
                    else if (selectedMinion != null)
                    {
                        selectedMinion.SelectMinionTarget(hitInfo.point, targetPoint);
                    }
                    
                }
            }
        }
        
        
    }
}
