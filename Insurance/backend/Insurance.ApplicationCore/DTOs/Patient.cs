using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Insurance.ApplicationCore.DTOs
{
    public class Patient
    {
      
        public string Id { get; set; }

        public string Name { get; set; }

        public string ContactNo { get; set; } 

        public string Type { get; set; } 


        public int Age { get; set; }

      
        public string InsuredBy { get; set; } 

        public List<string> VisitType { get; set; } 

        public string Description { get; set; } 
    }


}
