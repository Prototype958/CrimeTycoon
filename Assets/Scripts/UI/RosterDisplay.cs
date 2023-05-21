using UnityEngine;

public class RosterDisplay : MonoBehaviour
{
	private RosterCard _displayCard;
	private NotificationSystem _notifications;

	public RosterCard DisplayCardPrefab;

	private void Awake()
	{
		StatCard.RosterUpdated += AddToDisplay;
		_notifications = GameObject.FindObjectOfType<NotificationSystem>();
	}

	private void OnDestroy() => StatCard.RosterUpdated -= AddToDisplay;

	// New criminal to be added to roster. Instantiate prefab inside selection container
	// Update appropriate roster display values
	// Add criminal to main roster
	// Assign selected criminal to roster card for display on Assignments page
	private void AddToDisplay(Criminal criminal)
	{
		RosterManager.Instance.UpdateCurrentRoster(criminal);
		_notifications.LogNotification($"{criminal.Name} has joined the crew.", Message.MessageType.Simple);

		_displayCard = Instantiate(DisplayCardPrefab, this.gameObject.transform);
		_displayCard._criminal = criminal;
	}
}