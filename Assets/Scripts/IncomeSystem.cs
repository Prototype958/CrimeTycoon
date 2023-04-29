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

	[SerializeField] private Income<Job> PickPocketIncome;
	[SerializeField] private Income<Job> HackerIncome;
	[SerializeField] private Income<Job> MuggerIncome;
	[SerializeField] private Income<Job> ConArtistIncome;

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

	private void HandleJobIncome2(Job job)
	{
		switch (job)
		{
			case Job.PickPocket:
				_currentCash += CalculateIncomeResult(PickPocketIncome.Min, PickPocketIncome.Max);
				break;
			case Job.Hacker:
				_currentCash += CalculateIncomeResult(HackerIncome.Min, HackerIncome.Max);
				break;
			case Job.Mugger:
				_currentCash += CalculateIncomeResult(MuggerIncome.Min, MuggerIncome.Max);
				break;
			case Job.ConArtist:
				_currentCash += CalculateIncomeResult(ConArtistIncome.Min, ConArtistIncome.Max);
				break;
		}

		UpdatateCashDisplay();
	}

	private void HandleJobIncome(Job job)
	{
		switch (job)
		{
			case Job.PickPocket:
				_currentCash += CalculateIncomeResult(GameManager.Instance.PickPocket.Income.Min, GameManager.Instance.PickPocket.Income.Max);
				break;
			case Job.Hacker:
				_currentCash += CalculateIncomeResult(HackerIncome.Min, HackerIncome.Max);
				break;
			case Job.Mugger:
				_currentCash += CalculateIncomeResult(MuggerIncome.Min, MuggerIncome.Max);
				break;
			case Job.ConArtist:
				_currentCash += CalculateIncomeResult(ConArtistIncome.Min, ConArtistIncome.Max);
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

[Serializable]
public class Income<T>
{
	// Values initialized in editor
	[SerializeField]
	private float min;
	[SerializeField]
	private float max;

	public float Min { get { return min; } set { min = value; } }
	public float Max { get { return max; } set { max = value; } }

	public void UpdateIncome(float _newMin, float _newMax)
	{
		min = _newMin;
		max = _newMax;
	}
}