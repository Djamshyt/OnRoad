//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnRoad
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sales
    {
        public int Id { get; set; }
        public Nullable<int> IdCar { get; set; }
        public Nullable<int> IdAddService { get; set; }
        public Nullable<double> Sum { get; set; }
        public System.DateTime Date { get; set; }
        public int IdEmployee { get; set; }
        public Nullable<int> IdClient { get; set; }
        public string PayMethod { get; set; }
    
        public virtual AddService AddService { get; set; }
        public virtual Car Car { get; set; }
        public virtual Clients Clients { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
