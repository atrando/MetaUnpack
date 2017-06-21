using MySql.Data.MySqlClient;

namespace MetaUnpackProject
{
    public class ImportAddressesResponse
    {
        public string ADDRESS_REFERENCE_NR { get; set; }
        public string ADDRESS_TITLE { get; set; }
        public string ADDRESS_NAME1 { get; set; }
        public string ADDRESS_NAME2 { get; set; }
        public string ADDRESS_NAME3 { get; set; }
        public string ADDRESS_ZIP { get; set; }
        public string ADDRESS_CITY { get; set; }
        public string ADDRESS_STREET { get; set; }
        public string ADDRESS_HOME_NO { get; set; }
        public string ADDRESS_HOME_EXT { get; set; }
        public string ADDRESS_DISTRICT { get; set; }
        public string ADDRESS_TEL1 { get; set; }
        public string ADDRESS_MOBILE { get; set; }
        public string ADDRESS_EMAIL { get; set; }
        public string ADDRESS_REMARK { get; set; }
        public string ADDRESS_PROVINCE { get; set; }
        public string ADDRESS_BUILDING { get; set; }
        public string ADDRESS_FLOOR { get; set; }
        public string ADDRESS_AREA { get; set; }
        public string ADDRESS_DOOR_CODE { get; set; }
        public string ADDRESS_DEPARTMENT { get; set; }
        public string ADDRESS_COMPANY_NAME { get; set; }
        public string ADDRESS_COUNTRY { get; set; }
        public string STATUS { get; set; }
        public string MESSAGE { get; set; }

        public ImportAddressesResponse(MySqlDataReader reader)
        {
            ADDRESS_REFERENCE_NR = reader["ADDRESS_REFERENCE_NR"].ToString();
            ADDRESS_TITLE = reader["ADDRESS_TITLE"].ToString();
            ADDRESS_NAME1 = reader["ADDRESS_NAME1"].ToString();
            ADDRESS_NAME2 = reader["ADDRESS_NAME2"].ToString();
            ADDRESS_NAME3 = reader["ADDRESS_NAME3"].ToString();
            ADDRESS_ZIP = reader["ADDRESS_ZIP"].ToString();
            ADDRESS_CITY = reader["ADDRESS_CITY"].ToString();
            ADDRESS_STREET = reader["ADDRESS_STREET"].ToString();
            ADDRESS_HOME_NO = reader["ADDRESS_HOME_NO"].ToString();
            ADDRESS_HOME_EXT = reader["ADDRESS_HOME_EXT"].ToString();
            ADDRESS_DISTRICT = reader["ADDRESS_DISTRICT"].ToString();
            ADDRESS_TEL1 = reader["ADDRESS_TEL1"].ToString();
            ADDRESS_MOBILE = reader["ADDRESS_MOBILE"].ToString();
            ADDRESS_EMAIL = reader["ADDRESS_EMAIL"].ToString();
            ADDRESS_REMARK = reader["ADDRESS_REMARK"].ToString();
            ADDRESS_PROVINCE = reader["ADDRESS_PROVINCE"].ToString();
            ADDRESS_BUILDING = reader["ADDRESS_BUILDING"].ToString();
            ADDRESS_FLOOR = reader["ADDRESS_FLOOR"].ToString();
            ADDRESS_AREA = reader["ADDRESS_AREA"].ToString();
            ADDRESS_DOOR_CODE = reader["ADDRESS_DOOR_CODE"].ToString();
            ADDRESS_DEPARTMENT = reader["ADDRESS_DEPARTMENT"].ToString();
            ADDRESS_COMPANY_NAME = reader["ADDRESS_COMPANY_NAME"].ToString();
            ADDRESS_COUNTRY = reader["ADDRESS_COUNTRY"].ToString();
            STATUS = "SUCCESS";
            MESSAGE = "ADDRESS SUCCESFULY IMPORTED";
        }
    }
}