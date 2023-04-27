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

	public float pickpocketspeed = 5;

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
		if (TimeTickSystem.GetTick() % 1 == 0)
			ProcessJob(Job.PickPocket);
		if (TimeTickSystem.GetTick() % 3 == 0)
			ProcessJob(Job.Hacker);
		if (TimeTickSystem.GetTick() % 6 == 0)
			ProcessJob(Job.Mugger);
		if (TimeTickSystem.GetTick() % 10 == 0)
			ProcessJob(Job.ConArtist);
	}

	private void ProcessJob(Job job)
	{
		List<Criminal> tempList = new List<Criminal>();
		tempList = RosterManager.Instance.GetRoster(job);

		if (tempList.Count > 0)
		{
			foreach (Criminal c in tempList)
			{
				float success = BASE_SUCCESS_RATE + GetSuccessMod(c, job);
				float check = Random.Range(1, 100);

				if (success + check >= 100)
				{
					JobAttemptSuccess?.Invoke(job);
					//Debug.Log($"{c.Name} {job} completed successfully");
				}
				else
				{
					JobAttemptFailure?.Invoke(job);
					//Debug.Log($"{c.Name} {job} was a failure");
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
}
