using System;
using System.Collections.Generic;
using System.Text;
using System.Data;



    public interface PubDataAccess
    {
         DataTable GetTableByada(string sqlSelectStr);
         bool UpdateByada(DataTable dt);
    }

