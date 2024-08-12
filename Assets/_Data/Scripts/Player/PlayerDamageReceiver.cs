using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField] GameObject gameLose;
    [SerializeField] int currentHp = 50;
    protected override void ResetValue()
    {
        base.ResetValue();
        this.hpMax = currentHp;
    }

    protected override void Start()
    {
        base.Start();
        gameLose.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    protected override void OnDead()
    {
        Time.timeScale = 0;
        gameLose.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
