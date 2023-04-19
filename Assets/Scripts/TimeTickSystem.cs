using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeTickSystem
{
	public class OnTickEventArgs : EventArgs
	{
		public int tick;
	}

	public static event EventHandler<OnTickEventArgs> OnTick;

	private const float TICK_TIMER_MAX = 1f;

	private static int _tick;
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

	public static int GetTick()
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
				_tick++;
				OnTick?.Invoke(this, new OnTickEventArgs { tick = _tick });
			}
		}
	}
}
