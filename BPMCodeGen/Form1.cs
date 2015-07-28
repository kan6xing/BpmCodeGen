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
using System.Collections;
using System.Diagnostics;
namespace BPMCodeGen
{
    public partial class Form1 : Form
    {
        codes.BPMGen bpmg = new codes.BPMGen();
        Dictionary<string, string> mbDic = new Dictionary<string, string>();//组件字典Str,Dat等

        Dictionary<string, string> dicParam = new Dictionary<string, string>();//变量table名称等
        StringBuilder SQLsb = new StringBuilder();
        string subSQLsb="";

        ArrayList alist = new ArrayList();
        
        public Form1()
        {
            InitializeComponent();
            this.txtParam.Text = "tabName=CustName\r\ntab1=CustName1";
            try
            {
                setAllMob();
                setAllFile();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message+"\n"+ex.StackTrace);
            }

            
            
        }

        private void setAllMob(int id=0)
        {
            if(id>0)
            { bpmg.GetModel(id); }
            else
            {
                bpmg.GetModel("IDInt>0 order by createDat desc");
            }
            dicParam.Clear();
            mbDic.Clear();
            this.mobComb.Items.Clear();

            this.textBox1.Text = bpmg.mobStr;
            this.richTextBox1.Text = bpmg.docTxt;
            this.richSetup.Text = bpmg.paramStr;
            this.txtParam.Text = bpmg.TabStr;
            this.MobTxt.Text = bpmg.cmobStr;

            this.textBox2.Text = bpmg.jsStr;
            this.textBox5.Text = bpmg.ashxStr;
            this.textBox6.Text = bpmg.deleteStr;
            this.richTextBox3.Text = bpmg.gridParamStr;

            string[] paramStrs = this.txtParam.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string strs in paramStrs)
            {
                string[] ss = strs.Trim().Split('=');
                dicParam.Add(ss[0].Trim(), ss[1].Trim());
            }

