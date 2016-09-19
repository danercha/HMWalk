using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HWalk.STATIC
{
    public class WalkStuff
    {
    }

    public class LocalWalker
    {
        public int Walker_Id { get; set; }
        public string Name { get; set; }
        public double Milage { get; set; }
    }

    public class LocalMilage
    {
        public int Mileage_Id { get; set; }
        public int Walker_Id { get; set; }
        public double Mileage { get; set; }
        public DateTime Mileage_Date { get; set; }
    }
}