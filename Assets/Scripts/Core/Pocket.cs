using UnityEngine;

public class Pocket : MonoBehaviour
{
	public bool IsFree => _insertedBlock == null;
	[SerializeField] private bool _isActive;
	private Block _insertedBlock;
	private RectTransform _rect;
	private Bounds _bounds;

	private void Awake()
	{
		_rect = GetComponent<RectTransform>();
		_insertedBlock = GetComponentInChildren<Block>();

		_bounds = new Bounds()
		{
			center = transform.position,
			size = _rect.rect.size
		};
		if (_insertedBlock)
		{
			if (_isActive)
				_insertedBlock.Taked += OnBlockTaked;
			else
				_insertedBlock.Disable();
		}
	}

	public bool IsBlockOverlap(Vector2 position)
	{
		return _bounds.Contains(position);
	}

	public bool TryInsertBlock(Block block)
	{
		if (IsFree)
		{
			block.SetPosition(_rect.position);
			_insertedBlock = block;
			_insertedBlock.transform.SetParent(transform);
			block.Taked += OnBlockTaked;
			return true;
		}
		else
		{
			return false;
		}
	}

	public void RemoveBlock()
	{
		if (_insertedBlock)
		{
			_insertedBlock.Taked -= OnBlockTaked;
			_insertedBlock = null;
		}
	}

	private void OnBlockTaked()
	{
		_insertedBlock.Taked -= OnBlockTaked;
		_insertedBlock.transform.SetParent(transform.root);
		_insertedBlock = null;
	}
}