using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    //[SerializeField] PoolAmount[] poolAmounts;
    [SerializeField]
    GameUnit[] gameUnit;
    private void Awake()
    {
        gameUnit = Resources.LoadAll<GameUnit>("Pool/");
        for (int i = 0; i < gameUnit.Length; i++)
        {
            SimplePool.Preload(gameUnit[i], 0, new GameObject(gameUnit[i].name).transform);
        }
        /*for (int i = 0; i < poolAmounts.Length; i++)
        {
            SimplePool.Preload(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }*/
    }
}
[System.Serializable]
public class PoolAmount {
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}
public enum ObjectType
{
    Axe = 0,
    Dragonbit = 1,
    Spear = 2,
}

