using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private List<Upgrade> _upgradesPurchased;

	// Job Objects
	public JobStats PickPocket;
	public JobStats Hacker;
	public JobStats Mugger;
	public JobStats ConArtist;

	public void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		// Set up event listeners
		UpgradeButton.UpgradePurchased += UpdatePurchasedList;

		_upgradesPurchased = new List<Upgrade>();

		TimeTickSystem.Create(this.gameObject);
	}

	// set this up to be called when an upgrade is purchased
	// pass in upgrade to be applied, call appripriate function based on upgrade
	// private void ApplyUpgrade(Job job, Stat stat, float value, float cost)
	// {
	// 	Debug.Log($"Upgrade {job} {stat} by {value}");

	// 	if (stat == Stat.Unlock)
	// 	{
	// 	}
	// }

	private void UpdatePurchasedList(Upgrade u) => _upgradesPurchased.Add(u);
}