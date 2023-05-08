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
	private float _modCompletionSpeed = 1f;
	private float _modSuccessRate;
	private float _modSuspicionGain;

	public bool IsWorking = false;
	public float CompletionSpeed { get { return _baseCompletionSpeed * _modCompletionSpeed; } }
	//public float CompletionSpeed { get { if (_modCompletionSpeed > 0) { return _baseCompletionSpeed * _modCompletionSpeed; } else { return _baseCompletionSpeed; } } }
	public float SuccessRate { get { return _baseSuccessRate + _modCompletionSpeed; } }
	public float SuspicionGain { get { return _baseSuspicionGain + _modSuspicionGain; } }

	public JobStatsClass()
	{
		UpgradeButton.UpgradePurchased += ApplyUpgrade;
	}

	private void ApplyUpgrade(UpgradeClass u)
	{
		List<Job> jobList = u.upgrade.GetAffectedJobs();
		List<Stat> statList = u.upgrade.GetStatsToUpgrade();

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
							CompletionSpeedUpgrade(u.UpgradeValue);
							break;
						case Stat.SuccessRate:
							SuccessRateUpgrade(u.UpgradeValue);
							break;
						case Stat.SuspicionGain:
							SuspicionGainUpgrade(u.UpgradeValue);
							break;
						case Stat.IncomeGain:
							IncomeUpgrade(u.UpgradeValue);
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

	private void CompletionSpeedUpgrade(float value) => _modCompletionSpeed *= value;

	private void SuccessRateUpgrade(float value) => _modSuccessRate += value;

	private void SuspicionGainUpgrade(float value) => _modSuccessRate += value;

	private void IncomeUpgrade(float value)
	{
		Income.ModMin += value;
		Income.ModMax += value;
	}
}
