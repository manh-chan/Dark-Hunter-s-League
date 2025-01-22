using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameUnit : MonoBehaviour
{
    private Transform tf;
    public ObjectType ObjectType;
    public Transform TF { 
        get {
            tf = tf ?? gameObject.transform;
            return tf;
        } 
    }

    //public abstract void OnInit();

    
}
