﻿using System.ComponentModel.DataAnnotations;

namespace TheBlogProject.Models
{
    public class ContactMe
    {
        [Required]
        [StringLength(80, ErrorMessage = " The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = " The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string Email { get; set; }

        [Phone]
        public int Phone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = " The {0} must be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string Message { get; set; }


    }
}
