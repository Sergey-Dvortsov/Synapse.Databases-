//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synapse.Databases
{
    using System;
    using System.Collections.Generic;
    
    public partial class TTrade
    {
        public int TradeId { get; set; }
        public long ExchTradeId { get; set; }
        public System.DateTime Time { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }
        public string Side { get; set; }
        public int OrderId { get; set; }
    
        public virtual TOrder TOrder { get; set; }
    }
}
