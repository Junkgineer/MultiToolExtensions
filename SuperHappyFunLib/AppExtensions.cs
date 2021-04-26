using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace MultiToolExtensions
{
    public static class AppExtensions
    {
        /// <summary>
        /// Writes the contents of the string to a text file at the given path.
        /// </summary>
        /// <param name="path">[Required] The path of the file to write to.</param>
        /// <param name="append">[Optional] If true, will append to the file instead of overwriting it.</param>
        /// <param name="sender">[Optional] If given, will add the string to the linestamps.</param>
        /// <param name="linestamps">[Optional] If true, will prepend timestamps and optionally sender to each line written.</param>
        public static void WriteToFile(this string object_value, string path, bool append = false, string sender = null, bool linestamps = false)
        {
            if (System.IO.File.Exists(path) && !append)
            {
                System.IO.File.Delete(path);
            }
            if (!System.IO.File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(DateTime.Now.ToString("g") + " FILE OPENED" + "\n");
                }
            }
            using (System.IO.StreamWriter file = System.IO.File.AppendText(path))
            {
                if (linestamps)
                {
                    object_value = String.Format("[{0} > {1}] {2}\n", DateTime.Now.ToString("g"), sender == null ? "" : sender, object_value);
                }
                file.WriteLine(object_value + "\n");
            }
        }
        /// <summary>
        /// Takes any length URN and converts it to an unbracketed list.
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
        /// Returns a list of indexes in the string where [value] is located.
        /// </summary>
        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
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
        /// Takes a column name, and prepends it with the given table and database/schema name.
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
        public static string CleanFileName(string filename)
        {
            string outstr = filename;
            foreach (KeyValuePair<string, string> spc in Definitions.SpecialCharSubstitutions)
            {
                outstr = outstr.Replace(spc.Key, spc.Value);
            }
            outstr = outstr.Replace(".", "_");
            outstr = outstr.Replace(" ", "_");
            return outstr;
        }
        public static string toStringList(this List<string> object_value)
        {
            if (object_value.Count != 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string item in object_value)
                {
                    if (item != "")
                    {
                        builder.Append(item).Append(",");
                    }
                }
                string resultstr = builder.ToString();
                if (resultstr.Length >= 1)
                {
                    resultstr = resultstr.Substring(0, resultstr.Length - 1);
                }
                return resultstr;
            }
            else
            {
                return "";
            }
        }
        public static string toStringList(this List<string> object_value, string delimiter)
        {
            if (object_value.Count != 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string item in object_value)
                {
                    builder.Append(item).Append(delimiter);
                }
                string resultstr = builder.ToString();
                if (resultstr.Length >= 1)
                {
                    resultstr = resultstr.Substring(0, resultstr.Length - 1);
                }
                return resultstr;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Returns true if the object has a value of 0. Checks for other nulls that it can never be as well...just in case...
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static bool isNull(this int object_value)
        {
            if (object_value == default(int) || object_value == 0 || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns true if the object has a value of null, is of type null, or is of type DBNull. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static bool isNull(this object object_value)
        {
            if (object_value == default(object) || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true;
            }

            return false;
        }
        public static bool isNull(this string object_value)
        {
            if (object_value == default(string) || object_value == "" || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true;
            }
            return false;
        }
        public static bool isNull(this DateTime object_value)
        {
            if (object_value == default(DateTime))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns true_value parameter object if the calling object is null, or retains the object value if it is not null. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The object to return if the calling object is null.</param>
        public static object isNull(this object object_value, object true_value)
        {
            if (object_value == default(object) || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return object_value;
        }
        /// <summary>
        /// Returns true_value parameter string if the calling object is null (including an empty string!), or retains the object value if it is not null. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling string object.</param>
        /// <param name="true_value">[Optional] The string object to return if the calling string object is null.</param>
        public static string isNull(this string object_value, string true_value)
        {
            if (object_value == default(string) || object_value == "" || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return object_value;
        }
        /// <summary>
        /// Returns true if the object has a value of 0. I don't care if the rest of the null checks on an int are useless...it's here just because.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The string object to return if the calling string object is null.</param>
        public static int isNull(this int object_value, int true_value)
        {
            if (object_value == default(int) || object_value == 0 || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return object_value;
        }
        public static DateTime isNull(this DateTime object_value, DateTime true_value)
        {
            if (object_value == default(DateTime))
            {
                return true_value;
            }
            return object_value;
        }
        /// <summary>
        /// Returns the true_value parameter object if the object is null, or the false_value if it is not null. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The object to return if the calling object is null.</param>
        /// <param name="false_value">[Optional] The object to return if the calling object is not null.</param>
        public static object isNull(this object object_value, object true_value, object false_value)
        {
            if (object_value == default(object) || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return false_value;
        }
        /// <summary>
        /// Returns the true_value parameter object if the object is null, or the false_value if it is not null. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The object to return if the calling object is null.</param>
        /// <param name="false_value">[Optional] The object to return if the calling object is not null.</param>
        public static string isNull(this string object_value, string true_value, string false_value)
        {
            if (object_value == default(string) || object_value == "" || object_value == null || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return false_value;

        }
        /// <summary>
        /// Returns true if the object has a value of 0, or the false_value if it is not 0. I don't care if the rest of the null checks on an int are useless...it's here just because.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="true_value">[Optional] The string object to return if the calling string object is null.</param>
        /// <param name="false_value">[Optional] The object to return if the calling object is not null.</param>
        public static int isNull(this int object_value, int true_value, int false_value)
        {
            if (object_value == default(int) || object_value == 0 || object_value.GetType() == null || object_value.GetType() == typeof(System.DBNull))
            {
                return true_value;
            }
            return false_value;

        }
        public static DateTime isNull(this DateTime object_value, DateTime true_value, DateTime false_value)
        {
            if (object_value == default(DateTime))
            {
                return true_value;
            }
            return false_value;
        }
        private static Dictionary<string, string> _US_States = new Dictionary<string, string>()
        {
            {"﻿Alabama", "AL"},
            {"Alaska", "AK"},
            {"Arizona", "AZ"},
            {"Arkansas", "AR"},
            {"California", "CA"},
            {"Colorado", "CO"},
            {"Connecticut", "CT"},
            {"Delaware", "DE"},
            {"Florida", "FL"},
            {"Georgia", "GA"},
            {"Hawaii", "HI"},
            {"Idaho", "ID"},
            {"Illinois", "IL"},
            {"Indiana", "IN"},
            {"Iowa", "IA"},
            {"Kansas", "KS"},
            {"Kentucky", "KY"},
            {"Louisiana", "LA"},
            {"Maine", "ME"},
            {"Maryland", "MD"},
            {"Massachusetts", "MA"},
            {"Michigan", "MI"},
            {"Minnesota", "MN"},
            {"Mississippi", "MS"},
            {"Missouri", "MO"},
            {"Montana", "MT"},
            {"Nebraska", "NE"},
            {"Nevada", "NV"},
            {"New Hampshire", "NH"},
            {"New Jersey", "NJ"},
            {"New Mexico", "NM"},
            {"New York", "NY"},
            {"North Carolina", "NC"},
            {"North Dakota", "ND"},
            {"Ohio", "OH"},
            {"Oklahoma", "OK"},
            {"Oregon", "OR"},
            {"Pennsylvania", "PA"},
            {"Rhode Island", "RI"},
            {"South Carolina", "SC"},
            {"South Dakota", "SD"},
            {"Tennessee", "TN"},
            {"Texas", "TX"},
            {"Utah", "UT"},
            {"Vermont", "VT"},
            {"Virginia", "VA"},
            {"Washington", "WA"},
            {"West Virginia", "WV"},
            {"Wisconsin", "WI"},
            {"Wyoming", "WY"},
            {"District of Columbia", "DC"},
            {"Marshall Islands", "MH"},
            {"Armed Forces Africa", "AE"},
            {"Armed Forces Americas", "AA"},
            {"Armed Forces Canada", "AE"},
            {"Armed Forces Europe", "AE"},
            {"Armed Forces Middle East", "AE"},
            {"Armed Forces Pacific", "AP"}
        };
        private static Dictionary<string, string> _Countries_ISO_2 = new Dictionary<string, string>()
        {
            {"Afghanistan", "AF"},
            {"Aland Islands", "AX"},
            {"Albania", "AL"},
            {"Algeria", "DZ"},
            {"Andorra", "AD"},
            {"Angola", "AO"},
            {"Anguilla", "AI"},
            {"Antarctica", "AQ"},
            {"Antigua And Barbuda", "AG"},
            {"Argentina", "AR"},
            {"Armenia", "AM"},
            {"Aruba", "AW"},
            {"Australia", "AU"},
            {"Austria", "AT"},
            {"Azerbaijan", "AZ"},
            {"Bahamas", "BS"},
            {"Bahrain", "BH"},
            {"Bangladesh", "BD"},
            {"Barbados", "BB"},
            {"Belarus", "BY"},
            {"Belgium", "BE"},
            {"Belize", "BZ"},
            {"Benin", "BJ"},
            {"Bermuda", "BM"},
            {"Bhutan", "BT"},
            {"Bolivia", "BO"},
            {"Bonaire, Saint Eustatius and Saba", "BQ"},
            {"Bosnia and Herzegovina", "BA"},
            {"Botswana", "BW"},
            {"Bouvet Island", "BV"},
            {"Brazil", "BR"},
            {"British Indian Ocean Territory", "IO"},
            {"Brunei Darussalam", "BN"},
            {"Bulgaria", "BG"},
            {"Burkina Faso", "BF"},
            {"Burundi", "BI"},
            {"Cambodia", "KH"},
            {"Cameroon", "CM"},
            {"Canada", "CA"},
            {"Cape Verde", "CV"},
            {"Cayman Islands", "KY"},
            {"Central African Republic", "CF"},
            {"Chad", "TD"},
            {"Chile", "CL"},
            {"China", "CN"},
            {"Christmas Island", "CX"},
            {"Cocos (Keeling) Islands", "CC"},
            {"Colombia", "CO"},
            {"Comoros", "KM"},
            {"Congo", "CG"},
            {"Congo the Democratic Republic of the", "CD"},
            {"Cook Islands", "CK"},
            {"Costa Rica", "CR"},
            {"Cote d'Ivoire", "CI"},
            {"Croatia", "HR"},
            {"Cuba", "CU"},
            {"Curacao", "CW"},
            {"Cyprus", "CY"},
            {"Czech Republic", "CZ"},
            {"Denmark", "DK"},
            {"Djibouti", "DJ"},
            {"Dominica", "DM"},
            {"Dominican Republic", "DO"},
            {"Ecuador", "EC"},
            {"Egypt", "EG"},
            {"El Salvador", "SV"},
            {"Equatorial Guinea", "GQ"},
            {"Eritrea", "ER"},
            {"Estonia", "EE"},
            {"Ethiopia", "ET"},
            {"Falkland Islands (Malvinas)", "FK"},
            {"Faroe Islands", "FO"},
            {"Fiji", "FJ"},
            {"Finland", "FI"},
            {"France", "FR"},
            {"French Guiana", "GF"},
            {"French Polynesia", "PF"},
            {"French Southern Territories", "TF"},
            {"Gabon", "GA"},
            {"Gambia", "GM"},
            {"Georgia", "GE"},
            {"Germany", "DE"},
            {"Ghana", "GH"},
            {"Gibraltar", "GI"},
            {"Greece", "GR"},
            {"Greenland", "GL"},
            {"Grenada", "GD"},
            {"Guadeloupe", "GP"},
            {"Guam", "GU"},
            {"Guatemala", "GT"},
            {"Guernsey", "GG"},
            {"Guinea", "GN"},
            {"Guinea-Bissau", "GW"},
            {"Guyana", "GY"},
            {"Haiti", "HT"},
            {"Heard Island and McDonald Islands", "HM"},
            {"Holy See (Vatican City State)", "VA"},
            {"Honduras", "HN"},
            {"Hong Kong", "HK"},
            {"Hungary", "HU"},
            {"Iceland", "IS"},
            {"India", "IN"},
            {"Indonesia", "ID"},
            {"Iran Islamic Republic of", "IR"},
            {"Iraq", "IQ"},
            {"Ireland", "IE"},
            {"Isle of Man", "IM"},
            {"Israel", "IL"},
            {"Italy", "IT"},
            {"Jamaica", "JM"},
            {"Japan", "JP"},
            {"Jersey", "JE"},
            {"Jordan", "JO"},
            {"Kazakhstan", "KZ"},
            {"Kenya", "KE"},
            {"Kiribati", "KI"},
            {"Korea, Democratic People's Republic of", "KP"},
            {"Korea, Republic of", "KR"},
            {"Kuwait", "KW"},
            {"Kyrgyzstan", "KG"},
            {"Lao People's Democratic Republic", "LA"},
            {"Latvia", "LV"},
            {"Lebanon", "LB"},
            {"Lesotho", "LS"},
            {"Liberia", "LR"},
            {"Libyan Arab Jamahiriya", "LY"},
            {"Liechtenstein", "LI"},
            {"Lithuania", "LT"},
            {"Luxembourg", "LU"},
            {"Macao", "MO"},
            {"Macedonia, the Former Yugoslav Republic of", "MK"},
            {"Madagascar", "MG"},
            {"Malawi", "MW"},
            {"Malaysia", "MY"},
            {"Maldives", "MV"},
            {"Mali", "ML"},
            {"Malta", "MT"},
            {"Marshall Islands", "MH"},
            {"Martinique", "MQ"},
            {"Mauritania", "MR"},
            {"Mauritius", "MU"},
            {"Mayotte", "YT"},
            {"Mexico", "MX"},
            {"Micronesia, Federated States of", "FM"},
            {"Moldova, Republic of", "MD"},
            {"Monaco", "MC"},
            {"Mongolia", "MN"},
            {"Montenegro", "ME"},
            {"Montserrat", "MS"},
            {"Morocco", "MA"},
            {"Mozambique", "MZ"},
            {"Myanmar", "MM"},
            {"Namibia", "NA"},
            {"Nauru", "NR"},
            {"Nepal", "NP"},
            {"Netherlands", "NL"},
            {"New Caledonia", "NC"},
            {"New Zealand", "NZ"},
            {"Nicaragua", "NI"},
            {"Niger", "NE"},
            {"Nigeria", "NG"},
            {"Niue", "NU"},
            {"Norfolk Island", "NF"},
            {"Northern Mariana Islands", "MP"},
            {"Norway", "NO"},
            {"Oman", "OM"},
            {"Pakistan", "PK"},
            {"Palau", "PW"},
            {"Palestinian Territory, Occupied", "PS"},
            {"Panama", "PA"},
            {"Papua New Guinea", "PG"},
            {"Paraguay", "PY"},
            {"Peru", "PE"},
            {"Philippines", "PH"},
            {"Pitcairn", "PN"},
            {"Poland", "PL"},
            {"Portugal", "PT"},
            {"Puerto Rico", "PR"},
            {"Qatar", "QA"},
            {"Reunion", "RE"},
            {"Romania", "RO"},
            {"Russian Federation", "RU"},
            {"Rwanda", "RW"},
            {"Saint Barthelemy", "BL"},
            {"Saint Helena", "SH"},
            {"Saint Kitts and Nevis", "KN"},
            {"Saint Lucia", "LC"},
            {"Saint Martin (French part)", "MF"},
            {"Saint Pierre and Miquelon", "PM"},
            {"Saint Vincent and the Grenadines", "VC"},
            {"Samoa", "WS"},
            {"San Marino", "SM"},
            {"Sao Tome and Principe", "ST"},
            {"Saudi Arabia", "SA"},
            {"Senegal", "SN"},
            {"Serbia", "RS"},
            {"Seychelles", "SC"},
            {"Sierra Leone", "SL"},
            {"Singapore", "SG"},
            {"Sint Maarten", "SX"},
            {"Slovakia", "SK"},
            {"Slovenia", "SI"},
            {"Solomon Islands", "SB"},
            {"Somalia", "SO"},
            {"South Africa", "ZA"},
            {"South Georgia and the South Sandwich Islands", "GS"},
            {"South Sudan", "SS"},
            {"Spain", "ES"},
            {"Sri Lanka", "LK"},
            {"Sudan", "SD"},
            {"Suriname", "SR"},
            {"Svalbard and Jan Mayen", "SJ"},
            {"Swaziland", "SZ"},
            {"Sweden", "SE"},
            {"Switzerland", "CH"},
            {"Syrian Arab Republic", "SY"},
            {"Taiwan", "TW"},
            {"Tajikistan", "TJ"},
            {"Tanzania, United Republic of,", "TZ"},
            {"Thailand", "TH"},
            {"Timor-Leste", "TL"},
            {"Togo", "TG"},
            {"Tokelau", "TK"},
            {"Tonga", "TO"},
            {"Trinidad and Tobago", "TT"},
            {"Tunisia", "TN"},
            {"Turkey", "TR"},
            {"Turkmenistan", "TM"},
            {"Turks and Caicos Islands", "TC"},
            {"Tuvalu", "TV"},
            {"Uganda", "UG"},
            {"Ukraine", "UA"},
            {"United Arab Emirates", "AE"},
            {"United Kingdom", "GB"},
            {"United States", "US"},
            {"United States Minor Outlying Islands", "UM"},
            {"Uruguay", "UY"},
            {"Uzbekistan", "UZ"},
            {"Vanuatu", "VU"},
            {"Venezuela", "VE"},
            {"Viet Nam", "VN"},
            {"Virgin Islands, British", "VG"},
            {"Virgin Islands, U.S.", "VI"},
            {"Wallis and Futuna", "WF"},
            {"Western Sahara", "EH"},
            {"Yemen", "YE"},
            {"Zambia", "ZM"},
            {"Zimbabwe", "ZW"}
        };
        private static Dictionary<string, string> _Countries_ISO_3 = new Dictionary<string, string>()
        {
            {"﻿Afghanistan", "AFG"},
            {"Aland Islands", "ALA"},
            {"Albania", "ALB"},
            {"Algeria", "DZA"},
            {"Andorra", "AND"},
            {"Angola", "AGO"},
            {"Anguilla", "AIA"},
            {"Antarctica", "ATA"},
            {"Antigua And Barbuda", "ATG"},
            {"Argentina", "ARG"},
            {"Armenia", "ARM"},
            {"Aruba", "ABW"},
            {"Australia", "AUS"},
            {"Austria", "AUT"},
            {"Azerbaijan", "AZE"},
            {"Bahamas", "BHS"},
            {"Bahrain", "BHR"},
            {"Bangladesh", "BGD"},
            {"Barbados", "BRB"},
            {"Belarus", "BLR"},
            {"Belgium", "BEL"},
            {"Belize", "BLZ"},
            {"Benin", "BEN"},
            {"Bermuda", "BMU"},
            {"Bhutan", "BTN"},
            {"Bolivia", "BOL"},
            {"Bonaire, Saint Eustatius and Saba", "BES"},
            {"Bosnia and Herzegovina", "BIH"},
            {"Botswana", "BWA"},
            {"Bouvet Island", "BVT"},
            {"Brazil", "BRA"},
            {"British Indian Ocean Territory", "IOT"},
            {"Brunei Darussalam", "BRN"},
            {"Bulgaria", "BGR"},
            {"Burkina Faso", "BFA"},
            {"Burundi", "BDI"},
            {"Cambodia", "KHM"},
            {"Cameroon", "CMR"},
            {"Canada", "CAN"},
            {"Cape Verde", "CPV"},
            {"Cayman Islands", "CYM"},
            {"Central African Republic", "CAF"},
            {"Chad", "TCD"},
            {"Chile", "CHL"},
            {"China", "CHN"},
            {"Christmas Island", "CXR"},
            {"Cocos (Keeling) Islands", "CCK"},
            {"Colombia", "COL"},
            {"Comoros", "COM"},
            {"Congo", "COG"},
            {"Congo the Democratic Republic of the", "COD"},
            {"Cook Islands", "COK"},
            {"Costa Rica", "CRI"},
            {"Cote d'Ivoire", "CIV"},
            {"Croatia", "HRV"},
            {"Cuba", "CUB"},
            {"Curacao", "CUW"},
            {"Cyprus", "CYP"},
            {"Czech Republic", "CZE"},
            {"Denmark", "DNK"},
            {"Djibouti", "DJI"},
            {"Dominica", "DMA"},
            {"Dominican Republic", "DOM"},
            {"Ecuador", "ECU"},
            {"Egypt", "EGY"},
            {"El Salvador", "SLV"},
            {"Equatorial Guinea", "GNQ"},
            {"Eritrea", "ERI"},
            {"Estonia", "EST"},
            {"Ethiopia", "ETH"},
            {"Falkland Islands (Malvinas)", "FLK"},
            {"Faroe Islands", "FRO"},
            {"Fiji", "FJI"},
            {"Finland", "FIN"},
            {"France", "FRA"},
            {"French Guiana", "GUF"},
            {"French Polynesia", "PYF"},
            {"French Southern Territories", "ATF"},
            {"Gabon", "GAB"},
            {"Gambia", "GMB"},
            {"Georgia", "GEO"},
            {"Germany", "DEU"},
            {"Ghana", "GHA"},
            {"Gibraltar", "GIB"},
            {"Greece", "GRC"},
            {"Greenland", "GRL"},
            {"Grenada", "GRD"},
            {"Guadeloupe", "GLP"},
            {"Guam", "GUM"},
            {"Guatemala", "GTM"},
            {"Guernsey", "GGY"},
            {"Guinea", "GIN"},
            {"Guinea-Bissau", "GNB"},
            {"Guyana", "GUY"},
            {"Haiti", "HTI"},
            {"Heard Island and McDonald Islands", "HMD"},
            {"Holy See (Vatican City State)", "VAT"},
            {"Honduras", "HND"},
            {"Hong Kong", "HKG"},
            {"Hungary", "HUN"},
            {"Iceland", "ISL"},
            {"India", "IND"},
            {"Indonesia", "IDN"},
            {"Iran Islamic Republic of", "IRN"},
            {"Iraq", "IRQ"},
            {"Ireland", "IRL"},
            {"Isle of Man", "IMN"},
            {"Israel", "ISR"},
            {"Italy", "ITA"},
            {"Jamaica", "JAM"},
            {"Japan", "JPN"},
            {"Jersey", "JEY"},
            {"Jordan", "JOR"},
            {"Kazakhstan", "KAZ"},
            {"Kenya", "KEN"},
            {"Kiribati", "KIR"},
            {"Korea, Democratic People's Republic of", "PRK"},
            {"Korea, Republic of", "KOR"},
            {"Kuwait", "KWT"},
            {"Kyrgyzstan", "KGZ"},
            {"Lao People's Democratic Republic", "LAO"},
            {"Latvia", "LVA"},
            {"Lebanon", "LBN"},
            {"Lesotho", "LSO"},
            {"Liberia", "LBR"},
            {"Libyan Arab Jamahiriya", "LBY"},
            {"Liechtenstein", "LIE"},
            {"Lithuania", "LTU"},
            {"Luxembourg", "LUX"},
            {"Macao", "MAC"},
            {"Macedonia, the Former Yugoslav Republic of", "MKD"},
            {"Madagascar", "MDG"},
            {"Malawi", "MWI"},
            {"Malaysia", "MYS"},
            {"Maldives", "MDV"},
            {"Mali", "MLI"},
            {"Malta", "MLT"},
            {"Marshall Islands", "MHL"},
            {"Martinique", "MTQ"},
            {"Mauritania", "MRT"},
            {"Mauritius", "MUS"},
            {"Mayotte", "MYT"},
            {"Mexico", "MEX"},
            {"Micronesia, Federated States of", "FSM"},
            {"Moldova, Republic of", "MDA"},
            {"Monaco", "MCO"},
            {"Mongolia", "MNG"},
            {"Montenegro", "MNE"},
            {"Montserrat", "MSR"},
            {"Morocco", "MAR"},
            {"Mozambique", "MOZ"},
            {"Myanmar", "MMR"},
            {"Namibia", "NAM"},
            {"Nauru", "NRU"},
            {"Nepal", "NPL"},
            {"Netherlands", "NLD"},
            {"New Caledonia", "NCL"},
            {"New Zealand", "NZL"},
            {"Nicaragua", "NIC"},
            {"Niger", "NER"},
            {"Nigeria", "NGA"},
            {"Niue", "NIU"},
            {"Norfolk Island", "NFK"},
            {"Northern Mariana Islands", "MNP"},
            {"Norway", "NOR"},
            {"Oman", "OMN"},
            {"Pakistan", "PAK"},
            {"Palau", "PLW"},
            {"Palestinian Territory, Occupied", "PSE"},
            {"Panama", "PAN"},
            {"Papua New Guinea", "PNG"},
            {"Paraguay", "PRY"},
            {"Peru", "PER"},
            {"Philippines", "PHL"},
            {"Pitcairn", "PCN"},
            {"Poland", "POL"},
            {"Portugal", "PRT"},
            {"Puerto Rico", "PRI"},
            {"Qatar", "QAT"},
            {"Reunion", "REU"},
            {"Romania", "ROU"},
            {"Russian Federation", "RUS"},
            {"Rwanda", "RWA"},
            {"Saint Barthelemy", "BLM"},
            {"Saint Helena", "SHN"},
            {"Saint Kitts and Nevis", "KNA"},
            {"Saint Lucia", "LCA"},
            {"Saint Martin (French part)", "MAF"},
            {"Saint Pierre and Miquelon", "SPM"},
            {"Saint Vincent and the Grenadines", "VCT"},
            {"Samoa", "WSM"},
            {"San Marino", "SMR"},
            {"Sao Tome and Principe", "STP"},
            {"Saudi Arabia", "SAU"},
            {"Senegal", "SEN"},
            {"Serbia", "SRB"},
            {"Seychelles", "SYC"},
            {"Sierra Leone", "SLE"},
            {"Singapore", "SGP"},
            {"Sint Maarten", "SXM"},
            {"Slovakia", "SVK"},
            {"Slovenia", "SVN"},
            {"Solomon Islands", "SLB"},
            {"Somalia", "SOM"},
            {"South Africa", "ZAF"},
            {"South Georgia and the South Sandwich Islands", "SGS"},
            {"South Sudan", "SSD"},
            {"Spain", "ESP"},
            {"Sri Lanka", "LKA"},
            {"Sudan", "SDN"},
            {"Suriname", "SUR"},
            {"Svalbard and Jan Mayen", "SJM"},
            {"Swaziland", "SWZ"},
            {"Sweden", "SWE"},
            {"Switzerland", "CHE"},
            {"Syrian Arab Republic", "SYR"},
            {"Taiwan", "TWN"},
            {"Tajikistan", "TJK"},
            {"Tanzania, United Republic of,", "TZA"},
            {"Thailand", "THA"},
            {"Timor-Leste", "TLS"},
            {"Togo", "TGO"},
            {"Tokelau", "TKL"},
            {"Tonga", "TON"},
            {"Trinidad and Tobago", "TTO"},
            {"Tunisia", "TUN"},
            {"Turkey", "TUR"},
            {"Turkmenistan", "TKM"},
            {"Turks and Caicos Islands", "TCA"},
            {"Tuvalu", "TUV"},
            {"Uganda", "UGA"},
            {"Ukraine", "UKR"},
            {"United Arab Emirates", "ARE"},
            {"United Kingdom", "GBR"},
            {"United States", "USA"},
            {"United States Minor Outlying Islands", "UMI"},
            {"Uruguay", "URY"},
            {"Uzbekistan", "UZB"},
            {"Vanuatu", "VUT"},
            {"Venezuela", "VEN"},
            {"Viet Nam", "VNM"},
            {"Virgin Islands, British", "VGB"},
            {"Virgin Islands, U.S.", "VIR"},
            {"Wallis and Futuna", "WLF"},
            {"Western Sahara", "ESH"},
            {"Yemen", "YEM"},
            {"Zambia", "ZMB"},
            {"Zimbabwe", "ZWE"}
        };
        private static Dictionary<string, string> _UK_Countries = new Dictionary<string, string>()
        {
            {"CHI", "Channel Islands" },
            {"ENG", "England" },
            {"IOM", "Isle of Man" },
            {"IRL", "Ireland" },
            {"NIR", "Northern Ireland" },
            {"SCT", "Scotland" },
            {"WAL", "Wales" }
        };
        private static Dictionary<string, string> _UK_Counties = new Dictionary<string, string>()
        {
            {"﻿Aberdeenshire", "ABD"},
            {"Anglesey", "AGY"},
            {"Alderney", "ALD"},
            {"Angus", "ANS"},
            {"Co. Antrim", "ANT"},
            {"Argyllshire", "ARL"},
            {"Co. Armagh", "ARM"},
            {"Avon", "AVN"},
            {"Ayrshire", "AYR"},
            {"Banffshire", "BAN"},
            {"Bedfordshire", "BDF"},
            {"Berwickshire", "BEW"},
            {"Buckinghamshire", "BKM"},
            {"Borders", "BOR"},
            {"Breconshire", "BRE"},
            {"Berkshire", "BRK"},
            {"Bute", "BUT"},
            {"Caernarvonshire", "CAE"},
            {"Caithness", "CAI"},
            {"Cambridgeshire", "CAM"},
            {"Co. Carlow", "CAR"},
            {"Co. Cavan", "CAV"},
            {"Central", "CEN"},
            {"Cardiganshire", "CGN"},
            {"Cheshire", "CHS"},
            {"Co. Clare", "CLA"},
            {"Clackmannanshire", "CLK"},
            {"Cleveland", "CLV"},
            {"Cumbria", "CMA"},
            {"Carmarthenshire", "CMN"},
            {"Cornwall", "CON"},
            {"Co. Cork", "COR"},
            {"Cumberland", "CUL"},
            {"Clwyd", "CWD"},
            {"Derbyshire", "DBY"},
            {"Denbighshire", "DEN"},
            {"Devon", "DEV"},
            {"Dyfed", "DFD"},
            {"Dumfries-shire", "DFS"},
            {"Dumfries and Galloway", "DGY"},
            {"Dunbartonshire", "DNB"},
            {"Co. Donegal", "DON"},
            {"Dorset", "DOR"},
            {"Co. Down", "DOW"},
            {"Co. Dublin", "DUB"},
            {"Co. Durham", "DUR"},
            {"East Lothian", "ELN"},
            {"East Riding of Yorkshire", "ERY"},
            {"Essex", "ESS"},
            {"Co. Fermanagh", "FER"},
            {"Fife", "FIF"},
            {"Flintshire", "FLN"},
            {"Co. Galway", "GAL"},
            {"Glamorgan", "GLA"},
            {"Gloucestershire", "GLS"},
            {"Grampian", "GMP"},
            {"Gwent", "GNT"},
            {"Guernsey", "GSY"},
            {"Greater Manchester", "GTM"},
            {"Gwynedd", "GWN"},
            {"Hampshire", "HAM"},
            {"Herefordshire", "HEF"},
            {"Highland", "HLD"},
            {"Hertfordshire", "HRT"},
            {"Humberside", "HUM"},
            {"Huntingdonshire", "HUN"},
            {"Hereford and Worcester", "HWR"},
            {"Inverness-shire", "INV"},
            {"Isle of Wight", "IOW"},
            {"Jersey", "JSY"},
            {"Kincardineshire", "KCD"},
            {"Kent", "KEN"},
            {"Co. Kerry", "KER"},
            {"Co. Kildare", "KID"},
            {"Co. Kilkenny", "KIK"},
            {"Kirkcudbrightshire", "KKD"},
            {"Kinross-shire", "KRS"},
            {"Lancashire", "LAN"},
            {"Co. Londonderry", "LDY"},
            {"Leicestershire", "LEI"},
            {"Co. Leitrim", "LET"},
            {"Co. Laois", "LEX"},
            {"Co. Limerick", "LIM"},
            {"Lincolnshire", "LIN"},
            {"Lanarkshire", "LKS"},
            {"Co. Longford", "LOG"},
            {"Co. Louth", "LOU"},
            {"Lothian", "LTN"},
            {"Co. Mayo", "MAY"},
            {"Co. Meath", "MEA"},
            {"Merionethshire", "MER"},
            {"Mid Glamorgan", "MGM"},
            {"Montgomeryshire", "MGY"},
            {"Midlothian", "MLN"},
            {"Co. Monaghan", "MOG"},
            {"Monmouthshire", "MON"},
            {"Morayshire", "MOR"},
            {"Merseyside", "MSY"},
            {"Nairn", "NAI"},
            {"Northumberland", "NBL"},
            {"Norfolk", "NFK"},
            {"North Riding of Yorkshire", "NRY"},
            {"Northamptonshire", "NTH"},
            {"Nottinghamshire", "NTT"},
            {"North Yorkshire", "NYK"},
            {"Co. Offaly", "OFF"},
            {"Orkney", "OKI"},
            {"Oxfordshire", "OXF"},
            {"Peebles-shire", "PEE"},
            {"Pembrokeshire", "PEM"},
            {"Perth", "PER"},
            {"Powys", "POW"},
            {"Radnorshire", "RAD"},
            {"Renfrewshire", "RFW"},
            {"Ross and Cromarty", "ROC"},
            {"Co. Roscommon", "ROS"},
            {"Roxburghshire", "ROX"},
            {"Rutland", "RUT"},
            {"Shropshire", "SAL"},
            {"Selkirkshire", "SEL"},
            {"Suffolk", "SFK"},
            {"South Glamorgan", "SGM"},
            {"Shetland", "SHI"},
            {"Co. Sligo", "SLI"},
            {"Somerset", "SOM"},
            {"Sark", "SRK"},
            {"Surrey", "SRY"},
            {"Sussex", "SSX"},
            {"Strathclyde", "STD"},
            {"Stirlingshire", "STI"},
            {"Staffordshire", "STS"},
            {"Sutherland", "SUT"},
            {"East Sussex", "SXE"},
            {"West Sussex", "SXW"},
            {"South Yorkshire", "SYK"},
            {"Tayside", "TAY"},
            {"Co. Tipperary", "TIP"},
            {"Tyne and Wear", "TWR"},
            {"Co. Tyrone", "TYR"},
            {"Warwickshire", "WAR"},
            {"Co. Waterford", "WAT"},
            {"Co. Westmeath", "WEM"},
            {"Westmorland", "WES"},
            {"Co. Wexford", "WEX"},
            {"West Glamorgan", "WGM"},
            {"Co. Wicklow", "WIC"},
            {"Wigtownshire", "WIG"},
            {"Wiltshire", "WIL"},
            {"Western Isles", "WIS"},
            {"West Lothian", "WLN"},
            {"West Midlands", "WMD"},
            {"Worcestershire", "WOR"},
            {"West Riding of Yorkshire", "WRY"},
            {"West Yorkshire", "WYK"},
            {"Yorkshire", "YKS"}
        };
        private static Dictionary<string, string> _Canadian_Provinces = new Dictionary<string, string>()
        {
            {"﻿Newfoundland", "NL"},
            {"Labrador", "NL"},
            {"Prince Edward Island", "PE"},
            {"Nova Scotia", "NS"},
            {"New Brunswick", "NB"},
            {"Quebec", "QC"},
            {"Ontario", "ON"},
            {"Manitoba", "MB"},
            {"Saskatchewan", "SK"},
            {"Alberta", "AB"},
            {"British Columbia", "BC"},
            {"Yukon", "YT"},
            {"Northwest Territories", "NT"},
            {"Nunavut", "NU"}
        };
        private static Dictionary<string, string> _Mexican_States = new Dictionary<string, string>()
        {
            {"﻿Aguascalientes", "AG"},
            {"Baja California Norte", "BC"},
            {"Baja California Sur", "BS"},
            {"Chihuahua", "CH"},
            {"Colima", "CL"},
            {"Campeche", "CM"},
            {"Coahuila", "CO"},
            {"Chiapas", "CS"},
            {"Distrito Federal", "DF"},
            {"Durango", "DG"},
            {"Guerrero", "GR"},
            {"Guanajuato", "GT"},
            {"Hidalgo", "HG"},
            {"Jalisco", "JA"},
            {"Michoacan", "MI"},
            {"Morelos", "MO"},
            {"Nayarit", "NA"},
            {"Nuevo Leon", "NL"},
            {"Oaxaca", "OA"},
            {"Puebla", "PU"},
            {"Quintana Roo", "QR"},
            {"Queretaro", "QT"},
            {"Sinaloa", "SI"},
            {"San Luis Potosi", "SL"},
            {"Sonora", "SO"},
            {"Tabasco", "TB"},
            {"Tlaxcala", "TL"},
            {"Tamaulipas", "TM"},
            {"Veracruz", "VE"},
            {"Yucatan", "YU"},
            {"Zacatecas", "ZA"},
            {"Mexico", "EM"}
        };
        private static Dictionary<string, Dictionary<string, string>> _Country_Data = new Dictionary<string, Dictionary<string, string>>()
        {
            {"US", _US_States },
            {"GB", _UK_Counties },
            {"CA", _Canadian_Provinces },
            {"MX", _Mexican_States }
        };
        private static List<string> _Oklahoma_Counties = new List<string>()
        {
            "TestCounty",
            "Adair",
            "Alfalfa",
            "Atoka",
            "Beaver",
            "Beckham",
            "Blaine",
            "Bryan",
            "Caddo",
            "Canadian",
            "Carter",
            "Cherokee",
            "Choctaw",
            "Cimarron",
            "Cleveland",
            "Coal",
            "Comanche",
            "Cotton",
            "Craig",
            "Creek",
            "Custer",
            "Delaware",
            "Dewey",
            "Ellis",
            "Garfield",
            "Garvin",
            "Grady",
            "Grant",
            "Greer",
            "Harmon",
            "Harper",
            "Haskell",
            "Hughes",
            "Jackson",
            "Jefferson",
            "Johnston",
            "Kay",
            "Kingfisher",
            "Kiowa",
            "Latimer",
            "Le Flore",
            "Lincoln",
            "Logan",
            "Love",
            "McClain",
            "McCurtain",
            "McIntosh",
            "Major",
            "Marshall",
            "Mayes",
            "Murray",
            "Muskogee",
            "Noble",
            "Nowata",
            "Okfuskee",
            "Oklahoma",
            "Okmulgee",
            "Osage",
            "Ottawa",
            "Pawnee",
            "Payne",
            "Pittsburg",
            "Pontotoc",
            "Pottawatomie",
            "Pushmataha",
            "Roger Mills",
            "Rogers",
            "Seminole",
            "Sequoyah",
            "Stephens",
            "Texas",
            "Tillman",
            "Tulsa",
            "Wagoner",
            "Washington",
            "Washita",
            "Woods",
            "Woodward"
        };
        /// <summary>
        /// Returns the 2 character abbreviation for the object value if it's a valid US State, or the original value if it is not. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string abbrevUSState(this string object_value)
        {
            if (_US_States.ContainsKey(object_value))
            {
                return _US_States[object_value];
            }
            else
            {
                return object_value;
            }
        }
        /// <summary>
        /// Returns the 2 character abbreviation for the object value if it's a valid Canadian Province, or the original value if it is not. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string abbrevCanadianProvince(this string object_value)
        {
            if (_Canadian_Provinces.ContainsKey(object_value))
            {
                return _Canadian_Provinces[object_value];
            }
            else
            {
                return object_value;
            }
        }
        /// <summary>
        /// Returns the 3 character abbreviation for the object value if it's a valid UK County, or the original value if it is not. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string abbrevUKCounty(this string object_value)
        {
            if (_UK_Counties.ContainsKey(object_value))
            {
                return _UK_Counties[object_value];
            }
            else
            {
                return object_value;
            };
        }
        /// <summary>
        /// Returns the 2 character ISO abbreviation for the object value if it's a valid Country, or the original value if it is not. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string abbrevCountryISO2(this string object_value)
        {
            if (_Countries_ISO_2.ContainsKey(object_value))
            {
                return _Countries_ISO_2[object_value];
            }
            else
            {
                return object_value;
            }
        }
        /// <summary>
        /// Returns the 3 character ISO abbreviation for the object value if it's a valid Country, or the original value if it is not. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string abbrevCountryISO3(this string object_value)
        {
            if (_Countries_ISO_3.ContainsKey(object_value))
            {
                return _Countries_ISO_3[object_value];
            }
            else
            {
                return object_value;
            }
        }
        /// <summary>
        /// Returns the 3 character abbreviation for the object value if it's a valid UK Country, or the original value if it is not. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string abbrevUKCountry(this string object_value)
        {
            if (_UK_Countries.ContainsKey(object_value))
            {
                return _UK_Countries[object_value];
            }
            else
            {
                return object_value;
            }
        }
        /// <summary>
        /// Returns the 2 character abbreviation for the object value if it's a valid Mexican State, or the original value if it is not. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string abbrevMexicanState(this string object_value)
        {
            if (_Mexican_States.ContainsKey(object_value))
            {
                return _Mexican_States[object_value];
            }
            else
            {
                return object_value;
            }
        }
        /// <summary>
        /// Returns the 2 character abbreviation for the object value if it's a valid State of the given Country, or the original value if it is not. The original object
        /// value is overwritten.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="country">[Required] The country the calling object (state) is in.</param>
        public static string abbrevStateByCountry(this string object_value, string country)
        {
            if (_Countries_ISO_2.ContainsKey(country))
            {
                country = _Countries_ISO_2[country];
            }
            if (country.Length == 2 && _Country_Data.ContainsKey(country))
            {
                Dictionary<string, string> states = _Country_Data[country];
                if (states.Keys.Contains(object_value))
                {
                    return states[object_value];
                }
                else
                {
                    return object_value;
                }
            }
            else
            {
                return object_value;
            }
        }
        /// <summary>
        /// Replaces any text in the string that appears to be a valid SSN with "xxx-xx-xxxx".
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string SanitizeSSN(this string object_value)
        {
            string outstr;
            string pattern = "(?!666|000|9\\d{2})(\\d{3}-\\d{2}-\\d{4})";
            outstr = Regex.Replace(object_value, pattern, "xxx-xx-xxxx");
            return outstr;
        }
        public static bool isNumeric(this string object_value)
        {
            long number = 0;
            return long.TryParse(object_value, out number);
        }
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
        public static bool isValidUSState(this string object_value)
        {
            if (_US_States.Keys.Contains(object_value) || _US_States.Values.Contains(object_value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks to see if value is a valid name of an Oklahoma County.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static bool isValidOKCounty(this string object_value)
        {
            if (_Oklahoma_Counties.Contains(object_value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Capitalizes the first letter of the given string.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        public static string ProperNoun(this string object_value)
        {
            if (object_value.Length > 1)
            {
                return char.ToUpper(object_value[0]) + object_value.Substring(1);
            }
            else
            {
                return object_value;
            }

        }
        public static int LevenshteinDistance(this string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
        public static int DamerauLevenshteinDistance(this string s, string t)
        {
            var bounds = new { Height = s.Length + 1, Width = t.Length + 1 };

            int[,] matrix = new int[bounds.Height, bounds.Width];

            for (int height = 0; height < bounds.Height; height++) { matrix[height, 0] = height; };
            for (int width = 0; width < bounds.Width; width++) { matrix[0, width] = width; };

            for (int height = 1; height < bounds.Height; height++)
            {
                for (int width = 1; width < bounds.Width; width++)
                {
                    int cost = (s[height - 1] == t[width - 1]) ? 0 : 1;
                    int insertion = matrix[height, width - 1] + 1;
                    int deletion = matrix[height - 1, width] + 1;
                    int substitution = matrix[height - 1, width - 1] + cost;

                    int distance = Math.Min(insertion, Math.Min(deletion, substitution));

                    if (height > 1 && width > 1 && s[height - 1] == t[width - 2] && s[height - 2] == t[width - 1])
                    {
                        distance = Math.Min(distance, matrix[height - 2, width - 2] + cost);
                    }

                    matrix[height, width] = distance;
                }
            }

            return matrix[bounds.Height - 1, bounds.Width - 1];
        }
        public static string Print(this RangeTuple object_value)
        {
            return string.Format("({0},{1})", object_value.Start, object_value.End);
        }
        public static List<int> ToList(this RangeTuple object_value)
        {
            return new List<int>() { object_value.Start, object_value.End };
        }
        public static string Substring(this RangeTuple object_value, string string_value)
        {
            if(object_value.Length > 0 && string_value.Length > 0)
            {
                if (object_value.Start < string_value.Length)
                {
                    if (string_value.Length >= object_value.Length)
                    {
                        return string_value.Substring(object_value.Start, object_value.Length);
                    }
                    else
                    {
                        return string_value.Substring(object_value.Start, string_value.Length - 1);
                    }
                } else
                {
                    throw new ArgumentException("Start index is larger than given string", nameof(object_value.Start));
                }

            } else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Slices the given array matching the RangeTuple Start and End values. If array is shorter than the length
        /// of the RangeTuple, slice will be from RangeTuple.Start to the end of the given array.
        /// </summary>
        public static string[] ArraySlice<String>(this RangeTuple object_value, string[] array_value)
        {
            if(object_value.Length > 0 && array_value.Length > 0)
            {
                if (object_value.Start < array_value.Length)
                {
                    if (array_value.Length >= object_value.Length)
                    {
                        return new ArraySegment<string>(array_value, object_value.Start, object_value.Length).ToArray();
                    }
                    else
                    {
                        return new ArraySegment<string>(array_value, object_value.Start, array_value.Length - 1).ToArray();
                    }
                }
                else
                {
                    throw new ArgumentException("Start index is larger than given array", nameof(object_value.Start));
                }
            } else
            {
                return new string[0];
            }
        }
        public static List<dynamic> ListSlice(this RangeTuple object_value, List<dynamic> list_value)
        {
            if (object_value.Length > 0 && list_value.Count > 0)
            {
                if (object_value.Start < list_value.Count)
                {
                    if (list_value.Count >= object_value.Length)
                    {
                        return list_value.GetRange(object_value.Start, object_value.Length);
                    }
                    else
                    {
                        return list_value.GetRange(object_value.Start, list_value.Count - 1);
                    }
                }
                else
                {
                    throw new ArgumentException("Start index is larger than given List", nameof(object_value.Start));
                }
            }
            else
            {
                return new List<dynamic>();
            }
        }
    }
    public struct RangeTuple
    {
        public int Start { get; set; }
        private int _end { get; set; }
        public int End { get{ return _end; } set{ _end = _validate(Start, End); } }
        public int Length { get { return (End - Start); } }
        private static int _validate(int start, int end)
        {
            if (start < end)
            {
                return end;
            } else
            {
                throw new ArgumentException("RangeTuple.End cannot be less than RangeTuple.Start", nameof(end));
            }
        }
        public static RangeTuple Parse(string point_str)
        {
            RangeTuple point = new RangeTuple();
            point.Start = int.Parse(point_str.Split(',')[0].Replace("(", ""));
            point.End = int.Parse(point_str.Split(',')[1].Replace(")", ""));
            return point;
        }

    }
}
