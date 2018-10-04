using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerDisplay : MonoBehaviour {

	public Text level, health, attack,defense;
	public Player player;

	void Awake()
	{
		player = GetComponent<Player> ();
	}

	void Start()
	{

		UpdateDisplay ();

	}

	public void UpdateDisplay()
	{

		level.text = player.Level.ToString ();
		health.text = player.Health.ToString ();
		attack.text = player.Attack.ToString ();
		defense.text = player.Defense.ToString ();

	}

}
