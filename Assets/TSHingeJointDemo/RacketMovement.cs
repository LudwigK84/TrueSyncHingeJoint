using UnityEngine;
using System.Collections;
using TrueSync;

public class RacketMovement : TrueSyncBehaviour {

    [SerializeField] GameObject lightSphere;
    [SerializeField] GameObject heavySphere;

    TSVector[] StartPositions = new TSVector[] { new TSVector(0,8.56,0), new TSVector(0, 8.56, 0) };

    public override void OnSyncedStart()
    {
        int index = owner.Id - 1;
        tsTransform.position = StartPositions[index];
    }


    public override void OnSyncedInput()
	{
		FP move = Input.GetAxis ("Horizontal");
		TrueSyncInput.SetFP (0, move);
        FP side =Input.GetAxis("Vertical");
        TrueSyncInput.SetFP(2, side);
        if (Input.GetKeyDown(KeyCode.Space))
            TrueSyncInput.SetByte(1, 1);
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
            TrueSyncInput.SetByte(1, 2);
        else
            TrueSyncInput.SetByte(1, 0);		
	}

	public override void OnSyncedUpdate()
	{
		FP move = TrueSyncInput.GetFP (0);
		move *=  TrueSyncManager.DeltaTime;
        FP side = TrueSyncInput.GetFP(2);
        side *= TrueSyncManager.DeltaTime;
        tsTransform.Translate (move,0,side, Space.Self);
        byte releaseSphere = TrueSyncInput.GetByte(1);
        if (releaseSphere == 1)
            TrueSyncManager.SyncedInstantiate(lightSphere, tsTransform.position, TSQuaternion.identity);
        else if (releaseSphere==2)
            TrueSyncManager.SyncedInstantiate(heavySphere, tsTransform.position, TSQuaternion.identity);
    }


}
