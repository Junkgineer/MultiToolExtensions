using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiToolExtensions
{
    public static class URNFormatting
    {
        /// <summary>
        /// Takes any length URN and converts it to an unbracketed <code>List<string></code>.
        /// </summary>
        public static List<string> URNToList(this string fullurn)
        {
            List<string> rawurn = fullurn.Split(new string[] { "].[" }, StringSplitOptions.None).ToList<string>();
            List<string> urn = new List<string>();
            for (int i = 0; i <= rawurn.Count - 1; i++)
            {
                string item = rawurn[i].Replace("[", "");
                item = item.Replace("]", "");
                urn.Add(item.Trim());
            }
            return urn;
        }
        /// <summary>
        /// Splits a database URN string into a list, with each value properly bracketed.
        /// </summary>
        /// <example>
        /// <code>
        /// string database_column_address = "fooSchema.barTable.somecolumn";
        /// List<string> Column_Address = database_column_address.ToFullURN();
        /// foreach(string item in Column_Address)
        /// {
        ///     Console.WriteLine(item);
        /// }
        /// //Output:
        /// // [fooSchema]
        /// // [barTable]
        /// // [somecolumn]
        /// </code>
        /// </example>
        /// <param name="column_table_database">[Required] The calling object.</param>
        public static string ToFullURN(this string column_table_database)
        {
            List<string> rawurn = column_table_database.Split(new string[] { "." }, StringSplitOptions.None).ToList<string>();
            string database = rawurn[0];
            string table = rawurn[1];
            string column = rawurn[2];
            if (!column.Contains("["))
            {
                column = string.Format("[{0}", column);
            }
            if (!column.Contains("]"))
            {
                column = string.Format("{0}]", column);
            }

            if (!table.Contains("["))
            {
                table = string.Format("[{0}", table);
            }
            if (!table.Contains("]"))
            {
                table = string.Format("{0}]", table);
            }

            if (!database.Contains("["))
            {
                database = string.Format("[{0}", database);
            }
            if (!database.Contains("]"))
            {
                database = string.Format("{0}]", database);
            }
            string urn = string.Format("{0}.{1}.{2}", database, table, column);
            return urn;
        }
        /// <summary>
        /// Takes a basic column and table URN, and prepends it with the given database/schema name.
        /// </summary>
        /// <example>
        /// <code>
        /// string column_table = "barTable.someColumn";
        /// List<string> Column_Address = database_column_address.ToFullURN("fooSchema");
        /// foreach(string item in Column_Address)
        /// {
        ///     Console.WriteLine(item);
        /// }
        /// //Output:
        /// // [fooSchema]
        /// // [barTable]
        /// // [someColumn]
        /// </code>
        /// </example>
        /// <param name="column_table">[Required] The calling object.</param>
        /// <param name="database">[Required] The database/schema name to prepend to the string.</param>
        public static string ToFullURN(this string column_table, string database)
        {
            List<string> rawurn = column_table.Split(new string[] { "." }, StringSplitOptions.None).ToList<string>();
            string table = rawurn[0];
            string column = rawurn[1];
            if (!column.Contains("["))
            {
                column = string.Format("[{0}", column);
            }
            if (!column.Contains("]"))
            {
                column = string.Format("{0}]", column);
            }

            if (!table.Contains("["))
            {
                table = string.Format("[{0}", table);
            }
            if (!table.Contains("]"))
            {
                table = string.Format("{0}]", table);
            }

            if (!database.Contains("["))
            {
                database = string.Format("[{0}", database);
            }
            if (!database.Contains("]"))
            {
                database = string.Format("{0}]", database);
            }
            string urn = string.Format("{0}.{1}.{2}", database, table, column);
            return urn;
        }
        /// <summary>
        /// Takes a column name, and prepends it with the bracketed given table and bracketed database/schema name.
        /// </summary>
        /// <example>
        /// <code>
        /// string column_table = "someColumn";
        /// List<string> Column_Address = database_column_address.ToFullURN("barTable", "fooSchema");
        /// foreach(string item in Column_Address)
        /// {
        ///     Console.WriteLine(item);
        /// }
        /// //Output:
        /// // [fooSchema]
        /// // [barTable]
        /// // [someColumn]
        /// </code>
        /// </example>
        /// <param name="column">[Required] The calling object.</param>
        /// <param name="table">[Required] The database/schema name to prepend to the string.</param>
        /// <param name="database">[Required] The database/schema name to prepend to the string.</param>
        public static string ToFullURN(this string column, string table, string database)
        {
            if (!column.Contains("["))
            {
                column = string.Format("[{0}", column);
            }
            if (!column.Contains("]"))
            {
                column = string.Format("{0}]", column);
            }

            if (!table.Contains("["))
            {
                table = string.Format("[{0}", table);
            }
            if (!table.Contains("]"))
            {
                table = string.Format("{0}]", table);
            }

            if (!database.Contains("["))
            {
                database = string.Format("[{0}", database);
            }
            if (!database.Contains("]"))
            {
                database = string.Format("{0}]", database);
            }
            string urn = string.Format("{0}.{1}.{2}", database, table, column);
            return urn;
        }
        /// <summary>
        /// Converts an SQL table URN string into a <code>List<string></code>,
        /// If the object URN contains 2 or less items, the item at index 0 
        /// will be considered the table name, and the given parent database will be prepended.
        /// If the object URN contains 3 items, the item at index 1 will be considered the
        /// table name, and the given parent database will be prepended. 
        /// </summary>
        /// <example>
        /// <code>
        /// string table_urn_foo = "FooTable.BarColumn";
        /// List<string> table_urn_bar = table_urn_foo.ToTableURN();
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// string table_urn_foo = "[SomeDB].[FooTable].[BarColumn]";
        /// List<string> table_urn_bar = table_urn_foo.ToTableURN();
        /// </code>
        /// </example>
        public static string ToTableURN(this string object_urn)
        {
            List<string> rawurn = object_urn.Split(new string[] { "." }, StringSplitOptions.None).ToList<string>();
            string urn = "";
            if (rawurn.Count() == 2)
            {
                urn = rawurn[0];
            }
            else if (rawurn.Count() == 3)
            {
                urn = string.Format("{0}{1}", rawurn[0], rawurn[1]);
            }
            return urn;
        }
        /// <summary>
        /// Converts a SQL table URN string into a <code>List<string></code>,
        /// and prepends the given parent database. If the object URN contains 2 or less
        /// items, the item at index 0 will be considered the table name, and the given 
        /// parent database will be prepended.
        /// If the object URN contains 3 items, the item at index 1 will be considered the
        /// table name, and the given parent database will be prepended. 
        /// </summary>
        /// <example>
        /// <code>
        /// string table_urn_foo = "[FooTable].[BarColumn]";
        /// List<string> table_urn_bar = table_urn_foo.ToTableURN("SomeDB");
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// string table_urn_foo = "[SomeDB].[FooTable].[BarColumn]";
        /// List<string> table_urn_bar = table_urn_foo.ToTableURN("SomeOtherDB");
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// string table_urn_foo = "SomeDB.FooTable.BarColumn";
        /// List<string> table_urn_bar = table_urn_foo.ToTableURN("SomeOtherDB");
        /// </code>
        /// </example> 
        public static string ToTableURN(this string object_urn, string parentdatabase)
        {
            List<string> rawurn = object_urn.Split(new string[] { "." }, StringSplitOptions.None).ToList<string>();
            string urn = "";
            if (!parentdatabase.Contains("["))
            {
                parentdatabase = string.Format("[{0}", parentdatabase);
            }
            if (!parentdatabase.Contains("]"))
            {
                parentdatabase = string.Format("{0}]", parentdatabase);
            }
            if (rawurn.Count() <= 2)
            {
                urn = string.Format("{0}{1}", parentdatabase, rawurn[0]);
            }
            else if (rawurn.Count() == 3)
            {
                urn = string.Format("{0}{1}", parentdatabase, rawurn[1]);
            }
            return urn;
        }
        /// <summary>
        /// Converts a full, bracketed, SQL URN string into a <code>List<string></code>
        /// </summary>
        /// <example>
        /// <code>
        /// string urn_foo = "[SomeDB].[FooTable].[BarColumn]";
        /// List<string> urn_bar = urn_foo.BreakoutURN();
        /// </code>
        /// </example>
        public static List<string> BreakoutURN(this string fullurn)
        {
            List<string> rawurn = fullurn.Split(new string[] { "].[" }, StringSplitOptions.None).ToList<string>();
            List<string> urn = new List<string>();
            for (int i = 0; i <= rawurn.Count - 1; i++)
            {
                string item = rawurn[i].Replace("[", "");
                item = item.Replace("]", "");
                urn.Add(item.Trim());
            }
            return urn;
        }
    }
}
