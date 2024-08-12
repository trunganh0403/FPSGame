using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletText : BaseText
{
    public virtual void SetText(int bullet)
    {
        text.text = "Bullet : " + bullet.ToString();
    }
}
