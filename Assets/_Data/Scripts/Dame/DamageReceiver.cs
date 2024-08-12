using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : GameMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected int hp = 1;
    [SerializeField] protected int hpMax = 2;
    [SerializeField] protected bool isDead = false;

    public int HP => hp;
    public int HPMax => hpMax;

    protected override void Start()
    {
        base.Start();
        this.Reborn();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.Reborn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    public virtual void Reborn()
    {
        this.hp = this.hpMax;
        this.isDead = false;
    }

    public virtual void Add(int add)
    {
        if (this.isDead) return;

        this.hp += add;
        if (this.hp > this.hpMax) this.hp = this.hpMax;
    }

    public virtual void Deduct(int deduct)
    {
        if (this.isDead) return;

        this.hp -= deduct;
        if (this.hp < 0) this.hp = 0;
        this.CheckIsDead();
    }

    public virtual bool IsDead()
    {
        return this.hp <= 0;
    }

    protected virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.OnDead();
    }

    public virtual void SetHPMax(int hpMax)
    {
        this.hpMax = hpMax;
    }

    protected abstract void OnDead();
}
