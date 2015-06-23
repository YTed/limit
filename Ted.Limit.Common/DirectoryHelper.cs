using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ted.Limit.Common
{
    public class DirectoryHelper
    {
        public static string GetFileName(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new ArgumentNullException();
            }
            int slashIdx = fullPath.LastIndexOf("\\");
            if (slashIdx == -1)
            {
                return fullPath;
            }
            return fullPath.Substring(slashIdx + 1);
        }

        /// <summary>
        /// 判断指定路径是否为相对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsRelativePath(string path)
        {
            return !path.Contains(":");
        }

        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="crrPath">当前路径</param>
        /// <param name="rltPath">crrPath的相对路径</param>
        /// <returns>如果</returns>
        public static string GetRelativePath(string crrPath, string rltPath)
        {
            if (IsRelativePath(rltPath))
            {
                int idx = 0;
                while (rltPath.StartsWith("..\\"))
                {
                    rltPath = rltPath.Substring(3);
                    idx = crrPath.LastIndexOf("\\");
                    if (idx >= 0)
                    {
                        crrPath = crrPath.Substring(0, idx);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("{0}的相对路径{1}不存在!", crrPath, rltPath));
                    }
                }
                return crrPath + "\\" + rltPath;
            }
            return rltPath;
        }

        private static string s_basePath = string.Empty;

        /// <summary>
        /// 获得应用程序的基路径,即\bin文件夹的上级路径
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationBasePath()
        {
            if (string.IsNullOrEmpty(s_basePath))
            {
                s_basePath = Directory.GetCurrentDirectory();
                s_basePath = s_basePath.Substring(0, s_basePath.LastIndexOf("\\"));
            }
            return s_basePath;
        }

        private static string s_cfgPath = string.Empty;

        /// <summary>
        /// 获得应用程序的配置文件夹
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationConfigPath()
        {
            if (string.IsNullOrEmpty(s_cfgPath))
            {
                s_cfgPath = GetApplicationBasePath() + "\\config";
            }
            return s_cfgPath;
        }

        /// <summary>
        /// 获得指定路径的上级路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParentPath(string path)
        {
            return path.Substring(0, path.LastIndexOf("\\"));
        }

        private static string[] s_files = {
            "log.cfg",
            "uitbr.xml",
            "uidck.xml",
            "ext.su",
            "uifct.cfg"
        };

        private static ConfiguraionFile[] s_fileTypes = {
            ConfiguraionFile.Log,
            ConfiguraionFile.ToolbarManager,
            ConfiguraionFile.DockableManager,
            ConfiguraionFile.Extension,
            ConfiguraionFile.UIFactory
        };

        /// <summary>
        /// 获得指定配置类型的配置文件路径
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetConfigFile(ConfiguraionFile type)
        {
            for (int i = 0; i < s_files.Length; i++)
            {
                if (type == s_fileTypes[i])
                {
                    return GetApplicationConfigPath() + "\\" + s_files[i];
                }
            }
            throw new ArgumentException();
        }
    }

    public enum ConfiguraionFile
    {
        Log,
        ToolbarManager,
        DockableManager,
        Extension,
        UIFactory
    }
}
