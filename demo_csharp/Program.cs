using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wd;
using System.Threading;

// JSON库参考 https://archive.codeplex.com/?p=json

namespace demo_csharp
{
    class Program
    {
        static string APPID = "5b4564eaab00093b641e99b5";
        static string ACCESSKEY = "a59836a11b76dda496bac68c";
        static string SECRETKEY = "3dcc2142b326020d92b77b1c";
        static string PAGEID = "5b45b741aa4a3a3f5840d15b";
        static string PAGEID2 = "5bbc30e769516e40e8366d17";
        static string CIRCLE = "WDVZ6DE5OF";	// 页面1中的圆形组件，演示改变背景颜色
        static string BUTTON1 = "WDJTLFVEQQ";	// 页面1中的切换页面按钮
        static string BUTTON2 = "WDCIILJPWO";	// 页面1中的显示消息按钮
        static string BUTTON3 = "WDWLABJ5JW";	// 页面2中的切换页面按钮

        static void NoticeCallBack(string appid, string pageid, string wid, string name, string sid, string value, IntPtr user)
        {
            Console.WriteLine("rcv data wid = {0} , name = {1}, value = {2}\r\n", wid, name, value);

            if (pageid.CompareTo(PAGEID) == 0)
            {
                if (wid.CompareTo(BUTTON1) == 0)
                {
                    // 第一页的按钮 id
                    Console.WriteLine("收到 第一页 按钮的 点击事件, 准备跳转到第二页\n");

                    // 跳转到第二页
                    webdriver.showPage(APPID, PAGEID, PAGEID2, "0");
                }
                else if (wid.CompareTo(BUTTON2) == 0)
                {
                    // 显示消息
                    string title = "hello";
                    string message = "welcome to webdriver";

                    webdriver.sendMessage(APPID, PAGEID, "success", title, message, 3, sid);
                }
            }
            else if (pageid.CompareTo(PAGEID2) == 0)
            {
                if (wid.CompareTo(BUTTON3) == 0)
                {
                    // 第二页的按钮 id
                    Console.WriteLine("收到 第二页 按钮的 点击事件, 准备跳转到第一页\n");
                    // 跳转到第一页
                    webdriver.showPage(APPID, PAGEID2, PAGEID, sid);
                }
            }
        }

        static void ConnectStateCallBack(string appid, bool state)
        {
            Console.WriteLine("connect state, appid = {0} , state = {1}", appid, state);
        }

        static void Main(string[] args)
        {
            string[] colors = { "red", "blue", "yellow" };
            int index = 0;


            // 测试辅助函数
            Console.WriteLine("=== 测试辅助函数 ===");
            string strvalue = "123.456789";
            int a1 = wd.util.str_to_int(strvalue);
            uint a2 = wd.util.str_to_uint(strvalue);
            double a3 = wd.util.str_to_double(strvalue);
            bool a4 = wd.util.str_to_bool(strvalue);
            Console.WriteLine("a1={0} a2={1} a3={2} a4={3}", a1, a2, a3, a4);

            string d1 = wd.util.int_to_str(a1);
            string d2 = wd.util.uint_to_str(a2);
            string d3 = wd.util.double_to_str(a3);
            string d4 = wd.util.bool_to_str(a4);
            Console.WriteLine("d1={0} d2={1} d3={2} d4={3}", d1, d2, d3, d4);


            // 测试接口
            Console.WriteLine("=== 测试接口 ===");
            string serverip = "127.0.0.1";
            webdriver.config(serverip);
            webdriver.connect(APPID, ACCESSKEY, SECRETKEY, NoticeCallBack, ConnectStateCallBack);

            while (true)
            {
                Thread.Sleep(1000);
                // 改变圆形的背景颜色
                webdriver.write(APPID, PAGEID, CIRCLE, "bg", "0", colors[index]);
                index++;
                if (index >= 3) index = 0;
            }
        }
    }
}
