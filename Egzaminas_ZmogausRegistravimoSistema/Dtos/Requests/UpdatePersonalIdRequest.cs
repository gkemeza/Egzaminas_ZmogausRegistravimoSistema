﻿using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePersonalIdRequest
    {
        [Required]
        public long NewPersonalId { get; set; }
    }
}
