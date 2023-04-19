using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class IncomeSystem : MonoBehaviour
{
	// calculate and manage income and current cash

	private float _currentCash = 0.00f;
	private decimal _basePickPocketIncome = 10.00M;

	[SerializeField]
	private TextMeshProUGUI _cashDisplay;

	private void Awake()
	{
		JobsSystem.JobAttemptSuccess += HandleJobIncome;
		_cashDisplay.text = "$0.00";
	}

	private void HandleJobIncome(Job job)
	{
		if (job == Job.PickPocket)
		{
			float result = ((float)(Random.Range(3f, 10f)));
			_currentCash += result;
			Debug.Log(result);
		}
		//Debug.Log("cash " + _currentCash);
		_cashDisplay.text = _currentCash.ToString("c2");
	}
}