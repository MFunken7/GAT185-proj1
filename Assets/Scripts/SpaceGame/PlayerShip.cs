using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IDamagable
{
	[SerializeField] private PathFollower pathFollower;
	[SerializeField] private IntEvent scoreEvent;
	[SerializeField] private Event gameEndEvent;
	[SerializeField] private Inventory inventory;
	[SerializeField] private IntVariable score;
	[SerializeField] private FloatVariable health;

	[SerializeField] private GameObject hitPrefab;
	[SerializeField] private GameObject destroyPrefab;
	[SerializeField] private TMP_Text ScoreUI;

	

	private void Start()
	{
		scoreEvent.Subscribe(AddPoints);
		health.value = 100;
		//kjkj
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			inventory.Use();
		}
		if (Input.GetButtonUp("Fire1"))
		{
			inventory.StopUse();
		}

		pathFollower.speed = (Input.GetKey(KeyCode.Space)) ? 4 : 2;
		
		if(pathFollower.tdistance == 1)
		{
			gameEndEvent?.RaiseEvent();
		}
	}

	public void AddPoints(int points)
	{
		score.value += points;
		ScoreUI.text = "Score: " + score.value;
		//Debug.Log(score.value);
	}

	public void ApplyDamage(float damage)
	{
		health.value -= damage;
		if (health <= 0)
		{
			if (destroyPrefab != null)
			{
				Instantiate(destroyPrefab, gameObject.transform.position, Quaternion.identity);
			}
			Destroy(gameObject);
		}
		else
		{
			if (hitPrefab != null)
			{
				Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
			}
		}
	}

	public void ApplyHealth(float health)
	{
		this.health.value += health;
		this.health.value = Mathf.Min(this.health, 100);
	}
}