            //模板下拉框设置
            //mbDic.Clear();
            //this.mobComb.Items.Clear();
            try
            {
                FileStream fs = new FileStream(this.MobTxt.Text, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                string allStr = sr.ReadToEnd();
                string[] comStr = allStr.Split(new string[] { "@@" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string coms in comStr)
                {

                    string[] ccs = coms.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                    this.mobComb.Items.Add(new ComboBoxItem(ccs[0].Trim(new Char[] { '\r', '\n' }).Trim(), ccs[1]));
                    mbDic.Add(ccs[0].Trim(new Char[] { '\r', '\n' }).Trim(), ccs[1]);
                }

                sr.Close();
                fs.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("模板位置异常");
            }
            

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

            //**************生成Sql********************
            SQLsb.Clear();
            alist.Clear();
            SQLsb.Append("create table " + dicParam["tabName"]+" (TaskID int PRIMARY KEY,");
           
            


            dicParam.Clear();
            string[] paramStrs = this.txtParam.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string strs in paramStrs)
            {
                string[] ss = strs.Trim().Split('=');
                dicParam.Add(ss[0].Trim(), ss[1].Trim());
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
                    if(props.Length>1)
                    {
                        props[1] = props[1].Trim().Trim('\n');
                    }
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
                                    string allreplaceStrS = "";


                                    for (int j = 1; j < rowsptop.Count(); j++)
                                    {
                                        string[] rowsp = rowsptop[j].Trim().Trim(new Char[] { '\r', '\n' }).Split(new string[] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                                        allreplaceStr = "";
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
                                                string mbName = rowsp[i].Trim().Substring(0, rowsp[i].IndexOf("["));
                                                replaceStr = mbDic[mbName];
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
                                            else if (rowsp[i].Trim().StartsWith("tab"))
                                            {//**********************    tab1   *********************
                                                //***********行>>列-*********
                                                string subTabstr = rowsp[i].Trim().Substring(0, rowsp[i].Trim().IndexOf('['));
                                                //*************************************     insert     *********
                                                if (!subSQLsb.Contains(dicParam[subTabstr]))
                                                {
                                                    if(subSQLsb.EndsWith(","))
                                                    {
                                                        subSQLsb = subSQLsb.Substring(0, subSQLsb.Length-1);
                                                    }
                                                    subSQLsb += "\r\n create table " + dicParam[subTabstr] + " (ItemID int identity(1,1) PRIMARY KEY,TaskID int,";
                                                }
                                                replaceStr = mbDic["mt1"].Replace("colspan=\"1\"", "colspan=\"" + colCount + "\"");
                                                if(mbDic.ContainsKey(subTabstr))
                                                {
                                                    replaceStr = replaceStr.Replace("$[col$]", mbDic[subTabstr]);
                                                }else
                                                {
                                                    replaceStr = replaceStr.Replace("$[col$]", mbDic["tab1"]);
                                                }
                                                


                                                string[] tabStrs = subStrSE(rowsp[i].Trim(), "[", "]").Split(new String[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                                                string TTrow = "";
                                                string TTrowS = "";
                                                for (int x = 0; x < tabStrs.Length; x++)
                                                {
                                                    TTrow = "";

                                                    if (x == 0)
                                                    {
                                                        foreach (string ss in tabStrs[x].Split('-'))
                                                        {
                                                            TTrow += mbDic["mt1"].Replace("$[col$]", ss);
                                                        }
                                                    }
                                                    else
                                                    {

                                                        foreach (string ss in tabStrs[x].Split('-'))
                                                        {
                                                            string endStrrow = ss.Trim();
                                                            if (ss.Trim().Length > 3)
                                                                endStrrow = ss.Trim().Substring(ss.Trim().Length - 3);
                                                            

                                                            TTrow += GetCompStr(endStrrow, mbDic["mt1"], subTabstr, ss,false);


                                                        }

                                                    }

                                                    TTrowS += "<tr>" + TTrow + "</tr>";
                                                }
                                                replaceStr = replaceStr.Replace("$[$]", TTrowS);

                                            }
                                            else
                                            {
                                                foreach (ComboBoxItem item in mobComb.Items)
                                                {
                                                    if (rowsp.Count() == 1 && item.Text.Equals("mb1"))
                                                    {
                                                        replaceStr = item.Value.ToString();
                                                        break;
                                                    }
                                                    else if (rowsp.Count() > 1 && item.Text.Equals("mt1"))
                                                    {
                                                        replaceStr = item.Value.ToString();
                                                        break;
                                                    }
                                                }

                                                string endStrrow = rowsp[i].Trim();
                                                if (rowsp[i].Trim().Length > 3)
                                                    endStrrow = rowsp[i].Trim().Substring(rowsp[i].Trim().Length - 3);
                                                

                                                replaceStr = GetCompStr(endStrrow, replaceStr, "tabName", rowsp[i].Trim());

                                                if (rowsp.Count() == 1)
                                                {
                                                    replaceStr = replaceStr.Replace("colspan=\"1\"", "colspan=\"" + colCount + "\"");
                                                }
                                                else
                                                {
                                                    string colspStr = rowsp[i].Substring(rowsp[i].Length - 1);
                                                    int tempint = 1;
                                                    if (!string.IsNullOrEmpty(colspStr) && int.TryParse(colspStr, out tempint)&&tempint>1)
                                                    {
                                                        replaceStr = replaceStr.Replace("colspan=\"1\"", "colspan=\"" + colspStr + "\"");
                                                    }
                                                    else
                                                    {
                                                        if (rowsp[i].Length > 4)
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

                                        allreplaceStrS += "<tr>" + allreplaceStr + "</tr>";

                                    }

                                    
                                    allStr = allStr.Replace(subStr, allreplaceStrS);
                                    break;
                                default:
                                    break;

                            }
                            break;
                        }

                        if (props[0].Contains("$[#"))
                        {
                            allStr = allStr.Replace(subStr, dicParam["tabName"] + "." + props[1]);
                            SQLsb.Append(props[1] + "  nvarchar(Max),");
                            alist.Add(props[1]);
                        }
                        allStr = allStr.Replace(subStr, props[1]);
                        break;
                    }
                }

                startInt = allStr.IndexOf("$[", 0);
                TestStr += subStr + "\n";
            }
            SQLsb.Remove(SQLsb.Length - 1, 1).Append(")");
            if(!string.IsNullOrEmpty(subSQLsb))
            {
                subSQLsb = subSQLsb.Substring(0, subSQLsb.Length - 1) + ")";
                
            }
            this.sqlTxt.Text = SQLsb.ToString() + subSQLsb??"";
            this.richTextBox1.Text = allStr;
            this.webB1.DocumentText = allStr;

            string allField = "";
            foreach(string s in alist)
            {
                allField += s + "--";
            }
            this.textBox4.Text = allField;

            //************************         会追加         暂时保留，可先进行判断************8
            if (richTextBox3.Text.Contains("$[tabName,表名$]:" + dicParam["tabName"]))
            { }
            else
            {
                richTextBox3.Text = richTextBox3.Text.Replace("$[tabName,表名$]:", "$[tabName,表名$]:" + dicParam["tabName"]).Replace(";$[formName,表单应用$]:", ";$[formName,表单应用$]:" + bpmg.NameStr).Replace(";$[formName,流程名$]:", ";$[formName,流程名$]:" + bpmg.NameStr);
            }
            
            //fs.Flush();
            sr.Close();
            fs.Close();
        }

        private void mobBtn_Click(object sender, EventArgs e)
        {
            
            mbDic.Clear();
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
                    mbDic.Add(ccs[0].Trim(new Char[] { '\r', '\n' }).Trim(), ccs[1]);
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

        /// <summary>
        /// 查找替换组件
        /// </summary>
        /// <param name="endStrrow">组件名称</param>
        /// <param name="TTrow">替换的字符串</param>
        /// <param name="subTabstr">变量名称（表名）</param>
        /// <param name="ss">如果不是组件取的整个文本</param>
        /// <returns></returns>
        private string GetCompStr(string endStrrow, string TTrow, string subTabstr, string ss,bool isMain=true)
        {
            string strStrt = "";

            //if(subTabstr.IndexOf("tab")>=0)
            //{
            //    if (mbDic.ContainsKey(subTabstr))
            //    {
                    
            //    }
            //    else
            //    {
            //        subTabstr = "tab1";
            //    }
            //}

            switch (endStrrow)
            {
                case "GLO":
                    return TTrow.Replace(subStrSE(TTrow, "$[", "$]", true), mbDic["GLO"]);

                case "Str":
                case "St1":

                    strStrt = mbDic[endStrrow];

                    strStrt = strStrt.Replace("$[$]", dicParam[subTabstr] + "." + ss).Replace("$[id$]", dicParam[subTabstr] + ss);
                    //replaceStr = replaceStr.Replace(subStrSE(replaceStr, "$[", "$]", true), strStrt);
                    if(isMain)
                    {
                        SQLsb.Append(ss + " nvarchar(MAX),");
                        alist.Add(ss);
                    }else
                    {
                        subSQLsb += ss + " nvarchar(MAX),";
                    }
                    
                    return TTrow.Replace(subStrSE(TTrow, "$[", "$]", true), strStrt);
                    break;

                case "Dat":
                case "Da1":
                    strStrt = mbDic[endStrrow];

                    strStrt = strStrt.Replace("$[$]", dicParam[subTabstr] + "." + ss).Replace("$[id$]", dicParam[subTabstr] + ss);
                    //replaceStr = replaceStr.Replace(subStrSE(replaceStr, "$[", "$]", true), strStrt);
                    if (isMain)
                    {
                        SQLsb.Append(ss + " smalldatetime,");
                        alist.Add(ss);
                    }
                    else
                    {
                        subSQLsb += ss + " smalldatetime,";
                    }
                    
                    return TTrow.Replace(subStrSE(TTrow, "$[", "$]", true), strStrt);

                    break;
                case "Txt":
                case "Tx1":
                    strStrt = mbDic[endStrrow];
                    TTrow = mbDic["mt1"];
                    strStrt = strStrt.Replace("$[$]", dicParam[subTabstr] + "." + ss).Replace("$[id$]", dicParam[subTabstr] + ss);
                    if (isMain)
                    {
                        SQLsb.Append(ss + " nvarchar(MAX),");
                        alist.Add(ss);
                    }
                    else
                    {
                        subSQLsb += ss + " nvarchar(MAX),";
                    }
                    
                    return TTrow.Replace(subStrSE(TTrow, "$[", "$]", true), strStrt);
                    break;

                default:
                    if(mbDic.ContainsKey(endStrrow))
                    {//扩展不用改代码
                        strStrt = mbDic[endStrrow];
                        strStrt = strStrt.Replace("$[$]", dicParam[subTabstr] + "." + ss).Replace("$[id$]", dicParam[subTabstr] + ss);
                        switch(endStrrow)
                        {
                            case "Int":
                            case "In1":
                                if (isMain)
                                {
                                    SQLsb.Append(ss + " int,");
                                    alist.Add(ss);
                                }
                                else
                                {
                                    subSQLsb += ss + " int,";
                                }
                                
                                break;
                            case "Dec":
                            case "De1":
                                if (isMain)
                                {
                                    SQLsb.Append(ss + " decimal(18,2),");
                                    alist.Add(ss);
                                }
                                else
                                {
                                    subSQLsb += ss + " decimal(18,2),";
                                }
                                
                                break;

                            default:
                                if (isMain)
                                {
                                    SQLsb.Append(ss + " nvarchar(MAX),");
                                    alist.Add(ss);
                                }
                                else
                                {
                                    subSQLsb += ss + " nvarchar(MAX),";
                                }
                                break;
                        }
                        return TTrow.Replace(subStrSE(TTrow, "$[", "$]", true), strStrt);
                    }
                    return TTrow.Replace(subStrSE(TTrow, "$[", "$]", true), ss);
                    break;
            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(string.IsNullOrEmpty(this.textBox1.Text)||string.IsNullOrEmpty(this.MobTxt.Text))
            {
                return;
            }
            DialogResult dr = MessageBox.Show("保存退出", "保存", MessageBoxButtons.YesNoCancel);
            if(dr==DialogResult.Cancel)
            {
                e.Cancel = true;
            }else if(dr==DialogResult.Yes)
            {
                savetoDB();
                //codes.BPMGen bpmg = new codes.BPMGen();
                //if(bpmg.Exists(this.textBox1.Text,this.MobTxt.Text))
                //{
                    //DialogResult drz = MessageBox.Show("要覆盖吗？（是-覆盖，否-新增，取消-不保存）", "", MessageBoxButtons.YesNoCancel);
                    //if(drz==DialogResult.Yes)
                    //{
                    //    //bpmg.GetModel("mobStr='"+this.textBox1.Text+"' and cmobStr='"+this.MobTxt.Text+"'");
                    //    bpmg.mobStr = this.textBox1.Text;
                    //    bpmg.cmobStr = this.MobTxt.Text;
                    //    bpmg.docTxt = this.richTextBox1.Text;
                    //    bpmg.TabStr = this.txtParam.Text;
                    //    bpmg.paramStr = this.richSetup.Text;
                    //    bpmg.Update();
                    //}
                    //else if (drz == DialogResult.No)
                    //{
                    //    bpmg.mobStr = this.textBox1.Text;
                    //    bpmg.cmobStr = this.MobTxt.Text;
                    //    bpmg.docTxt = this.richTextBox1.Text;
                    //    bpmg.TabStr = this.txtParam.Text;
                    //    bpmg.paramStr = this.richSetup.Text;
                    //    bpmg.Add();
                    //}else
                    //{
                    //    //不保存
                    //}
                //}else
                //{
                //    bpmg.mobStr = this.textBox1.Text;
                //    bpmg.cmobStr = this.MobTxt.Text;
                //    bpmg.docTxt = this.richTextBox1.Text;
                //    bpmg.TabStr = this.txtParam.Text;
                //    bpmg.paramStr = this.richSetup.Text;
                //    bpmg.Add();
                //}
            }
        }

        private void savedbBtn_Click(object sender, EventArgs e)
        {
            savetoDB();
        }

        private void savetoDB()
        {
            DialogResult drz;
            string titleName=subStrSE(this.richSetup.Text, ":", "\n").Trim();
            if(titleName.Equals(bpmg.NameStr))
            {
                drz = MessageBox.Show("要覆盖吗？（是-覆盖，否-新增，取消-不保存）", "", MessageBoxButtons.YesNoCancel);
                
            }else
            {
                drz = MessageBox.Show("注意标题不一样!\n 要覆盖吗？（是-覆盖，否-新增，取消-不保存）", "注意", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                
            }
            
            if (drz == DialogResult.Yes)
            {
                //bpmg.GetModel("mobStr='"+this.textBox1.Text+"' and cmobStr='"+this.MobTxt.Text+"'");
                bpmg.mobStr = this.textBox1.Text;
                bpmg.cmobStr = this.MobTxt.Text;
                bpmg.docTxt = this.richTextBox1.Text;
                bpmg.TabStr = this.txtParam.Text;
                bpmg.paramStr = this.richSetup.Text;
                bpmg.NameStr = titleName;

                bpmg.jsStr = this.textBox2.Text;
                bpmg.ashxStr = this.textBox5.Text;
                bpmg.deleteStr = this.textBox6.Text;
                bpmg.gridParamStr = this.richTextBox3.Text;
                bpmg.Update();
            }
            else if (drz == DialogResult.No)
            {
                bpmg.mobStr = this.textBox1.Text;
                bpmg.cmobStr = this.MobTxt.Text;
                bpmg.docTxt = this.richTextBox1.Text;
                bpmg.TabStr = this.txtParam.Text;
                bpmg.paramStr = this.richSetup.Text;
                bpmg.NameStr = titleName;

                bpmg.jsStr = this.textBox2.Text;
                bpmg.ashxStr = this.textBox5.Text;
                bpmg.deleteStr = this.textBox6.Text;
                bpmg.gridParamStr = this.richTextBox3.Text;
                bpmg.Add();
                bpmg.GetModel("IDInt>0 order by createDat desc");
            }
            else
            {
                //不保存
            }
        }

        private void 历史仓库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            historyWin hist = new historyWin();
            hist.ShowDialog();
            if(hist.idInt>0)
            {
                setAllMob(hist.idInt);
            }
            //MessageBox.Show("aaaaaa");
        }


        private void setAllFile()
        {
            if(!string.IsNullOrWhiteSpace(this.textBox2.Text))
            {
                FileStream fs = new FileStream(this.textBox2.Text, FileMode.Open);
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
                        if (!TestStr.Contains(subStr))
                            TestStr += ";" + subStr + ":\n";
                    }
                }
                this.richTextBox2.Text = TestStr + allStr;
                this.richTextBox3.Text = TestStr.Trim();

                fs.Flush();
                sr.Close();
                fs.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Word文档(*.doc)|*.doc|其它文档(*.*)|*.*";
            open.Filter = "所有文档(*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = open.FileName;
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
                        if(!TestStr.Contains(subStr))
                        TestStr += ";" + subStr + ":\n";
                    }
                }
                this.richTextBox2.Text = TestStr + allStr;
                this.richTextBox3.Text = TestStr.Trim();
               
                fs.Flush();
                sr.Close();
                fs.Close();
            }
        }

