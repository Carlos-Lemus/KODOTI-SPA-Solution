using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.DTOs
{
    public class ClientCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }

    public class ClientDTO
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
    }

}
