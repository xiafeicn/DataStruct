namespace Ruanmou.EFCore.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  

    [Table("UserMenuMapping")]
    public partial class UserMenuMapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MenuId { get; set; }
    }
}
