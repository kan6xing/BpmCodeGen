<%@ WebHandler Language="C#" Class="OA_Conference" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class OA_Conference : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        YZAuthHelper.AshxAuthCheck();

        GridPageInfo gridPageInfo = new GridPageInfo(context);
        SqlServerProvider queryProvider = new SqlServerProvider(context);

        string searchType = context.Request.Params["SearchType"];
        string keyword = context.Request.Params["Keyword"];

        //获得查询条件
        string filter = null;

        if (searchType == "QuickSearch")
        {
            //应用关键字过滤

            if (!String.IsNullOrEmpty(keyword))
                filter = queryProvider.CombinCond(filter, String.Format("proposerStr LIKE N'%{0}%' shipperStr LIKE N'%{0}%' toStr LIKE N'%{0}%' expressTypeStr LIKE N'%{0}%' or ", queryProvider.EncodeText(keyword)));
        }
        else if (searchType == "AdvancedSearch")
        {
          
            //string cType = context.Request.Params["Type"].ToString();
            //string cTitle = context.Request.Params["Title"].ToString();
            //string cCreateUser = context.Request.Params["CreateUser"].ToString();

            //if(string.IsNullOrEmpty(filter))
            //{
            //    filter = " 1=1 ";
            //}
            //if (!string.IsNullOrEmpty(cType))
            //{
            //    filter = filter + string.Format(" and Type like '%{0}%'", cType);
            //}
            //if (!string.IsNullOrEmpty(cTitle))
            //{
            //    filter = filter + string.Format(" and Title like '%{0}%'", cTitle);
            //}
            //if (!string.IsNullOrEmpty(cCreateUser))
            //{
            //    filter = filter + string.Format(" and CreateUser like '%{0}%'", cCreateUser);
            //}

        }

        if (!string.IsNullOrEmpty(filter))
            filter = "where 1=1" + filter; 
        //获得排序子句
        string order = queryProvider.GetSortString("id") ;
        
        

        //获得SQL
        //string query = String.Format("with T as (select top {0} ROW_NUMBER() OVER(order by {1}) as RowNum,"+
        //    "id,Name,DepartmentID,remark,Picture,OwnerID,ISNULL(OUName,DepartmentID) as OUName,ISNULL(DisplayName,OwnerID) as DisplayName from iStamp LEFT JOIN BPMSysOUs ON iStamp.DepartmentID=BPMSysOUs.Code " +
        //    "LEFT JOIN BPMSysUsers ON iStamp.OwnerID=BPMSysUsers.Account {2}) select * from T where RowNum >= {3};select count(*) from iStamp {2}",
        //    gridPageInfo.RowNumEnd,
        //    order,
        //    filter,
        //    gridPageInfo.RowNumStart);

        string query = String.Format("with T as (select top {0} ROW_NUMBER() OVER(order by {1}) as RowNum,* from ExpressTab {2}) select * from T where RowNum >= {3};select count(*) from ExpressTab {2}",
            gridPageInfo.RowNumEnd,
            order,
            filter,
            gridPageInfo.RowNumStart);

        //执行查询
        JsonItem rv = new JsonItem();
        using (SqlConnection cn = new SqlConnection())
        {
            cn.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BPMDATA"].ConnectionString;
            cn.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = cn;
                cmd.CommandText = query;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //将数据转化为Json集合
                    JsonItemCollection children = new JsonItemCollection();
                    rv.Attributes.Add("children", children);
                    int rowNum = gridPageInfo.RowNumStart;
                    while (reader.Read())
                    {
                        JsonItem item = new JsonItem();
                        children.Add(item);
                        item.Attributes.Add("RowNumber", rowNum);
                        item.Attributes.Add("TaskID", Convert.ToInt32(reader["TaskID"]));
                        item.Attributes.Add("proposerStr", Convert.ToString(reader["proposerStr"]));
item.Attributes.Add("shipperStr", Convert.ToString(reader["shipperStr"]));
item.Attributes.Add("toStr", Convert.ToString(reader["toStr"]));
item.Attributes.Add("expressTypeStr", Convert.ToString(reader["expressTypeStr"]));

                        rowNum++;
                        
                    }

                    //总行数

                    reader.NextResult();
                    reader.Read();
                    rv.Attributes.Add(JsonItem.TotalRows, Convert.ToInt32(reader[0]));
                }
            }
        }

        //输出数据
        context.Response.Write(rv.ToString());
    }

    //private void GetAllOU(BPMConnection cn, OU ou, OUCollection allous)
    //{
    //    allous.Add(ou);

    //    OUCollection cous = ou.GetChildren(cn);
    //    foreach (OU cou in cous)
    //        GetAllOU(cn, cou, allous);
    //}

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}