
/********************************************************************
created:    2017-07-04
author:     lixianmin

Copyright (C) - All Rights Reserved
*********************************************************************/

using UnityEngine;
using UnityEditor;
using System;
using System.Xml;

namespace EnableUnsafeCode
{
    partial class ProjectTools
    {
        public static void SyncMonoProject ()
        {
            EditorApplication.ExecuteMenuItem("Assets/Open C# Project");
        }

        public static void EnableProjectUnsafeCode (string csproj_path)
        {
            var doc = new XmlDocument();
            doc.Load(csproj_path);

            var nodes = doc.ChildNodes;
            var nodesCount = nodes.Count;
            var isChanged = false;

            for (int i= 0; i< nodesCount; ++i)
            {
                var node = nodes[i];
                if (node.Name == "Project")
                {
                    var isNodeChanged = _ProcessProjectNode(doc, node);
                    if (isNodeChanged)
                    {
                        isChanged = true;
                    }
                    break;
                }
            }

            if (isChanged)
            {
                doc.Save(csproj_path);
            }
        }

        private static bool _ProcessProjectNode (XmlDocument doc, XmlNode projectNode)
        {
            var nodes = projectNode.ChildNodes;
            var nodesCount = nodes.Count;
            var isChanged = false;

            for (int i= 0; i< nodesCount; ++i)
            {
                var node = nodes[i];
                if (node.Name == "PropertyGroup" 
                    && _IsTargetPropertyGroupNode(node)
                    && !_HasAllowUnsafeBlocksChild(node))
                {
                    var unsafeNode = doc.CreateElement("AllowUnsafeBlocks", doc.DocumentElement.NamespaceURI);
                    unsafeNode.InnerText = "true";
                    node.AppendChild(unsafeNode);

                    isChanged = true;
                }
            }

            return isChanged;
        }

        private static bool _IsTargetPropertyGroupNode (XmlNode node)
        {
            var attributes = node.Attributes;
            var attributesCount = attributes.Count;

            for (int i= 0; i < attributesCount; ++i)
            {
                var attribute = attributes[i];
                if (attribute.InnerText.Contains("'$(Configuration)|$(Platform)'"))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool _HasAllowUnsafeBlocksChild (XmlNode propertyGroupNode)
        {
            var nodes = propertyGroupNode.ChildNodes;
            var nodesCount = nodes.Count;

            for (int i= 0; i< nodesCount; ++i)
            {
                var node = nodes[i];
                if (node.Name == "AllowUnsafeBlocks")
                {
                    return true;
                }
            }

            return false;
        }
    }
}