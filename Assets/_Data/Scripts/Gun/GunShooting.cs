using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : GameMonoBehaviour
{
    [Header("Obj Shooting")]
    [SerializeField] protected Camera fpsCamera;
    [SerializeField] protected GunCtrl gunCtrl;

    [SerializeField] protected bool isShooting = false;
    [SerializeField] protected float shootDelay = 1f;
    [SerializeField] protected float shootTimer = 0f;

    [SerializeField] protected float range = 100f;
    [SerializeField] protected int bulletsUsedPerShot = 1;

    [SerializeField] protected BulletSpawner bulletSpawner;
    [SerializeField] protected Transform gunBarrel;
    [SerializeField] protected GameObject effGun;
    [SerializeField] protected GameObject flashlightGun;
    [SerializeField] protected GunAnimation gunAnimation;

    [SerializeField] protected CinemachineImpulseSource impulseSource;

    void Update()
    {
        this.IsShooting();
    }

    private void FixedUpdate()
    {
        this.Shooting();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.range = gunCtrl.ScriptableObjectSO.range;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCamera();
        this.LoadGunCtrl();
    }

    protected virtual void LoadCamera()
    {
        if (this.fpsCamera != null) return;
        this.fpsCamera = FindAnyObjectByType<Camera>();
        Debug.LogWarning(transform.name + ": LoadCamera", gameObject);
    }

    protected virtual void LoadGunCtrl()
    {
        if (this.gunCtrl != null) return;
        this.gunCtrl = transform.parent.GetComponent<GunCtrl>();
        Debug.LogWarning(transform.name + ": LoadGunCtrl", gameObject);
    }

    protected virtual void Shooting()
    {
        if (gunAnimation != null)
        {
            gunAnimation.ChangeBullet(false);
        }

        if (gunCtrl.AmmoLimit.IsDead()) return;

        this.IncreaseTimer();

        if (!this.IsShooting()) return;
        if (this.shootTimer < this.shootDelay) return;

        flashlightGun.SetActive(true);
        Invoke(nameof(SetActiveLifgt), 0.05f);

        this.GenerateImpulse();
        this.ChangeBullet();
        this.ResetShootTimer();
        this.ConsumeAmmo();
        this.RayCast();
   
    }

    protected virtual void GenerateImpulse()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }  
    
    protected virtual void ChangeBullet()
    {
        if (gunAnimation != null)
        {
            gunAnimation.ChangeBullet(true);
        }
    }    

    protected virtual void SetActiveLifgt()
    {
        flashlightGun.SetActive(false);
    }    

    protected virtual void RayCast()
    {
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, range))
        {
            if (hit.transform == null) return;

            if (hit.transform.gameObject.CompareTag(GameTag.Map))
            {
                CreateEffGun(hit);
            }
            
            if (hit.transform.gameObject.CompareTag(GameTag.Enemy))
            {
                this.DameSender(hit);
                this.SpawnBullet(hit);
            }
        }
    }

    protected virtual void SpawnBullet(RaycastHit hit)
    {
        Transform bullet = bulletSpawner.Spawn("Bullet_1", hit.point, hit.transform.rotation);
        bullet.gameObject.SetActive(true);
    }

    protected virtual void CreateEffGun(RaycastHit hit)
    {
       GameObject effgun = Instantiate(effGun, hit.point,Quaternion.LookRotation(hit.normal));
        effgun.SetActive(true);
    }

    protected virtual void DameSender(RaycastHit hit)
    {
        gunCtrl.GunDamageSender.Send(hit.transform);
        EnemyFollowTaget enemyFollowTaget = hit.transform.GetComponent<EnemyFollowTaget>();
        enemyFollowTaget.chaseRange = 100f;
    }
    protected virtual void ConsumeAmmo()
    {
        gunCtrl.AmmoLimit.Deduct(bulletsUsedPerShot);
    }

    protected virtual void ResetShootTimer()
    {
        this.shootTimer = 0;
    }

    protected virtual void IncreaseTimer()
    {
        this.shootTimer += Time.fixedDeltaTime;
    }
    protected virtual bool IsShooting()
    {
        this.isShooting = InputManager.Instance.Fire1Input ==1;
        return this.isShooting;
    }
}
