using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

public class AmmoLimit : DamageReceiver
{
    [SerializeField] protected GunCtrl gunCtrl;
    [SerializeField] protected GunAnimation gunAnimation;
    [SerializeField] protected BulletText bulletText;

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
        this.hpMax = gunCtrl.ScriptableObjectSO.ammoCount;
    }


    private void FixedUpdate()
    {
        bulletText.SetText(this.hp);
        LoadBullet();
       
    }
    protected virtual void LoadBullet()
    {
        if(hp <= 0)
        {
            gunAnimation.ChangeBullet(true);
            Invoke(nameof(SetGunAnimation), 1f);
            Invoke(nameof(Reborn), 1.1f);
            bulletText.SetText(this.hp);
        } 

    }

    protected virtual void SetGunAnimation( )
    {
        gunAnimation.ChangeBullet(false);
    }

    protected override void OnDead()
    {
        //Debug.Log("AmmoLimit");
    }
}
