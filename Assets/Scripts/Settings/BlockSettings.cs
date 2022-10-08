using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockSettings", menuName = "ScriptableObjects/BlockSettings", order = 0)]
public class BlockSettings : ScriptableObject
{
	public float Speed;
	public float DestroyDuration;
	public AnimationCurve DestroyCurve;
}
