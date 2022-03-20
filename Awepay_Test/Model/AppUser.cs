using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Awepay_Test.Model
{
    public class AppUser
    {
        [KeyAttribute]
         public Guid  id { get; set; }

        [RequiredAttribute]
        public string name { get; set; }

        [RequiredAttribute]
        public string email { get; set; }
        public int phone { get; set; }
        public int age { get; set; }
    }
}
