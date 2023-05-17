using UnityEngine;

public class RosterDisplay : MonoBehaviour
{
	private RosterCard _displayCard;

	public RosterCard DisplayCardPrefab;

	private void Awake()
	{
		StatCard.RosterUpdated += AddToDisplay;
	}

	private void OnDestroy() => StatCard.RosterUpdated -= AddToDisplay;

	// New criminal to be added to roster. Instantiate prefab inside selection container
	// Update appropriate roster display values
	// Add criminal to main roster
	// Assign selected criminal to roster card for display on Assignments page
	private void AddToDisplay(Criminal criminal)
	{
		RosterManager.Instance.UpdateCurrentRoster(criminal);

		_displayCard = Instantiate(DisplayCardPrefab, this.gameObject.transform);
		_displayCard._criminal = criminal;
	}
}
