using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerPresenter : PresenterBehaviour<SoundManager>
{
	[SerializeField] private TextMeshProUGUI _valueField;
	[SerializeField] private Slider _slider;

	protected override void OnInject()
	{
		_slider.value = Model.Volume;
		_valueField.text = $"Звук: {(int)(Model.Volume * 100)}%";
		_slider.onValueChanged.AddListener(ChangeVolume);
		Model.VolumeChanged += OnVolumeChanged;
	}

	protected override void OnRemove()
	{
		Model.VolumeChanged -= OnVolumeChanged;
		_slider.onValueChanged.RemoveListener(ChangeVolume);
	}

	private void ChangeVolume(float value)
	{
		Model.SetVolume(value);
	}

	private void OnVolumeChanged()
	{
		_slider.SetValueWithoutNotify(Model.Volume);
		_valueField.text = $"Звук: {(int)(Model.Volume * 100)}%";
	}
}