using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    [SerializeField]
    public float fieldofImpact;
    public float force;
    public bool Explosion = false;

    public GameObject ExplosionEffect;
    public LayerMask LayerToHit;

    public void Explode()
    {
        if (State == BirdState.HitSomething && !Explosion)
        {
            Explosion = true;
        }
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, LayerToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffect, 10);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldofImpact);
    }

    public override void OnHit()
    {
        Explode();
    }
}
