//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_tag
    {
        public int Id { get; set; }
        public Nullable<int> Id_product { get; set; }
        public Nullable<int> Id_tag { get; set; }

        public override string ToString()
        {
            return $"id={Id}, id_product={Id_product}, id_tag={Id_tag}";
        }

        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }
    }
}