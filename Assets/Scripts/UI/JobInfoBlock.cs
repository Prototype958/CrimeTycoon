using System;
using UnityEngine;
using TMPro;

public class JobInfoBlock : MonoBehaviour
{
	[SerializeField] private Job _job;
	[SerializeField] private TextMeshProUGUI _successRateDisplay;
	[SerializeField] private TextMeshProUGUI _workersDisplay;
	[SerializeField] private TextMeshProUGUI _incomeRateDisply;
	[SerializeField] private TextMeshProUGUI _completionSpeedDisplay;

	// Update is called once per frame
	void Update()
	{
		_workersDisplay.text = RosterManager.Instance.GetRoster(_job).Count.ToString();
		_completionSpeedDisplay.text = Math.Round(GameManager.Instance.jobMap[_job].CompletionSpeed, 2).ToString();
		_successRateDisplay.text = Math.Round(GameManager.Instance.jobMap[_job].SuccessRate, 2).ToString() + "%";
		_incomeRateDisply.text = Math.Round((GameManager.Instance.jobMap[_job].Income.Min + GameManager.Instance.jobMap[_job].Income.Max) / 2, 2).ToString();
	}
}
