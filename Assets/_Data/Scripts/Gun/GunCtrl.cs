using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : GameMonoBehaviour
{
    [SerializeField] protected GunDamageSender gunDamageSender;
    public GunDamageSender GunDamageSender => gunDamageSender;

    [SerializeField] protected AmmoLimit ammoLimit;
    public AmmoLimit AmmoLimit => ammoLimit;

    [SerializeField] private ScriptableObjectSO scriptableObjectSO;
    public ScriptableObjectSO ScriptableObjectSO { get => scriptableObjectSO; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGunDamageSender();
        this.LoadAmmoLimit();
        this.LoadScriptableObjectSO();
    }

    protected virtual void LoadGunDamageSender()
    {
        if (this.gunDamageSender != null) return;
        this.gunDamageSender = FindAnyObjectByType<GunDamageSender>();
        Debug.LogWarning(transform.name + ": LoadGunDamageSender", gameObject);
    }

    protected virtual void LoadAmmoLimit()
    {
        if (this.ammoLimit != null) return;
        this.ammoLimit = FindAnyObjectByType<AmmoLimit>();
        Debug.LogWarning(transform.name + ": LoadAmmoLimit", gameObject);
    } 
    
    protected virtual void LoadScriptableObjectSO()
    {
        if (this.scriptableObjectSO != null) return;
        string resPath = "ScriptableObject/Gun/"+ transform.name;
        this.scriptableObjectSO = Resources.Load<ScriptableObjectSO>(resPath);
    }
}
