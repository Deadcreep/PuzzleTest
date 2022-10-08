using UnityEngine;

public class Installer : MonoBehaviour
{
	[SerializeField] private Game _game;
	[SerializeField] private SoundManager _soundManager;
	[SerializeField] private EndGamePresenter _endGamePresenter;
	[SerializeField] private SoundManagerPresenter _soundManagerPresenter;


	private void Awake()
	{
		_endGamePresenter.Inject(_game);
		_soundManagerPresenter.Inject(_soundManager);
	}
}