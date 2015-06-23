using System;
using System.Collections.Generic;
using System.Text;

using Infragistics.Win.UltraWinToolbars;

using Ted.Limit.Core;
using Infragistics.Win;


namespace Ted.Limit.WinUI
{
    class StateButtonToolProducer : IUIToolWorker
    {
        #region IUIToolWorker 成员

        bool IUIToolWorker.CanProduce(Ted.Limit.Core.IItem item)
        {
            return item is ITool;
        }

        ToolBase IUIToolWorker.ProduceActualTool(Ted.Limit.Core.IItem item, string key)
        {
            StateButtonTool shareTool = new StateButtonTool(key);
            shareTool.Checked = item.Checked;
            UIToolFactory.FillShareProperties(shareTool.SharedProps, item);

            return shareTool;
        }

        ToolBase IUIToolWorker.ProducePresentTool(IItem item, string key, bool beginGroup)
        {
            ToolBase instanceTool = new StateButtonTool(key);
            UIToolFactory.FillInstanceProps(instanceTool.InstanceProps, beginGroup);
            return instanceTool;
        }

        #endregion
    }

    class ButtonToolProducer : IUIToolWorker
    {

        #region IUIToolWorker 成员

        bool IUIToolWorker.CanProduce(IItem item)
        {
            return item is ICommand;
        }

        ToolBase IUIToolWorker.ProduceActualTool(IItem item, string key)
        {
            ButtonTool shareTool = new ButtonTool(key);
            UIToolFactory.FillShareProperties(shareTool.SharedProps, item);

            return shareTool;
        }

        ToolBase IUIToolWorker.ProducePresentTool(IItem item, string key, bool beginGroup)
        {
            ToolBase instanceTool = new ButtonTool(key);
            UIToolFactory.FillInstanceProps(instanceTool.InstanceProps, beginGroup);
            return instanceTool;
        }

        #endregion
    }

    public class LabelToolProducer : IUIToolWorker
    {

        #region IUIToolWorker 成员

        bool IUIToolWorker.CanProduce(IItem item)
        {
            return item is ILabel;
        }

        ToolBase IUIToolWorker.ProduceActualTool(IItem item, string key)
        {
            LabelTool shareTool = new LabelTool(key);
            UIToolFactory.FillShareProperties(shareTool.SharedProps, item);

            return shareTool;
        }

        ToolBase IUIToolWorker.ProducePresentTool(IItem item, string key, bool beginGroup)
        {
            ToolBase instanceTool = new LabelTool(key);
            UIToolFactory.FillInstanceProps(instanceTool.InstanceProps, beginGroup);
            return instanceTool;
        }

        #endregion
    }

    public class ComboBoxToolProducer : IUIToolWorker
    {

        #region IUIToolWorker 成员

        bool IUIToolWorker.CanProduce(IItem item)
        {
            return item is ISelector;
        }

        ToolBase IUIToolWorker.ProduceActualTool(IItem item, string key)
        {
            ComboBoxTool shareTool = new ComboBoxTool(key);
            UIToolFactory.FillShareProperties(shareTool.SharedProps, item);

            ISelector iSel = item as ISelector;
            SelNotice update = new SelNotice();
            update.Selector = iSel;
            update.Tool = shareTool;
            iSel.Notice = new SelectorNotice(update.Notice);
            update.Notice();

            return shareTool;
        }

        ToolBase IUIToolWorker.ProducePresentTool(IItem item, string key, bool beginGroup)
        {
            ToolBase instanceTool = new ComboBoxTool(key);
            UIToolFactory.FillInstanceProps(instanceTool.InstanceProps, beginGroup);
            return instanceTool;
        }

        #endregion

        private class SelNotice
        {
            private ComboBoxTool m_cmbxTool;

            private ISelector m_sel;

            public ComboBoxTool Tool
            {
                get
                {
                    return m_cmbxTool;
                }
                set
                {
                    m_cmbxTool = value;
                }
            }

            public ISelector Selector
            {
                get
                {
                    return m_sel;
                }
                set
                {
                    m_sel = value;
                }
            }

            public void Notice()
            {
                m_cmbxTool.ValueList.ValueListItems.Clear();
                object[] valArray = Selector.ValueList;
                int count = valArray.Length;
                for (int i = 0; i < count; i++)
                {
                    m_cmbxTool.ValueList.ValueListItems.Add(valArray[i]);
                }
            }
        }

    }
}