        private string openFileGetSet(string fileName)
        {
            

            
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string TestStr = richTextBox3.Text+"\n";
            try{
                string allStr = sr.ReadToEnd();
                int startInt = 0;
                startInt = allStr.IndexOf("$[", startInt);

                
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
                        if (!TestStr.Contains(subStr))
                            TestStr += ";" + subStr + ":\n";
                    }
                }

               
            }catch(Exception ex)
            {
                
            }finally{
                fs.Flush();
                sr.Close();
                fs.Close();
                
            }
            return TestStr;
            
            //this.richTextBox2.Text = TestStr + allStr;
            //this.richTextBox3.Text = TestStr.Trim();

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.richTextBox2.Text.Trim().Equals(""))
            {
                return;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text.Trim()))
            {
                MessageBox.Show("请选择一个模板");
                return;
            }

            //**************生成Sql********************
            //SQLsb.Clear();
            //SQLsb.Append("create table " + dicParam["tabName"] + " (TaskID int PRIMARY KEY,");




            //dicParam.Clear();
            //string[] paramStrs = this.txtParam.Text.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (string strs in paramStrs)
            //{
            //    string[] ss = strs.Trim().Split('=');
            //    dicParam.Add(ss[0].Trim(), ss[1].Trim());
            //}


            string fileName = this.textBox2.Text;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            string allStr = sr.ReadToEnd();
            int startInt = 0;
            startInt = allStr.IndexOf("$[", startInt);

            string TestStr = "";
            while (startInt > 0)
            {
                string subStr = allStr.Substring(startInt, allStr.IndexOf("]", startInt) - startInt + 1);

                string[] strs = this.richTextBox3.Text.Split(';');
                foreach (string str in strs)
                {
                    string[] props = str.Split(':');
                    if (props.Length > 1)
                    {
                        props[1] = props[1].Trim().Trim('\n');
                    }
                    if (subStr.Contains(props[0].Trim().Trim('\n')))
                    {

                        if (subStr.Contains("$[@"))
                        {
                            switch (subStr)
                            {
                                case "$[@rows$]":
                                    //allStr = allStr.Replace(subStr, props[1]+"neibu");
                                    string TempFStr = "";
                                    string[] FStrs;
                                    if(props.Count()>1)
                                    {
                                        FStrs = props[1].Trim('\n').Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
                                    }else
                                    {
                                        FStrs = this.textBox4.Text.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
                                    }
                                    
                                    if(FStrs.Count()>0)
                                    { 
                                        for(int i=0;i<FStrs.Count();i++)
                                        {
                                            if(i!=FStrs.Count()-1)
                                            TempFStr += "{ name: '"+FStrs[i]+"' },";
                                            else
                                            {
                                                TempFStr += "{ name: '" + FStrs[i] + "' }";
                                            }
                                        }
                                    }
                                    allStr = allStr.Replace(subStr, TempFStr);
                                    break;
                                case "$[@rows1$]":
                                    TempFStr = "";
                                    FStrs = props[1].Trim('\n').Split(new string[]{"--"},StringSplitOptions.RemoveEmptyEntries);
                                    if(FStrs.Count()>0)
                                    { 
                                        for(int i=0;i<FStrs.Count();i++)
                                        {
                                            string[] tfs = FStrs[i].Split('*');
                                            if(i!=FStrs.Count()-1)
                                            {
                                                TempFStr += "{ header: '" + tfs[1] + "', dataIndex: '" + tfs[0] + "', width: 150, align: 'left' },";
                                                
                                            }
                                            else
                                            {
                                                TempFStr += "{ header: '" + tfs[1] + "', dataIndex: '" + tfs[0] + "', id: 'extcol', align: 'left' }";
                                            }
                                        }
                                    }
                                    allStr = allStr.Replace(subStr, TempFStr);
                                    break;
                                default:
                                    allStr = allStr.Replace(subStr, props[1]);
                                    break;

                            }
                            
                            break;
                        }

                        if (props[0].Contains("$[#"))
                        {
                            allStr = allStr.Replace(subStr, dicParam["tabName"] + "." + props[1]);
                            SQLsb.Append(props[1] + "  nvarchar(Max),");
                        }
                        allStr = allStr.Replace(subStr, props[1]);
                        break;
                    }
                }

                startInt = allStr.IndexOf("$[", 0);
                TestStr += subStr + "\n";
            }
            //SQLsb.Remove(SQLsb.Length - 1, 1).Append(")");
            //this.sqlTxt.Text = SQLsb.ToString();
            this.richTextBox2.Text = allStr;
            //this.webB1.DocumentText = allStr;

            //fs.Flush();
            sr.Close();
            fs.Close();
            MessageBox.Show("生成完毕");
        }

        private string GenCode(string filePath)
        {
            string fileName = filePath;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            string allStr = sr.ReadToEnd();
            int startInt = 0;
            startInt = allStr.IndexOf("$[", startInt);

            string TestStr = "";
            while (startInt > 0)
            {
                string subStr = allStr.Substring(startInt, allStr.IndexOf("]", startInt) - startInt + 1);

                string[] strs = this.richTextBox3.Text.Split(';');
                foreach (string str in strs)
                {
                    string[] props = str.Split(':');
                    if (props.Length > 1)
                    {
                        props[1] = props[1].Trim().Trim('\n');
                    }
                    if (subStr.Contains(props[0].Trim().Trim('\n')))
                    {
                      
                        if (subStr.Contains("$[@"))
                        {
                            switch (subStr)
                            {
                                case "$[@rows$]":
                                    //allStr = allStr.Replace(subStr, props[1]+"neibu");
                                    string TempFStr = "";
                                    string[] FStrs;
                                    if (props.Count() > 1)
                                    {
                                        FStrs = props[1].Trim('\n').Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
                                    }
                                    else
                                    {
                                        FStrs = this.textBox4.Text.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
                                    }

                                    if (FStrs.Count() > 0)
                                    {
                                        for (int i = 0; i < FStrs.Count(); i++)
                                        {
                                            if (i != FStrs.Count() - 1)
                                                TempFStr += "{ name: '" + FStrs[i] + "' },";
                                            else
                                            {
                                                TempFStr += "{ name: '" + FStrs[i] + "' }";
                                            }
                                        }
                                    }
                                    allStr = allStr.Replace(subStr, TempFStr);
                                    break;
                                case "$[@rows2$]":
                                    TempFStr = "";
                                    FStrs = props[1].Trim('\n').Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (FStrs.Count() > 0)
                                    {
                                        for (int i = 0; i < FStrs.Count(); i++)
                                        {
                                            if (i != FStrs.Count() - 1)
                                            {
                                                TempFStr += FStrs[i] + " LIKE N'%{0}%' or ";
                                            } 
                                            else
                                            {
                                                TempFStr += FStrs[i] + " LIKE N'%{0}%' ";
                                            }
                                        }
                                    }
                                    allStr = allStr.Replace(subStr, TempFStr);
                                    break;
                                case "$[@rows1$]":
                                    TempFStr = "";
                                    FStrs = props[1].Trim('\n').Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
                                    if (FStrs.Count() > 0)
                                    {
                                        for (int i = 0; i < FStrs.Count(); i++)
                                        {
                                            string[] tfs = FStrs[i].Split('*');
                                            if (i != FStrs.Count() - 1)
                                            {
                                                TempFStr += "{ header: '" + tfs[1] + "', dataIndex: '" + tfs[0] + "', width: 150, align: 'left' },";

                                            }
                                            else
                                            {
                                                TempFStr += "{ header: '" + tfs[1] + "', dataIndex: '" + tfs[0] + "', id: 'extcol', align: 'left' }";
                                            }
                                        }
                                    }
                                    allStr = allStr.Replace(subStr, TempFStr);
                                    break;
                                case "$[@rows3$]":
                                    //allStr = allStr.Replace(subStr, props[1]+"neibu");
                                    
                                    TempFStr = "";
                                    FStrs = props[1].Trim('\n').Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);

                                    if (FStrs.Count() > 0)
                                    {
                                        for (int i = 0; i < FStrs.Count(); i++)
                                        {
                                            string endStr = FStrs[i].Substring(FStrs[i].Length - 3);
                                            switch(endStr)
                                            {
                                                case "Txt":
                                                case "Str":
                                                case "Tx1":
                                                case "St1":
                                                    TempFStr += "item.Attributes.Add(\""+FStrs[i]+"\", Convert.ToString(reader[\""+FStrs[i]+"\"]));\n";
                                                    break;
                                                case "Int":
                                                case "In1":
                                                    TempFStr += "item.Attributes.Add(\"" + FStrs[i] + "\", Convert.ToInt32(reader[\"" + FStrs[i] + "\"]));\n";
                                                    break;
                                                case "Dat":
                                                case "Da1":
                                                    TempFStr += "item.Attributes.Add(\"" + FStrs[i] + "\", Convert.ToDateTime(reader[\"" + FStrs[i] + "\"]).ToString(\"yyyy-MM-dd\"));\n";
                                                    break;
                                                case "Dec":
                                                case "De1":
                                                    TempFStr += "item.Attributes.Add(\"" + FStrs[i] + "\", Convert.ToDecimal(reader[\"" + FStrs[i] + "\"]));\n";
                                                    break;
                                                default:
                                                    TempFStr += "item.Attributes.Add(\"" + FStrs[i] + "\", Convert.ToString(reader[\"" + FStrs[i] + "\"]));\n";
                                                    break;
                                            }
                                            
                                        }
                                    }
                                    allStr = allStr.Replace(subStr, TempFStr);
                                    break;
                                default:
                                    allStr = allStr.Replace(subStr, props[1].Trim('\n'));
                                    break;

                            }

                            break;
                        }

                        if (props[0].Contains("$[#"))
                        {
                            allStr = allStr.Replace(subStr, dicParam["tabName"] + "." + props[1].Trim('\n'));
                            SQLsb.Append(props[1] + "  nvarchar(Max),");
                        }
                        allStr = allStr.Replace(subStr, props[1].Trim('\n'));
                        break;
                    }
                }

                startInt = allStr.IndexOf("$[", 0);
                TestStr += subStr + "\n";
            }
            //SQLsb.Remove(SQLsb.Length - 1, 1).Append(")");
            //this.sqlTxt.Text = SQLsb.ToString();
            
            //this.webB1.DocumentText = allStr;

            //fs.Flush();
            sr.Close();
            fs.Close();
            return allStr;
        }
        private void button7_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Word文档(*.doc)|*.doc|其它文档(*.*)|*.*";
            open.Filter = "所有文档(*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.textBox5.Text = open.FileName;
                this.richTextBox3.Text = openFileGetSet(open.FileName);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Word文档(*.doc)|*.doc|其它文档(*.*)|*.*";
            open.Filter = "所有文档(*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.textBox6.Text = open.FileName;
                this.richTextBox3.Text = openFileGetSet(open.FileName);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string currDir = System.Environment.CurrentDirectory + "/codes/" + "/" + dicParam["tabName"] + "/";
            if(!Directory.Exists(currDir))
            {
                Directory.CreateDirectory(currDir);
            }

            StreamWriter sw = new StreamWriter(currDir + dicParam["tabName"] + ".aspx", false, Encoding.UTF8);
            sw.Write(this.richTextBox1.Text);
            sw.Flush();
            sw.Close();
            MessageBox.Show("aspx成功");

            currDir = System.Environment.CurrentDirectory + "/codes/" + "/" + dicParam["tabName"] + "/Modules/";
            if (!Directory.Exists(currDir))
            {
                Directory.CreateDirectory(currDir);
            }
            sw = new StreamWriter(currDir + dicParam["tabName"] + ".js", false, Encoding.UTF8);
            sw.Write(GenCode(this.textBox2.Text));
            sw.Flush();
            sw.Close();
            MessageBox.Show("Modules成功");

            currDir = System.Environment.CurrentDirectory + "/codes/" + "/" + dicParam["tabName"] + "/StoreDataService/";
            if (!Directory.Exists(currDir))
            {
                Directory.CreateDirectory(currDir);
            }
            sw = new StreamWriter(currDir + dicParam["tabName"] + ".ashx", false, Encoding.UTF8);
            sw.Write(GenCode(this.textBox5.Text));
            sw.Flush();
            sw.Close();
            MessageBox.Show("StoreDataService成功");

            currDir = System.Environment.CurrentDirectory + "/codes/" + "/" + dicParam["tabName"] + "/RFC/";
            if (!Directory.Exists(currDir))
            {
                Directory.CreateDirectory(currDir);
            }
            sw = new StreamWriter(currDir + "Delete_"+dicParam["tabName"] + ".ashx", false, Encoding.UTF8);
            sw.Write(GenCode(this.textBox6.Text));
            sw.Flush();
            sw.Close();

            MessageBox.Show("成功");
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo pstart = new ProcessStartInfo();
            pstart.FileName = System.Environment.CurrentDirectory + "/codes/" + "/" + dicParam["tabName"] + "/";
           
            if (!Directory.Exists(pstart.FileName))
            {
                Directory.CreateDirectory(pstart.FileName);
            }
            pstart.Verb = "Open";
            p.StartInfo = pstart;
            p.Start();
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
