using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationSystem : MonoBehaviour
{
	[SerializeField] private GameObject _text;
	[SerializeField] private List<Message> _messages = new();

	public void LogNotification(string msg)
	{
		Message message = new Message();
		GameObject notification = Instantiate(_text, this.gameObject.transform);

		message.TextPrefab = notification.GetComponent<TextMeshProUGUI>();
		message.TextPrefab.text = msg;

		_messages.Add(message);
	}
}

[System.Serializable]
public class Message
{
	public string Text;
	public TextMeshProUGUI TextPrefab;
}
