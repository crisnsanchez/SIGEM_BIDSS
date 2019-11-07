using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEM_BIDSS.Models
{
    public class FunctionGenerals
    {
        public DateTime DatetimeNow()
        {
            DateTime dt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-6)).DateTime;
            return dt;
        }
    }
}