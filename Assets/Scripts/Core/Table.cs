using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{
	public IReadOnlyCollection<Block> StartBlocks => _installedBlocks;
	private List<Block> _installedBlocks;
	[SerializeField] private List<Pocket> _pockets;

	private void Awake()
	{
		_installedBlocks = GetComponentsInChildren<Block>().ToList();
	}

	public bool TryGetPocket(Vector2 position, out Pocket pocket)
	{
		for (int i = 0; i < _pockets.Count; i++)
		{
			if (_pockets[i].IsBlockOverlap(position))
			{
				pocket = _pockets[i];
				return true;
			}
		}
		pocket = null;
		return false;
	}
}