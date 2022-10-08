using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
