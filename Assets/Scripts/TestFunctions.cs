using UnityEngine;
using UnityEngine.UI;

public class TestFunctions : MonoBehaviour
{
    public Canvas CharSelectPanel;
    public Canvas FadeCanvas;

    public GameObject CharPanel;

    bool toggle = false;

    public void IAmAButton()
    {
        CharPanel.SetActive(toggle = !toggle);
    }

    public void UpgradeRoster()
    {
        GameManager.Instance.MaxRosterSize += 3;
    }
}
