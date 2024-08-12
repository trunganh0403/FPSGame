using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField] protected float speed = 100f;
    [SerializeField] protected Vector3 direction = Vector3.forward;

    private void Update()
    {
        this.Fly();
    }

    protected virtual void Fly()
    {
        transform.parent.Translate(this.direction * this.speed * Time.deltaTime);
    }
}
