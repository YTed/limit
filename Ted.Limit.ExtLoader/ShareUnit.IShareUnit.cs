using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.ExtLoader
{
    partial class ShareUnit : IShareUnit, IExtLoader
    {
        #region properties for IShareUnit

        private const string CRLF = "\r\n", LF = "\n";

        private IExtLoader m_ExtDefLoader;

        private Dictionary<string, ShareUnitLine> m_ExtPool;

        private List<ShareUnitLine> m_suLineLst;

        private string m_appPath;

        #endregion

        public ShareUnit()
        {
            //最后m_appPath => /
            string startPath = System.IO.Directory.GetCurrentDirectory();
            m_appPath = startPath.Substring(0, startPath.LastIndexOf('\\'));
            m_ExtDefLoader = this;
            m_suLineLst = new List<ShareUnitLine>();
            m_ExtPool = new Dictionary<string, ShareUnitLine>();
        }

        #region IShareUnit 成员

        /// <summary>
        /// 返回\ext文件夹的绝对路径
        /// </summary>
        /// <returns></returns>
        private string GetExtPath()
        {
            return m_appPath + @"\ext";
        }

        /// <summary>
        /// 构造加载模块出现异常时的错误信息。
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        private string LogMsg(Exception exp, string moduleName)
        {
            return DateTime.Now + CRLF +
                "Exceptions occurs while loading module [" + moduleName + "]" + CRLF +
                "Exception message : " + CRLF +
                exp.Message + CRLF +
                "Exception stack : " + CRLF +
                exp.StackTrace + CRLF;
        }

        /**
         * .su文件的格式是：
         * *****************************************
         * #line comment here
         * [group]
         * path\to\.ted\file
         * *****************************************
         * load方法解析一个.su格式字符串，把字符串分割成行。
         * 如果一行里含有字符"["，则该行为组。
         * 每行以"#"分割，'#'前面为code，'#'后面为comment。
         * 对非组的行，把其code传递给IExtLoader.LoadFile方法，
         * 获得一个IExt对象，并把它存储在对应的ShareUnitLine
         * 里面。含有IExt对象的ShareUnitLine以IExt.Key为键散列。
         * 备用。对加载异常的模块ShareUnit不会对它进行记录，
         * 以为下次加载清除存在的异常。
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suText"></param>
        /// <returns></returns>
        string IShareUnit.Load(string suText)
        {
            if (string.IsNullOrEmpty(suText))
            {
                throw new ArgumentNullException();
            }
            string loadMsg = "";
            try
            {
                suText = suText.Replace(CRLF, LF);
                string[] suLines = suText.Split(LF.ToCharArray());
                for (int i = 0; i < suLines.Length; i++)
                {
                    if (suLines[i] != null)
                    {
                        ShareUnitLine tmpSuLine = ShareUnitLine.Create(suLines[i]);
                        if (!tmpSuLine.IsGroup && tmpSuLine.HasCode)
                        {
                            try
                            {
                                tmpSuLine.ReflectExt = m_ExtDefLoader.LoadFromFile(tmpSuLine.Code);
                                m_ExtPool.Add(tmpSuLine.ReflectExt.Key, tmpSuLine);
                            }
                            catch (Exception exp)
                            {
                                loadMsg += LogMsg(exp,tmpSuLine.Code);
                                continue;
                            }
                        }
                        m_suLineLst.Add(tmpSuLine);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new ExtError("", exp);
            }
            if (string.IsNullOrEmpty(loadMsg))
            {
                loadMsg = "Modules load successfully.";
            }
            return loadMsg;
        }

        Ted.Limit.Core.IMake IShareUnit.Add(string ExtDefPath, out string key)
        {
            IExt ext = null;
            ShareUnitLine suLine = null;
            Ted.Limit.Core.IMake make = null;
            try
            {
                ext = m_ExtDefLoader.LoadFromFile(ExtDefPath);
                make = ext.Make;
                if (make == null)
                {
                    throw new ExtLoadingException();
                }
                if (m_ExtPool.ContainsKey(ext.Key))
                {
                    throw new ExtExistException();
                }
                suLine = new ShareUnitLine();
                suLine.ReflectExt = ext;
                string newExtPath = GetExtPath() + @"\" + ext.Key;
                if (!System.IO.Directory.Exists(newExtPath))
                {
                    System.IO.Directory.CreateDirectory(newExtPath);
                }
                m_ExtDefLoader.SaveToFile(newExtPath, ext);
                suLine.Code = newExtPath + "\\" + ext.Key + ".ted";
                m_ExtPool.Add(ext.Key, suLine);
                m_suLineLst.Add(suLine);
            }
            catch (ExtLoadingException elExp)
            {
                throw elExp;
            }
            catch (Exception exp)
            {
                throw new ExtLoadingException("", exp);
            }
            key = ext.Key;
            return make;
        }

        void IShareUnit.Remove(string key)
        {
            if (m_ExtPool.ContainsKey(key))
            {
                ShareUnitLine suLine = m_ExtPool[key];
                m_ExtPool.Remove(key);
                m_suLineLst.Remove(suLine);
            }
        }

        string IShareUnit.TextFormat
        {
            get 
            {
                string text = "";
                for (int i = 0; i < m_suLineLst.Count; i++)
                {
                    ShareUnitLine suLine = m_suLineLst[i];
                    if (!suLine.IsBlankLine)
                    {
                        if (suLine.HasCode)
                        {
                            text += suLine.Code;
                        }
                        if (suLine.HasComment)
                        {
                            text += suLine.Comment;
                        }
                    }
                    text += CRLF;
                }
                return text;
            }
        }

        bool IShareUnit.Exist(string key)
        {
            return m_ExtPool.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        string[] IShareUnit.All
        {
            get
            {
                Dictionary<string, ShareUnitLine>.KeyCollection keyColl = m_ExtPool.Keys;
                string[] keyArr = new string[keyColl.Count];
                keyColl.CopyTo(keyArr, 0);
                return keyArr;
            }
        }

        #endregion

        class ShareUnitLine
        {
            private const char COMMENT_SIGN = '#';

            public ShareUnitLine()
            {

            }

            public static ShareUnitLine Create(string line)
            {
                ShareUnitLine suLine = new ShareUnitLine();
                line = line.Trim();
                if (line.Length == 0)
                {
                    return suLine;
                }
                if (line.IndexOf('[') >= 0)
                {
                    suLine.m_isGroup = true;
                }
                int cmtIdx = line.IndexOf('#');
                if (cmtIdx >= 0)
                {
                    string code = line.Substring(0, cmtIdx).Trim();
                    if (!string.IsNullOrEmpty(code))
                    {
                        suLine.Code = code;
                    }
                    string comment = line.Substring(cmtIdx).Trim();
                    suLine.Comment = comment;
                    return suLine;
                }
                else
                {
                    suLine.Code = line;
                    return suLine;
                }
            }

            public override string ToString()
            {
                string text = "";
                if (!string.IsNullOrEmpty(Code))
                {
                    text += Code;
                }
                if (!string.IsNullOrEmpty(Comment))
                {
                    text += Comment;
                }
                return text;
            }

            #region properties

            private string m_comment;

            public string Comment
            {
                get { return m_comment; }
                set { m_comment = value; }
            }

            private string m_code;

            public string Code
            {
                get { return m_code; }
                set { m_code = value; }
            }

            private bool m_isGroup = false;

            public bool IsGroup
            {
                get
                {
                    return m_isGroup;
                }
            }

            private static bool IsNullEmptyOrNothing(string str)
            {
                return str == "" || string.IsNullOrEmpty(str);
            }

            public bool IsBlankLine
            {
                get
                {
                    return IsNullEmptyOrNothing(Code) & IsNullEmptyOrNothing(Comment);
                }
            }

            public bool HasCode
            {
                get
                {
                    return !IsNullEmptyOrNothing(Code);
                }
            }

            public bool HasComment
            {
                get
                {
                    return !IsNullEmptyOrNothing(Comment);
                }
            }

            private IExt m_reflectExt;

            public IExt ReflectExt
            {
                get
                {
                    return m_reflectExt;
                }
                set
                {
                    m_reflectExt = value;
                }
            }

            #endregion
        }



        #region IShareUnit 成员


        Ted.Limit.Core.IMake IShareUnit.LoadModule(string key)
        {
            if (m_ExtPool.ContainsKey(key))
            {
                IExt ext = m_ExtPool[key].ReflectExt;
                return ext.Make;
            }
            throw new ExtNotFoundException();
        }

        #endregion
    }
}
