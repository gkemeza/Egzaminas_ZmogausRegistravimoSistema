﻿using Egzaminas_ZmogausRegistravimoSistema.Validators;

namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests
{
    public class UpdatePhotoRequest
    {
        [MaxPhotoSize(5 * 1024 * 1024)]
        [AllowedExtensions([".jpg", ".jpeg", ".png", ".bmp", ".gif"])]
        public IFormFile NewPhoto { get; set; } = null!;
    }
}
