using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrueSync;

public class AddT : TrueSyncBehaviour {

    [SerializeField]
    TSVector Torque;
    
    TSRigidBody thisBody;

    public override void OnSyncedStart()
    {
        thisBody = GameObject.FindGameObjectWithTag("Bar").GetComponent<TSRigidBody>();
    }

    public override void OnSyncedInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
            TrueSyncInput.SetBool(3, true);
        else
            TrueSyncInput.SetBool(3, false);
    }

    public override void OnSyncedUpdate()
    {
        if (TrueSyncInput.GetBool(3))
        {
            thisBody.AddRelativeTorque(Torque);
        }
    }

}
