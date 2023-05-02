using UnityEngine;

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


	public UpgradeClass(Upgrade u)
	{
		upgrade = u;

		_name = u.Name;
		_cost = u.Cost;
		_description = u.Description;
		_isRepeatable = u.IsRepeatable;
		_rank = u.Rank;
		_upgradeValue = u.UpgradeValue;
	}

	public bool CheckForNextRank()
	{
		if (IsRepeatable)
		{
			_rank++;
			_cost *= 1.25f;
			_upgradeValue *= 1.25f;
			_upgradeValue = Mathf.Round(_upgradeValue * 4) / 4;
			return true;
		}

		return false;
	}
}
