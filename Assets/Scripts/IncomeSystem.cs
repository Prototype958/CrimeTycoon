using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class IncomeSystem : MonoBehaviour
{
	// calculate and manage income and current cash
	// References
	[SerializeField]
	private TextMeshProUGUI _cashDisplay;

	// Properties
	private float _currentCash = 0.00f;

	[SerializeField] private Income<Job> PickPocketIncome;
	[SerializeField] private Income<Job> HackerIncome;
	[SerializeField] private Income<Job> MuggerIncome;
	[SerializeField] private Income<Job> ConArtistIncome;

	private void Awake()
	{
		JobsSystem.JobAttemptSuccess += HandleJobIncome;
		_cashDisplay.text = "$0.00";
	}

	private void HandleJobIncome(Job job)
	{
		if (job == Job.PickPocket)
			_currentCash += CalculateIncomeResult(PickPocketIncome.Min, PickPocketIncome.Max);
		else if (job == Job.Hacker)
			_currentCash += CalculateIncomeResult(HackerIncome.Min, HackerIncome.Max);
		else if (job == Job.Mugger)
			_currentCash += CalculateIncomeResult(MuggerIncome.Min, MuggerIncome.Max);
		else if (job == Job.ConArtist)
			_currentCash += CalculateIncomeResult(ConArtistIncome.Min, ConArtistIncome.Max);

		_cashDisplay.text = _currentCash.ToString("c2");
	}

	private float CalculateIncomeResult(float min, float max)
	{
		float result = Random.Range(min, max);
		Debug.Log(result);
		return result;
	}
}

[Serializable]
public class Income<T>
{
	[SerializeField]
	private float min;
	[SerializeField]
	private float max;

	public float Min { get { return min; } set { min = value; } }
	public float Max { get { return max; } set { max = value; } }

	// public Income(float _min, float _max)
	// {
	// 	min = _min;
	// 	max = _max;
	// }

	public void UpdateIncome(float _newMin, float _newMax)
	{
		min = _newMin;
		max = _newMax;
	}

}