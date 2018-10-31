using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suiseiwalk : MonoBehaviour {
	private Animator animator;
	private const float Distance = 2;

	[SerializeField]
	private int _player_num;
	public int Player_num
	{
		get { return _player_num; }
	}

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
		Debug.Log(animator.tag);
	}

	// Update is called once per frame
	void Update()
	{
		if (animator.tag=="Enemy")
		{
			GetInputKey();
		}
	}

	public void GetInputKey()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			animator.SetInteger("state", 0);
			Invoke("Move", 0.5f);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			animator.SetInteger("state", 1);
			Invoke("Move", 0.5f);

		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			animator.SetInteger("state", 2);
			Invoke("Move", 0.5f);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			animator.SetInteger("state", 3);
			Invoke("Move", 0.5f);
		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			animator.SetInteger("state", 4);
		}

	}

	public void Move()
	{
		switch (animator.GetInteger("state"))
		{
			case 0:
				transform.Translate(0, Distance, 0);
				break;
			case 1:
				transform.Translate(0, -1 * Distance, 0);
				break;
			case 2:
				transform.Translate(-1 * Distance, 0, 0);
				break;
			case 3:
				transform.Translate(Distance, 0, 0);
				break;
			default:
				break;
		}
	}

	public void Wait()
	{
		animator.SetInteger("state", 4);
	}
}
