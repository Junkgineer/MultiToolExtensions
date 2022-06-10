using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MultiToolExtensions
{
    /// <summary>
    /// Non-static class containing tools that requires a reference to System.Data.SqlClient
    /// </summary>
    public static class SQLExtensions
    {
        /// <summary>
        /// Creates a string containing a C# boilerplate class from the results of the given query.
        /// The results of the given query are not considered; only the columns it returns.
        /// </summary>
        /// <example>
        /// <code>
        /// string connection_string = @"Data Source=192.168.0.1; Initial Catalog=My_Database; User ID=MrUser; Password=Password12";
        /// string query = @"SELECT TOP(1) * FROM [foo].[bar];
        /// string newclasscode = GenerateClassFromQuery(connection_string, query, "MyNewClass");
        /// </code>
        /// </example>
        /// <param name="connection_string">[Required] The connection string to the SQL server in which to run the query.</param>
        /// <param name="query">[Required] The query to execute.</param>
        /// <param name="new_class_name">[Required] The desired name of the generated class.</param>
        public static string GenerateClassFromQuery(string connection_string, string query, string new_class_name)
        {
            StringBuilder sb = new StringBuilder();
            List<string> columns = new List<string>();
            using (SqlConnection con = new SqlConnection(connection_string))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataReader reader = command.ExecuteReader();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columns.Add(reader.GetName(i).Replace("-", "_"));
                    }
                    reader.Close();
                }
            }
            string outstr = string.Format("public class {0} {{", new_class_name);
            sb.AppendLine(outstr);
            foreach (string column in columns)
            {
                outstr = string.Format("public string {0} {{get; set;}}", column);
                sb.AppendLine(outstr);
            }
            outstr = string.Format("public {0}()\n{{\n\n}}\n}}", new_class_name);
            Console.WriteLine(outstr);
            sb.AppendLine(outstr);
            return sb.ToString();
        }
        /// <summary>
        /// Creates a string containing a C# boilerplate class from the results of the given query.
        /// The results of the given query are not considered; only the columns it returns.
        /// </summary>
        /// <example>
        /// <code>
        /// string connection_string = @"Data Source=192.168.0.1; Initial Catalog=My_Database; User ID=MrUser; Password=Password12";
        /// string query = @"SELECT TOP(1) * FROM [foo].[bar] WHERE [Item] = @itemparam";
        /// Dictionary<string, string> parameters = new Dictionary<string, string>();
        /// parameters.Add("itemparam", "SomeItemName")
        /// string newclasscode = GenerateClassFromQuery(connection_string, query, parameters, "MyNewClass");
        /// </code>
        /// </example>
        /// <param name="connection_string">[Required] The connection string to the SQL server in which to run the query.</param>
        /// <param name="query">[Required] The query to execute.</param>
        /// <param name="parameters">[Required] A Dictionary containing the parameter names and values</param>
        /// <param name="new_class_name">[Required] The desired name of the generated class.</param>
        public static string GenerateClassFromQuery(string connection_string, string query, Dictionary<string, string> parameters, string new_class_name)
        {
            StringBuilder sb = new StringBuilder();
            List<string> columns = new List<string>();
            using (SqlConnection con = new SqlConnection(connection_string))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    foreach (KeyValuePair<string, string> parameter in parameters)
                    {
                        command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                    }
                    SqlDataReader reader = command.ExecuteReader();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columns.Add(reader.GetName(i).Replace("-", "_"));
                    }
                    reader.Close();
                }
            }
            string outstr = string.Format("public class {0} {{", new_class_name);
            sb.AppendLine(outstr);
            foreach (string column in columns)
            {
                outstr = string.Format("public string {0} {{get; set;}}", column);
                sb.AppendLine(outstr);
            }
            outstr = string.Format("public {0}()\n{{\n\n}}\n}}", new_class_name);
            Console.WriteLine(outstr);
            sb.AppendLine(outstr);
            return sb.ToString();
        }
        /// <summary>
        /// Generates a dictionary containing a string of basic CRUD functions for all parameters in the object class.
        /// Defaults to a schema name of "SCHEMA" and a table name of "TABLE" in URN's if none is given.
        /// </summary>
        public static Dictionary<string, string> GenerateSQLFunctions(this object value, string schemaname = "SCHEMA", string tablename = "TABLE")
        {
            Dictionary<string, string> allfunctions = new Dictionary<string, string>();
            Dictionary<string, object> attribs = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(value, null)
            );
            string SELECTFunction = @"
                SELECT
";
            string UPDATEFunction = "\n        public bool Save()\n        {            bool _success = false;\n            if (Connection is null)\n            {                return _success;\n            }\n            string query = @\"\n                UPDATE ";
            UPDATEFunction += string.Format("[{0}].[{1}] SET \n", schemaname, tablename);
            string INSERTFunction = "            public bool Create()            {                bool _success = false;                if (Connection.State == ConnectionState.Closed)                {                    Connection.Open();                }                string query = @\"INSERT INTO ";
            INSERTFunction += string.Format("[{0}].[{1}] (\n", schemaname, tablename);
            string insertcolumns = "\n              ) OUTPUT INSERTED.ID VALUES (\n";
            string parameters = "";
            string assignments = "";
            foreach (string attrib in attribs.Keys)
            {
                INSERTFunction += string.Format("                   [{0}],\n", attrib);
                insertcolumns += string.Format("                    @{0},\n", attrib);
                UPDATEFunction += string.Format("                   [{0}] = @{0},\n", attrib);
                SELECTFunction += string.Format("                   [{0}],\n", attrib);
                parameters += string.Format("               command.Parameters.Add(new SqlParameter(\"@{0}\", this.{0}.isNull(\"\")));\n", attrib);
                assignments += string.Format("                      record.{0} = reader[\"{0}\"].isNull(\"\").ToString();\n", attrib);
            }
            INSERTFunction = INSERTFunction.Substring(0, INSERTFunction.Length - 2);
            insertcolumns = insertcolumns.Substring(0, insertcolumns.Length - 2);
            INSERTFunction += insertcolumns;
            UPDATEFunction = string.Format("{0}\n WHERE [ID] = @ID\n", UPDATEFunction.Substring(0, UPDATEFunction.Length - 2));
            SELECTFunction = string.Format("{0}\nFROM [{1}].[{2}]\n\";", SELECTFunction.Substring(0, SELECTFunction.Length - 2), schemaname, tablename);

            string FunctionAddParameters = @"
"";
            using (SqlCommand command = new SqlCommand(query, this.Connection))
            {
                ";
            FunctionAddParameters += parameters;
            string FunctionExecuteClose = @"
                if (this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();
                }
                int newID = Convert.ToInt32(command.ExecuteScalar());
            }
            _success = true;
            return _success;
        }";
            FunctionAddParameters += FunctionExecuteClose;
            INSERTFunction += FunctionAddParameters;
            UPDATEFunction += FunctionAddParameters;

            string SelectAddReader = @"
            using (SqlCommand command = new SqlCommand(query, this.Connection))
            {
                if (this.Connection.State != ConnectionState.Open)
                {
                    this.Connection.Open();
                }
                List<Object> records = new List<Object>();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Object record = new Object();
                        ";
            SelectAddReader += assignments;
            SelectAddReader += @"
                        records.Add(record);
                    }
                }

                reader.Close();
            }

            return _success;
        }
";
            SELECTFunction += SelectAddReader;
            allfunctions.Add("INSERT", INSERTFunction);
            allfunctions.Add("SELECT", SELECTFunction);
            allfunctions.Add("UPDATE", UPDATEFunction);
            return allfunctions;
        }
    }
}
