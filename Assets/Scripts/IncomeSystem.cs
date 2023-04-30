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
	private float _currentCash = 0.00f;

	public float CurrentCash => _currentCash;

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;

		JobsSystem.JobAttemptSuccess += HandleJobIncome;
		_cashDisplay.text = "$0.00";
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
		switch (job)
		{
			case Job.PickPocket:
				_currentCash += CalculateIncomeResult(GameManager.Instance.PickPocket.Income.Min, GameManager.Instance.PickPocket.Income.Max);
				break;
			case Job.Hacker:
				_currentCash += CalculateIncomeResult(GameManager.Instance.Hacker.Income.Min, GameManager.Instance.Hacker.Income.Max);
				break;
			case Job.Mugger:
				_currentCash += CalculateIncomeResult(GameManager.Instance.Mugger.Income.Min, GameManager.Instance.Mugger.Income.Max);
				break;
			case Job.ConArtist:
				_currentCash += CalculateIncomeResult(GameManager.Instance.ConArtist.Income.Min, GameManager.Instance.ConArtist.Income.Max);
				break;
		}

		UpdatateCashDisplay();
	}

	private float CalculateIncomeResult(float min, float max)
	{
		float result = Random.Range(min, max);
		Debug.Log(result);
		return result;
	}
}