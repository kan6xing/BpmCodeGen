using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DisplayValue(); //这里不会阻塞  
            System.Diagnostics.Debug.WriteLine("MyClass() End.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.label1.Text += "\n程序开始\n";
            // SetLibelAsync();
            //for (int i = 0; i < 5; i++)
            //{

            //    this.label1.Text += "\n主程序后" + i + " " + DateTime.Now + "\n";
            //    Thread.Sleep(1000);
            //}
            //this.label1.Text += "\n程序结束\n";

            DisplayValue(); //这里不会阻塞  
            System.Diagnostics.Debug.WriteLine("MyClass() End.");
        }

        private async Task<string> SetLibelAsync()
        {
            for(int i=0;i<5;i++)
            {
                await Task.Delay(200);
                
                this.label1.Text += "\n子程序"+i + " " + DateTime.Now + "\n";
                
            }
            return "AA";

        }


        public Task<double> GetValueAsync(double num1, double num2)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    num1 = num1 / num2;
                }
                return num1;
            });
        }
        public async void DisplayValue()
        {
            double result = await GetValueAsync(1234.5, 1.01);//此处会开新线程处理GetValueAsync任务，然后方法马上返回  
                                                              //这之后的所有代码都会被封装成委托，在GetValueAsync任务完成时调用  
            System.Diagnostics.Debug.WriteLine("Value is : " + result);
        }
    }
}
