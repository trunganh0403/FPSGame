using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    public Animator anmator;

    private void Start()
    {
        anmator = GetComponent<Animator>();
    }

   public virtual void ChangeBullet(bool isReloading)
    {
        anmator.SetBool("reloading", isReloading);
    }    
}
