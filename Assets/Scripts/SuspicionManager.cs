using UnityEngine;
using TMPro;

public class SuspicionManager : MonoBehaviour
{
	[SerializeField] private float _suspicion = 0;

	[SerializeField] private TextMeshProUGUI _suspicionDisplay;

	private void Awake()
	{
		// Subscribe to the job failure event from the JobsSystem and assign it to UpdateSuspicion
		JobsSystem.JobAttemptFailure += UpdateSuspicion;

		// Init value to 0;
		_suspicionDisplay.text = _suspicion.ToString();
	}

	private void OnDestroy()
	{
		JobsSystem.JobAttemptFailure -= UpdateSuspicion;
	}

	private void Update()
	{
		// Passively reduce suspicion - Can be upgraded

		// 100 Suspicion - Game over
		if (_suspicion >= 100)
		{
			SceneController.Instance.ExitToMenu();
		}
	}

	private void UpdateSuspicion(Job job)
	{
		// Mathf.Floor(f) - returns the largest integer smaller to or equal to f
		_suspicionDisplay.text = Mathf.Floor(_suspicion += GameManager.Instance.jobMap[job].SuspicionGain).ToString();
	}
}
