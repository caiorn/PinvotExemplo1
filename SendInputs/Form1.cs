using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SendInputs
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private const int BN_CLICKED = 245;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);        

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
                
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        static IntPtr childWindow;
        private const uint WM_MOUSEMOVE = 0x0200;
        private const uint WM_LBUTTONDOWN = 0x201;
        private const uint WM_LBUTTONUP = 0x202;
        private const uint MK_LBUTTON = 0x0001;

        private void btnDesenhar_Click(object sender, EventArgs e)
        {
            Process process = Process.GetProcessesByName("mspaint").FirstOrDefault();
            if (process == null)
            {

                process = Process.Start("mspaint");
                //esperar o processo iniciar para desenhar
                while (string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    Thread.Sleep(1000);
                    process.Refresh();
                }
            }
            Desenhar();
        }

        //Metodo teste
        private void Desenhar()
        {     
            //procura o ponteiro do paint
            IntPtr hwndMain = FindWindow("MSPaintApp", null);
            //procura o componente view onde é feito os desenhos
            IntPtr hwndView = FindWindowEx(hwndMain, IntPtr.Zero, "MSPaintView", null);
            // Getting the child windows of MSPaintView because it seems that the class name of the child isn't constant
            EnumChildWindows(hwndView, new EnumWindowsProc(EnumWindow), IntPtr.Zero);
           
            //LETER C            
            SendMessage(childWindow, WM_LBUTTONDOWN, new IntPtr(MK_LBUTTON), CreateLParam(127, 14));// Simulate press of left mouse button on coordinates 127, 14
            SendMessage(childWindow, WM_MOUSEMOVE, new IntPtr(MK_LBUTTON), CreateLParam(14, 108)); // Simulate release of left mouse button
            SendMessage(childWindow, WM_LBUTTONUP, new IntPtr(MK_LBUTTON), CreateLParam(121, 200));// Simulate release of move mouse button 
            Thread.Sleep(10);
            //LETER A
            SendMessage(childWindow, WM_LBUTTONDOWN, new IntPtr(MK_LBUTTON), CreateLParam(186, 202));
            SendMessage(childWindow, WM_MOUSEMOVE, new IntPtr(MK_LBUTTON), CreateLParam(238, 18));
            SendMessage(childWindow, WM_LBUTTONUP, new IntPtr(MK_LBUTTON), CreateLParam(289, 209));
            Thread.Sleep(10);
            //LETTER I
            SendMessage(childWindow, WM_LBUTTONDOWN, new IntPtr(MK_LBUTTON), CreateLParam(350, 206));
            SendMessage(childWindow, WM_LBUTTONUP, new IntPtr(MK_LBUTTON), CreateLParam(350, 27));
            Thread.Sleep(10);
            //LETTR O
            SendMessage(childWindow, WM_LBUTTONDOWN, new IntPtr(MK_LBUTTON), CreateLParam(400, 124));
            SendMessage(childWindow, WM_MOUSEMOVE, new IntPtr(MK_LBUTTON), CreateLParam(450, 15));
            SendMessage(childWindow, WM_MOUSEMOVE, new IntPtr(MK_LBUTTON), CreateLParam(500, 130));
            SendMessage(childWindow, WM_MOUSEMOVE, new IntPtr(MK_LBUTTON), CreateLParam(457, 216));
            SendMessage(childWindow, WM_LBUTTONUP, new IntPtr(MK_LBUTTON), CreateLParam(400, 124));
        }

        static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            // Get the first child because it seems that MSPaintView has only this child
            childWindow = handle;
            // Stop enumerating the windows1
            return false;
        }

        private static IntPtr CreateLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        private void captureTextHandle_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            IntPtr handle = getHandleProcessComboBox();
            Dictionary<IntPtr, string> list = CaptureTextFromHandle.getAllKeyAndValueChildsFromWindow(handle); //Dictionary<IntPtr, string> a = CaptureTextFromHandle.getAllTextChilds(handle);
                        
            if (list != null) { 
                foreach (var t in list) {
                    IntPtr reconvert = new IntPtr((int)t.Key);
                    listView1.Items.Add(new ListViewItem(new[] { t.Key.ToString(), t.Value }));
                }
            }
        }


        private IntPtr getHandleProcessComboBox() {
            string processName = comboBox1.SelectedItem.ToString();
            processName = processName.Substring(0, processName.IndexOf('('));

            Process process = Process.GetProcessesByName(processName).FirstOrDefault();

            IntPtr hwnd = IntPtr.Zero;
            hwnd = process.MainWindowHandle;
            return hwnd;
        }


        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listView1.SelectedItems;

                ListViewItem lvItem = items[0];
                txtNomeButton.Text = lvItem.Text;
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            Process[] processos = Process.GetProcesses();
            foreach (Process p in processos)
                comboBox1.Items.Add(p.ProcessName + "(" + p.MainWindowTitle + ")");
        }

        private void btnEnviarClique_Click(object sender, EventArgs e)
        {
            IntPtr hwnd = getHandleProcessComboBox();
            IntPtr hwndView = IntPtr.Zero;

            int p;
            if (int.TryParse(txtNomeButton.Text, out p))
            {
                hwndView = new IntPtr(p);
            }
            else
            {
                hwndView = SearchHandle.GetChildWindowHandle(txtNomeButton.Text, "WindowsForms10.BUTTON", hwnd);
            }

            if (hwndView != IntPtr.Zero)
            {
                SendAction.SendClick(hwndView);
            }
        }

        [DllImport("User32")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        private const int SW_MAXIMIZE = 3;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;
        private void btnAlterarTextoEdit_Click(object sender, EventArgs e)
        {
            IntPtr hwnd = getHandleProcessComboBox();
            IntPtr hwndView = IntPtr.Zero;

            int p;
            if (int.TryParse(txtNomeButton.Text, out p))
            {
                hwndView = new IntPtr(p);
            }
            else
            {
                hwndView = SearchHandle.GetChildWindowHandle(txtNomeButton.Text, "WindowsForms10", hwnd);
            }

            if (hwndView != IntPtr.Zero)
            {
                SendAction.SendText(hwndView, "caio");
                //o restante dos componentes que nao forem textbox, precisam ser minimizado e maximizados para atualizarem.
                ShowWindow(hwndView, SW_MINIMIZE);
                ShowWindow(hwndView, SW_RESTORE);
            }
        }
    }
}


//https://www.codeproject.com/Questions/379518/Can-I-SIMULATE-a-MOUSE-CLICK-in-a-minimized-window