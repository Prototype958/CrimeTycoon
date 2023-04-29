using UnityEngine;

public class CharSelectManager : MonoBehaviour
{
	public StatCard CardPrefab;
	public StatCard[] StatCards;

	[SerializeField] private GameObject _blocker;

	public void Awake()
	{
		StatCards = new StatCard[3];

		StatCard.RosterUpdated += DisablePanel;
	}
	public void ToggleDisplay() => this.gameObject.SetActive(!gameObject.activeSelf);
	public void DisablePanel(Criminal c) => ToggleDisplay();

	public void OnEnable()
	{
		_blocker.SetActive(true);
		for (int i = 0; i < StatCards.Length; i++)
		{
			StatCards[i] = Instantiate(CardPrefab, this.transform);
		}
	}

	public void OnDisable()
	{
		_blocker.SetActive(false);
		foreach (StatCard card in StatCards)
		{
			if (card != null)
				Destroy(card.gameObject);
		}
	}
}
