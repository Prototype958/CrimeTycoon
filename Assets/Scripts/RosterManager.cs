using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RosterManager : MonoBehaviour
{
	private struct RosterSize
	{
		public int max;
		public int current;

		public string display;

		public RosterSize(int _m, int _c)
		{
			max = _m;
			current = _c;

			display = "";
		}
	}

	public static RosterManager Instance;

	public RosterCard RosterCardPrefab;
	public GameObject Container;

	private RosterCard RosterCard;
	private RosterSize Roster;
	private List<Criminal> _mainRoster, _pickPocketRoster, _hackerRoster, _muggerRoster, _conArtistRoster;

	[SerializeField]
	private TextMeshProUGUI _count;

	public void Awake()
	{
		// Set up listeners
		StatCard.RosterUpdated += AddToDisplay;
		StatDisplay.JobUpdated += UpdateJobRosters;

		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		// Initialization
		Roster = new RosterSize(2, 0);
		_count.text = GetRosterDisplay();

		// Lists of criminals and assignments
		_mainRoster = new List<Criminal>();
		_pickPocketRoster = new List<Criminal>();
		_hackerRoster = new List<Criminal>();
		_muggerRoster = new List<Criminal>();
		_conArtistRoster = new List<Criminal>();
	}

	public List<Criminal> GetRoster(Job job)
	{
		List<Criminal> list = new List<Criminal>();

		switch (job)
		{
			case Job.PickPocket:
				list = _pickPocketRoster;
				break;
			case Job.Hacker:
				list = _hackerRoster;
				break;
			case Job.Mugger:
				list = _muggerRoster;
				break;
			case Job.ConArtist:
				list = _conArtistRoster;
				break;
		}

		return list;
	}

	public bool AnyAssignedJobs()
	{
		if (_pickPocketRoster.Count > 0 ||
			_hackerRoster.Count > 0 ||
			_muggerRoster.Count > 0 ||
			_conArtistRoster.Count > 0)
		{
			return true;
		}
		else
			return false;
	}

	public bool UpdateCurrentRosterSize(int i)
	{
		if (Roster.current < Roster.max)
		{
			Roster.current += i;
			_count.text = GetRosterDisplay();
			return true;
		}
		else
		{
			// Roster is full, unable to add new member
			return false;
		}
	}

	public void UpdateMaxRosterSize(int i)
	{
		Roster.max += i;
		_count.text = GetRosterDisplay();
	}

	private void AddToDisplay(Criminal criminal)
	{
		RosterCard = Instantiate(RosterCardPrefab, Container.transform);
		RosterCard._criminal = criminal;
		_mainRoster.Add(criminal);
	}

	private void UpdateJobRosters(Criminal criminal, Job job)
	{
		switch (job)
		{
			case Job.PickPocket:
				_pickPocketRoster.Add(criminal);
				break;
			case Job.Hacker:
				_hackerRoster.Add(criminal);
				break;
			case Job.Mugger:
				_muggerRoster.Add(criminal);
				break;
			case Job.ConArtist:
				_conArtistRoster.Add(criminal);
				break;
			default:
				RemoveFromAllJobs(criminal);
				break;
		}
	}

	private void RemoveFromAllJobs(Criminal criminal)
	{
		if (_pickPocketRoster.Contains(criminal))
		{
			_pickPocketRoster.Remove(criminal);
		}

		if (_hackerRoster.Contains(criminal))
		{
			_hackerRoster.Remove(criminal);
		}

		if (_muggerRoster.Contains(criminal))
		{
			_muggerRoster.Remove(criminal);
		}

		if (_conArtistRoster.Contains(criminal))
		{
			_conArtistRoster.Remove(criminal);
		}

	}

	public int GetCurrentRosterSize()
	{
		return Roster.current;
	}

	public int GetMaxRosterSize()
	{
		return Roster.max;
	}

	public string GetRosterDisplay()
	{
		return $"{Roster.current}/{Roster.max} 'Employees'";
	}


}

