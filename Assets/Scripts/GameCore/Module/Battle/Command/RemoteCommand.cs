using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using FixedMathSharp;
using GC.Utils.Define;
using UnityEngine;

namespace GC.Module.Command
{
	/// <summary>
	/// 서버로부터 전달받거나, 서버로 보내야하는 명령
	/// </summary>
	public class RemoteCommand : PoolingObject<RemoteCommand>
	{
		public Battle.RemoteCommandType CommandType { get; set; }
		public List<int> IntParams;
		public List<Fixed64> FloatParams;

		public RemoteCommand()
		{
			IntParams = new List<int>();
			FloatParams = new List<Fixed64>();
		}
	}
}