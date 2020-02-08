using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    /// <summary>
    /// Переменная нужная для легкой смены базы данных. 
    /// Задает условное обозначение базы данных, принимая которое, GlobalConfig подключается к классу, который обеспечивает соединение с соответствующей базой данных
    /// </summary>
    public enum DataBaseType
    {
        sql,
        sqlite
    }
}
