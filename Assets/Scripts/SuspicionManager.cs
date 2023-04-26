using UnityEngine;
using TMPro;

public class SuspicionManager : MonoBehaviour
{
	[SerializeField] private float _suspicion;

	[SerializeField]
	private TextMeshProUGUI _suspicionDisplay;

	private void Awake()
	{
		// Subscribe to the job failure event from the JobsSystem and assign it to UpdateSuspicion
		JobsSystem.JobAttemptFailure += UpdateSuspicion;

		// Init value to 0;
		_suspicionDisplay.text = _suspicion++.ToString("F0");
	}

	private void UpdateSuspicion(Job obj)
	{
		// Mathf.Floor(f) - returns the largest integer smaller to or equal to f
		_suspicionDisplay.text = Mathf.Floor(_suspicion += .25f).ToString();
	}
}
