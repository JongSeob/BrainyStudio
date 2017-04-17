using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdk.Models
{
    [Serializable()]
    public class Expressions
    {
        //Eyes
        public List<Double> Blink = new List<double>();
        public List<Double> WinkRight = new List<double>();
        public List<Double> WinkLeft = new List<double>();
        public List<Double> LookRight = new List<double>();
        public List<Double> LookLeft = new List<double>();

        //Upper Face
        public List<Double> BrowRaise = new List<double>();
        public List<Double> BrowFurrow = new List<double>();

        //Lower Face
        public List<Double> Smile = new List<double>();
        public List<Double> Clench = new List<double>();
        public List<Double> SmirkRight = new List<double>();
        public List<Double> SmirkLeft = new List<double>();
        public List<Double> Laugh = new List<double>();
    }
}
