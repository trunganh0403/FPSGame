using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameMonoBehaviour
{
    [SerializeField] protected List<Transform> guns;
    [SerializeField] protected int currentGunIndex = 0;

    protected override void Start()
    {
        ActivateGun(currentGunIndex);

    }
    protected override void LoadComponents()
    {
        this.LoadGuns();
    }

    protected virtual void LoadGuns()
    {
        if (this.guns.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.guns.Add(prefab);
        }

        this.HidePrefabs();

        Debug.LogWarning(transform.name + ": LoadGuns", gameObject);
    }

    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.guns)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        //float scroll = InputManager.Instance.MouseScrollWheel;
        //if (scroll != 0f)
        //{
        //    this.SwitchToNextGun();
        //}

        if(Input.GetKeyUp(KeyCode.Q))
        {
            this.SwitchToNextGun();
        }    
    }

   

    private void SwitchToNextGun()
    {
        guns[currentGunIndex].gameObject.SetActive(false);
        currentGunIndex = (currentGunIndex + 1) % guns.Count;
        ActivateGun(currentGunIndex);
    }

    private void ActivateGun(int index)
    {
        guns[index].gameObject.SetActive(true);
    }
}
