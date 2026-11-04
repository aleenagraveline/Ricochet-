using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using System;

public class PlayerShip : Player
{
	[SerializeField] private float speed = 5f;
	[SerializeField] private int health;

	[SerializeField] private PlayerProjectile bullet;
	private int startSize = 8;
	private List<PlayerProjectile> bulletPool = new List<PlayerProjectile>();

	public static event Action<string, int> OnTakeDamage;
	public static event Action<string, int> OnIncreaseScore;
	public static event Action<PlayerShip> OnShipDeath;

	private bool isDead = false;

	// Start is called before the first frame update
	void Awake()
	{
		for (int i = 0; i < startSize; i++)
		{
			AddBullet();
		}
	}

    void Start()
    {
		SetScore(0);
		SetInputPrefix(PlayerID);
		OnTakeDamage?.Invoke(GetInputPrefix(), health);
		OnIncreaseScore?.Invoke(GetInputPrefix(), GetScore());
	}

    // Update is called once per frame
    void Update()
	{
		Vector2 playerInput = new Vector2(Input.GetAxisRaw(GetInputPrefix() + "Horizontal"), Input.GetAxisRaw(GetInputPrefix() + "Vertical"));

		if (!isDead)
        {
			transform.position += Vector3.right * playerInput.x * speed * Time.deltaTime;

			if (Input.GetButtonDown(GetInputPrefix() + "Button2"))
			{
				GetBullet(this);
			}

			if (health == 0)
			{
				Die();
			}
		}
	}

	public void TakeDamage(int amount)
	{
		health -= amount;
		OnTakeDamage?.Invoke(GetInputPrefix(), health);
	}

	public void Die()
    {
		OnShipDeath?.Invoke(this);
		gameObject.SetActive(false);
		isDead = true;
    }

	public void AddToScore(int amount)
	{
		SetScore(GetScore() + amount);
		OnIncreaseScore?.Invoke(GetInputPrefix(), GetScore());
	}

	private PlayerProjectile AddBullet()
	{
		PlayerProjectile newBullet = Instantiate(bullet);
		newBullet.gameObject.SetActive(false);
		bulletPool.Add(newBullet);
		return newBullet;
	}

	public PlayerProjectile GetBullet(PlayerShip owner)
	{
		Vector3 position = new Vector3(this.transform.position.x, -1.3f, this.transform.position.z);

		for (int i = 0; i < bulletPool.Count; i++)
		{
			if (!bulletPool[i].gameObject.activeSelf)
			{
				bulletPool[i].transform.position = position;
				bulletPool[i].Spawn();
				bulletPool[i].SetOwner(owner);
				return bulletPool[i];
			}
		}

		PlayerProjectile newBullet = AddBullet();
		newBullet.transform.position = position;
		newBullet.Spawn();
		newBullet.SetOwner(owner);
		return newBullet;
	}
}
