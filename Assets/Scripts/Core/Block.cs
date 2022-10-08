using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Block : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] private BlockSettings _settings;

	private RectTransform _rect;
	private Image _image;
	private Vector2 _offset;
	private Vector3 _lastPosition;

	public event Action Taked;

	public event Action<Vector2, Block> DragEnded;

	private void Awake()
	{
		_rect = GetComponent<RectTransform>();
		_image = GetComponent<Image>();
		_lastPosition = _rect.position;
	}

	public void Disable()
	{
		_image.raycastTarget = false;
	}

	public void SetPosition(Vector3 position)
	{
		_rect.position = position;
		_lastPosition = position;
	}

	public void Remove()
	{
		StartCoroutine(DestroyCoroutine());
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_offset = (Vector2)_rect.position - eventData.position;
		Taked?.Invoke();
	}

	public void OnDrag(PointerEventData eventData)
	{
		_rect.position = eventData.position + _offset;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		DragEnded?.Invoke(_rect.position, this);
	}

	public void Return()
	{
		StartCoroutine(ReturnCoroutine());
	}

	private IEnumerator ReturnCoroutine()
	{
		_image.raycastTarget = false;
		var direction = (_lastPosition - _rect.position).normalized;
		var step = direction * _settings.Speed * Time.deltaTime;

		while (Vector3.Distance(_rect.position, _lastPosition) > step.magnitude)
		{
			_rect.position += step;
			yield return null;
		}
		_rect.localPosition = Vector2.zero;
		_image.raycastTarget = true;
	}

	private IEnumerator DestroyCoroutine()
	{
		Disable();
		float time = 0;
		float normalizedTime;
		while (time < _settings.DestroyDuration)
		{
			time += Time.deltaTime;
			normalizedTime = time / _settings.DestroyDuration;
			_rect.localScale = Vector2.one * _settings.DestroyCurve.Evaluate(normalizedTime);
			var color = _image.color;
			color.a = _rect.localScale.x;
			_image.color = color;
			yield return null;
		}
		gameObject.SetActive(false);
	}
}