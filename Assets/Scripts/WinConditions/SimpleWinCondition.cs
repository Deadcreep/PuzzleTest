using UnityEngine;

public class SimpleWinCondition : WinCondition
{
	[SerializeField] private Pocket _targetPocket;
	[SerializeField] private Block _block;

	public override WinStatus GetStatus()
	{
		if (_block.transform.position == _targetPocket.transform.position)
		{
			return WinStatus.Win;
		}
		else
		{
			return WinStatus.Fail;
		}
	}
}