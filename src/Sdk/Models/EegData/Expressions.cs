using System;
using System.Collections.Generic;

namespace Sdk.Models
{
    [Serializable()]
    public class Expressions
    {
        //Eyes
        public List<double> Blink = new List<double>();

        public List<double> WinkRight = new List<double>();
        public List<double> WinkLeft = new List<double>();
        public List<double> LookRight = new List<double>();
        public List<double> LookLeft = new List<double>();

        //Upper Face
        public List<double> BrowRaise = new List<double>();
        public List<double> BrowFurrow = new List<double>();

        //Lower Face
        public List<double> Smile = new List<double>();
        public List<double> Clench = new List<double>();
        public List<double> SmirkRight = new List<double>();
        public List<double> SmirkLeft = new List<double>();
        public List<double> Laugh = new List<double>();
    }
}