//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BalanceProject2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Saldo
    {
        public int Id_Saldo { get; set; }
        public int Input { get; set; }
        public System.DateTime Data { get; set; }
        public int Id_Personal_accounts { get; set; }
    
        public virtual Personal_accounts Personal_accounts { get; set; }
    }
}
