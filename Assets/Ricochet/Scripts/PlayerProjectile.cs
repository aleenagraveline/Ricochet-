using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float top = 5.85f;
    private float bottom = -4.4f;
    private bool ricochet = false;

    private PlayerShip owner;

    void Update()
    {
        if(transform.position.y >= top)
        {
            ricochet = true;
        }
        else if(transform.position.y <= bottom)
        {
            Die();
        }

        if (ricochet)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
    public void Spawn()
    {
        gameObject.SetActive(true);  // Spawned enemies should reactivate
    }
    public void Die()
    {
        gameObject.SetActive(false);
        ricochet = false;
    }

    public void SetOwner(PlayerShip ship)
    {
        owner = ship;
    }

    public PlayerShip GetOwner()
    {
        return owner;
    }

    public bool GetRicochet()
    {
        return ricochet;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerShip>(out PlayerShip playerShip) && ricochet)
        {
            if(playerShip != owner)
            {
                owner.AddToScore(1);
                playerShip.TakeDamage(1);
            }
        }
    }
}
