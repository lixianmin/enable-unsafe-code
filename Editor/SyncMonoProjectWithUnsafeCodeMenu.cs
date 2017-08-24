
/********************************************************************
created:    2016-01-23
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
#if !UNITY_2017_1_OR_NEWER

using UnityEngine;
using UnityEditor;
using System.IO;

namespace EnableUnsafeCode
{
	static class SyncMonoProjectWithUnsafeCodeMenu
	{
		[MenuItem("Assets/Open C# Project (enable unsafe code)", false, 200)]
		public static void Execute ()
		{
            ProjectTools.SyncMonoProject();

			foreach (var filepath in Directory.GetFiles("."))
			{
				if (filepath.EndsWith(".csproj"))
				{
                    ProjectTools.EnableProjectUnsafeCode(filepath);
				}
			}
		}
	}
}
#endif