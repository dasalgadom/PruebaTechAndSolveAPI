using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTechAndSolve.Dto
{
    public class UserTripsDto
    {
        public int Id { get; set; }
        public int Document { get; set; }
        public System.DateTime DateProcess { get; set; }
        public string UrlFileProcess { get; set; }
    }
}
