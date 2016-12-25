using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.Databases
{
    public interface IStrategyInfo
    {
        string Name { get; set; }
        Guid GUID { get; set; }
        IStrategyInfo Parent { get; set; }

    }

    public interface IOrderInfo
    {
        long TransactionId { get; set; }
        long ExchOrderId { get; set; }
        DateTime Time { get; set; }
        DateTime ChangeTime { get; set; }
        decimal Price { get; set; }
        int Volume { get; set; }
        int Balance { get; set; }
        string State { get; set; }
        string Type { get; set; }
        string Side { get; set; }
        bool Canceled { get; set; }
        string Portfolio { get; set; }
        string Security { get; set; }
        TimeSpan? CancelLatency { get; set; }
        TimeSpan? RegisterLatency { get; set; }
    }

    public interface ITradeInfo
    {
         long ExchTradeId { get; set; }
         DateTime Time { get; set; }
         decimal Price { get; set; }
         int Volume { get; set; }
         string Side { get; set; }
    }
}


//public int TradeId { get; set; }
//public Nullable<long> ExchTradeId { get; set; }
//public Nullable<System.DateTime> Time { get; set; }
//public Nullable<decimal> Price { get; set; }
//public Nullable<int> Volume { get; set; }
//public string Side { get; set; }
//public Nullable<int> OrderId { get; set; }

//public int OrderId { get; set; }
//public Nullable<long> TransactionId { get; set; }
//public Nullable<long> ExchOrderId { get; set; }
//public Nullable<System.DateTime> Time { get; set; }
//public Nullable<System.DateTime> ChangeTime { get; set; }
//public Nullable<decimal> Price { get; set; }
//public Nullable<int> Volume { get; set; }
//public Nullable<int> Balance { get; set; }
//public string State { get; set; }
//public string Type { get; set; }
//public string Side { get; set; }
//public Nullable<bool> Canceled { get; set; }
//public string Portfolio { get; set; }
//public string Security { get; set; }
//public Nullable<System.TimeSpan> CancelLatency { get; set; }
//public Nullable<System.TimeSpan> RegisterLatency { get; set; }
//public Nullable<int> StrategyId { get; set; }