using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maticsoft.DBUtility;
using System.Data;

namespace BPMCodeGen.codes
{
    public class BPMGen
    {
        public BPMGen()
        { }
        #region Model
        private int _idint;
        private string _doctxt;
        private string _paramstr;
        private string _tabstr;
        private string _mobstr;
        private string _cmobstr;
        private DateTime _createDat;
        private string _NameStr;

        private string _jsStr;

        public string jsStr
        {
            get { return _jsStr; }
            set { _jsStr = value; }
        }
        private string _ashxStr;

        public string ashxStr
        {
            get { return _ashxStr; }
            set { _ashxStr = value; }
        }
        private string _deleteStr;

        public string deleteStr
        {
            get { return _deleteStr; }
            set { _deleteStr = value; }
        }
        private string _gridParamStr;

        public string gridParamStr
        {
            get { return _gridParamStr; }
            set { _gridParamStr = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IDInt
        {
            set { _idint = value; }
            get { return _idint; }
        }
        /// <summary>
        /// 主体内容
        /// </summary>
        public string docTxt
        {
            set { _doctxt = value; }
            get { return _doctxt; }
        }
        /// <summary>
        /// 参数内容
        /// </summary>
        public string paramStr
        {
            set { _paramstr = value; }
            get { return _paramstr; }
        }
        /// <summary>
        /// 右侧表参数内容
        /// </summary>
        public string TabStr
        {
            set { _tabstr = value; }
            get { return _tabstr; }
        }
        /// <summary>
        /// 主模板地址
        /// </summary>
        public string mobStr
        {
            set { _mobstr = value; }
            get { return _mobstr; }
        }
        /// <summary>
        /// 详细模板地址
        /// </summary>
        public string cmobStr
        {
            set { _cmobstr = value; }
            get { return _cmobstr; }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createDat
        {
            get;
            set;
        }

        public string NameStr
        {
            get;
            set;
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BPMGen(int IDInt)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select IDInt,docTxt,paramStr,TabStr,mobStr,cmobStr,createDat,NameStr,jsStr,ashxStr,deleteStr,gridParamStr ");
            strSql.Append(" FROM [BPMGen] ");
            strSql.Append(" where IDInt=@IDInt ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@IDInt", OleDbType.Integer,4)};
            parameters[0].Value = IDInt;

            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IDInt"] != null && ds.Tables[0].Rows[0]["IDInt"].ToString() != "")
                {
                    this.IDInt = int.Parse(ds.Tables[0].Rows[0]["IDInt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["docTxt"] != null)
                {
                    this.docTxt = ds.Tables[0].Rows[0]["docTxt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["paramStr"] != null)
                {
                    this.paramStr = ds.Tables[0].Rows[0]["paramStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TabStr"] != null)
                {
                    this.TabStr = ds.Tables[0].Rows[0]["TabStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobStr"] != null)
                {
                    this.mobStr = ds.Tables[0].Rows[0]["mobStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cmobStr"] != null)
                {
                    this.cmobStr = ds.Tables[0].Rows[0]["cmobStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["createDat"] != null)
                {
                    this.createDat = DateTime.Parse(ds.Tables[0].Rows[0]["createDat"].ToString());
                }
                if (ds.Tables[0].Rows[0]["NameStr"] != null)
                {
                    this.NameStr = ds.Tables[0].Rows[0]["NameStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["jsStr"] != null)
                {
                    this.NameStr = ds.Tables[0].Rows[0]["jsStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ashxStr"] != null)
                {
                    this.NameStr = ds.Tables[0].Rows[0]["ashxStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["deleteStr"] != null)
                {
                    this.NameStr = ds.Tables[0].Rows[0]["deleteStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["gridParamStr"] != null)
                {
                    this.NameStr = ds.Tables[0].Rows[0]["gridParamStr"].ToString();
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int IDInt)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [BPMGen]");
            strSql.Append(" where IDInt=@IDInt ");

            OleDbParameter[] parameters = {
					new OleDbParameter("@IDInt", OleDbType.Integer,4)};
            parameters[0].Value = IDInt;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string mobStr, string cmobStr, string NameStr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [BPMGen]");
            strSql.Append(" where mobStr=@mobStr and cmobStr=@cmobStr and NameStr=@NameStr");

            OleDbParameter[] parameters = {
					new OleDbParameter("@mobStr", OleDbType.VarWChar,255),
                    new OleDbParameter("@NameStr", OleDbType.VarWChar,255),
                    new OleDbParameter("@cmobStr", OleDbType.VarWChar,255)};
            parameters[0].Value = mobStr;
            parameters[1].Value = cmobStr;
            parameters[2].Value = NameStr;
            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [BPMGen] (");
            strSql.Append("docTxt,paramStr,TabStr,mobStr,cmobStr,NameStr,jsStr,ashxStr,deleteStr,gridParamStr)");
            strSql.Append(" values (");
            strSql.Append("@docTxt,@paramStr,@TabStr,@mobStr,@cmobStr,@NameStrr,@jsStr,@ashxStr,@deleteStr,@gridParamStr)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@docTxt", OleDbType.LongVarChar),
					new OleDbParameter("@paramStr", OleDbType.LongVarChar),
					new OleDbParameter("@TabStr", OleDbType.LongVarChar),
					new OleDbParameter("@mobStr", OleDbType.VarChar,255),
					new OleDbParameter("@cmobStr", OleDbType.VarChar,255),
                    new OleDbParameter("@NameStr", OleDbType.VarChar,255),
                                          new OleDbParameter("@jsStr", OleDbType.VarChar,255),
                                          new OleDbParameter("@ashxStr", OleDbType.VarChar,255),
                                          new OleDbParameter("@deleteStr", OleDbType.VarChar,255),
                                          new OleDbParameter("@gridParamStr", OleDbType.LongVarChar)};
            parameters[0].Value = docTxt;
            parameters[1].Value = paramStr;
            parameters[2].Value = TabStr;
            parameters[3].Value = mobStr;
            parameters[4].Value = cmobStr;
            parameters[5].Value = NameStr;
            parameters[6].Value = jsStr;
            parameters[7].Value = ashxStr;
            parameters[8].Value = deleteStr;
            parameters[9].Value = gridParamStr;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [BPMGen] set ");
            strSql.Append("docTxt=@docTxt,");
            strSql.Append("paramStr=@paramStr,");
            strSql.Append("TabStr=@TabStr,");
            strSql.Append("mobStr=@mobStr,");
            strSql.Append("createDat=Now(),");
            strSql.Append("cmobStr=@cmobStr,");
            strSql.Append("NameStr=@NameStr,");
            strSql.Append("jsStr=@jsStr,");
            strSql.Append("ashxStr=@ashxStr,");
            strSql.Append("deleteStr=@deleteStr,");
            strSql.Append("gridParamStr=@gridParamStr");
            strSql.Append(" where IDInt=@IDInt ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@docTxt", OleDbType.LongVarChar),
					new OleDbParameter("@paramStr", OleDbType.LongVarChar),
					new OleDbParameter("@TabStr", OleDbType.LongVarChar),
					new OleDbParameter("@mobStr", OleDbType.VarChar,255),
					new OleDbParameter("@cmobStr", OleDbType.VarChar,255),
					new OleDbParameter("@NameStr", OleDbType.VarChar,255),
                    new OleDbParameter("@jsStr", OleDbType.VarChar,255),
                    new OleDbParameter("@ashxStr", OleDbType.VarChar,255),
                    new OleDbParameter("@deleteStr", OleDbType.VarChar,255),
                    new OleDbParameter("@gridParamStr", OleDbType.LongVarChar),
					new OleDbParameter("@IDInt", OleDbType.Integer,4)};
            parameters[0].Value = docTxt;
            parameters[1].Value = paramStr;
            parameters[2].Value = TabStr;
            parameters[3].Value = mobStr;
            parameters[4].Value = cmobStr;
            parameters[5].Value = NameStr;
            parameters[6].Value = jsStr;
            parameters[7].Value = ashxStr;
            parameters[8].Value = deleteStr;
            parameters[9].Value = gridParamStr;
            parameters[10].Value = IDInt;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int IDInt)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [BPMGen] ");
            strSql.Append(" where IDInt=@IDInt ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@IDInt", OleDbType.Integer,4)};
            parameters[0].Value = IDInt;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int IDInt)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select IDInt,docTxt,paramStr,TabStr,mobStr,cmobStr,createDat,NameStr ");
            strSql.Append("select * ");
            strSql.Append(" FROM [BPMGen] ");
            strSql.Append(" where IDInt=@IDInt ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@IDInt", OleDbType.Integer,4)};
            parameters[0].Value = IDInt;

            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IDInt"] != null && ds.Tables[0].Rows[0]["IDInt"].ToString() != "")
                {
                    this.IDInt = int.Parse(ds.Tables[0].Rows[0]["IDInt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["docTxt"] != null)
                {
                    this.docTxt = ds.Tables[0].Rows[0]["docTxt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["paramStr"] != null)
                {
                    this.paramStr = ds.Tables[0].Rows[0]["paramStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TabStr"] != null)
                {
                    this.TabStr = ds.Tables[0].Rows[0]["TabStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobStr"] != null)
                {
                    this.mobStr = ds.Tables[0].Rows[0]["mobStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NameStr"] != null)
                {
                    this.NameStr = ds.Tables[0].Rows[0]["NameStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cmobStr"] != null)
                {
                    this.cmobStr = ds.Tables[0].Rows[0]["cmobStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["jsStr"] != null)
                {
                    this.jsStr = ds.Tables[0].Rows[0]["jsStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ashxStr"] != null)
                {
                    this.ashxStr = ds.Tables[0].Rows[0]["ashxStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["deleteStr"] != null)
                {
                    this.deleteStr = ds.Tables[0].Rows[0]["deleteStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["gridParamStr"] != null)
                {
                    this.gridParamStr = ds.Tables[0].Rows[0]["gridParamStr"].ToString();
                }

                if (ds.Tables[0].Rows[0]["createDat"] != null)
                {
                    this.createDat = DateTime.Parse( ds.Tables[0].Rows[0]["createDat"].ToString());
                }
            }
        }

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public void GetModel()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select IDInt,docTxt,paramStr,TabStr,mobStr,cmobStr,createDat ");
        //    strSql.Append(" FROM [BPMGen] ");
        //    strSql.Append(" where mobStr=@mobStr and cmobStr=@cmobStr  ");
        //    OleDbParameter[] parameters = {
        //            new OleDbParameter("@IDInt", OleDbType.Integer,4)};
        //    parameters[0].Value = IDInt;

        //    DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["IDInt"] != null && ds.Tables[0].Rows[0]["IDInt"].ToString() != "")
        //        {
        //            this.IDInt = int.Parse(ds.Tables[0].Rows[0]["IDInt"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["docTxt"] != null)
        //        {
        //            this.docTxt = ds.Tables[0].Rows[0]["docTxt"].ToString();
        //        }
        //        if (ds.Tables[0].Rows[0]["paramStr"] != null)
        //        {
        //            this.paramStr = ds.Tables[0].Rows[0]["paramStr"].ToString();
        //        }
        //        if (ds.Tables[0].Rows[0]["TabStr"] != null)
        //        {
        //            this.TabStr = ds.Tables[0].Rows[0]["TabStr"].ToString();
        //        }
        //        if (ds.Tables[0].Rows[0]["mobStr"] != null)
        //        {
        //            this.mobStr = ds.Tables[0].Rows[0]["mobStr"].ToString();
        //        }
        //        if (ds.Tables[0].Rows[0]["cmobStr"] != null)
        //        {
        //            this.cmobStr = ds.Tables[0].Rows[0]["cmobStr"].ToString();
        //        }
        //        if (ds.Tables[0].Rows[0]["createDat"] != null)
        //        {
        //            this.createDat = ds.Tables[0].Rows[0]["createDat"].ToString();
        //        }
        //    }
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string whereStr)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select IDInt,docTxt,paramStr,TabStr,mobStr,cmobStr,createDat,NameStr ");
            strSql.Append("select * ");
            strSql.Append(" FROM [BPMGen] ");
            strSql.Append(" where "+whereStr);
            

            DataSet ds = DbHelperOleDb.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IDInt"] != null && ds.Tables[0].Rows[0]["IDInt"].ToString() != "")
                {
                    this.IDInt = int.Parse(ds.Tables[0].Rows[0]["IDInt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["docTxt"] != null)
                {
                    this.docTxt = ds.Tables[0].Rows[0]["docTxt"].ToString();
                }
                if (ds.Tables[0].Rows[0]["paramStr"] != null)
                {
                    this.paramStr = ds.Tables[0].Rows[0]["paramStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TabStr"] != null)
                {
                    this.TabStr = ds.Tables[0].Rows[0]["TabStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobStr"] != null)
                {
                    this.mobStr = ds.Tables[0].Rows[0]["mobStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["cmobStr"] != null)
                {
                    this.cmobStr = ds.Tables[0].Rows[0]["cmobStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["NameStr"] != null)
                {
                    this.NameStr = ds.Tables[0].Rows[0]["NameStr"].ToString();
                }

                if (ds.Tables[0].Rows[0]["jsStr"] != null)
                {
                    this.jsStr = ds.Tables[0].Rows[0]["jsStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ashxStr"] != null)
                {
                    this.ashxStr = ds.Tables[0].Rows[0]["ashxStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["deleteStr"] != null)
                {
                    this.deleteStr = ds.Tables[0].Rows[0]["deleteStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["gridParamStr"] != null)
                {
                    this.gridParamStr = ds.Tables[0].Rows[0]["gridParamStr"].ToString();
                }
                if (ds.Tables[0].Rows[0]["createDat"] != null)
                {
                    this.createDat =DateTime.Parse( ds.Tables[0].Rows[0]["createDat"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [BPMGen] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        #endregion  Method
    }
}
