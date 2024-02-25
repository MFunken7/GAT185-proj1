using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] GameObject titleUI;
	[SerializeField] GameObject winUI;
	[SerializeField] TMP_Text livesUI;
	[SerializeField] TMP_Text timerUI;
	[SerializeField] Slider healthUI;

	[SerializeField] FloatVariable health;
	[SerializeField] IntVariable score;
	[SerializeField] GameObject respawn;

	[Header("Events")]
	[SerializeField] Event gameStartEvent;
	[SerializeField] Event gameEndEvent;
	[SerializeField] GameObjectEvent respawnEvent;

	public enum State
	{
		TITLE,
		START_GAME,
		PLAY_GAME,
		GAME_OVER,
		GAME_END
	}

	private State state = State.TITLE;
	private float timer = 0;
	private int lives = 0;

	public int Lives { 
		get { return lives; } 
		set { 
			lives = value; 
			livesUI.text = "LIVES: " + lives.ToString(); 
			} 
		}

	public float Timer
	{
		get { return timer; }
		set
		{
			timer = value;
			timerUI.text = string.Format("{0:F1}", timer);
		}
	}

    private void Start()
    {
		gameEndEvent.Subscribe(GameEnd);
    }

    void Update()
	{
		switch (state)
		{
			case State.TITLE:
				titleUI.SetActive(true);
				winUI.SetActive(false);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				titleUI.SetActive(false);
				Timer = 120;
				Lives = 3;
				health.value = 100;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				gameStartEvent.RaiseEvent();
				respawnEvent.RaiseEvent(respawn);
				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				Timer = Timer - Time.deltaTime;
				if (Timer <= 0)
				{
					state = State.GAME_OVER;
				}
				break;
			case State.GAME_OVER:
				break;
			case State.GAME_END:
				winUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
			default:
				break;
		}

		healthUI.value = health / 100.0f;
	}

	public void OnStartGame()
	{
		state = State.START_GAME;
	}

	public void OnPlayerDead()
	{
		state = State.START_GAME;
	}

	public void OnAddPoints(int points)
	{
		print(points);
	}

	public void GameEnd()
	{
		state = State.GAME_END;
	}
}
