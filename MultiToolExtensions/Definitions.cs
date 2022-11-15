﻿using System.Collections.Generic;

namespace MultiToolExtensions
{
    /// <summary>
    /// A static class containing various dictionaries to aid in standardizing character 
    /// subsitution and other misc tasks.
    /// </summary>
    public static class Definitions
    {
        /// <summary>
        /// A standardized set of file-safe replacements for special characters commonly found in Microsoft Access Database queries, table and column names.
        /// </summary>
        public static Dictionary<string, string> SpecialCharSubstitutions = new Dictionary<string, string>()
        {
            {"$", "__DOL__" },
            {"&", "__AMP__" },
            {"(", "__OPR__" },
            {")", "__CPR__" },
            {"%", "__MOD__" },
            {"!", "__BNG__" },
            {"@", "__CAT__" },
            {"^", "__CAR__" },
            {"*", "__AST__" },
            {"#", "__PND__" },
            {"~", "__TLD__" },
            {"`", "__ACC__" },
            {"?", "__QUE__" },
            {">", "__GRT__" },
            {"<", "__LST__" },
            {"|", "__PPE__" },
            {":", "__COL__" },
            {";", "__SMC__" },
            {"/", "__FSL__" },
            {"\\", "__BSL__" },
            {"\"", "__DBQ__" },
            {"'", "__SGQ__" }
        };
    }
    /// <summary>
    /// Dictionaries and Lists of ODBC and T-SQL specific reserved words and operators. 
    /// </summary>
    public static class SQLDefinitions
    {
        /// <summary>
        /// Find and Replace based Dictionary for changing the common MS Access query syntax to valid T-SQL.
        /// </summary>
        public static Dictionary<string, string> Easy_Clean = new Dictionary<string, string>() {
            { "DELETE *", "DELETE" },
            { "DISTINCTROW", "DISTINCT" },
            { "&", "+" },
            { "]!","]." },
            { "\"","'" }
        };
        /// <summary>
        /// List of primary query types to aid in query classification during parsing.
        /// </summary>
        public static List<string> PrimaryQueryTypes = new List<string>() { "SELECT", "INSERT", "UPDATE", "DELETE", "TRANSFORM", "PARAMETERS" };
        /// <summary>
        /// List of all ODBC reserved words to aid in programmatic query creation and parsing.
        /// </summary>
        public static List<string> Reserved_ODBC = new List<string>()
        {
            "ABSOLUTE",
            "EXEC",
            "OVERLAPS",
            "ACTION",
            "EXECUTE",
            "PAD",
            "ADA",
            "EXISTS",
            "PARTIAL",
            "ADD",
            "EXTERNAL",
            "PASCAL",
            "ALL",
            "EXTRACT",
            "POSITION",
            "ALLOCATE",
            "FALSE",
            "PRECISION",
            "ALTER",
            "FETCH",
            "PREPARE",
            "AND",
            "FIRST",
            "PRESERVE",
            "ANY",
            "FLOAT",
            "PRIMARY",
            "ARE",
            "FOR",
            "PRIOR",
            "AS",
            "FOREIGN",
            "PRIVILEGES",
            "ASC",
            "FORTRAN",
            "PROCEDURE",
            "ASSERTION",
            "FOUND",
            "PUBLIC",
            "AT",
            "FROM",
            "READ",
            "AUTHORIZATION",
            "FULL",
            "REAL",
            "AVG",
            "GET",
            "REFERENCES",
            "BEGIN",
            "GLOBAL",
            "RELATIVE",
            "BETWEEN",
            "GO",
            "RESTRICT",
            "BIT",
            "GOTO",
            "REVOKE",
            "BIT_LENGTH",
            "GRANT",
            "RIGHT",
            "BOTH",
            "GROUP",
            "ROLLBACK",
            "BY",
            "HAVING",
            "ROWS",
            "CASCADE",
            "HOUR",
            "SCHEMA",
            "CASCADED",
            "IDENTITY",
            "SCROLL",
            "CASE",
            "IMMEDIATE",
            "SECOND",
            "CAST",
            "IN",
            "SECTION",
            "CATALOG",
            "INCLUDE",
            "SELECT",
            "CHAR",
            "INDEX",
            "SESSION",
            "CHAR_LENGTH",
            "INDICATOR",
            "SESSION_USER",
            "CHARACTER",
            "INITIALLY",
            "SET",
            "CHARACTER_LENGTH",
            "INNER",
            "SIZE",
            "CHECK",
            "INPUT",
            "SMALLINT",
            "CLOSE",
            "INSENSITIVE",
            "SOME",
            "COALESCE",
            "INSERT",
            "SPACE",
            "COLLATE",
            "INT",
            "SQL",
            "COLLATION",
            "INTEGER",
            "SQLCA",
            "COLUMN",
            "INTERSECT",
            "SQLCODE",
            "COMMIT",
            "INTERVAL",
            "SQLERROR",
            "CONNECT",
            "INTO",
            "SQLSTATE",
            "CONNECTION",
            "IS",
            "SQLWARNING",
            "CONSTRAINT",
            "ISOLATION",
            "SUBSTRING",
            "CONSTRAINTS",
            "JOIN",
            "SUM",
            "CONTINUE",
            "KEY",
            "SYSTEM_USER",
            "CONVERT",
            "LANGUAGE",
            "TABLE",
            "CORRESPONDING",
            "LAST",
            "TEMPORARY",
            "COUNT",
            "LEADING",
            "THEN",
            "CREATE",
            "LEFT",
            "TIME",
            "CROSS",
            "LEVEL",
            "TIMESTAMP",
            "CURRENT",
            "LIKE",
            "TIMEZONE_HOUR",
            "CURRENT_DATE",
            "LOCAL",
            "TIMEZONE_MINUTE",
            "CURRENT_TIME",
            "LOWER",
            "TO",
            "CURRENT_TIMESTAMP",
            "MATCH",
            "TRAILING",
            "CURRENT_USER",
            "MAX",
            "TRANSACTION",
            "CURSOR",
            "MIN",
            "TRANSLATE",
            "DATE",
            "MINUTE",
            "TRANSLATION",
            "DAY",
            "MODULE",
            "TRIM",
            "DEALLOCATE",
            "MONTH",
            "TRUE",
            "DEC",
            "NAMES",
            "UNION",
            "DECIMAL",
            "NATIONAL",
            "UNIQUE",
            "DECLARE",
            "NATURAL",
            "UNKNOWN",
            "DEFAULT",
            "NCHAR",
            "UPDATE",
            "DEFERRABLE",
            "NEXT",
            "UPPER",
            "DEFERRED",
            "NO",
            "USAGE",
            "DELETE",
            "NONE",
            "USER",
            "DESC",
            "NOT",
            "USING",
            "DESCRIBE",
            "NULL",
            "VALUE",
            "DESCRIPTOR",
            "NULLIF",
            "VALUES",
            "DIAGNOSTICS",
            "NUMERIC",
            "VARCHAR",
            "DISCONNECT",
            "OCTET_LENGTH",
            "VARYING",
            "DISTINCT",
            "OF",
            "VIEW",
            "DOMAIN",
            "ON",
            "WHEN",
            "DOUBLE",
            "ONLY",
            "WHENEVER",
            "DROP",
            "OPEN",
            "WHERE",
            "ELSE",
            "OPTION",
            "WITH",
            "END",
            "OR",
            "WORK",
            "END-EXEC",
            "ORDER",
            "WRITE",
            "ESCAPE",
            "OUTER",
            "YEAR",
            "EXCEPT",
            "OUTPUT",
            "ZONE",
            "EXCEPTION"
        };
        /// <summary>
        /// List of all T-SQL reserved words to aid in programmatic query creation and parsing.
        /// </summary>
        public static List<string> Reserved_TSQL = new List<string>() {
        "ADD",
        "EXTERNAL",
        "PROCEDURE",
        "ALL",
        "FETCH",
        "PUBLIC",
        "ALTER",
        "FILE",
        "RAISERROR",
        "AND",
        "FILLFACTOR",
        "READ",
        "ANY",
        "FOR",
        "READTEXT",
        "AS",
        "FOREIGN",
        "RECONFIGURE",
        "ASC",
        "FREETEXT",
        "REFERENCES",
        "AUTHORIZATION",
        "FREETEXTTABLE",
        "REPLICATION",
        "BACKUP",
        "FROM",
        "RESTORE",
        "BEGIN",
        "FULL",
        "RESTRICT",
        "BETWEEN",
        "FUNCTION",
        "RETURN",
        "BREAK",
        "GOTO",
        "REVERT",
        "BROWSE",
        "GRANT",
        "REVOKE",
        "BULK",
        "GROUP",
        "RIGHT",
        "BY",
        "HAVING",
        "ROLLBACK",
        "CASCADE",
        "HOLDLOCK",
        "ROWCOUNT",
        "CASE",
        "IDENTITY",
        "ROWGUIDCOL",
        "CHECK",
        "IDENTITY_INSERT",
        "RULE",
        "CHECKPOINT",
        "IDENTITYCOL",
        "SAVE",
        "CLOSE",
        "IF",
        "SCHEMA",
        "CLUSTERED",
        "IN",
        "SECURITYAUDIT",
        "COALESCE",
        "INDEX",
        "SELECT",
        "COLLATE",
        "INNER",
        "SEMANTICKEYPHRASETABLE",
        "COLUMN",
        "INSERT",
        "SEMANTICSIMILARITYDETAILSTABLE",
        "COMMIT",
        "INTERSECT",
        "SEMANTICSIMILARITYTABLE",
        "COMPUTE",
        "INTO",
        "SESSION_USER",
        "CONSTRAINT",
        "IS",
        "SET",
        "CONTAINS",
        "JOIN",
        "SETUSER",
        "CONTAINSTABLE",
        "KEY",
        "SHUTDOWN",
        "CONTINUE",
        "KILL",
        "SOME",
        "CONVERT",
        "LEFT",
        "STATISTICS",
        "CREATE",
        "LIKE",
        "SYSTEM_USER",
        "CROSS",
        "LINENO",
        "TABLE",
        "CURRENT",
        "LOAD",
        "TABLESAMPLE",
        "CURRENT_DATE",
        "MERGE",
        "TEXTSIZE",
        "CURRENT_TIME",
        "NATIONAL",
        "THEN",
        "CURRENT_TIMESTAMP",
        "NOCHECK",
        "TO",
        "CURRENT_USER",
        "NONCLUSTERED",
        "TOP",
        "CURSOR",
        "NOT",
        "TRAN",
        "DATABASE",
        "NULL",
        "TRANSACTION",
        "DBCC",
        "NULLIF",
        "TRIGGER",
        "DEALLOCATE",
        "OF",
        "TRUNCATE",
        "DECLARE",
        "OFF",
        "TRY_CONVERT",
        "DEFAULT",
        "OFFSETS",
        "TSEQUAL",
        "DELETE",
        "ON",
        "UNION",
        "DENY",
        "OPEN",
        "UNIQUE",
        "DESC",
        "OPENDATASOURCE",
        "UNPIVOT",
        "DISK",
        "OPENQUERY",
        "UPDATE",
        "DISTINCT",
        "OPENROWSET",
        "UPDATETEXT",
        "DISTRIBUTED",
        "OPENXML",
        "USE",
        "DOUBLE",
        "OPTION",
        "USER",
        "DROP",
        "OR",
        "VALUES",
        "DUMP",
        "ORDER",
        "VARYING",
        "ELSE",
        "OUTER",
        "VIEW",
        "END",
        "OVER",
        "WAITFOR",
        "ERRLVL",
        "PERCENT",
        "WHEN",
        "ESCAPE",
        "PIVOT",
        "WHERE",
        "EXCEPT",
        "PLAN",
        "WHILE",
        "EXEC",
        "PRECISION",
        "WITH",
        "EXECUTE",
        "PRIMARY",
        "WITHIN",
        "GROUP",
        "EXISTS",
        "PRINT",
        "WRITETEXT",
        "EXIT",
        "PROC"
    };
        /// <summary>
        /// List of all T-SQL functions to aid in programmatic query creation and parsing.
        /// </summary>
        public static List<string> Functions_TSQL = new List<string>() {
        "ASCII",
        "CHAR",
        "CHARINDEX",
        "CONCAT",
        "DATALENGTH",
        "LEFT",
        "LEN",
        "LOWER",
        "LTRIM",
        "NCHAR",
        "PATINDEX",
        "REPLACE",
        "RIGHT",
        "RTRIM",
        "SPACE",
        "STR",
        "STUFF",
        "SUBSTRING",
        "UPPER",
        "ABS",
        "AVG",
        "CEILING",
        "COUNT",
        "FLOOR",
        "MAX",
        "MIN",
        "RAND",
        "ROUND",
        "SIGN",
        "SUM",
        "CURRENT_TIMESTAMP",
        "DATEADD",
        "DATEDIFF",
        "DATENAME",
        "DATEPART",
        "DAY",
        "GETDATE",
        "GETUTCDATE",
        "MONTH",
        "YEAR",
        "CAST",
        "CONVERT",
        "CASE",
        "COALESCE",
        "CURRENT_USER",
        "IIF",
        "ISDATE",
        "ISNULL",
        "ISNUMERIC",
        "LEAD",
        "NULLIF",
        "SESSION_USER",
        "SESSIONPROPERTY",
        "SYSTEM_USER",
        "USER_NAME"
    };
        /// <summary>
        /// List of all T-SQL JOINs to aid in programmatic query creation and parsing.
        /// </summary>
        public static List<string> Joins_TSQL = new List<string>() {
        "INNER",
        "LEFT",
        "RIGHT",
        "FULL",
        "SELF",
        "CARTESIAN"
    };
        /// <summary>
        /// List of all T-SQL logical operators to aid in programmatic query creation and parsing.
        /// </summary>
        public static List<string> Logical_Operators_TSQL = new List<string>() {
        "ALL",
        "AND",
        "ANY",
        "BETWEEN",
        "EXISTS",
        "IN",
        "LIKE",
        "NOT",
        "OR",
        "SOME"
    };
        /// <summary>
        /// List of all T-SQL comparison operators to aid in programmatic query creation and parsing.
        /// </summary>
        public static List<string> Comparison_Operators_TSQL = new List<string>() {
        "=",
        ">",
        "<",
        ">=",
        "<=",
        "<>"
    };
    }
    /// <summary>
    /// Dictionaries and Lists of Microsoft Access Database specific reserved words,
    /// object attributes, as well as some VBA built-in functions.
    /// </summary>
    public static class MSAccessDefinitions
    {
        /// <summary>
        /// List of all parameters found in MS Access Form objects, such as textboxes, labels, etc to aid in code parsing.
        /// </summary>
        public static List<string> PrimaryObjectParameters { get; set; } = new List<string>() {
            "Name",
            "Width",
            "Height",
            "Top",
            "Left",
            "Visible",
            "EventProcPrefix",
            "FontName",
            "FontSize",
            "FontWeight",
            "TextAlign",
            "ForeColor",
            "BackColor",
            "Width",
            "Alignment",
            "ForeColor",
            "BackColor",
            "TopPadding",
            "RightPadding",
            "BottomPadding",
            "LeftPadding",
            "TopMargin",
            "RightMargin",
            "BottomMargin",
            "LeftMargin",
            "Caption",
            "BorderStyle"
        };
        /// <summary>
        /// List of all DoCmds in MS Access VBA scripting to aid in code parsing.
        /// </summary>
        public static List<string> DoCmd = new List<string>() {
            "AddMenu",
            "ApplyFilter",
            "Beep",
            "BrowseTo",
            "CancelEvent",
            "ClearMacroError",
            "Close",
            "CloseDatabase",
            "CopyDatabaseFile",
            "CopyObject",
            "DeleteObject",
            "DoMenuItem",
            "Echo",
            "FindNext",
            "FindRecord",
            "GoToControl",
            "GoToPage",
            "GoToRecord",
            "Hourglass",
            "LockNavigationPane",
            "Maximize",
            "Minimize",
            "MoveSize",
            "NavigateTo",
            "OpenDataAccessPage",
            "OpenDiagram",
            "OpenForm",
            "OpenFunction",
            "OpenModule",
            "OpenQuery",
            "OpenReport",
            "OpenStoredProcedure",
            "OpenTable",
            "OpenView",
            "OutputTo",
            "PrintOut",
            "Quit",
            "RefreshRecord",
            "Rename",
            "RepaintObject",
            "Requery",
            "Restore",
            "RunCommand",
            "RunDataMacro",
            "RunMacro",
            "RunSavedImportExport",
            "RunSQL",
            "Save",
            "SearchForRecord",
            "SelectObject",
            "SendObject",
            "SetDisplayedCategories",
            "SetFilter",
            "SetMenuItem",
            "SetOrderBy",
            "SetParameter",
            "SetProperty",
            "SetWarnings",
            "ShowAllRecords",
            "ShowToolbar",
            "SingleStep",
            "TransferDatabase",
            "TransferSharePointList",
            "TransferSpreadsheet",
            "TransferSQLDatabase",
            "TransferText"
            };
        /// <summary>
        /// List of all Events in MS Access VBA scripting to aid in code parsing.
        /// </summary>
        public static List<string> EventList = new List<string>() {
            "OnClick",
            "OnClickEmMacro",
            "BeforeUpdate",
            "BeforeUpdateEmMacro",
            "AfterUpdate",
            "AfterUpdateEmMacro",
            "OnGotFocus",
            "OnGotFocusEmMacro",
            "OnLostFocus",
            "OnLostFocusEmMacro",
            "OnDblClick",
            "OnDblClickEmMacro",
            "OnMouseDown",
            "OnMouseDownEmMacro",
            "OnMouseUp",
            "OnMouseUpEmMacro",
            "OnMouseMove",
            "OnMouseMoveEmMacro",
            "OnKeyDown",
            "OnKeyDownEmMacro",
            "OnKeyUp",
            "OnKeyUpEmMacro",
            "OnKeyPress",
            "OnKeyPressEmMacro",
            "OnEnter",
            "OnEnterEmMacro",
            "OnExit",
            "OnExitEmMacro"
        };
        /// <summary>
        /// Lookup Dictionary for normalizing MS Access Form object border styles to aid in object parsing.
        /// </summary>
        public static Dictionary<int, string> BorderStyles = new Dictionary<int, string>()
        {
            {0, "hidden" },
            {1, "solid" },
            {2, "dashed" },
            {3, "dashed" },
            {4, "dotted" },
            {5, "dotted" },
            {6, "dotted dashed" },
            {7, "dashed dotted dotted" },
            {8, "double" }
        };
        /// <summary>
        /// Lookup Dictionary for normalizing MS Access Form section names to aid in Form parsing.
        /// </summary>
        public static Dictionary<int, string> SectionNames = new Dictionary<int, string>()
        {
            { 0, "acDetail" },
            { 1, "acHeader"},
            { 2, "acFooter" },
            { 3, "acPageHeader" },
            { 4, "acPageFooter" }
        };
        /// <summary>
        /// Lookup Dictionary for normalizing MS Access Form Form object types to aid in Form parsing.
        /// </summary>
        public static Dictionary<int, string> ControlTypes = new Dictionary<int, string>()
        {
            {100, "acLabel"},
            {101, "acRectangle"},
            {102, "acLine"},
            {103, "acImage"},
            {104, "acCommandButton"},
            {105, "acOptionButton"},
            {106, "acCheckBox"},
            {107, "acOptionGroup"},
            {108, "acBoundObjectFrame"},
            {109, "acTextBox"},
            {110, "acListBox"},
            {111, "acComboBox"},
            {112, "acSubForm"},
            {114, "acObjectFrame"},
            {118, "acPageBreak"},
            {119, "acCustomControl"},
            {122, "acToggleButton"},
            {123, "acTabCtl"},
            {124, "acPage"},
            {126, "acAttachment"},
            {127, "acEmptyCell" },
            {128, "acWebBrowser"},
            {129, "acNavigationControl"},
            {130, "acNavigationButton"}
        };
        /// <summary>
        /// Lookup Dictionary for normalizing MS Access table column types to aid in schema parsing.
        /// Please refer to source for additional information: http://allenbrowne.com/ser-49.html
        /// </summary>
        public static Dictionary<int, string> ColumnTypes = new Dictionary<int, string>()
        {
            {1, "bit" },                // dbBoolean
            {2, "binary" },             // dbByte
            {3, "int" },                // dbInteger
            {4, "bigint" },             // dbLong
            {5, "money" },              // dbCurrency
            {6, "tinyint" },            // dbSingle
            {7, "int" },                // dbDouble
            {8, "datetime" },           // dbDate
            {10, "varchar(max)" },      // dbText
            {9, "binary"},              // dbBinary
            {11, "binary" },            // dbLongBinary
            {12, "varchar(max)" },      // dbMemo
            {15, "uniqueidentifier" },  // dbGUID
            {20, "decimal" },           // dbDecimal
            {101, "varchar(max)" },     // dbAttachment
            {102, "binary" },           // dbComplexByte
            {103, "int" },              // dbComplexInteger
            {104, "bigint" },           // dbComplexLong
            {105, "bigint" },           // dbComplexSingle
            {106, "bigint" },           // dbComplexDouble
            {107, "uniqueidentifier" }, // dbComplexGUID
            {108, "decimal" },          // dbComplexDecimal
            {109, "varchar(max)" }      // dbComplexText
        };
    }
}
