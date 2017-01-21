using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour {

    public Tile tile;

    void Update()
    {
        float distanceToCamera = (FollowingCamera.Instance.transform.position - transform.position).magnitude;
        if(distanceToCamera > 100)
        {
            Lean.LeanPool.Despawn(gameObject);
            tile.instantiated = false;
        }
    }

}
