<%@ WebHandler Language="C#" Class="DeleteLeadSale" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class DeleteLeadSale : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        YZDebugHelper.Init();

        string clientMethod = context.Request.Params["Method"];
        string method = clientMethod;
        if (method != null) 
        {
            method = method.Trim().ToLower();
        }
        if (method == "delete") 
        {
            try
            {
                int count = Int32.Parse(context.Request.Params["Count"]);
                using(SqlConnection cn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BPMDB"].ConnectionString))
                {
                    cn.Open();
                    for(int i=0; i<count; i++)
                    {
                       string id = context.Request.Params["ID"+i.ToString()];
                      // string id = context.Request .Params["ID"];
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection=cn;
                        cmd.CommandText = " delete from ExpressTab  where (TaskID='" + id + "')";
                        cmd.ExecuteNonQuery();
                  
                    }
                }
                JsonItem rv = new JsonItem();
                rv.Attributes.Add("success", true);

                context.Response.Write(rv.ToString());
            }
            catch (Exception  exp)
            {
                JsonItem rv = new JsonItem();
                rv.Attributes.Add("success", false);
                rv.Attributes.Add("errorMessage", exp.Message);
                context.Response.Write(rv.ToString());
            }
        }
        else
        {
            JsonItem rv = new JsonItem();
            rv.Attributes.Add("success", false);
            rv.Attributes.Add("errorMessage", String.Format("未知的命令：{0}", clientMethod));

            context.Response.Write(rv.ToString());
        }
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }

}