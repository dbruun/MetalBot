using System;
using System.ComponentModel.DataAnnotations;

namespace MetalBotDAL
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }

    }
}
