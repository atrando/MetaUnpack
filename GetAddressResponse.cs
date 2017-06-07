using MySql.Data.MySqlClient;

namespace MetaUnpackProject
{
    public class GetAddressResponse
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

        public GetAddressResponse(MySqlDataReader reader)
        {
            if (!reader.IsDBNull(0))
            {
                ADDRESS_REFERENCE_NR = reader[0].ToString();
            }
            if (!reader.IsDBNull(1))
            {
                ADDRESS_TITLE = reader[1].ToString();
            }
            if (!reader.IsDBNull(2))
            {
                ADDRESS_NAME1 = reader[2].ToString();
            }
            if (!reader.IsDBNull(3))
            {
                ADDRESS_NAME2 = reader[3].ToString();
            }
            if (!reader.IsDBNull(4))
            {
                ADDRESS_NAME3 = reader[4].ToString();
            }
            if (!reader.IsDBNull(5))
            {
                ADDRESS_ZIP = reader[5].ToString();
            }
            if (!reader.IsDBNull(6))
            {
                ADDRESS_CITY = reader[6].ToString();
            }
            if (!reader.IsDBNull(7))
            {
                ADDRESS_STREET = reader[7].ToString();
            }
            if (!reader.IsDBNull(8))
            {
                ADDRESS_HOME_NO = reader[8].ToString();
            }
            if (!reader.IsDBNull(9))
            {
                ADDRESS_HOME_EXT = reader[9].ToString();
            }
            if (!reader.IsDBNull(10))
            {
                ADDRESS_DISTRICT = reader[10].ToString();
            }
            if (!reader.IsDBNull(11))
            {
                ADDRESS_TEL1 = reader[11].ToString();
            }
            if (!reader.IsDBNull(12))
            {
                ADDRESS_MOBILE = reader[12].ToString();
            }
            if (!reader.IsDBNull(13))
            {
                ADDRESS_EMAIL = reader[13].ToString();
            }
             if (!reader.IsDBNull(14))
            {
                ADDRESS_REMARK = reader[14].ToString();
            }
            if (!reader.IsDBNull(15))
            {
                ADDRESS_PROVINCE = reader[15].ToString();
            }
            if (!reader.IsDBNull(16))
            {
                ADDRESS_BUILDING = reader[16].ToString();
            }
            if (!reader.IsDBNull(17))
            {
                ADDRESS_FLOOR = reader[17].ToString();
            }
            if (!reader.IsDBNull(18))
            {
                ADDRESS_AREA = reader[18].ToString();
            }
            if (!reader.IsDBNull(19))
            {
                ADDRESS_DOOR_CODE = reader[19].ToString();
            }
            if (!reader.IsDBNull(20))
            {
                ADDRESS_DEPARTMENT = reader[20].ToString();
            }
            if (!reader.IsDBNull(21))
            {
                ADDRESS_COMPANY_NAME = reader[21].ToString();
            }
            if (!reader.IsDBNull(22))
            {
                ADDRESS_COUNTRY = reader[22].ToString();
            }  
            STATUS = "SUCCESS";
            MESSAGE = "ADDRESS HAS BEEN DISPLAYED";
        } 
    }
}