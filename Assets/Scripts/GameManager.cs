using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private List<Upgrade> _upgradesPurchased;

	// Job Objects
	[Header("Job Stats")]
	public JobStatsClass PickPocket;
	public JobStatsClass Hacker;
	public JobStatsClass Mugger;
	public JobStatsClass ConArtist;

	private List<JobStatsClass> jobs;
	public Dictionary<Job, JobStatsClass> jobMap;

	public void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 45;

		// Set up event listeners
		UpgradeButton.UpgradePurchased += UpdatePurchasedList;

		_upgradesPurchased = new List<Upgrade>();

		TimeTickSystem.Create(this.gameObject);

		//Init Jobs Dictionary
		InitializeJobMapDict();
	}

	private void OnDestroy()
	{
		UpgradeButton.UpgradePurchased -= UpdatePurchasedList;
	}

	public bool CheckForPrerequisiteUpgrade(Upgrade u)
	{

		if (_upgradesPurchased.Contains(u))
			return true;

		return false;
	}

	private void InitializeJobMapDict()
	{
		jobs = new();
		jobMap = new();

		jobs.Add(PickPocket);
		jobs.Add(Hacker);
		jobs.Add(Mugger);
		jobs.Add(ConArtist);

		foreach (var job in jobs)
		{
			jobMap[job.Type] = job;
		}
	}

	private void UpdatePurchasedList(UpgradeClass u) => _upgradesPurchased.Add(u.upgrade);
}