using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace BPMCodeGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Word文档(*.doc)|*.doc|其它文档(*.*)|*.*";
            open.Filter = "所有文档(*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = open.FileName;
                FileStream fs = new FileStream(open.FileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                string allStr = sr.ReadToEnd();
                int startInt = 0;
                startInt = allStr.IndexOf("$[", startInt);

                string TestStr = "";
                while (startInt > 0)
                {
                    string subStr = allStr.Substring(startInt, allStr.IndexOf("]", startInt) - startInt + 1);
                    //allStr= allStr.Replace(subStr,"字段"+startInt);
                    startInt = allStr.IndexOf("$[", startInt + 1);

                    if (string.IsNullOrEmpty(TestStr))
                    {
                        TestStr += subStr + ":\n";
                    }
                    else
                    {
                        TestStr += ";" + subStr + ":\n";
                    }
                }
                this.richTextBox1.Text = TestStr + allStr;
                this.richSetup.Text = TestStr.Trim();
                fs.Flush();
                sr.Close();
                fs.Close();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "文本文件|*.txt";
            //sf.FilterIndex=2;
            //sf.RestoreDirectory = true;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sf.FileName, false, Encoding.Default);
                sw.Write(this.richTextBox1.Text);
                sw.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.richSetup.Text.Trim().Equals(""))
            {
                return;
            }
            if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
            {
                MessageBox.Show("请选择一个模板");
                return;
            }

            string fileName = this.textBox1.Text;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            string allStr = sr.ReadToEnd();
            int startInt = 0;
            startInt = allStr.IndexOf("$[", startInt);

            string TestStr = "";
            while (startInt > 0)
            {
                string subStr = allStr.Substring(startInt, allStr.IndexOf("]", startInt) - startInt + 1);

                string[] strs = this.richSetup.Text.Split(';');
                foreach (string str in strs)
                {
                    string[] props = str.Split(':');
                    if (subStr.Contains(props[0].Trim().Trim('\n')))
                    {

                        if (subStr.Contains("$[@"))
                        {
                            switch (subStr)
                            {
                                case "$[@tr$]":
                                    //allStr = allStr.Replace(subStr, props[1]+"neibu");
                                    int colCount = 1;
                                    string[] rowsptop = props[1].Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                    colCount = int.Parse(rowsptop[0]);
                                    string replaceStr = "";
                                    string allreplaceStr = "";


                                    for (int j = 1; j < rowsptop.Count(); j++)
                                    {
                                        string[] rowsp = rowsptop[j].Trim().Trim(new Char[] { '\r', '\n' }).Split(new string[] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 0; i < rowsp.Count(); i++)
                                        {
                                            if (rowsp[i].Trim().StartsWith("mb"))
                                            {
                                                string mbName = rowsp[i].Trim().Substring(0, rowsp[i].IndexOf("["));

                                                foreach (ComboBoxItem item in mobComb.Items)
                                                {
                                                    if (item.Text.Equals(mbName))
                                                    {
                                                        replaceStr = item.Value.ToString();
                                                        if (rowsp.Count() == 1)
                                                        {
                                                            replaceStr = replaceStr.Replace("colspan=\"1\"", "colspan=\"" + colCount + "\"");
                                                        }
                                                        else
                                                        {
                                                            string colspStr = rowsp[i].Substring(rowsp[i].IndexOf("]") + 1);
                                                            if (!string.IsNullOrEmpty(colspStr))
                                                            {
                                                                replaceStr = replaceStr.Replace("colspan=\"1\"", "colspan=\"" + colspStr + "\"");
                                                            }
                                                        }

                                                        break;
                                                    }
                                                }

                                                string submb = subStrSE(rowsp[i], "[", "]");
                                                if (!string.IsNullOrEmpty(submb))
                                                {
                                                    if (submb.Contains("->"))
                                                    {
                                                        string[] ppsub = submb.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                                        foreach (string psub in ppsub)
                                                        {
                                                            string[] psmsubs = psub.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                                                            replaceStr = replaceStr.Replace("$[" + psmsubs[0] + "$]", psmsubs[1]);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        replaceStr = replaceStr.Replace(subStrSE(replaceStr, "$[", "$]", true), submb);
                                                    }

                                                }
                                                else
                                                {

                                                }

                                            }
                                            else if (rowsp[i].Trim().StartsWith("mt"))//no start mb
                                            {

                                            }
                                            else if (rowsp[i].Trim().StartsWith("tab"))
                                            {

                                            }
                                            else
                                            {
                                                foreach (ComboBoxItem item in mobComb.Items)
                                                {
                                                    if (rowsp.Count() == 1&&item.Text.Equals("mb1"))
                                                    {
                                                        replaceStr = item.Value.ToString();
                                                        break;
                                                    }
                                                    else if (rowsp.Count() > 1&&item.Text.Equals("mt1"))
                                                    {
                                                        replaceStr = item.Value.ToString();
                                                        break;
                                                    }
                                                }

                                                string endStrrow = rowsp[i].Trim();
                                                if (rowsp[i].Trim().Length > 3)
                                                    endStrrow = rowsp[i].Trim().Substring(rowsp[i].Trim().Length - 3);
                                                string strStrt = "";
                                                switch (endStrrow)
                                                {
                                                    case "Str":

                                                        foreach (ComboBoxItem item in mobComb.Items)
                                                        {
                                                            if (item.Text.Equals("Str"))
                                                            {
                                                                strStrt = item.Value.ToString();
                                                                break;
                                                            }
                                                        }

                                                        strStrt = strStrt.Replace("$[$]", rowsp[i].Trim());
                                                        replaceStr = replaceStr.Replace(subStrSE(replaceStr, "$[", "$]", true), strStrt);
                                                        break;

                                                    case "Dat":

                                                        foreach (ComboBoxItem item in mobComb.Items)
                                                        {
                                                            if (item.Text.Equals("Dat"))
                                                            {
                                                                strStrt = item.Value.ToString();
                                                                break;
                                                            }
                                                        }

                                                        strStrt = strStrt.Replace("$[$]", rowsp[i].Trim());
                                                        replaceStr = replaceStr.Replace(subStrSE(replaceStr, "$[", "$]", true), strStrt);
                                                        break;

                                                    default:

                                                        replaceStr = replaceStr.Replace(subStrSE(replaceStr, "$[", "$]", true), rowsp[i].Trim());
                                                        break;
                                                }

                                                if (rowsp.Count() == 1)
                                                {
                                                    replaceStr = replaceStr.Replace("colspan=\"1\"", "colspan=\"" + colCount + "\"");
                                                }
                                                else
                                                {
                                                    string colspStr = rowsp[i].Substring(rowsp[i].Length - 1);
                                                    int tempint = 1;
                                                    if (!string.IsNullOrEmpty(colspStr) && int.TryParse(colspStr, out tempint))
                                                    {
                                                        replaceStr = replaceStr.Replace("colspan=\"1\"", "colspan=\"" + colspStr + "\"");
                                                    }else
                                                    {
                                                        if(rowsp[i].Length>4)
                                                        {
                                                            string ccStr = rowsp[i].Substring(rowsp[i].Length - 4, 1);
                                                            if (!string.IsNullOrEmpty(ccStr) && int.TryParse(ccStr, out tempint))
                                                            {
                                                                replaceStr = replaceStr.Replace("colspan=\"1\"", "colspan=\"" + ccStr + "\"");
                                                            }
                                                        }
                                                        
                                                    }
                                                }
                                            }
                                            allreplaceStr += replaceStr;
                                        }

                                        allreplaceStr = "<tr>" + allreplaceStr + "</tr>";

                                    }
                                    allStr = allStr.Replace(subStr, allreplaceStr);
                                    break;
                                default:
                                    break;

                            }
                            break;
                        }

                        allStr = allStr.Replace(subStr, props[1]);
                        break;
                    }
                }

                startInt = allStr.IndexOf("$[", 0);
                TestStr += subStr + "\n";
            }
            this.richTextBox1.Text = allStr;
            this.webB1.DocumentText = allStr;

            //fs.Flush();
            sr.Close();
            fs.Close();
        }

        private void mobBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Word文档(*.doc)|*.doc|其它文档(*.*)|*.*";
            open.Filter = "所有文档(*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.mobComb.Items.Clear();
                this.MobTxt.Text = open.FileName;

                FileStream fs = new FileStream(open.FileName, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                string allStr = sr.ReadToEnd();
                string[] comStr = allStr.Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string coms in comStr)
                {

                    string[] ccs = coms.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                    this.mobComb.Items.Add(new ComboBoxItem(ccs[0].Trim(new Char[] { '\r', '\n' }).Trim(), ccs[1]));
                }

                sr.Close();
                fs.Close();

            }
        }

        private void mobComb_SelectedValueChanged(object sender, EventArgs e)
        {
            this.richMob.Text = ((ComboBoxItem)this.mobComb.SelectedItem).Value.ToString();
            if (richMob.Text.Trim().StartsWith("<td"))
            {
                this.webB1.DocumentText = "<table><tr>" + ((ComboBoxItem)this.mobComb.SelectedItem).Value.ToString() + "</tr></table>";
            }
            else
            {
                this.webB1.DocumentText = ((ComboBoxItem)this.mobComb.SelectedItem).Value.ToString();
            }

        }

        private string subStrSE(string sourceString, string startStr, string endStr, bool isContains = false)
        {
            int startInt = sourceString.IndexOf(startStr);
            if ((sourceString.IndexOf(endStr) - startInt) <= 1)
                return "";
            string retStr = "";
            if (isContains)
            {
                retStr = sourceString.Substring(startInt, sourceString.IndexOf(endStr, startInt) - startInt + endStr.Length);

            }
            else
            {
                retStr = sourceString.Substring(startInt + startStr.Length, sourceString.IndexOf(endStr, startInt) - startInt - 1);
            }
            return retStr;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.webB1.DocumentText = this.richTextBox1.Text;
        }
    }

    public class ComboBoxItem : IComparable
    {
        public ComboBoxItem() { }
        public ComboBoxItem(string text, string value)
        {
            this._text = text;
            this._value = value;
        }
        private string _text = null;
        private object _value = null;
        public string Text { get { return this._text; } set { this._text = value; } }
        public object Value { get { return this._value; } set { this._value = value; } }
        public override string ToString()
        {
            return this._text;
        }

        public int CompareTo(object obj)
        {
            if (obj is ComboBoxItem)
            {
                if (((ComboBoxItem)obj).Text.Equals(this.Text))
                {
                    return 0;
                }
                else
                {
                    return ((ComboBoxItem)obj).Text.CompareTo(this.Text);
                }
            }
            else
            {
                if (((string)obj).Equals(this.Text))
                {
                    return 0;
                }
                else
                {
                    return ((string)obj).CompareTo(this.Text);
                }
            }


        }
    }
}
