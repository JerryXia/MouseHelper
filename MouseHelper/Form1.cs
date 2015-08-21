using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace MouseHelper
{
    public partial class MainForm : Form
    {
        private IKeyboardMouseEvents m_Events;
        private KeyboardHook k_hook;
        private Version CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;
        //private string guid = Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId.ToString();
        private string CurrentGuid = AttributeHelper.GetAssemblyGuid();
        private bool started = false;
        private System.Timers.Timer timer;

        public MainForm()
        {
            InitializeComponent();
            SubscribeGlobal();

            timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Worker);

            MouseEventHelper.MouseEvent(OperatingFlag.MouseEvent_Move, 0, 0);
            this.cmb_mpress.SelectedIndex = 0;
            this.cmb_maction.SelectedIndex = 0;
            this.lbl_version.Text = string.Format("v{0}", CurrentVersion);

            BindFastKeys("start", txt_start.Text.Trim());
            BindFastKeys("stop", txt_stop.Text.Trim());
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            //m_Events.KeyDown += OnKeyDown;
            //m_Events.KeyUp += OnKeyUp;
            //m_Events.KeyPress += HookManager_KeyPress;

            //m_Events.MouseUp += OnMouseUp;
            //m_Events.MouseClick += OnMouseClick;
            //m_Events.MouseDoubleClick += OnMouseDoubleClick;

            m_Events.MouseMove += HookManager_MouseMove;

            //m_Events.MouseWheelExt += HookManager_MouseWheelExt;

            //m_Events.MouseDownExt += HookManager_Supress;

            k_hook = new KeyboardHook();
            k_hook.KeyDownEvent += Hook_KeyDown;//钩住键按下
            k_hook.Start();//安装键盘钩子
        }

        private void Unsubscribe()
        {
            if (m_Events != null)
            {
                //m_Events.KeyDown -= OnKeyDown;
                //m_Events.KeyUp -= OnKeyUp;
                //m_Events.KeyPress -= HookManager_KeyPress;

                //m_Events.MouseUp -= OnMouseUp;
                //m_Events.MouseClick -= OnMouseClick;
                //m_Events.MouseDoubleClick -= OnMouseDoubleClick;

                m_Events.MouseMove -= HookManager_MouseMove;

                //if (checkBoxSupressMouseWheel.Checked)
                //    m_Events.MouseWheelExt -= HookManager_MouseWheelExt;
                //else
                //    m_Events.MouseWheel -= HookManager_MouseWheel;

                //if (checkBoxSuppressMouse.Checked)
                //    m_Events.MouseDownExt -= HookManager_Supress;
                //else
                //    m_Events.MouseDown -= OnMouseDown;

                m_Events.Dispose();
                m_Events = null;
            }

            if (k_hook != null)
            {
                k_hook.KeyDownEvent -= Hook_KeyDown;
                k_hook.Stop();
                k_hook = null;
            }
        }

        private void HookManager_MouseWheelExt(object sender, MouseEventExtArgs e)
        {
            //labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
            e.Handled = true;
        }
        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            //labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
            int sw = Screen.PrimaryScreen.Bounds.Width;
            int sh = Screen.PrimaryScreen.Bounds.Height;
            if (!this.chk_lockx.Checked)
            {
                this.txt_mx.Text = string.Format("{0}", e.X < 0 ? 0 : (e.X > sw ? sw : e.X));
            }
            if (!this.chk_locky.Checked)
            {
                this.txt_my.Text = string.Format("{0}", e.Y < 0 ? 0 : (e.Y > sh ? sh : e.Y));
            }
        }
        private void HookManager_Supress(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                //Log(string.Format("MouseDown \t\t {0}\n", e.Button));
                return;
            }

            //Log(string.Format("MouseDown \t\t {0} Suppressed\n", e.Button));
            e.Handled = true;
        }


        private Keys k_start, k_stop;
        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyValue == (int)k_start)
            {
                WorkerStart();
            }
            else if (Control.ModifierKeys == Keys.Control && e.KeyValue == (int)k_stop)
            {
                WorkerStop();
            }
            else
            {
            
            }
        }

        private void chk_lockx_CheckedChanged(object sender, EventArgs e)
        {
            //MouseEventHelper.MouseEvent(OperatingFlag.MouseEvent_Move, 0, 0);
        }
        private void chk_locky_CheckedChanged(object sender, EventArgs e)
        {
            //MouseEventHelper.MouseEvent(OperatingFlag.MouseEvent_Move, 0, 0);
        }


        private DateTime controlLastChanged = DateTime.Now;
        TimeSpan milliSenconds = TimeSpan.FromMilliseconds(700);
        private void txt_start_TextChanged(object sender, EventArgs e)
        {
            controlLastChanged = DateTime.Now;
            ThreadPool.QueueUserWorkItem(CheckChangeState, sender);
        }
        private void txt_stop_TextChanged(object sender, EventArgs e)
        {
            controlLastChanged = DateTime.Now;
            ThreadPool.QueueUserWorkItem(CheckChangeState, sender);
        }
        private void CheckChangeState(object sender)
        {
            Thread.Sleep(milliSenconds);
            if ((DateTime.Now - controlLastChanged) >= milliSenconds)
            {
                TextBox txtBox = sender as TextBox;
                if (Enum.IsDefined(typeof(Keys), txtBox.Text))
                {
                    //LogHelper.Info(txtBox.Name.Substring(4));
                    BindFastKeys(txtBox.Name.Substring(4), txtBox.Text.Trim());
                }
                else
                {
                    MessageBox.Show("快捷键无效");
                }
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            WorkerStart();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            WorkerStop();
        }

        private int x;
        private int y;
        private int clickType;
        private int action;


        private void WorkerStart()
        {
            if (!started)
            {
                try
                {
                    x = Convert.ToInt32(this.txt_mx.Text.Trim());
                    y = Convert.ToInt32(this.txt_my.Text.Trim());

                    clickType = this.cmb_mpress.SelectedIndex;
                    action = this.cmb_maction.SelectedIndex;

                    double hours = (double)this.numericHour.Value * 60 * 60 * 1000;
                    double minute = (double)this.numericMill.Value * 60 * 1000;
                    double second0 = (double)this.numericSecond.Value * 1000;
                    double second1 = (double)this.numericSecond1.Value * 100;
                    double second2 = (double)this.numericSecond2.Value * 10;
                    double seperateTime = hours + minute + second0 + second1 + second2;

                    timer.Interval = seperateTime;

                    MouseEventHelper.MouseEventAbs(OperatingFlag.MouseEvent_Move, x, y);
                    timer.Start();
                    ChanageState(true);
                }
                catch
                {
                    MessageBox.Show(this, "输入的参数非法", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void WorkerStop()
        {
            if (started)
            {
                ChanageState(false);
                timer.Stop();
            }
        }

        private void ChanageState(bool v)
        {
            started = v;
            this.btn_start.Enabled = this.txt_mx.Enabled = this.txt_my.Enabled = cmb_mpress.Enabled = cmb_maction.Enabled = !started;
            this.btn_stop.Enabled = started;
            
        }

        private void Timer_Worker(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (started)
            {
                try
                {
                    //这里是鼠标左键按下和松开两个事件的组合即一次单击
                    //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

                    //模拟鼠标右键单击事件
                    //mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);

                    //两次连续的鼠标左键单击事件 构成一次鼠标双击事件： 
                    //mouse_event (MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0 ) 
                    //mouse_event (MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0 ) 

                    switch (clickType)
                    {
                        case 0:
                            MouseEventHelper.MouseEventAbs(OperatingFlag.MouseEvent_LeftDown | OperatingFlag.MouseEvent_LeftUp, x, y, action + 1);
                            break;
                        case 1:
                            MouseEventHelper.MouseEventAbs(OperatingFlag.MouseEvent_RightDown | OperatingFlag.MouseEvent_RightUp, x, y, action + 1);
                            break;
                        case 2:
                            MouseEventHelper.MouseEventAbs(OperatingFlag.MouseEvent_MiddleDown | OperatingFlag.MouseEvent_MiddleUp, x, y, action + 1);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    ChanageState(false);
                    //MessageBox.Show(this, "输入的参数非法", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BindFastKeys(string action, string key)
        {
            switch (action)
            {
                case "start":
                    if (Enum.IsDefined(typeof(Keys), key))
                    {
                        k_start = (Keys)Enum.Parse(typeof(Keys), key, true);
                    }
                    else
                    {
                        MessageBox.Show("启动快捷键无效");
                    }
                    break;
                case "stop":
                    if (Enum.IsDefined(typeof(Keys), key))
                    {
                        k_stop = (Keys)Enum.Parse(typeof(Keys), key, true);
                    }
                    else
                    {
                        MessageBox.Show("停用快捷键无效");
                    }
                    break;
                default:
                    break;
            }
        }

    }
}