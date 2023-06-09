using UnityEngine;

public class Arrests
{
	[SerializeField] private float _arrestThreshold;

	public float ArrestThreshold { get { return _arrestThreshold; } set { _arrestThreshold = value; } }

	private SuspicionManager _suspicion;
	private NotificationSystem _notifications;

	public Arrests()
	{
		_arrestThreshold = 50f;
		_suspicion = GameObject.FindObjectOfType<SuspicionManager>();
		_notifications = GameObject.FindObjectOfType<NotificationSystem>();
	}

	// Check if parameters for arrest have been met
	// If parameters met, attempt an arrest
	// If arrest successful, return true
	public bool CheckForArrest(Criminal criminal)
	{
		if (_suspicion.Suspicion > _arrestThreshold)
		{
			CriminalManager.Instance.OnArrest(criminal);

			_notifications.LogNotification($"{criminal.Name} has been arrested!", Message.MessageType.Alert);
			return true;
		}
		return false;
	}
}
