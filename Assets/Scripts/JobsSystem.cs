using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JobsSystem : MonoBehaviour
{
	// When a criminal is added to any job list
	// start their "work"
	// calculate success rate, and work speeds per criminal

	// each success returns cash
	// each failure returns suspicion

	// TODO - NEED TO ADJUST ALL VALUES
	// SOME JOBS SHOULD WORK ON LONGER TICKS

	// Each job needs its own pros/cons.
	// Pick Pocket: 
	// quick completion time
	// low suspicion
	// low reward
	// average success rate
	// Hacking
	// lower success rate
	// higher reward
	// low suspicion
	// slower completion time?
	// Mugging: 
	// fast completion
	// high suspicion
	// high reward
	// average success?
	// Con Artist:
	// longest completion
	// highest reward
	// average suspicion
	// average success rate?

	const float BASE_SUCCESS_RATE = 20f;

	public static event Action<Job> JobAttemptSuccess;
	public static event Action<Job> JobAttemptFailure;

	private bool _jobsWorking = false;

	private void Update()
	{
		if (RosterManager.Instance.AnyAssignedJobs() && !_jobsWorking)
		{
			_jobsWorking = true;
			TimeTickSystem.OnTick += RunJobs_OnTick;
		}
		else if (!RosterManager.Instance.AnyAssignedJobs())
		{
			_jobsWorking = false;
			TimeTickSystem.OnTick -= RunJobs_OnTick;
			// maybe passive reduce suspicion while running no jobs?
		}
	}

	private void RunJobs_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
	{
		ProcessPickPocketJobs();
		if (TimeTickSystem.GetTick() % 3 == 0)
			ProcessHackerJobs();
	}

	private void ProcessPickPocketJobs()
	{
		List<Criminal> tempList = new List<Criminal>();
		tempList = RosterManager.Instance.GetRoster(Job.PickPocket);

		if (tempList.Count > 0)
		{
			foreach (Criminal c in tempList)
			{
				float success = BASE_SUCCESS_RATE + c.Stealth;
				float check = Random.Range(1, 100);

				if (success + check >= 100)
				{
					JobAttemptSuccess?.Invoke(Job.PickPocket);
					Debug.Log($"{c.Name} Pick Pocket completed successfully");
				}
				else
				{
					JobAttemptFailure?.Invoke(Job.PickPocket);
					Debug.Log($"{c.Name} Pick Pocket was a failure");
				}
			}
		}
	}

	private void ProcessHackerJobs()
	{
		List<Criminal> tempList = new List<Criminal>();
		tempList = RosterManager.Instance.GetRoster(Job.Hacker);

		if (tempList.Count > 0)
		{
			foreach (Criminal c in tempList)
			{
				float success = BASE_SUCCESS_RATE + c.Tech;
				float check = Random.Range(1, 100);

				if (success + check >= 100)
				{
					JobAttemptSuccess?.Invoke(Job.Hacker);
					Debug.Log($"{c.Name} Hack completed successfully");
				}
				else
				{
					JobAttemptFailure?.Invoke(Job.Hacker);
					Debug.Log($"{c.Name} Hack was a failure");
				}
			}
		}
	}
}
