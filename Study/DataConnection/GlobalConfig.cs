using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Study
{
    /// <summary>
    /// Класс нужный для легкой смены базы данных. Позволяет сменить подключение к классу
    /// </summary>
    public static class GlobalConfig
    {
        public static IDataConnection connection { get; private set; }

        public static void InitializeConnection(DataBaseType db)
        {
            switch(db)
            {
                case DataBaseType.sql:
                    SqlConnector sql = new SqlConnector();
                    connection = sql;
                    break;
            }

             
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
