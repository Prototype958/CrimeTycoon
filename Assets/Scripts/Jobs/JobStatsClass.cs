using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JobStatsClass
{
	public static event Action<Job> EnableJob;

	public Job Type;

	// base stats, will not change
	[SerializeField] private float _baseCompletionSpeed;
	[SerializeField] private float _baseSuccessRate;
	[SerializeField] private float _baseSuspicionGain;
	[SerializeField] private bool _jobEnabled;

	[SerializeField] public IncomeGain Income;

	[System.Serializable]
	public struct IncomeGain
	{
		[SerializeField] private float _baseMin;
		[SerializeField] private float _baseMax;

		private float _modMin;
		private float _modMax;

		public float Min { get { return _baseMin + _modMin; } }
		public float Max { get { return _baseMax + _modMax; } }
		public float ModMin { get { return _modMin; } set { _modMin = value; } }
		public float ModMax { get { return _modMax; } set { _modMax = value; } }
	}

	// modified stats, improved with upgrades
	private float _modSuccessRate = 1;
	private float _modSuspicionGain = 1;

	public bool IsWorking = false;
	public bool JobEnabled => _jobEnabled;
	public float CompletionSpeed => _baseCompletionSpeed;
	public float SuccessRate { get { return _baseSuccessRate * _modSuccessRate; } }
	public float SuspicionGain { get { return _baseSuspicionGain * _modSuspicionGain; } }

	public JobStatsClass()
	{
		UpgradeButton.UpgradePurchased += ApplyUpgrade;
	}

	private void ApplyUpgrade(UpgradeClass upgrade)
	{
		List<Job> jobList = upgrade.upgrade.GetAffectedJobs();
		List<Stat> statList = upgrade.upgrade.GetStatsToUpgrade();

		foreach (Job job in jobList)
		{
			if (job == this.Type)
			{
				foreach (Stat stat in statList)
				{
					switch (stat)
					{
						case Stat.Power:
							break;
						case Stat.Tech:
							break;
						case Stat.Stealth:
							break;
						case Stat.Charm:
							break;
						case Stat.CompletionSpeed:
							CompletionSpeedUpgrade(upgrade);
							break;
						case Stat.SuccessRate:
							SuccessRateUpgrade(upgrade.UpgradeValue);
							break;
						case Stat.SuspicionGain:
							SuspicionGainUpgrade(upgrade.UpgradeValue);
							break;
						case Stat.IncomeGain:
							IncomeUpgrade(upgrade.UpgradeValue);
							break;
						case Stat.Unlock:
							EnableJob?.Invoke(job);
							break;
						default:
							break;
					}
				}
			}
		}
	}

	private void CompletionSpeedUpgrade(UpgradeClass u) => _baseCompletionSpeed *= Mathf.Pow(u.UpgradeValue, u.Rank);

	private void SuccessRateUpgrade(float value) => _modSuccessRate += value;

	private void SuspicionGainUpgrade(float value) => _modSuccessRate += value;

	private void IncomeUpgrade(float value)
	{
		Income.ModMin += value;
		Income.ModMax += value;
	}

	private void OnDestroy()
	{
		UpgradeButton.UpgradePurchased -= ApplyUpgrade;
	}
}
