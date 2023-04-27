using System;
using UnityEngine;

public static class TimeTickSystem
{
	public class OnTickEventArgs : EventArgs
	{
		public float tick;
	}

	public static event EventHandler<OnTickEventArgs> OnTick;

	// Set tick time to 1/4 second
	// This allows for a larger variety in completion times
	// and speed upgrades.
	private const float TICK_TIMER_MAX = .25f;

	private static float _tick;
	private static GameObject timeTickSystemObject;

	public static void Create(GameObject parent)
	{
		if (timeTickSystemObject == null)
		{
			timeTickSystemObject = new GameObject("TimeTickSystem");
			timeTickSystemObject.transform.parent = parent.transform;
			timeTickSystemObject.AddComponent<TimeTickSystemObject>();
		}
	}

	public static float GetTick()
	{
		return _tick;
	}

	private class TimeTickSystemObject : MonoBehaviour
	{
		private float _tickTimer;

		private void Awake()
		{
			_tick = 0;
		}

		private void Update()
		{
			_tickTimer += Time.deltaTime;
			if (_tickTimer >= TICK_TIMER_MAX)
			{
				_tickTimer -= TICK_TIMER_MAX;
				_tick += 0.25f;
				OnTick?.Invoke(this, new OnTickEventArgs { tick = _tick });
			}
		}
	}
}
