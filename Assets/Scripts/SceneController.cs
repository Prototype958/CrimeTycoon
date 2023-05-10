using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public static SceneController Instance;

	public void Awake()
	{
		DontDestroyOnLoad(this);

		if (Instance != null && Instance != this)
			Destroy(this.gameObject);
		else
			Instance = this;
	}

	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void ExitToMenu()
	{
		SceneManager.LoadScene("StartMenu");
	}
}
