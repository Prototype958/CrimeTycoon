using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private List<Upgrade> _upgradesPurchased;

	// // Job Objects
	// public JobStats PickPocket;
	// public JobStats Hacker;
	// public JobStats Mugger;
	// public JobStats ConArtist;

	public JobStatsClass PickPocket;
	public JobStatsClass Hacker;
	public JobStatsClass Mugger;
	public JobStatsClass ConArtist;

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

		// PickPocket = new JobStatsClass();
		// Hacker = new JobStatsClass();
		// Mugger = new JobStatsClass();
		// ConArtist = new JobStatsClass();
	}

	private void UpdatePurchasedList(Upgrade u) => _upgradesPurchased.Add(u);
}