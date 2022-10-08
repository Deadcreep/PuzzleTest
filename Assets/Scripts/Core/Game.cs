using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField] private WinCondition _condition;
	[SerializeField] private Table _table;
	[SerializeField] private List<Pocket> _freePockets;
	[SerializeField] private List<Block> _freeBlocks;
	[SerializeField] private BlockSettings _blockSettings;

	public event Action<bool> GameEnded;

	private void Awake()
	{
		foreach (var block in _freeBlocks)
		{
			block.DragEnded += OnBlockDropped;
		}
	}

	private void OnDestroy()
	{
		foreach (var block in _freeBlocks)
		{
			block.DragEnded -= OnBlockDropped;
		}
	}

	private void OnBlockDropped(Vector2 position, Block block)
	{
		for (int i = 0; i < _freePockets.Count; i++)
		{
			if (_freePockets[i].IsBlockOverlap(position))
			{
				if (_freePockets[i].TryInsertBlock(block))
				{
					return;
				}
			}
		}

		if (_table.TryGetPocket(position, out Pocket pocket))
		{
			if (pocket.TryInsertBlock(block))
			{
				var winStatus = _condition.GetStatus();
				switch (winStatus)
				{
					case WinStatus.Win:
						block.Remove();
						foreach (var item in _table.StartBlocks)
						{
							item.Remove();
						}
						StartCoroutine(WinCoroutine());
						return;

					case WinStatus.Fail:
						GameEnded?.Invoke(false);
						return;

					default:
						return;
				}
			}
		}
		block.Return();
	}

	private IEnumerator WinCoroutine()
	{
		yield return new WaitForSeconds(_blockSettings.DestroyDuration);
		GameEnded?.Invoke(true);
	}
}