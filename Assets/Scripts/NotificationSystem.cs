using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationSystem : MonoBehaviour
{
	[SerializeField] private GameObject _text;
	[SerializeField] private List<Message> _messages = new();

	[Header("Notification Text Colors")]
	[SerializeField] private Color _simpleColor;
	[SerializeField] private Color _warningColor;
	[SerializeField] private Color _alertColor;

	public void LogNotification(string msg, Message.MessageType type)
	{
		Message message = new Message();
		GameObject notification = Instantiate(_text, this.gameObject.transform);

		message.TextPrefab = notification.GetComponent<TextMeshProUGUI>();

		switch (type)
		{
			case Message.MessageType.Simple:
				message.TextPrefab.color = _simpleColor;
				break;
			case Message.MessageType.Alert:
				message.TextPrefab.color = _alertColor;
				break;
			case Message.MessageType.Warning:
				message.TextPrefab.color = _warningColor;
				break;
		}

		message.TextPrefab.text = msg;

		_messages.Add(message);
	}
}

[System.Serializable]
public class Message
{
	public string Text;
	public TextMeshProUGUI TextPrefab;
	public MessageType messageType;

	public enum MessageType
	{
		Simple,
		Warning,
		Alert
	}
}