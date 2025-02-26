using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TodayWebAPi.DAL.Data.Models
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Processing")]
        Processing,
        [EnumMember(Value = "Shipped")]
        Shipped,
        [EnumMember(Value = "Delivered")]
        Delivered,
        [EnumMember(Value = "Cancelled")]
        Cancelled
    }
}
