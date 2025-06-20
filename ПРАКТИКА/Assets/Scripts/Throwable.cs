using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    [SerializeField] float damageRadius = 20000f;
    [SerializeField] float explosionForce = 1200f;
    public int grenadeDamage;

    float countdown;
    public bool hasExploded = false;
    public bool hasBeenThrown = false;
    
    public float blindTime = 30f;
    private Animator animator;
 
    public enum ThrowableType
    {
        None,
        Grenade,
        Smoke
    }

    public ThrowableType throwableType;

    public void Start()
    {
        countdown = delay;
    }

    public void Update()
    {
        if (hasBeenThrown)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f && hasExploded == false)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    private void Explode()
    {
        GetThrowableEffect();
        print("взрыв");
        Destroy(gameObject);
    }

    private void GetThrowableEffect()
    {
        switch (throwableType)
        {
            case ThrowableType.Grenade:
                GrenadeEffect();
                break;
            case ThrowableType.Smoke:
                SmokeGrenadeEffect();
                break;
            
        }
    }

    private void SmokeGrenadeEffect()
    {
        //Visual Effect
        GameObject smokeEffect = GlobalReferences.Instance.grenadeSmokeEffect;
        Instantiate(smokeEffect, transform.position, transform.rotation);

        //Audio Effects
        SoundManager.Instance.throwablesChannel.PlayOneShot(SoundManager.Instance.grenadeSound);


        //Physical Effect
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);

        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //apply blindess to enemy
               
            }

            //Also apply damage to enemy over here
        }

    }

    private void GrenadeEffect()
    {
        //Visual Effect
        GameObject explosionEffect = GlobalReferences.Instance.grenadeExplosionEffect;
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Audio Effects
        SoundManager.Instance.throwablesChannel.PlayOneShot(SoundManager.Instance.grenadeSound);


        //Physical Effect
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);



        foreach (Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();

            if (objectInRange.GetComponent<BeerBottle>())
            {
                objectInRange.GetComponent<BeerBottle>().Shatter();
            }


            //Also apply damage to enemy over here
            if (objectInRange.gameObject.GetComponent<Enemy>())
            {
                print("ybiabu");
                objectInRange.gameObject.GetComponent<Enemy>().TakeDamage(grenadeDamage);
            }
        }
    }
}
