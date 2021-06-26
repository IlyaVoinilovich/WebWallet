using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWal.Helpers
{
    namespace FakeUserApi.Models
    {
        /// <summary>
        /// MyLogEvents
        /// </summary>
        public class LogEvents
        {
            /// <summary>
            /// GenerateItems
            /// </summary>
            public const int GenerateItems = 1000;
            /// <summary>
            /// 
            /// </summary>
            public const int ListItems = 1001;
            /// <summary>
            /// 
            /// </summary>
            public const int GetItem = 1002;
            /// <summary>
            /// 
            /// </summary>
            public const int InsertItem = 1003;
            /// <summary>
            /// 
            /// </summary>
            public const int UpdateItem = 1004;
            /// <summary>
            /// 
            /// </summary>
            public const int DeleteItem = 1005;
            /// <summary>
            /// 
            /// </summary>
            public const int TestItem = 3000;
            /// <summary>
            /// 
            /// </summary>
            public const int GetItemNotFound = 4000;
            /// <summary>
            /// 
            /// </summary>
            public const int UpdateItemNotFound = 4001;
        }
    }

}
