using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Ted.Limit.ExtLoader
{
    partial class ShareUnit
    {
        #region IExtLoader 成员

        IExt IExtLoader.LoadFromFile(string ExtDefFilePath)
        {
            if (!File.Exists(ExtDefFilePath))
            {
                throw new ExtNotFoundException();
            }
            IExt ext = null;
            try
            {
                //.ted文件的旧路径
                string extOldDir = ExtDefFilePath.Substring(0, ExtDefFilePath.LastIndexOf("\\"));

                //.ted格式字符串
                string ExtDefText = File.ReadAllText(ExtDefFilePath);
                ext = new DefaultExt(ExtDefText, extOldDir);

                //.ted文件的新路径
                string key = ext.Key;

                if (string.IsNullOrEmpty(key))
                {
                    throw new ExtDefFormatException();
                }
                if (m_ExtPool.ContainsKey(key))
                {
                    throw new ExtExistException();
                }
            }
            catch (ExtLoadingException loadExp)
            {
                throw loadExp;
            }
            catch (Exception exp)
            {
                throw new ExtLoadingException("", exp);
            }
            return ext;
        }

        void IExtLoader.SaveToFile(string output, IExt ext)
        {
            if (!Directory.Exists(output))
            {
                Directory.CreateDirectory(output);
            }
            CopyAssembly(output, ext);
            CopyResource(output, ext);
            SaveDotTed(output, ext);
        }

        private void CopyResource(string output, IExt ext)
        {
            string oldResource = ((DefaultExt)ext).Resource;
            if (!string.IsNullOrEmpty(oldResource))
            {
                string newResource = output + oldResource.Substring(oldResource.LastIndexOf("\\"));
                CopyDirectory(oldResource, newResource);
            }
        }

        private void CopyDirectory(string oldDir, string newDir)
        {
            try
            {
                if (!Directory.Exists(newDir))
                {
                    Directory.CreateDirectory(newDir);
                }
                Queue<string> dirQueue = new Queue<string>();
                dirQueue.Enqueue(oldDir);
                int oldDirLength = oldDir.Length;
                while (dirQueue.Count > 0)
                {
                    string crrPath = dirQueue.Dequeue();
                    string tail = crrPath.Substring(oldDirLength);

                    string newFolder = newDir + tail;
                    if (!Directory.Exists(newFolder))
                    {
                        Directory.CreateDirectory(newFolder);
                    }
                    string[] subFiles = Directory.GetFiles(crrPath);
                    for (int i = 0; i < subFiles.Length; i++)
                    {
                        string newFile = newDir + tail + subFiles[i].Substring(subFiles[i].LastIndexOf("\\"));
                        File.Copy(subFiles[i], newFile, true);
                    }

                    string[] subPath = Directory.GetDirectories(crrPath);
                    for (int i = 0; i < subPath.Length; i++)
                    {
                        dirQueue.Enqueue(subPath[i]);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new ExtLoadingException("", exp);
            }
        }

        private void SaveDotTed(string output, IExt ext)
        {
            DefaultExt de = ext as DefaultExt;
            string version = "";
            string assembly = "";
            string path = "";
            string make = "";
            string key = "";
            string resource = "";
            string TedFileName = output + "\\" + ext.Key + ".ted";
            IExtPart[] parts = ext.Parts;
            foreach (IExtPart part in parts)
            {
                switch (part.PartType)
                {
                    case ExtPartType.Version:
                        version += MakeFileLine("version", part);
                        break;
                    case ExtPartType.Assembly:
                        assembly += MakeFileLine("assembly", part);
                        break;
                    case ExtPartType.Path:
                        path += MakeFileLine("path", part);
                        break;
                    case ExtPartType.Make:
                        make += MakeFileLine("make", part);
                        break;
                    case ExtPartType.Key:
                        key += MakeFileLine("key", part);
                        break;
                    case ExtPartType.Resources:
                        resource += MakeFileLine("resource", part);
                        break;
                }
            }
            string FileContent = version + assembly + path + make + key;
            try
            {
                File.WriteAllText(TedFileName, FileContent);
            }
            catch (IOException ioExp)
            {
                throw new ExtLoadingException("", ioExp);
            }
        }

        private void CopyAssembly(string output, IExt ext)
        {
            if (string.IsNullOrEmpty(output) || ext == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                string oldAsmSource = ((DefaultExt)ext).AssemblySource;
                string newAsmSource = output + "\\" + oldAsmSource.Substring(oldAsmSource.LastIndexOf("\\") + 1);
                File.Copy(oldAsmSource, newAsmSource, true);
                ((DefaultExt)ext).AssemblySource = newAsmSource;
            }
            catch (IOException ioExp)
            {
                throw new ExtLoadingException("", ioExp);
            }
        }

        private string MakeFileLine(string group , IExtPart ep)
        {
            StringBuilder build = new StringBuilder();
            build.Append("[" + group + "]" + CRLF);
            int lmt = ep.ValueCount;
            for (int i = 0; i < lmt; i++)
            {
                build.Append(ep.GetValue(0) + CRLF);
            }
            return build.ToString();
        }

        #endregion

        /**
         * this version, comment is not supported.
         */
        private class DefaultExt : IExt
        {
            private List<IExtPart> m_parts;

            private const char c_PartStart = '[', c_PartEnd = ']';

            public DefaultExt(string ExtDefText, string ExtDefPath)
            {
                if (string.IsNullOrEmpty(ExtDefText) || string.IsNullOrEmpty(ExtDefPath))
                {
                    throw new ExtDefFormatException();
                }
                m_parts = new List<IExtPart>();
                m_path = ExtDefPath;
                CreateParts(ExtDefText);
            }

            private void CreateParts(string ExtDefText)
            {
                StringBuilder strBuilder = new StringBuilder();
                ExtDefText = ExtDefText.Trim();
                char[] tedTex = ExtDefText.ToCharArray();
                int start = ExtDefText.IndexOf(c_PartStart);
                for (int i = start + 1; i <= tedTex.Length; i++)
                {
                    if (i == tedTex.Length || tedTex[i] == c_PartStart)
                    {
                        strBuilder.Remove(0, strBuilder.Length);
                        strBuilder.Append(tedTex, start, i - start);
                        IExtPart ep = DefaultExtPart.Create(strBuilder.ToString());
                        switch (ep.PartType)
                        {
                            case ExtPartType.Assembly:
                                //Assembly是.dll文件相对于.ted文件的路径
                                m_assembly = SetRelativeSource(ep.GetValue(0));
                                break;
                            case ExtPartType.Key:
                                m_key = ep.GetValue(0);
                                break;
                            case ExtPartType.Make:
                                m_makeClassName = ep.GetValue(0);
                                break;
                            case ExtPartType.Resources:
                                m_resource = SetRelativeSource(ep.GetValue(0));
                                break;
                        }
                        m_parts.Add(ep);
                        start = i;
                    }
                }
            }

            private string m_assembly;

            /**
             * m_path先定义.
             */
            ///<summary>
            /// 根据相对路径和部分绝对路径决定全部绝对路径.
            ///</summary>
            ///<param name="originSrc">相对路径</param>
            private string SetRelativeSource(string relativePath)
            {
                string parentPath = @"..\";
                string path = m_path;
                while (relativePath.StartsWith(parentPath))
                {
                    relativePath = relativePath.Substring(3);
                    path = path.Substring(0, path.LastIndexOf('\\'));
                }
                return path + "\\" + relativePath;
            }

            public string AssemblySource
            {
                get
                {
                    return m_assembly;
                }
                set
                {
                    m_assembly = value;
                    foreach (IExtPart part in m_parts)
                    {
                        if (part.PartType == ExtPartType.Path)
                        {
                            DefaultExtPart dep = part as DefaultExtPart;
                            dep.SetValue(m_assembly, 0);
                        }
                    }
                }
            }

            private string m_path;

            public string Path
            {
                get
                {
                    return m_path;
                }
                set
                {
                    m_path = value;
                }
            }

            private string m_resource;

            public string Resource
            {
                get { return m_resource; }
                set { m_resource = value; }
            }

            #region IExt 成员

            private Ted.Limit.Core.IMake m_make;

            private string m_makeClassName;

            Ted.Limit.Core.IMake IExt.Make
            {
                get
                {
                    if (m_make == null)
                    {
                        Assembly asm = Assembly.LoadFile(AssemblySource);
                        m_make = asm.CreateInstance(m_makeClassName) as Ted.Limit.Core.IMake;
                    }
                    return m_make;
                }
            }

            private string m_key = string.Empty;

            string IExt.Key
            {
                get
                {
                    return m_key;
                }
            }

            IExtPart[] IExt.Parts
            {
                get
                {
                    return m_parts.ToArray();
                }
            }

            #endregion
        }

        private class DefaultExtPart : IExtPart
        {
            #region const properties

            private static string[] c_PartStr = {
                "version",
                "assembly",
                "path",
                "make",
                "key",
                "resources",
                "toolbar",
                "tool"
            };
            private static ExtPartType[] c_PartTypes = {
                ExtPartType.Version,
                ExtPartType.Assembly,
                ExtPartType.Path,
                ExtPartType.Make,
                ExtPartType.Key,
                ExtPartType.Resources,
                ExtPartType.Toolbar,
                ExtPartType.Tool
            };

            private const string c_PartStart = "[", c_PartEnd = "]";

            private const string c_EqualSign = "=";

            #endregion

            private DefaultExtPart()
            {

            }

            private static string ValidateCode(string code)
            {
                code = code.Trim();
                //空参数
                if (code == null)
                {
                    throw new ArgumentNullException();
                }
                //多于1个'['或者']'字符
                int startIdx = code.IndexOf(c_PartStart);
                int endIdx = code.LastIndexOf(c_PartStart);
                if (startIdx != endIdx)
                {
                    throw new ArgumentException();
                }
                startIdx = code.IndexOf(c_PartEnd);
                //']'在'['前
                if (startIdx < endIdx)
                {
                    throw new ArgumentException();
                }
                endIdx = code.LastIndexOf(c_PartEnd);
                if (startIdx != endIdx || endIdx == code.Length - 1)
                {
                    throw new ArgumentException();
                }
                return code;
            }

            /// <summary>
            /// code必须是形如
            /// [part]
            /// value
            /// 的形式,否则抛出异常
            /// </summary>
            /// <param name="code">用于创建IExtPart的配置字符串</param>
            /// <returns>IExtPart</returns>
            public static IExtPart Create(string code)
            {
                code = ValidateCode(code);
                DefaultExtPart ExtPart = new DefaultExtPart();

                string[] partValue = code.Split(new char[] { ']' });
                string part = partValue[0].Trim();
                part = part.Substring(1, part.Length - 1);
                for (int i = 0; i < c_PartStr.Length; i++)
                {
                    if (part.Equals(c_PartStr[i], StringComparison.InvariantCultureIgnoreCase))
                    {
                        ExtPart.m_type = c_PartTypes[i];
                        break;
                    }
                }
                if (ExtPart.m_type == ExtPartType.None)
                {
                    throw new ExtDefFormatException();
                }

                partValue[1] = partValue[1].Replace(CRLF, LF);
                string[] KeyValues = partValue[1].Split(LF.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (ExtPart.m_type == ExtPartType.Tool || ExtPart.m_type == ExtPartType.Toolbar)
                {
                    List<string> keys = new List<string>(), values = new List<string>();
                    for (int i = 0; i < KeyValues.Length; i++)
                    {
                        string[] pair = KeyValues[i].Split(c_EqualSign.ToCharArray());
                        keys.Add(pair[0]);
                        values.Add(pair[1]);
                    }
                    ExtPart.m_key = keys.ToArray();
                    ExtPart.m_value = values.ToArray();
                }
                else
                {
                    ExtPart.m_value = KeyValues;
                }
                return ExtPart;
            }

            public void SetValue(string newValue, int idx)
            {
                m_value[idx] = newValue;
            }

            #region IExtPart 成员

            private ExtPartType m_type = ExtPartType.None;

            private string[] m_value;

            private string[] m_key;

            ExtPartType IExtPart.PartType
            {
                get
                {
                    return m_type;
                }
            }

            string IExtPart.GetValue(int idx)
            {
                return m_value[idx];
            }

            string IExtPart.GetKey(int idx)
            {
                if (m_key != null)
                {
                    return m_key[idx];
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            int IExtPart.ValueCount
            {
                get
                {
                    return m_value.Length;
                }
            }

            #endregion
        }
    }
}
