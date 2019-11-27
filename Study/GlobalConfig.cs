using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Study
{
    public static class GlobalConfig
    {
        public static IDataConnection connection { get; private set; }

        public static void InitializeConnection(DataBaseType db)
        {
            if(db == DataBaseType.sql)
            {
                SqlConnector sql = new SqlConnector();
                connection = sql;
            }
            else if (db == DataBaseType.textfile)
            {
                TextConnector text = new TextConnector();
                connection = text;
                //TODO - create the text connection
            }

             
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
