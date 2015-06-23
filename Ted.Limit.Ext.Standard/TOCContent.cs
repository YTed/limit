using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

namespace Ted.Limit.Ext.Map.Standard
{
    public partial class TOCContent : UserControl
    {
        private static int s_NONE = 0 ,s_SHIFT = 1, s_CTRL = 2, s_ALT = 4;

        public TOCContent(object buddy)
        {
            InitializeComponent();
            TOC.SetBuddyControl(buddy);
            TOC.EnableLayerDragDrop = true;
            m_tocAction = new TOCAction();
            m_tocAction.toc = TOC.Object as ITOCControlDefault;
        }

        private TOCAction m_tocAction;

        private void TOC_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            IBasicMap map = null;
            ILayer lyr = null;
            object unk = null;
            object data = null;
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            TOC.HitTest(e.x, e.y, ref item, ref map, ref lyr, ref unk, ref data);
            DoHitDown down = m_tocAction.SetAction(item, map, lyr, unk, data);
            DoHitUp up = down(e.x, e.y, e.button, e.shift);
            m_tocAction.Up = up;
        }

        private void TOC_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            DoHitUp up = m_tocAction.Up;
            if (up != null)
            {
                IBasicMap map = null;
                ILayer lyr = null;
                object unk = null;
                object data = null;
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                TOC.HitTest(e.x, e.y, ref item, ref map, ref lyr, ref unk, ref data);
                m_tocAction.SetAction(item, map, lyr, unk, data);
                up(e.x, e.y, e.button, e.shift);
            }
        }

        private class TOCAction
        {
            public ITOCControlDefault toc;

            public IBasicMap map = null;
            public ILayer lyr = null;
            public object unk = null;
            public object data = null;
            public esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;

            public TOCAction()
            {
                m_doHitNone = new DoHitDown(DoHitNone);
                m_doHitMap = new DoHitDown(DoHitMap);
                m_doHitLegendClass = new DoHitDown(DoHitLegendClass);
                m_doHitLayer = new DoHitDown(DoHitLayer);
                m_doHitHeading = new DoHitDown(DoHitHeading);

                m_doMoveLayer = new DoHitUp(DoMoveLayer);
                LayerSelector.s_action = this;
            }

            private DoHitDown
                m_doHitNone,
                m_doHitMap,
                m_doHitHeading,
                m_doHitLayer,
                m_doHitLegendClass;

            private DoHitUp m_up,
                m_doMoveLayer;

            public DoHitUp Up
            {
                get { return m_up; }
                set { m_up = value; }
            }

            public DoHitDown SetAction(esriTOCControlItem item, IBasicMap map, ILayer layer, object unk, object data)
            {
                this.map = map;
                this.lyr = layer;
                this.unk = unk;
                this.data = data;
                this.item = item;

                switch (item)
                {
                    case esriTOCControlItem.esriTOCControlItemNone:
                        return m_doHitNone;
                    case esriTOCControlItem.esriTOCControlItemMap:
                        return m_doHitMap;
                    case esriTOCControlItem.esriTOCControlItemLegendClass:
                        return m_doHitLegendClass;
                    case esriTOCControlItem.esriTOCControlItemLayer:
                        return m_doHitLayer;
                    case esriTOCControlItem.esriTOCControlItemHeading:
                        return m_doHitHeading;
                }
                throw new ArgumentException("传入非法esriTOCControlItem值:"+item);
            }

            private DoHitUp DoHitNone(int x, int y, int button, int shif)
            {
                return null;
            }

            private DoHitUp DoHitMap(int x, int y, int button, int shif)
            {
                return null;
            }

            private DoHitUp DoHitHeading(int x, int y, int button, int shif)
            {
                return null;
            }

            /// <summary>
            /// 命中图层,左键选择,右键菜单.
            /// </summary>
            /// <param name="button"></param>
            /// <param name="shif"></param>
            /// <returns></returns>
            private DoHitUp DoHitLayer(int x, int y, int button, int shif)
            {
                DoHitUp rsl = null;
                // select layer
                if (button == 1)
                {
                    LayerSelector.Select(shif);
                }
                // context menu
                else if (button == 2)
                {
                    IToolbarMenu iTbrMenu = TOCContextMenu.CreateLayerMenu(lyr, toc.Buddy);
                    iTbrMenu.PopupMenu(x, y, toc.hWnd);
                }
                return rsl;
            }

            private DoHitUp DoHitLegendClass(int x, int y, int button, int shif)
            {
                return null;
            }

            private void DoMoveLayer(int x, int y, int button, int shift)
            {
                // move 左键按下,且HitTest命中的图层不在选择的图层内
                if( (button == 1) && ShouldMove())
                {
                    //移动:方向由两次序号比较得到.移动后所有选择图层在命中图层的同一侧.
                    int lastSelIdx = LayerSelector.LayerIndex(map, LayerSelector.s_lastSelect);
                    int hitIdx = LayerSelector.LayerIndex(map, lyr);
                    bool directionUp = lastSelIdx < hitIdx;
                    int[] mvLyrArr = new int[LayerSelector.s_lyrLst.Count];
                    //图层向上移动,则所有索引大于hitIdx的图层按顺序移动到hitIdx的位置
                    if (directionUp)
                    {
                        int mvLyrCount = 0;
                        foreach (ILayer tmpLyr in LayerSelector.s_lyrLst)
                        {
                            int tmpLyrIdx = LayerSelector.LayerIndex(map, tmpLyr);
                            if (tmpLyrIdx > hitIdx)
                            {
                                mvLyrArr[mvLyrCount++] = tmpLyrIdx;
                            }
                        }

                    }
                }
            }

            /// <summary>
            /// TOC中图层移动的条件:
            /// 上一次命中图层与当前命中图层间存在未被选择的图层
            /// </summary>
            /// <param name="upLayer"></param>
            /// <returns></returns>
            private bool ShouldMove()
            {
                int from, to;
                LayerSelector.LayerFromTo(LayerSelector.s_lastSelect, lyr, out from, out to);
                IList<ILayer> lst = LayerSelector.s_lyrLst;
                for (int i = from; i <= to; i++)
                {
                    if (!lst.Contains(map.get_Layer(i)))
                    {
                        return true;
                    }
                }
                return false;
            }

            private class LayerSelector
            {
                public static List<ILayer> s_lyrLst = new List<ILayer>();

                public static ILayer s_lastSelect;

                public static TOCAction s_action;

                /// <summary>
                /// alt : 聚焦到图层   优先级-0     
                /// ctrl : 选取单个    优先级-1      
                /// shift : 选取连续   优先级-2       
                /// 无叠加.
                /// </summary>
                /// <param name="shift"></param>
                public static void Select(int shift)
                {
                    ILayer pLyr = s_action.lyr;
                    IBasicMap pMap = s_action.map;
                    ITOCControlDefault toc = s_action.toc;
                    if((shift & TOCContent.s_ALT) != TOCContent.s_NONE)
                    {
                        s_lyrLst.Clear();
                        s_lastSelect = pLyr;
                        s_lyrLst.Add(pLyr);
                        IEnvelope pEnv = pLyr.AreaOfInterest;
                        ((ITOCBuddy)toc.Buddy).GetActiveView().Extent = pEnv;
                        ((ITOCBuddy)toc.Buddy).GetActiveView().Refresh();
                    }
                    else if((shift & TOCContent.s_CTRL )!= TOCContent.s_NONE)
                    {
                        s_lastSelect = pLyr;
                        s_lyrLst.Add(pLyr);
                    }
                    else if ((shift & TOCContent.s_SHIFT) != TOCContent.s_NONE)
                    {
                        // 已选取了某个图层
                        if (s_lastSelect != null)
                        {
                            s_lyrLst.Clear();
                            int from, to;
                            LayerFromTo(pLyr, s_lastSelect, out from, out to);
                            // 连续选取2个图层间的所有图层
                            for (int i = from; i <= to; i++)
                            {
                                ILayer tmpLyr = pMap.get_Layer(i);
                                s_lyrLst.Add(tmpLyr);
                                toc.SelectItem(tmpLyr, Type.Missing);
                            }
                        }
                        // 尚未选取任何图层,选择之
                        else
                        {
                            s_lyrLst.Add(pLyr);
                            s_lastSelect = pLyr;
                        }
                    }
                    // 清除所有选择,选择之
                    else
                    {
                        s_lyrLst.Clear();
                        s_lastSelect = pLyr;
                        s_lyrLst.Add(pLyr);
                    }
                }

                public static void LayerFromTo(ILayer fromLyr, ILayer toLyr, out int fromIdx, out int toIdx)
                {
                    fromIdx = LayerIndex(s_action.map, fromLyr);
                    toIdx = LayerIndex(s_action.map, toLyr);
                    if (fromIdx > toIdx)
                    {
                        int tmp = fromIdx;
                        fromIdx = toIdx;
                        toIdx = tmp;
                    }
                }

                public static int LayerIndex(IBasicMap map, ILayer lyr)
                {
                    for (int i = map.LayerCount - 1; i >= 0; i--)
                    {
                        if (map.get_Layer(i).Equals(lyr))
                        {
                            return i;
                        }
                    }
                    return -1;
                }
            }


        }

        private delegate DoHitUp DoHitDown(int x, int y, int button, int shift);

        private delegate void DoHitUp(int x, int y, int button, int shift);
    }
}
