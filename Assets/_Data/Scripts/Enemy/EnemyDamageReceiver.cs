using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{

    [SerializeField] EnemyFollowTaget enemyFollowTaget;
    protected override void Start()
    {
        base.Start();
        hpMax = Random.Range(15, 20);
        hp = hpMax;
    }
    protected override void OnDead()
    {
        enemyFollowTaget.Die();
        Invoke(nameof(DeletEnemy), 1f);
    }

    protected virtual void DeletEnemy()
    {
        transform.gameObject.SetActive(false);
    }
}
