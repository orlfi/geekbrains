﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace Core.Interfaces
{
    public interface IConnectionManager
    {
        string ConnectionString { get;}
        SQLiteConnection GetOpenedConnection();
    }
}
