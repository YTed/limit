using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Ted.Limit.Editor
{
    public delegate void EditOperation(object[] parameter);

    /**
     * 跨图层，捕捉，移动，节点编辑
     * 
     */
    public class EditCommon
    {
        #region private properties

        private Dictionary<string, EditProperties> m_wkspEdtLst;

        private List<string> m_wkspEdtOrderLst;

        private bool m_isEditing;

        #endregion

        #region constructor

        public EditCommon()
        {
            m_wkspEdtLst = new Dictionary<string, EditProperties>();
            m_wkspEdtOrderLst = new List<string>();
            m_isEditing = false;
        }

        #endregion

        #region general editing

        /// <summary>
        /// 保证m_isEditing为假时，两个容器都被清除
        /// </summary>
        private bool IsEditing
        {
            get
            {
                return m_isEditing;
            }
            set
            {
                m_isEditing = value;
                if (!m_isEditing)
                {
                    m_wkspEdtLst.Clear();
                    m_wkspEdtOrderLst.Clear();
                }
            }
        }

        /// <summary>
        /// 开始编辑一个地图。
        /// </summary>
        /// <param name="map">要编辑的地图</param>
        /// <param name="store">定义获取编辑工作空间的方法的对象</param>
        public void StartEdit(IMap map, IWorkspaceStore store)
        {
            bool initSucc = true;
            if (IsEditing)
            {
                throw new FeatureEditException("");
            }
            try
            {
                int lyrCount = map.LayerCount;
                for (int i = 0; i < lyrCount; i++)
                {
                    IFeatureLayer pFeatLyr = map.get_Layer(i) as IFeatureLayer;
                    if (pFeatLyr != null)
                    {
                        IWorkspaceEdit pWkspEdt = store.GetWorkspaceEdit(pFeatLyr);
                        pWkspEdt.StartEditing(true);
                        pWkspEdt.EnableUndoRedo();

                        EditProperties ep = new EditProperties();
                        ep.DisplayName = pFeatLyr.Name;
                        ep.WorkspaceEdit = pWkspEdt;

                        string aliasName = pFeatLyr.FeatureClass.AliasName;

                        m_wkspEdtLst.Add(aliasName, ep);
                        m_wkspEdtOrderLst.Add(aliasName);
                    }
                }
            }
            catch (Exception exp)
            {
                initSucc = false;
                throw new FeatureEditException("",exp);
            }
            finally
            {
                if (initSucc)
                {
                    IsEditing = true;
                }
                else
                {
                    IsEditing = false;
                }
            }
        }

        /// <summary>
        /// 停止编辑
        /// </summary>
        /// <param name="save">是否保存所做过的编辑</param>
        public void StopEdit(bool save)
        {
            if (IsEditing)
            {
                Dictionary<string, EditProperties>.Enumerator enumerator = m_wkspEdtLst.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<string, EditProperties> pair = enumerator.Current;
                        IWorkspaceEdit pWkspEdt = pair.Value.WorkspaceEdit;
                        pWkspEdt.StopEditing(save);
                    }
                }
                //如果出现异常，撤销所有编辑
                catch (Exception exp)
                {
                    while (enumerator.MoveNext())
                    {
                        try
                        {
                            KeyValuePair<string, EditProperties> pair = enumerator.Current;
                            IWorkspaceEdit pWkspEdt = pair.Value.WorkspaceEdit;
                            pWkspEdt.StopEditing(false);
                        }
                        catch (Exception innerExp)
                        {
                            //ignore these exceptions
                        }
                    }
                    throw new FeatureEditException("", exp);
                }
                finally
                {
                    IsEditing = false;
                }
            }
        }

        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="FeatClsAliasName">要保存的要素类别名</param>
        public void SaveEdit(string FeatClsAliasName)
        {
            if (IsEditing)
            {
                EditProperties ep = null;
                try
                {
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        if (ep == null)
                        {
                            throw new FeatureEditException();
                        }
                        IWorkspaceEdit pWkspEdt = ep.WorkspaceEdit;
                        pWkspEdt.StopEditing(true);
                        pWkspEdt.StartEditing(true);
                        pWkspEdt.EnableUndoRedo();
                    }
                    else
                    {
                        throw new FeatureEditException();
                    }
                }
                catch (FeatureEditException feExp)
                {
                    throw feExp;
                }
                catch (Exception exp)
                {
                    throw new FeatureEditException("", exp);
                }
            }
        }

        /// <summary>
        /// 保存所做的全部编辑操作
        /// </summary>
        public void SaveEdit()
        {
            if (IsEditing)
            {
                Dictionary<string, EditProperties>.Enumerator enumerator = m_wkspEdtLst.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<string, EditProperties> pair = enumerator.Current;
                        EditProperties ep = pair.Value;
                        if (ep != null && ep.WorkspaceEdit != null)
                        {
                            ep.WorkspaceEdit.StopEditing(true);
                            ep.WorkspaceEdit.StartEditing(true);
                            ep.WorkspaceEdit.EnableUndoRedo();
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new FeatureEditException("", exp);
                }
            }
        }

        /// <summary>
        /// 判断是否存在可保存的编辑
        /// </summary>
        /// <param name="FeatClsAliasName">要查询的要素类名</param>
        /// <returns>存在可保存的编辑则返回true</returns>
        public bool HasEdits(string FeatClsAliasName)
        {
            if (IsEditing)
            {
                EditProperties ep = null;
                try
                {
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        if (ep == null || ep.WorkspaceEdit == null)
                        {
                            throw new FeatureEditException();
                        }
                        if (ep.Editable)
                        {
                            bool hasEdt = false;
                            ep.WorkspaceEdit.HasEdits(ref hasEdt);
                            return hasEdt;
                        }
                    }
                }
                catch (FeatureEditException feExp)
                {
                    throw feExp;
                }
                catch (Exception exp)
                {
                    throw new FeatureEditException("", exp);
                }
            }
            throw new FeatureEditException();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="FeatClsAliasName">要进行编辑的要素类别名</param>
        /// <param name="operation">编辑操作</param>
        /// <param name="operParam">操作参数</param>
        public void Edit(string FeatClsAliasName, EditOperation operation, object[] operParam)
        {
            if (operation == null)
            {
                throw new ArgumentNullException();
            }
            if (IsEditing)
            {
                IWorkspaceEdit pWkspEdt = null;
                try
                {
                    EditProperties ep = null;
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        if (ep == null || ep.WorkspaceEdit == null)
                        {
                            throw new FeatureEditException();
                        }
                        if (ep.Editable)
                        {
                            pWkspEdt = ep.WorkspaceEdit;
                            pWkspEdt.StartEditOperation();
                            operation(operParam);
                            pWkspEdt.StopEditOperation();
                        }
                    }
                    else
                    {
                        throw new FeatureEditException();
                    }
                }
                catch (FeatureEditException feExp)
                {
                    throw feExp;
                }
                catch (Exception exp)
                {
                    pWkspEdt.AbortEditOperation();
                    throw new FeatureEditException("", exp);
                }
            }
        }

        /// <summary>
        /// 判断是否存在可撤销的操作
        /// </summary>
        /// <param name="FeatClsAliasName"></param>
        /// <returns></returns>
        public bool HasUndos(string FeatClsAliasName)
        {
            if (IsEditing)
            {
                try
                {
                    EditProperties ep = null;
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        if (ep == null || ep.WorkspaceEdit == null)
                        {
                            throw new FeatureEditException();
                        }
                        bool hasUndos = false;
                        ep.WorkspaceEdit.HasUndos(ref hasUndos);
                        return hasUndos;
                    }
                    else
                    {
                        throw new FeatureEditException();
                    }
                }
                catch (FeatureEditException feExp)
                {
                    throw feExp;
                }
                catch (Exception exp)
                {
                    throw new FeatureEditException("", exp);
                }
            }
            throw new ArgumentException();
        }

        /// <summary>
        /// 判断是否存在可重做的操作
        /// </summary>
        /// <param name="FeatClsAliasName"></param>
        /// <returns></returns>
        public bool HasRedos(string FeatClsAliasName)
        {
            if (IsEditing)
            {
                try
                {
                    EditProperties ep = null;
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        if (ep == null || ep.WorkspaceEdit == null)
                        {
                            throw new FeatureEditException();
                        }
                        bool hasRedos = false;
                        ep.WorkspaceEdit.HasRedos(ref hasRedos);
                        return hasRedos;
                    }
                    else
                    {
                        throw new FeatureEditException();
                    }
                }
                catch (FeatureEditException feExp)
                {
                    throw feExp;
                }
                catch (Exception exp)
                {
                    throw new FeatureEditException("", exp);
                }
            }
            throw new ArgumentException();
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="FeatClsAliasName"></param>
        public void Undo(string FeatClsAliasName)
        {
            if (IsEditing)
            {
                EditProperties ep = null;
                try
                {
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        if (ep == null || ep.WorkspaceEdit == null)
                        {
                            throw new FeatureEditException();
                        }
                        if (ep.Editable)
                        {
                            bool hasUndos = false;
                            ep.WorkspaceEdit.HasUndos(ref hasUndos);
                            if (hasUndos)
                            {
                                ep.WorkspaceEdit.UndoEditOperation();
                            }
                        }
                    }
                    else
                    {
                        throw new FeatureEditException();
                    }
                }
                catch (FeatureEditException feExp)
                {
                    throw feExp;
                }
                catch (Exception exp)
                {
                    throw new FeatureEditException("", exp);
                }
            }
        }

        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="FeatClsAliasName"></param>
        public void Redo(string FeatClsAliasName)
        {
            if (IsEditing)
            {
                try
                {
                    EditProperties ep = null;
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        if (ep == null || ep.WorkspaceEdit == null)
                        {
                            throw new FeatureEditException();
                        }
                        if (ep.Editable)
                        {
                            bool hasRedos = false;
                            ep.WorkspaceEdit.HasRedos(ref hasRedos);
                            if (hasRedos)
                            {
                                ep.WorkspaceEdit.RedoEditOperation();
                            }
                        }
                    }
                    else
                    {
                        throw new FeatureEditException();
                    }
                }
                catch (FeatureEditException feExp)
                {
                    throw feExp;
                }
                catch (Exception exp)
                {
                    throw new FeatureEditException("", exp);
                }
            }
        }

        #endregion

        #region edit properties

        public void SetEditProperty(string FeatClsAliasName, EditConfigProp property, object value)
        {
            if (IsEditing)
            {
                EditProperties ep = null;
                try
                {
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        switch (property)
                        {
                            case EditConfigProp.Editable:
                                bool editable = Convert.ToBoolean(value);
                                ep.Editable = editable;
                                break;
                            case EditConfigProp.Snapping:
                                bool snapping = Convert.ToBoolean(value);
                                ep.Snapping = snapping;
                                break;
                            case EditConfigProp.SnapPixel:
                                int pixel = Convert.ToInt32(value);
                                if (pixel <= 0)
                                {
                                    throw new ArgumentException();
                                }
                                ep.SnapPixel = pixel;
                                break;
                            case EditConfigProp.SnapOption:
                                ep.SnapOptions = (SnapOption)value;
                                break;
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
        }

        public object GetEditProperty(string FeatClsAliasName, EditConfigProp property)
        {
            if (IsEditing)
            {
                EditProperties ep = null;
                try
                {
                    if (m_wkspEdtLst.TryGetValue(FeatClsAliasName, out ep))
                    {
                        switch (property)
                        {
                            case EditConfigProp.Editable:
                                return ep.Editable;
                            case EditConfigProp.Snapping:
                                return ep.Snapping;
                            case EditConfigProp.SnapPixel :
                                return ep.SnapPixel;
                            case EditConfigProp.SnapOption:
                                return ep.SnapOptions;
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
            throw new ArgumentException();
        }

        #endregion
    }

    class EditProperties : ICloneable
    {
        private IWorkspaceEdit m_wkspEdt;

        public IWorkspaceEdit WorkspaceEdit
        {
            get { return m_wkspEdt; }
            set { m_wkspEdt = value; }
        }

        private string m_displayName = string.Empty;

        public string DisplayName
        {
            get { return m_displayName; }
            set { m_displayName = value; }
        }

        private bool m_editable = true;

        public bool Editable
        {
            get { return m_editable; }
            set { m_editable = value; }
        }

        private bool m_snapping = false;

        public bool Snapping
        {
            get { return m_snapping; }
            set { m_snapping = value; }
        }

        private int m_snapPixel;

        public int SnapPixel
        {
          get { return m_snapPixel; }
          set { m_snapPixel = value; }
        }

        private SnapOption m_snapOptions = SnapOption.None;

        public SnapOption SnapOptions
        {
            get { return m_snapOptions; }
            set { m_snapOptions = value; }
        }

        #region ICloneable 成员

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    public enum EditConfigProp
    {
        Editable,

        Snapping,

        SnapPixel,

        SnapOption
    }


    [Flags]
    public enum SnapOption
    {
        None = 0,

        Boundary = 1,

        EndPoint = 2,

        MidPoint = 4
    }
}
