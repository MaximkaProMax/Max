using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Max.Models
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Lastname { get; set; } = string.Empty;

        public string Firstname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string Password { get; set; } = string.Empty;

        public int? FailedLoginAttempts { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsFirstLogin { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}