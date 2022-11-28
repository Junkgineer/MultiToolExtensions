using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiToolExtensions
{
    public static class Geography
    {
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
        /// Checks to see if string object value is a valid name of an Oklahoma County.
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
    }
}
