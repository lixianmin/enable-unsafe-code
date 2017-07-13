
/********************************************************************
created:    2017-07-04
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/
using UnityEditor;
using System;
using System.IO;

namespace EnableUnsafeCode
{
    internal class EnableUnsafeCodeProcessor
	{
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void _OnScriptsReloaded () 
        {
            _ProcessProjectFiles();
        }

        private static void _ProcessProjectFiles ()
        {
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
