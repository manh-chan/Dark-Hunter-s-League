using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    private static Dictionary<ObjectType, Pool> poolInstance = new Dictionary<ObjectType, Pool>();
    // khoi tao pool moi
    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS EMPTY !!");
            return;
        }
        if (!poolInstance.ContainsKey(prefab.ObjectType) || poolInstance[prefab.ObjectType] == null)
        {
            Pool p = new Pool();
            p.Preload(prefab, amount, parent);
            poolInstance[prefab.ObjectType] = p;
        }
    }
    //lay phan tu ra
    public static T Spawn<T>(ObjectType weaponType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(weaponType))
        {
            Debug.LogError(weaponType + "IS NOT PRELOAD !!");
            return null;
        }
        return poolInstance[weaponType].Spawn(pos, rot) as T;
    }
    // tra phan tu vao
    public static void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.ObjectType))
        {
            Debug.LogError(unit.ObjectType + "IS NOT PRELOAD !!");
        }
        poolInstance[unit.ObjectType].Despawn(unit);
    }
    // thu thap phan tu
    public static void Collect(ObjectType weaponType)
    {
        if (!poolInstance.ContainsKey(weaponType))
        {
            Debug.LogError(weaponType + "IS NOT PRELOAD !!");
        }
        poolInstance[weaponType].Collect();
    }
    // thu thap  tat phan tu
    public static void CollectAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Collect();
        }
    }
    //destroy 1 pool
    public static void Release(ObjectType weaponType)
    {
        if (!poolInstance.ContainsKey(weaponType))
        {
            Debug.LogError(weaponType + "IS NOT PRELOAD !!");
        }
        poolInstance[weaponType].Release();
    }
    //destroy tat ca pool
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}
    public class Pool
    {
        Transform parent;
        GameUnit prefab;
        Queue<GameUnit> inactives = new Queue<GameUnit>();
        List<GameUnit> actives = new List<GameUnit>();
        public void Preload(GameUnit prefab, int amount, Transform parent)
        {
            this.parent = parent;
            this.prefab = prefab;
            for (int i = 0; i < amount; i++)
            {
                Despawn(GameObject.Instantiate(prefab, parent));
            }
        }

        public GameUnit Spawn(Vector3 pos, Quaternion rot)
        {
            GameUnit unit;
            if (inactives.Count <= 0)
            {
                unit = GameObject.Instantiate(prefab, parent);
            }
            else
            {
                unit = inactives.Dequeue();
            }
            unit.TF.SetPositionAndRotation(pos, rot);
            actives.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
        }
        public void Despawn(GameUnit unit)
        {
            if (unit != null && unit.gameObject.activeSelf)
            {
                actives.Remove(unit);
                inactives.Enqueue(unit);
                unit.gameObject.SetActive(false);
            }
        }

        public void Collect()
        {
            while (actives.Count > 0)
            {
                Despawn(actives[0]);
            }

        }

        public void Release()
        {
            Collect();
            while (inactives.Count > 0)
            {
                GameObject.Destroy(inactives.Dequeue().gameObject);
            }
            inactives.Clear();
        }

    }
