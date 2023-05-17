using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
	public static event Action<Criminal, Job> JobUpdated;

	//references to text fields to update
	[SerializeField] private TextMeshProUGUI _nameField, _powerField, _stealthField, _techField, _charmField;
	[SerializeField] private Toggle _pickPocketToggle, _hackerToggle, _muggerToggle, _conArtistToggle;
	[SerializeField] private ToggleGroup _assignmentOptions;

	private Criminal _currentCriminal;

	public Criminal CurrentCriminal { get { return _currentCriminal; } }

	private void Awake()
	{
		JobStatsClass.EnableJob += EnableJobToggle;

		this.gameObject.SetActive(!gameObject.activeSelf);

		_hackerToggle.interactable = false;
		_muggerToggle.interactable = false;
		_conArtistToggle.interactable = false;
	}

	private void OnDestroy()
	{
		JobStatsClass.EnableJob -= EnableJobToggle;
	}

	public void Update()
	{
		// replace with "X" to close button eventually
		if (Input.GetKeyDown(KeyCode.Escape))
			this.gameObject.SetActive(false);
	}

	private void EnableJobToggle(Job job)
	{
		if (job == Job.Hacker)
			_hackerToggle.interactable = true;
		if (job == Job.Mugger)
			_muggerToggle.interactable = true;
		if (job == Job.ConArtist)
			_conArtistToggle.interactable = true;
	}

	public void UpdateDisplay(Criminal criminal)
	{
		// Realtime updating as stats change
	}

	public void ToggleActive(Criminal criminal)
	{
		// if not active, activate and update display
		// if new roster card selected, update display
		// if roster card deselected, de-activate

		gameObject.SetActive(true);

		_currentCriminal = criminal;
		SelectCurrentJob();

		_nameField.text = criminal.Name;
		_powerField.text = criminal.Power.ToString();
		_stealthField.text = criminal.Stealth.ToString();
		_techField.text = criminal.Tech.ToString();
		_charmField.text = criminal.Charm.ToString();
	}

	public void Deactivate() => gameObject.SetActive(false);

	private void SelectCurrentJob()
	{
		if (_currentCriminal.CurrentJob == Job.PickPocket)
			_pickPocketToggle.isOn = true;
		else if (_currentCriminal.CurrentJob == Job.Hacker)
			_hackerToggle.isOn = true;
		else if (_currentCriminal.CurrentJob == Job.Mugger)
			_muggerToggle.isOn = true;
		else if (_currentCriminal.CurrentJob == Job.ConArtist)
			_conArtistToggle.isOn = true;
		else
			_assignmentOptions.SetAllTogglesOff();
	}

	public void ToggleGroupUpdate(bool tog)
	{
		Job newJob = Job.None;
		Toggle selectedToggle = _assignmentOptions.ActiveToggles().FirstOrDefault();

		if (tog)
		{
			if (selectedToggle == _pickPocketToggle)
				newJob = Job.PickPocket;
			else if (selectedToggle == _hackerToggle)
				newJob = Job.Hacker;
			else if (selectedToggle == _muggerToggle)
				newJob = Job.Mugger;
			else if (selectedToggle == _conArtistToggle)
				newJob = Job.ConArtist;

			AssignJob(newJob);
		}
		else
		{
			if (selectedToggle == null)
				AssignJob(Job.None);
		}
	}

	private void AssignJob(Job job)
	{
		if (_currentCriminal.CurrentJob != job)
		{
			// assign selected job to criminal
			_currentCriminal.CurrentJob = job;
			JobUpdated?.Invoke(_currentCriminal, job);
		}
	}
}