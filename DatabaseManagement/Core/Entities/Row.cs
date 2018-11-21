using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManagement.Core.Entities
{
    public enum Types
    {
        VARCHAR,
        INTEGER,
        DOUBLE,
        DATE,
        CHAR
    }
    [Serializable()]
    public class Row
    {
        public string name;
        
        // 这里kv不检查类型，类型信息统一存储在constraint中
        public Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();


        public Dictionary<string, object> data
        {
            get { return keyValuePairs; }
        }


    }
}
