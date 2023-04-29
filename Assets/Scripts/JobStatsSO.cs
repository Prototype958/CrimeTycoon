using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Job")]
public class JobStats : ScriptableObject
{
	public static event Action<Job> EnableJob;

	public Job Type;

	// base stats, do will not change
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

	private bool _isLocked;

	// modified stats, improved with upgrades
	[SerializeField] private float _modCompletionSpeed;
	private float _modSuccessRate;
	private float _modSuspicionGain;

	public float CompletionSpeed { get { return _baseCompletionSpeed + _modCompletionSpeed; } }
	public float SuccessRate { get { return _baseSuccessRate + _modCompletionSpeed; } }
	public float SuspicionGain { get { return _baseSuspicionGain + _modSuspicionGain; } }

	public JobStats()
	{
		UpgradeButton.UpgradePurchased += ApplyUpgrade;
	}

	private void ApplyUpgrade(Upgrade u)
	{
		List<Job> jobList = u.GetAffectedJobs();
		List<Stat> statList = u.GetStatsToUpgrade();

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
							break;
						case Stat.SuspicionGain:
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

	private void CompletionSpeedUpgrade(float value) => _modCompletionSpeed += value;

	private void IncomeUpgrade(float value)
	{
		Income.ModMin += value;
		Income.ModMax += value;
	}

	public void OnEnable()
	{
		_modCompletionSpeed = 0;
		_modSuccessRate = 0;
		_modSuspicionGain = 0;
		Income.ModMin = 0;
		Income.ModMax = 0;
	}
}
