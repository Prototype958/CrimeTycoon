using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/BaseUpgrade")]
public class Upgrade : ScriptableObject
{
	[SerializeField] private string _name;
	[SerializeField] private float _cost;
	[SerializeField] private string _description;
	[SerializeField] private bool _isRepeatable;
	[SerializeField] private int _rank;
	[SerializeField] private float _upgradeValue;

	public string Name => _name;
	public float Cost => _cost;
	public string Description => _description;
	public bool IsRepeatable => _isRepeatable;
	public int Rank => _rank;
	public float UpgradeValue => _upgradeValue;

	[SerializeField] private List<Job> AffectedJobs = new List<Job>();
	[SerializeField] private List<Stat> StatsToUpgrade = new List<Stat>();
	[SerializeField] private List<Upgrade> preRequisites = new List<Upgrade>();

	public List<Job> GetAffectedJobs() => AffectedJobs;
	public List<Stat> GetStatsToUpgrade() => StatsToUpgrade;
	public List<Upgrade> GetPrerequisites() => preRequisites;
}

public enum Stat
{
	Power,
	Tech,
	Stealth,
	Charm,
	CompletionSpeed,
	SuccessRate,
	SuspicionGain,
	IncomeGain,
	Unlock,
	Rank
}

