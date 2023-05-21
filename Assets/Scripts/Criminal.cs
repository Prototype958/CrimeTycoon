using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Job
{
	None,
	PickPocket,
	Hacker,
	Mugger,
	ConArtist
}

public class Criminal
{
	public string Name;

	public int Rank;

	public int Power { get { return _powerValue; } }
	public int Stealth { get { return _stealthValue; } }
	public int Tech { get { return _techValue; } }
	public int Charm { get { return _charmValue; } }
	public string ID { get { return _guid; } }
	public Job CurrentJob { get { return _currentJob; } set { _currentJob = value; } }

	private int _powerValue, _stealthValue, _techValue, _charmValue;
	private string _guid;
	private Job _currentJob;

	private int _specialBonus = 2;

	public int Specialty;

	public Criminal()
	{
		InitCharStats();
	}

	private void InitCharStats()
	{
		Rank = CriminalManager.Instance.RecruitRank;

		// Assign ID
		_guid = Guid.NewGuid().ToString();

		//Build Name
		Name = CriminalManager.Instance.GenerateName();

		//Initialize stat values
		_powerValue = _stealthValue = _techValue = _charmValue = 0;

		// Set Speciality
		// range is max exclusive
		Specialty = Random.Range(1, 5);

		_powerValue = RollStats(Specialty == 1 ? Rank + 1 : Rank);
		_stealthValue = RollStats(Specialty == 2 ? Rank + 1 : Rank);
		_techValue = RollStats(Specialty == 3 ? Rank + 1 : Rank);
		_charmValue = RollStats(Specialty == 4 ? Rank + 1 : Rank);
	}

	private int RollStats(int rank)
	{
		int val = 0;
		List<int> results = new List<int>();

		for (int i = 0; i < 2 + Rank; i++)
		{
			results.Add(Random.Range(1, 7));
		}

		results.Remove(results.Min());

		foreach (int i in results)
			val += i;

		return val;
	}
}
