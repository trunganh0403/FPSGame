using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamageSender : DamageSender
{
    [SerializeField] protected GunCtrl gunCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGunCtrl();
    }

    protected virtual void LoadGunCtrl()
    {
        if (this.gunCtrl != null) return;
        this.gunCtrl = transform.parent.GetComponent<GunCtrl>();
        Debug.LogWarning(transform.name + ": LoadGunCtrl", gameObject);
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.damage = gunCtrl.ScriptableObjectSO.dame;
    }
}
