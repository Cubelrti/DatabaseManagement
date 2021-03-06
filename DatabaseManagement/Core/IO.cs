﻿using DatabaseManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core
{
    class IO
    {
        public const string filename = "ti.db";
        public static void Serialize(Main instance, string filename = filename)
        {
            using (var filestream = File.OpenWrite(filename))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(filestream, instance.databases);
            }
                
        }
        public static void Deserialize(Main instance, string filename = filename)
        {
            if (File.Exists(filename))
            {
                using (var filestream = File.OpenRead(filename))
                {
                    BinaryFormatter f = new BinaryFormatter();
                    instance.databases = (List<Database>)f.Deserialize(filestream);
                }

            }
        }
    }
}
