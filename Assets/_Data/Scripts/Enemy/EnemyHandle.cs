using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHandle : GameMonoBehaviour
{

    [SerializeField] protected Transform wall;
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected int count = 0;
    [SerializeField] protected bool hasWallBeenActivated = false; 

    protected override void LoadComponents()
    {
        this.LoadPrefabs();
        this.LoadWall();
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }

        Debug.LogWarning(transform.name + ": LoadPrefabs", gameObject);
    }

    protected virtual void LoadWall()
    {
        if (this.wall != null) return;
        this.wall = transform.parent.Find("Wall").transform;
        Debug.LogWarning(transform.name + ": LoadWall", gameObject);
    }

    protected override void Start()
    {
        wall.gameObject.SetActive(true);
    }

    protected virtual void Update()
    {
        CheckPrefabs();
    }

    protected virtual void CheckPrefabs()
    {
        if (hasWallBeenActivated) return; 

        bool allPrefabsInactive = true;

        foreach (Transform prefab in prefabs)
        {
            if (prefab.gameObject.activeSelf)
            {
                allPrefabsInactive = false;
                break;
            }
        }

        if (allPrefabsInactive)
        {
            wall.gameObject.SetActive(false); 
        }
    }

    public virtual void SetActiveWall()
    {
        wall.gameObject.SetActive(true);
        hasWallBeenActivated = true;
    }    
}
