using System.Text;

namespace MetaUnpackProject
{
    public class AddAddressResponse
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

        public AddAddressResponse(AddAddress item)
        {
            STATUS = "DONE";
            MESSAGE = "ADDRESS HAS BEEN ADDED";
            ADDRESS_REFERENCE_NR = item.ADDRESS_REFERENCE_NR;
            ADDRESS_TITLE = item.ADDRESS_TITLE;
            ADDRESS_NAME1 = item.ADDRESS_NAME1;
            ADDRESS_NAME2 = item.ADDRESS_NAME2;
            ADDRESS_NAME3 = item.ADDRESS_NAME3;
            ADDRESS_ZIP = item.ADDRESS_ZIP;
            ADDRESS_CITY = item.ADDRESS_CITY;
            ADDRESS_STREET = item.ADDRESS_STREET;
            ADDRESS_HOME_NO = item.ADDRESS_HOME_NO;
            ADDRESS_HOME_EXT = item.ADDRESS_HOME_EXT;
            ADDRESS_DISTRICT = item.ADDRESS_DISTRICT;
            ADDRESS_TEL1 = item.ADDRESS_TEL1;
            ADDRESS_MOBILE = item.ADDRESS_MOBILE;
            ADDRESS_EMAIL = item.ADDRESS_EMAIL;
            ADDRESS_REMARK = item.ADDRESS_REMARK;
            ADDRESS_PROVINCE = item.ADDRESS_PROVINCE;
            ADDRESS_BUILDING = item.ADDRESS_BUILDING;
            ADDRESS_FLOOR = item.ADDRESS_FLOOR;
            ADDRESS_AREA = item.ADDRESS_AREA;
            ADDRESS_DOOR_CODE = item.ADDRESS_DOOR_CODE;
            ADDRESS_DEPARTMENT = item.ADDRESS_DEPARTMENT;
            ADDRESS_COMPANY_NAME = item.ADDRESS_COMPANY_NAME;
            ADDRESS_COUNTRY = item.ADDRESS_COUNTRY;
        }        
    }
}