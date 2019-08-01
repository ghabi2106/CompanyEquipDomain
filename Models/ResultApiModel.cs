using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ResultApiModel<T>
    {
        public T Data { get; set; }
        public int TotalCount { get; set; }
        public bool Success { get; set; }
    }
}
