using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SendInputs
{
    class SearchHandle
    {

        //Used by EnumChildWindows returns true if should continue
        delegate bool CallBack(IntPtr hwnd, ref SearchData sdata);

        //Enumerate the child controls until the callback returns false
        [DllImport("user32.dll")]
        static extern bool EnumChildWindows(IntPtr hwnd, CallBack lpEnumFunc, ref SearchData sdata);

        //Gets the text of the window at the passed pointer address
        [DllImport("user32.dll")]
        private static extern Int32 GetWindowText(IntPtr hWnd, StringBuilder s, int nMaxCount);

        //Captura handle de componentes em determinada cordenadas do window.
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        //Gets the classname of the window at the passed pointer address
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        struct SearchData
        {
            public string WindowText;
            public string classNamePart;
            public IntPtr ResultHandle;
        }

        //Get the text in the object at the pointer
        private static string GetText(IntPtr hWnd)
        {
            StringBuilder formDetails = new StringBuilder(256);
            GetWindowText(hWnd, formDetails, 256);
            return formDetails.ToString().Trim();
        }

        //Get the class name of the object at the pointer
        private static string GetClassName(IntPtr hWnd)
        {
            StringBuilder formDetails = new StringBuilder(256);
            GetClassName(hWnd, formDetails, 256);
            return formDetails.ToString().Trim();
        }

        /// <summary>
        /// FindWindoEx só procura componente filho com o nome exato da classe do componente.
        /// este metodo procura um componente filho, neto, com o nome texto e parte do nome do componente... 
        /// </summary>
        /// <param name="windowText">Texto exibido no componente</param>
        /// <param name="partClassName">nome da classe inicia com ...</param>
        /// <param name="hwnd">Janela a ser procurado o componente</param>
        /// <returns></returns>
        public static IntPtr GetChildWindowHandle(string windowText, string partClassName, IntPtr hwnd)
        {
            var searchData = new SearchData { classNamePart = partClassName, WindowText = windowText };

            EnumChildWindows(hwnd, CallBackSearch, ref searchData);

            return searchData.ResultHandle;
        }
        // This method is called as a delegate each time a child is enumerated. 
        // If it returns false, enumeration will stop.  
        // The whole tree will be traversed without further code despite only
        // passing the root.
        private static bool CallBackSearch(IntPtr hWnd, ref SearchData sdata)
        {
            string text = GetText(hWnd);
            string className = GetClassName(hWnd);

            //OK: These need to be class level feilds or something
            if (text == sdata.WindowText && className.StartsWith(sdata.classNamePart))
            {
                sdata.ResultHandle = hWnd;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Cast de um ponteiro de uma janela em um processo.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);
        public static Process HandleToProcess(IntPtr hwnd) {
            int processId;
            GetWindowThreadProcessId(hwnd, out processId);
            return Process.GetProcessById(processId);
        }
    }
}
