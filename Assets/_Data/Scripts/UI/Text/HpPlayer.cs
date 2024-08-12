using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPlayer : BaseText
{
    [SerializeField] PlayerDamageReceiver playerDamageReceiver;

    private void FixedUpdate()
    {
        SetText();
    }
    protected virtual void SetText()
    {
        int hp = playerDamageReceiver.HP;
        text.text = "Hp : " + hp.ToString();
    }    
}
