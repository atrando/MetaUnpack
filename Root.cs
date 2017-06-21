
using System;
using System.Collections.Generic;

namespace MetaUnpackProject
{
    public class Root
    {
        public List<AddAddress> AddAddress { get; set; }
        public GetAddressCount GetAddressCount { get; set; }
        public GetAddresses GetAddresses { get; set; }
        public GetAddresses GetAddressesCSV { get; set; }
        public List<AddAddressResponse> AddAddressResponse { get; set; }
        public ImportAddress ImportAddress { get; set; }
        public ImportAddress ExportAddress { get; set; }
    }
}