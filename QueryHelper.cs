using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace MetaUnpackProject
{
    public class QueryHelper
    {
        public string CompareAddressesQueryCreator(AddAddress item)
        {
            StringBuilder sbCompareAddresses = new StringBuilder("SELECT COUNT(*) FROM tb_evls_address WHERE ");
            sbCompareAddresses.Append("ADDRESS_REFERENCE_NR = '" + item.ADDRESS_REFERENCE_NR + "' AND ");
            sbCompareAddresses.Append("ADDRESS_TITLE = '" + item.ADDRESS_TITLE + "' AND ");
            sbCompareAddresses.Append("ADDRESS_NAME1 = '" + item.ADDRESS_NAME1 + "' AND ");
            sbCompareAddresses.Append("ADDRESS_NAME2 = '" + item.ADDRESS_NAME2 + "' AND ");
            sbCompareAddresses.Append("ADDRESS_NAME3 = '" + item.ADDRESS_NAME3 + "' AND ");
            sbCompareAddresses.Append("ADDRESS_ZIP = '" + item.ADDRESS_ZIP + "' AND ");
            sbCompareAddresses.Append("ADDRESS_CITY = '" + item.ADDRESS_CITY + "' AND ");
            sbCompareAddresses.Append("ADDRESS_STREET = '" + item.ADDRESS_STREET + "' AND ");
            sbCompareAddresses.Append("ADDRESS_HOME_NO = '" + item.ADDRESS_HOME_NO + "' AND ");
            sbCompareAddresses.Append("ADDRESS_HOME_EXT = '" + item.ADDRESS_HOME_EXT + "' AND ");
            sbCompareAddresses.Append("ADDRESS_DISTRICT = '" + item.ADDRESS_DISTRICT + "' AND ");
            sbCompareAddresses.Append("ADDRESS_TEL1 = '" + item.ADDRESS_TEL1 + "' AND ");
            sbCompareAddresses.Append("ADDRESS_MOBILE = '" + item.ADDRESS_MOBILE + "' AND ");
            sbCompareAddresses.Append("ADDRESS_EMAIL = '" + item.ADDRESS_EMAIL + "' AND ");
            sbCompareAddresses.Append("ADDRESS_REMARK = '" + item.ADDRESS_REMARK + "' AND ");
            sbCompareAddresses.Append("ADDRESS_PROVINCE = '" + item.ADDRESS_PROVINCE + "' AND ");
            sbCompareAddresses.Append("ADDRESS_BUILDING = '" + item.ADDRESS_BUILDING + "' AND ");
            sbCompareAddresses.Append("ADDRESS_FLOOR = '" + item.ADDRESS_FLOOR + "' AND ");
            sbCompareAddresses.Append("ADDRESS_AREA = '" + item.ADDRESS_AREA + "' AND ");
            sbCompareAddresses.Append("ADDRESS_DOOR_CODE = '" + item.ADDRESS_DOOR_CODE + "' AND ");
            sbCompareAddresses.Append("ADDRESS_DEPARTMENT = '" + item.ADDRESS_DEPARTMENT + "' AND ");
            sbCompareAddresses.Append("ADDRESS_COMPANY_NAME = '" + item.ADDRESS_COMPANY_NAME + "' AND ");
            sbCompareAddresses.Append("ADDRESS_COUNTRY = '" + item.ADDRESS_COUNTRY + "'");

            return sbCompareAddresses.ToString();
        }

        public string InsertDataQueryCreator(AddAddress item)
        {

            string[] tableColumns = {"ADDRESS_REFERENCE_NR", "ADDRESS_TITLE", "ADDRESS_NAME1", "ADDRESS_NAME2", "ADDRESS_NAME3", "ADDRESS_ZIP", "ADDRESS_CITY",
            "ADDRESS_STREET", "ADDRESS_HOME_NO", "ADDRESS_HOME_EXT", "ADDRESS_DISTRICT", "ADDRESS_TEL1", "ADDRESS_MOBILE", "ADDRESS_EMAIL", "ADDRESS_REMARK",
            "ADDRESS_PROVINCE", "ADDRESS_BUILDING", "ADDRESS_FLOOR", "ADDRESS_AREA", "ADDRESS_DOOR_CODE", "ADDRESS_DEPARTMENT", "ADDRESS_COMPANY_NAME", "ADDRESS_COUNTRY"};

            StringBuilder sbAddAddressessQuery = new StringBuilder("INSERT INTO tb_evls_address (");
            foreach (string column in tableColumns)
            {
                if (!column.Equals("ADDRESS_COUNTRY"))
                {
                    sbAddAddressessQuery.Append(column + ",\n ");
                }
                else
                    sbAddAddressessQuery.Append(column + ") ");
            }
            sbAddAddressessQuery.Append("VALUES (");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_REFERENCE_NR + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_TITLE + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_NAME1 + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_NAME2 + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_NAME3 + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_ZIP + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_CITY + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_STREET + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_HOME_NO + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_HOME_EXT + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_DISTRICT + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_TEL1 + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_MOBILE + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_EMAIL + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_REMARK + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_PROVINCE + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_BUILDING + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_FLOOR + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_AREA + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_DOOR_CODE + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_DEPARTMENT + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_COMPANY_NAME + "', ");
            sbAddAddressessQuery.Append("'" + item.ADDRESS_COUNTRY + "')");

            return sbAddAddressessQuery.ToString();
        }
        public void SqlDataReaderToCSV(MySqlDataReader reader, string path)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path)))
            {
                string[] headers = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    headers[i] = reader.GetName(i);
                }
                writer.WriteLine(string.Join(",", headers));

                while (reader.Read())
                {
                    string[] row = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader.GetValue(i).ToString();
                    }
                    writer.WriteLine(string.Join(",", row));
                }
                reader.Close();
            }
        }
        public string ConvertFileToBase64(string path)
        {
            Byte[] contentBytes = File.ReadAllBytes(path);
            string content = Convert.ToBase64String(contentBytes);
            return content;
        }
    }
}