using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;
using System.Drawing;

namespace MultiToolExtensions
{
    /// <summary>
    /// A collection of uncategorized, misc tools.
    /// </summary>
    public static class MiscExtensions
    {
        /// <summary>
        /// Returns a random color with an alpha of 255
        /// </summary>
        /// <param name="object_value"></param>
        /// <returns></returns>
        public static Color RandomColor(this Color object_value)
        {
            var rand = new Random();
            var bytes = new byte[3];
            rand.NextBytes(new byte[3]);
            Color randColor = Color.FromArgb(255, bytes[0], bytes[1], bytes[2]);
            return randColor;
        }
        /// <summary>
        /// Returns a random color with the given alpha value
        /// </summary>
        /// <param name="object_value"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color RandomColor(this Color object_value, byte alpha)
        {
            var rand = new Random();
            var bytes = new byte[3];
            rand.NextBytes(new byte[3]);
            Color randColor = Color.FromArgb(255, bytes[0], bytes[1], bytes[2]);
            return randColor;
        }
        /// <summary>
        /// Updates the value of the given key in the appSettings section of the project App.config to the value of the string.
        /// </summary>
        public static void SaveAppConfigSetting(this string object_value, string key)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = object_value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        /// <summary>
        /// Writes the contents of the string to a text file at the given path.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
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
        /// Returns a list of indexes in the string where [value] is located.
        /// </summary>
        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("The string to find must not be empty", "value");
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
        /// Removes unsafe characters that are commonly used as spaces (or spaces themselves)
        /// and replaces them with underscores.
        /// </summary>
        public static string CleanFileName(this string object_value)
        {
            string outstr = object_value;
            foreach (KeyValuePair<string, string> spc in Definitions.SpecialCharSubstitutions)
            {
                outstr = outstr.Replace(spc.Key, spc.Value);
            }
            outstr = outstr.Replace(".", "_");
            outstr = outstr.Replace(" ", "_");
            return outstr;
        }
        /// <summary>
        /// Converts a List/<string/> object to a string using the given delimiter.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="delimiter">[Optional] The delimiter to use (defaults to a comma).</param>
        public static string toStringList(this List<string> object_value, string delimiter=",")
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
        /// Replaces any text in the string that appears to be a valid SSN with "xxx-xx-xxxx".
        /// WARNING: This is NOT to be a considered an absolute 'fail safe' sanitizer for PII, and will
        /// also treat strings with a similar structure as an SSN. Use discretion!
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

        /// <summary>
        /// Capitalizes the first letter of the given string object value.
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
        /// <summary>
        /// Calculates the Levenshtein Distance score of the string object value and the given string parameter.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="t">[Required] The comparison string.</param>
        public static int LevenshteinDistance(this string object_value, string t)
        {
            int n = object_value.Length;
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
                    int cost = (t[j - 1] == object_value[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
        /// <summary>
        /// Calculates the Levenshtein Distance score of the string object value and the given string parameter with the inclusion of transpositions.
        /// </summary>
        /// <param name="object_value">[Required] The calling object.</param>
        /// <param name="t">[Required] The comparison string.</param>
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

    }

}
