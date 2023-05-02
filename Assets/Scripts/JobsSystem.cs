using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JobsSystem : MonoBehaviour
{
	const float BASE_SUCCESS_RATE = 20f;

	public static event Action<Job> JobAttemptSuccess;
	public static event Action<Job> JobAttemptFailure;

	private Array _allJobs;

	private void Awake()
	{
		_allJobs = Enum.GetValues(typeof(Job));
	}

	private void Update()
	{
		foreach (Job job in _allJobs)
		{
			if (RosterManager.Instance.GetRoster(job).Count > 0 && !GameManager.Instance.jobMap[job].IsWorking)
				StartCoroutine(ProcessJobRepeat(job, GameManager.Instance.jobMap[job].CompletionSpeed));
		}
	}

	private IEnumerator ProcessJobRepeat(Job job, float dur)
	{
		GameManager.Instance.jobMap[job].IsWorking = true;
		while (RosterManager.Instance.GetRoster(job).Count > 0)
		{
			ProcessJob(job);
			yield return new WaitForSeconds(dur);
		}
		GameManager.Instance.jobMap[job].IsWorking = false;
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