//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp19
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int ID_Order { get; set; }
        public Nullable<int> ID_Dishes { get; set; }
        public Nullable<int> ID_User { get; set; }
        public Nullable<System.DateTime> Date_ { get; set; }
        public string Status { get; set; }
    
        public virtual Dishes Dishes { get; set; }
        public virtual Users Users { get; set; }
    }
}