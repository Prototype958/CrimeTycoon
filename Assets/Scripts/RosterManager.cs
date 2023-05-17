using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Purpose of this class is to track and manage the numerous lists (Rosters)
// used to identify 
public class RosterManager : MonoBehaviour
{
	// Track current and max size of main roster
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

	private RosterCard _rosterCard;
	private RosterSize Roster;
	private List<Criminal> _mainRoster, _arrestedRoster, _pickPocketRoster, _hackerRoster, _muggerRoster, _conArtistRoster;

	[SerializeField] private TextMeshProUGUI _count;

	public void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this.gameObject);
		else
			Instance = this;

		// Set up listeners
		StatDisplay.JobUpdated += UpdateJobRosters;

		// Initialization
		Roster = new RosterSize(2, 0); // RosterSize(max, cur)
		UpdateRosterSizeDisplay();

		// Lists of criminals and assignments
		_mainRoster = new List<Criminal>();
		_arrestedRoster = new List<Criminal>();
		_pickPocketRoster = new List<Criminal>();
		_hackerRoster = new List<Criminal>();
		_muggerRoster = new List<Criminal>();
		_conArtistRoster = new List<Criminal>();
	}

	private void OnDestroy()
	{
		StatDisplay.JobUpdated -= UpdateJobRosters;
	}

	// Return an individual roster for a specific job
	// Called in JobSystem.cs
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

	// Check if any jobs currently have criminal assigned
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

	// Increase value representing current members in roster
	// and update visual display to reflect
	public void UpdateCurrentRoster(Criminal c)
	{
		UpdateCurrentRosterValue(1);
		_mainRoster.Add(c);
	}

	public void UpdateCurrentRosterValue(int i)
	{
		Roster.current += i;
		UpdateRosterSizeDisplay();
	}

	// Increase value representing max members allowed in roster
	// and update visual display to reflect
	public void UpdateMaxRosterSize(int i)
	{
		Roster.max += i;
		UpdateRosterSizeDisplay();
	}

	public void MoveToArrested(Criminal c)
	{
		RemoveFromAllJobs(c);
		UpdateCurrentRosterValue(-1);
		_mainRoster.Remove(c);
		_arrestedRoster.Add(c);
	}

	// When new job is selected for a criminal, move update/move them to
	// the correct job specific roster
	// When no jobs selected, remove from all job specific rosters
	private void UpdateJobRosters(Criminal criminal, Job job)
	{
		RemoveFromAllJobs(criminal);
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

	// Remove from all job rosters.
	// Necessary for when switching between jobs
	private void RemoveFromAllJobs(Criminal criminal)
	{
		if (_pickPocketRoster.Contains(criminal))
			_pickPocketRoster.Remove(criminal);

		if (_hackerRoster.Contains(criminal))
			_hackerRoster.Remove(criminal);

		if (_muggerRoster.Contains(criminal))
			_muggerRoster.Remove(criminal);

		if (_conArtistRoster.Contains(criminal))
			_conArtistRoster.Remove(criminal);
	}

	// Return current number of criminals on main roster
	public int GetCurrentRosterSize() => Roster.current;

	// Return max number of criminals allowed on main roster
	public int GetMaxRosterSize() => Roster.max;

	// Return whether main roster is currently full
	public bool IsRosterFull()
	{
		if (Roster.current >= Roster.max)
			return true;
		else
			return false;
	}

	// Update visual text display of current/max roster size
	private void UpdateRosterSizeDisplay() => _count.text = $"{Roster.current}/{Roster.max} 'Employees'";
}