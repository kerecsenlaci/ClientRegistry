﻿using CRegistry.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRegistry
{
    public class County
    {
        public int ID { get; set; }
        public string CountyName { get; set; }
        public County()
        {

        }
        public County(CountyDbModel dbModel)
        {
            ID = dbModel.ID;
            CountyName = dbModel.CountyName;
        }
    }
}
