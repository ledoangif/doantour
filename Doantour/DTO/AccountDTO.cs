﻿using System.ComponentModel.DataAnnotations;

namespace Doantour.DTO
{
    public class AccountDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
