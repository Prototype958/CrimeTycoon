using UnityEngine;

public class MenuButtons : MonoBehaviour
{
	public void StartButtonPressed()
	{
		SceneController.Instance.LoadGameScene();
	}
}
