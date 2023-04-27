using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public static event Action<Job> EnableJob;

	// Public properties
	public int MaxRosterSize { get { return _maxRosterSize; } set { _maxRosterSize = value; } }
	public int CurrentRosterSize { get { return _currentRosterSize; } set { _currentRosterSize = value; } }

	// Private variables
	private int _maxRosterSize;
	private int _currentRosterSize;

	[SerializeField] private float _recruitCost;

	public float RecruitCost => _recruitCost;

	public void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		// Set up event listeners
		Upgrade.ApplyUpgrade += ApplyUpgrade;

		TimeTickSystem.Create(this.gameObject);

		// Set up initial values
		_maxRosterSize = 2;
		_currentRosterSize = 0;
	}

	// set this up to be called when an upgrade is purchased
	// pass in upgrade to be applied, call appripriate function based on upgrade
	private void ApplyUpgrade(Job job, Stat stat, float value, float cost)
	{
		Debug.Log($"Upgrade {job} {stat} by {value}");

		if (stat == Stat.Unlock)
		{
			EnableJob?.Invoke(job);
		}
	}

	private void Apply2(Upgrade u)
	{
	}
}