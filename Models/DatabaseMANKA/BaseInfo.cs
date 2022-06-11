using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class BaseInfo
    {
        [Required(ErrorMessage = "Введите базовый текст о фотостудии")]
        public string BasicText { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        public string OwnerLogin { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string OwnerPassword { get; set; }
    }
}
