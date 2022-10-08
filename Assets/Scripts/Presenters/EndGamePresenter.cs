using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGamePresenter : PresenterBehaviour<Game>
{
	[SerializeField] private GameObject _panel;
	[SerializeField] private GameObject _blockPanel;
	[SerializeField] private TextMeshProUGUI _messageField;
	[SerializeField] private Button _retryButton;

	[Space]
	[SerializeField] private string _successMessage = "Победа";

	[SerializeField] private string _failMessage = "Ошибка";

	protected override void OnInject()
	{
		Model.GameEnded += OnGameEnded;
		_retryButton.onClick.AddListener(RestartLevel);
	}

	protected override void OnRemove()
	{
		_retryButton.onClick.RemoveListener(RestartLevel);
		Model.GameEnded -= OnGameEnded;
	}

	private void RestartLevel()
	{
		SceneManager.LoadScene(0);
	}

	private void OnGameEnded(bool result)
	{
		if (result)
		{
			_messageField.text = _successMessage;
		}
		else
		{
			_messageField.text = _failMessage;
		}
		_panel.SetActive(true);
		_blockPanel.SetActive(true);
	}
}