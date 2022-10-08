using UnityEngine;

public enum WinStatus
{
	Win,
	Fail,
	InProgress
}

public abstract class WinCondition : MonoBehaviour
{
	public abstract WinStatus GetStatus();
}