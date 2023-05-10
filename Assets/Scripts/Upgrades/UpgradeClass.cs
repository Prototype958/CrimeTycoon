using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeClass
{
	public Upgrade upgrade;

	private string _name;
	private float _cost;
	private string _description;
	private bool _isRepeatable;
	private int _rank;
	private float _upgradeValue;

	public string Name
	{
		get
		{
			if (_rank >= 1)
			{
				return _name + " " + _rank;
			}
			else
			{
				return _name;
			}
		}
	}
	public float Cost => _cost;
	public string Description => _description;
	public bool IsRepeatable => _isRepeatable;
	public int Rank => _rank;
	public float UpgradeValue => _upgradeValue;

	private List<Job> AffectedJobs = new List<Job>();
	private List<Stat> StatsToUpgrade = new List<Stat>();
	private List<Upgrade> preRequisites = new List<Upgrade>();

	public List<Job> GetAffectedJobs() => AffectedJobs;
	public List<Stat> GetStatsToUpgrade() => StatsToUpgrade;
	public List<Upgrade> GetPrerequisites() => preRequisites;

	public UpgradeClass(Upgrade u)
	{
		upgrade = u;

		_name = u.Name;
		_cost = u.Cost;
		_description = u.Description;
		_isRepeatable = u.IsRepeatable;
		_rank = u.Rank;
		_upgradeValue = u.UpgradeValue;

		AffectedJobs = u.GetAffectedJobs();
		StatsToUpgrade = u.GetStatsToUpgrade();
		preRequisites = u.GetPrerequisites();
	}

	public bool CheckForNextRank()
	{
		if (IsRepeatable)
		{
			_rank++;
			_cost += (_cost * .25f);
			//_upgradeValue += (_upgradeValue * .33f);
			//_upgradeValue = _upgradeValue * (Mathf.Pow(_rank, 1.25f));
			return true;
		}

		return false;
	}
}
