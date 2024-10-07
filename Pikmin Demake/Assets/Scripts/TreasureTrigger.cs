using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureTrigger : MonoBehaviour
{
    public TreasureLogic myLogic;

    public void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.GetComponent<BasicNav>() == true && col.gameObject.GetComponent<BasicNav>().myState == MinionState.NavToTreasure)
        {
            //Debug.Log("hit");
            myLogic.AttachMinion(col.gameObject.GetComponent<BasicNav>());
            //Camera.main.GetComponent<CameraTargeter>().selectedMinion = null;
        }
    }
}
