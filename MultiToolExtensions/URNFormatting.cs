using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiToolExtensions
{
    public static class URNFormatting
    {
        /// <summary>
        /// Splits a database URN string into a list, with each value properly bracketed.
        /// </summary>
        /// <code>
        /// string database_column_address = "[fooSchema].[barTable].[somecolumn]";
        /// List<string> URN_List = database_column_address.URNToList();
        /// foreach(string item in URN_List)
        /// {
        ///     Console.WriteLine(item);
        /// }
        /// //Output:
        /// // fooSchema
        /// // barTable
        /// // somecolumn
        /// </code>
        /// <param name="full_urn">[Required] The calling object.</param>
        public static List<string> URNToList(this string full_urn)
        {
            List<string> rawurn = full_urn.Split(new string[] { "." }, StringSplitOptions.None).ToList<string>();
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
        /// Takes a basic schema, table, and column URN string, and adds proper bracketed formatting.
        /// </summary>
        /// <example>
        /// <code>
        /// string schema_table_column = "fooSchema.barTable.someColumn";
        /// string Column_Address = schema_table_column.ToFullURN("fooSchema");
        /// Console.WriteLine(Column_Address);
        /// //Output:
        /// // [fooSchema].[barTable].[someColumn]
        /// </code>
        /// </example>
        /// <param name="urn">[Required] The calling string object.</param>
        public static string ToFullURN(this string urn)
        {
            urn = urn.Replace("[", "").Replace("]", "");
            List<string> rawurn = urn.Split(new string[] { "." }, StringSplitOptions.None).ToList<string>();

            string database = "";
            string schema = "";
            string table = "";
            string column = "";

            if (rawurn.Count == 2)
            {
                table = rawurn[0];
                column = rawurn[1];
                urn = string.Format("[{0}].[{1}]", table, column);
            } else if (rawurn.Count == 3)
            {
                schema = rawurn[0];
                table = rawurn[1];
                column = rawurn[2];
                urn = string.Format("[{0}].[{1}].[{2}]", schema, table, column);
            }
            else if (rawurn.Count == 4)
            {
                database = rawurn[0];
                schema = rawurn[1];
                table = rawurn[2];
                column = rawurn[3];
                urn = string.Format("[{0}].[{1}].[{2}].[{3}]", database, schema, table, column);
            } else
            {
                urn = string.Format("[{0}]", urn);
            }

            return urn;
        }
        /// <summary>
        /// Takes a basic schema, table, and column URN string, and adds proper bracketed formatting.
        /// </summary>
        /// <example>
        /// <code>
        /// string table_column = "barTable.someColumn";
        /// string full_address = table_column.ToFullURN("fooSchema");
        /// Console.WriteLine(full_address);
        /// //Output:
        /// // [fooSchema].[barTable].[someColumn]
        /// </code>
        /// </example>
        /// <param name="table_column">[Required] The calling string object.</param>
        /// <param name="schema">[Required] The database/schema name to prepend to the string.</param>
        public static string ToFullURN(this string table_column, string schema)
        {
            table_column = table_column.Replace("[", "").Replace("]", "");
            schema = schema.Replace("[", "").Replace("]", "");
            List<string> rawurn = table_column.Split(new string[] { "." }, StringSplitOptions.None).ToList<string>();

            string database = "";
            string table = "";
            string column = "";

            if (rawurn.Count == 2)
            {
                table = rawurn[0];
                column = rawurn[1];
                table_column = string.Format("[{0}].[{1}]", table, column);
            }
            else if (rawurn.Count == 3)
            {
                // Ignores whatever is at index 0 in favor of the schema parameter.
                table = rawurn[1];
                column = rawurn[2];
                table_column = string.Format("[{0}].[{1}].[{2}]", schema, table, column);
            }
            else if (rawurn.Count == 4)
            {
                // Ignores indexes 0 and 1, and uses the schema parameter.
                table = rawurn[2];
                column = rawurn[3];
                table_column = string.Format("[{0}].[{1}].[{2}]", schema, table, column);
            }
            else
            {
                // Simply tacks the schema parameter value in front of the string value.
                table_column = string.Format("[{0}].[{1}]", schema, table_column);
            }
            return table_column;
        }
        /// <summary>
        /// Takes a basic schema, table, and column URN string, and adds proper bracketed formatting.
        /// </summary>
        /// <example>
        /// <code>
        /// string column = "someColumn";
        /// string full_address = table_column.ToFullURN("fooSchema", "barTable");
        /// Console.WriteLine(full_address);
        /// //Output:
        /// // [fooSchema].[barTable].[someColumn]
        /// </code>
        /// </example>
        /// <param name="column">[Required] The calling object.</param>
        /// <param name="table">[Required] The database/schema name to prepend to the string.</param>
        /// <param name="schema">[Required] The database/schema name to prepend to the string.</param>
        public static string ToFullURN(this string column, string table, string schema)
        {
            column = column.Replace("[", "").Replace("]", "");
            table = schema.Replace("[", "").Replace("]", "");
            schema = schema.Replace("[", "").Replace("]", "");
            List<string> rawurn = column.Split(new string[] { "." }, StringSplitOptions.None).ToList<string>();

            if (rawurn.Count == 1)
            {
                column = string.Format("[{0}].[{1}].[{2}]", schema, table, rawurn[0]);
            }
            else if (rawurn.Count == 2)
            {
                column = string.Format("[{0}].[{1}].[{2}]", schema, table, rawurn[1]);
            }
            else if (rawurn.Count == 3)
            {
                column = string.Format("[{0}].[{1}].[{2}]", schema, table, rawurn[2]);
            }
            else if (rawurn.Count == 4)
            {
                column = string.Format("[{0}].[{1}].[{2}]", schema, table, rawurn[3]);
            }
            else
            {
                // Simply tacks the schema parameter value in front of the string value and hopes for the best.
                column = string.Format("[{0}].[{1}]", schema, column);
            }
            return column;
        }
    }
}
