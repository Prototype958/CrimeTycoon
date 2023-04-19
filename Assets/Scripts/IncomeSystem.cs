using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeSystem : MonoBehaviour
{
	// calculate and manage income and current cash

	private float _currentCash = 0.0f;
	private float _basePickPocketIncome = 10f;

	private void Awake()
	{
		JobsSystem.JobAttemptSuccess += HandleJobIncome;
	}

	private void HandleJobIncome(Job job)
	{
		if (job == Job.PickPocket)
			_currentCash += _basePickPocketIncome;

		Debug.Log("cash " + _currentCash);
	}
}