using System;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

// Calculate and manage income and current cash
public class IncomeSystem : MonoBehaviour
{
	public static IncomeSystem Instance;

	// References
	[SerializeField] private TextMeshProUGUI _cashDisplay;

	// Properties
	[SerializeField] private float _startingCash;

	private float _currentCash;

	public float CurrentCash => _currentCash;

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		JobsSystem.JobAttemptSuccess += HandleJobIncome;

		_currentCash = _startingCash;
		_cashDisplay.text = _currentCash.ToString("c2");
	}

	private void OnDestroy()
	{
		JobsSystem.JobAttemptSuccess -= HandleJobIncome;
	}

	//
	// This is really really ugly, please fix it
	//
	private void ModifyIncome(Job arg1, Stat arg2, float arg3, float cost)
	{
		_currentCash -= cost;
		UpdatateCashDisplay();
	}

	public void Spend(float cost)
	{
		_currentCash -= cost;
		UpdatateCashDisplay();
	}

	private void UpdatateCashDisplay() => _cashDisplay.text = _currentCash.ToString("c2");

	public bool CanAfford(float cost)
	{
		if (cost <= _currentCash)
		{
			return true;
		}
		else
			return false;
	}

	private void HandleJobIncome(Job job)
	{
		_currentCash += CalculateIncomeResult(GameManager.Instance.jobMap[job].Income.Min, GameManager.Instance.jobMap[job].Income.Max);
		UpdatateCashDisplay();
	}

	private float CalculateIncomeResult(float min, float max)
	{
		float result = Random.Range(min, max);
		Debug.Log(result);
		return result;
	}
}