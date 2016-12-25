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
    
    public partial class TOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOrder()
        {
            this.TTrades = new HashSet<TTrade>();
        }
    
        public int OrderId { get; set; }
        public long TransactionId { get; set; }
        public long ExchOrderId { get; set; }
        public System.DateTime Time { get; set; }
        public System.DateTime ChangeTime { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }
        public int Balance { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public string Side { get; set; }
        public bool Canceled { get; set; }
        public string Portfolio { get; set; }
        public string Security { get; set; }
        public Nullable<System.TimeSpan> CancelLatency { get; set; }
        public Nullable<System.TimeSpan> RegisterLatency { get; set; }
        public int StrategyId { get; set; }
    
        public virtual TStrategy TStrategy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TTrade> TTrades { get; set; }
    }
}