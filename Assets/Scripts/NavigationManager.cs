using UnityEngine;

public class NavigationManager : MonoBehaviour
{
	private GameObject _currentActiveCanvas;

	public GameObject AssignmentScreen;
	public GameObject UpgradeScreen;
	public GameObject StatsScreen;
	public GameObject MenuScreen;

	public void Start()
	{
		AssignmentScreen.SetActive(true);
		UpgradeScreen.SetActive(true);
		StatsScreen.SetActive(true);
		MenuScreen.SetActive(true);

		AssignmentScreen.SetActive(false);
		UpgradeScreen.SetActive(false);
		StatsScreen.SetActive(false);
		MenuScreen.SetActive(false);
	}

	public void Update()
	{
		// replace with "X" to close button eventually
		if (_currentActiveCanvas != null && Input.GetKeyDown(KeyCode.Escape))
		{
			_currentActiveCanvas.SetActive(false);
			_currentActiveCanvas = null;
		}
	}

	public void ActivatePanel(GameObject screen)
	{
		DeactivateCurrentScreen();

		if (_currentActiveCanvas == screen)
		{
			_currentActiveCanvas = null;
		}
		else
		{
			_currentActiveCanvas = screen;
			screen.SetActive(true);
		}
	}

	public void DeactivateCurrentScreen()
	{
		if (_currentActiveCanvas != null)
			_currentActiveCanvas.SetActive(false);
	}
}