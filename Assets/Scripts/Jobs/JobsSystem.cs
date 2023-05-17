using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JobsSystem : MonoBehaviour
{
	public static event Action<Job> JobAttemptSuccess;
	public static event Action<Job> JobAttemptFailure;

	[SerializeField] private CompletionTickBar[] _bars;

	private Array _allJobs;
	private Arrests _arrests;

	private void Awake()
	{
		_allJobs = Enum.GetValues(typeof(Job));
		_bars = FindObjectsOfType<CompletionTickBar>();

		_arrests = new Arrests();
	}

	private void Update()
	{
		foreach (CompletionTickBar bar in _bars)
		{
			if (RosterManager.Instance.GetRoster(bar.job).Count > 0 && !GameManager.Instance.jobMap[bar.job].IsWorking)
				StartCoroutine(UpdateCompletionBar(bar));
		}
	}

	private IEnumerator UpdateCompletionBar(CompletionTickBar bar)
	{
		GameManager.Instance.jobMap[bar.job].IsWorking = true;
		float _fillTime = 0;
		while (RosterManager.Instance.GetRoster(bar.job).Count > 0)
		{
			bar.Slider.value = Mathf.Lerp(bar.Slider.minValue, bar.Slider.maxValue, _fillTime);
			_fillTime += Time.deltaTime / GameManager.Instance.jobMap[bar.job].CompletionSpeed;

			if (bar.Slider.value == bar.Slider.maxValue)
			{
				_fillTime = 0;
				bar.Slider.value = bar.Slider.minValue;
				ProcessJob(bar.job);
			}
			yield return null;
		}
		GameManager.Instance.jobMap[bar.job].IsWorking = false;
	}

	private void ProcessJob(Job job)
	{
		List<Criminal> tempList = new List<Criminal>();
		tempList = RosterManager.Instance.GetRoster(job);

		if (tempList.Count > 0)
		{
			foreach (Criminal c in tempList)
			{
				float success = GetBaseSuccessRatePerJob(job) + GetSuccessMod(c, job);
				float check = Random.Range(1, 100);

				if (success + check >= 100)
				{
					JobAttemptSuccess?.Invoke(job);
				}
				else
				{
					JobAttemptFailure?.Invoke(job);
					if (_arrests.CheckForArrest(c))
						break;
				}
			}
		}
	}

	private float GetSuccessMod(Criminal c, Job j)
	{
		float mod = 0;

		if (j == Job.PickPocket)
			mod = c.Stealth;
		else if (j == Job.Hacker)
			mod = c.Tech;
		else if (j == Job.Mugger)
			mod = c.Power;
		else if (j == Job.ConArtist)
			mod = c.Charm;

		return mod;
	}

	private float GetBaseSuccessRatePerJob(Job j) => GameManager.Instance.jobMap[j].SuccessRate;
}