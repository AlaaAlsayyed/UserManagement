using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class SearchUserModel
    {
        public string name { get; set; }

        public string username { get; set; }

        public string addressZipcode { get; set; }

        public string companyName { get; set; }
    }
}