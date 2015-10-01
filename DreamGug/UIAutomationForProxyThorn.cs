using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;
using System.Windows;


namespace DreamGun
{
    class UIAutomationForProxyThorn
    {

        public int gridRowCount = 0;     //可用的IP数
        private GridPattern gridPattern = null;
        public enum MouseEventFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0800,
            Absolute = 0x8000
        }
        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);
        [DllImport("User32")]
        private extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

        public bool GetUseableIP()
        {
            bool result = false;
            try
            {
                FindButtonAndInvoke("取消IE代理");
                FindButtonAndInvoke("▲下载代理资源");
                FindButtonAndInvoke("验证全部");
                FindButtonAndInvoke("清理");
                FindButtonAndInvoke("是(Y)");
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 清理下载无效的
        /// </summary>
        /// <param name="winName"></param>
        /// <param name="btnName"></param>
        /// <returns></returns>
        private bool FindWindowButtonAndInvoke(string winName, string btnName)
        {
            bool result = false;
            try
            {
                AutomationElement proxyThorn = AutomationElement.RootElement.FindFirst(TreeScope.Children,
new PropertyCondition(AutomationElement.NameProperty, "花刺代理验证（ProxyThorn）1.8")); ;
                var btnCondition = new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                    new PropertyCondition(AutomationElement.NameProperty, btnName));
                var btn = proxyThorn.FindFirst(TreeScope.Subtree, btnCondition);
                while (btn == null || !btn.Current.IsEnabled)
                {
                    Thread.Sleep(500);
                    btn = proxyThorn.FindFirst(TreeScope.Children, btnCondition);

                }
                var clickBtn = (InvokePattern)btn.GetCurrentPattern(InvokePattern.Pattern);
                clickBtn.Invoke();
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }



        private bool FindButtonAndInvoke(string btnName)
        {
            bool result = false;
            try
            {
                AutomationElement proxyThorn = AutomationElement.RootElement.FindFirst(TreeScope.Children,
    new PropertyCondition(AutomationElement.NameProperty, "花刺代理验证（ProxyThorn）1.8")); ;
                var btnCondition = new AndCondition(
                     new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                     new PropertyCondition(AutomationElement.NameProperty, btnName));
                var btn = proxyThorn.FindFirst(TreeScope.Subtree, btnCondition);

                while (btn == null || !btn.Current.IsEnabled)
                {
                    Thread.Sleep(500);
                    btn = proxyThorn.FindFirst(TreeScope.Children, btnCondition);

                }
                var clickBtn = (InvokePattern)btn.GetCurrentPattern(InvokePattern.Pattern);

                clickBtn.Invoke();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 初始化(ip列表)，并得到IP可用数
        /// </summary>
        /// <returns></returns>
        public bool InitGridPattern()
        {
            bool result = false;
            try
            {
                AutomationElement proxyThorn = AutomationElement.RootElement.FindFirst(TreeScope.Children,
new PropertyCondition(AutomationElement.NameProperty, "花刺代理验证（ProxyThorn）1.8"));
                var listCondition = new AndCondition(
                         new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataGrid),
                         new PropertyCondition(AutomationElement.NameProperty, "List2"));
                var listGrid = proxyThorn.FindFirst(TreeScope.Children, listCondition);
                var grid = (GridPattern)listGrid.GetCurrentPattern(GridPattern.Pattern);
                gridPattern = grid;
                gridRowCount = grid.Current.RowCount;
                result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }


        /// <summary>
        ///   会将rowNumber排的ip设置为IE代理
        /// </summary>
        /// <param name="rowNumber">第几排（从0开始）</param>
        /// <returns>是否设置成功</returns>
        public bool SetIPandSetIE(int rowNumber)
        {
            bool result = false;
            try
            {
                SelectedRow(rowNumber);
                Thread.Sleep(100);
                FindButtonAndInvoke("设为IE代理");
            }
            catch (Exception)
            {

            }
            return result;
        }
        //选中一行
        private void SelectedRow(int rowNumber)
        {
            Rect rect = gridPattern.GetItem(rowNumber, 3).Current.BoundingRectangle;
            int IncrementX = (int)(rect.Left + rect.Width / 2);
            int IncrementY = (int)(rect.Top + rect.Height / 2);
            //Make the cursor position to the element.
            SetCursorPos(IncrementX, IncrementY);
            //Make the left mouse down and up.
            mouse_event((int)MouseEventFlags.LeftDown, IncrementX, IncrementY, 0, IntPtr.Zero);
            mouse_event((int)MouseEventFlags.LeftUp, IncrementX, IncrementY, 0, IntPtr.Zero);
        }

        /// <summary>
        /// 删除所有的IP
        /// </summary>
        /// <returns></returns>
        public bool DeleteIPList()    //list2
        {
            bool result = false;
            try
            {
                if (gridRowCount != 0)
                {
                    for (int i = gridRowCount; i > 0; i--)
                    {
                        SelectedRow(i - 1);
                        FindButtonAndInvoke("删除");
                        FindButtonAndInvoke("是(Y)");
                    }
                    FindButtonAndInvoke("取消IE代理");
                    result = true;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
      
        public AutomationElement WaitForElement(AutomationElement parent, Condition condition)

        {
            var waitTime = 0;
            var element = parent.FindFirst(TreeScope.Children, condition);
            while (element == null)
            {
                Thread.Sleep(500);
                waitTime += 500;
                element = parent.FindFirst(TreeScope.Children, condition);
            }
            return element;
        }
    }
}
