using System;
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

	public string Specialty;

	public Criminal()
	{
		InitCharStats();
	}

	private void InitCharStats()
	{
		// Assign ID
		_guid = Guid.NewGuid().ToString();

		//Build Name
		Name = CriminalManager.Instance.GenerateName();

		//Initialize stat values
		_powerValue = _stealthValue = _techValue = _charmValue = 0;

		_powerValue += Random.Range(5, 20);
		_stealthValue += Random.Range(5, 20);
		_techValue += Random.Range(5, 20);
		_charmValue += Random.Range(5, 20);


		// Set Specialty
		// TODO - conditionalize so specialty doesn't get assigned to low value stat
		switch (Random.Range(1, 4))
		{
			case 1:
				_powerValue += _specialBonus;
				break;
			case 2:
				_stealthValue += _specialBonus;
				break;
			case 3:
				_techValue += _specialBonus;
				break;
			case 4:
				_charmValue += _specialBonus;
				break;
		}
	}
}
