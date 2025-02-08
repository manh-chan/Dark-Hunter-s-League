using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Cache 
{
    private static Dictionary<Collider, FireBall> fireBall = new Dictionary<Collider, FireBall>();

    public static FireBall GetFireBall(Collider collider)
    {
        if (!fireBall.ContainsKey(collider))
        {
            fireBall.Add(collider, collider.GetComponent<FireBall>());
        }

        return fireBall[collider];
    }

}
