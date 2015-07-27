using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayValue(); //这里不会阻塞  
            Console.WriteLine("mainI hou begin : ");
            for (int i = 0; i < 10000000; i++)
            {
                
                if (i % 1000000 == 0)
                {
                    Console.WriteLine("mainI is : " + i);
                }
            }

            Console.WriteLine("MyClass() End.");
            Console.Read();
        }

        public async static Task<double> GetValueAsync(double num1, double num2)
        {
            //return Task.Run(() =>
            //{
                for (int i = 0; i < 1000000; i++)
                {
                    num1 = num1 / num2;
                    if(i%100000==0)
                    {
                        Console.WriteLine("i is : " + i);
                    }
                }
                return num1;
            //});
        }
        public async static void DisplayValue()
        {
            Console.WriteLine("VALUES BEGIN.");
            double result = await GetValueAsync(1234.5, 1.01);//此处会开新线程处理GetValueAsync任务，然后方法马上返回  
                                                              //这之后的所有代码都会被封装成委托，在GetValueAsync任务完成时调用  
            for (int i = 0; i < 1000000; i++)
            {
                
                if (i % 100000 == 0)
                {
                    Console.WriteLine("wai iiiii is : " + i);
                }
            }
            Console.WriteLine("Value is : " + result);
        }
    }
}
