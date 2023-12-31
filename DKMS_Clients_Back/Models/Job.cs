﻿using System.ComponentModel.DataAnnotations;

namespace DKMS_Clients_Back.Models
{
    public class Job
    {
       
       
        public Guid Id { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Deposit { get; set; } = 0m;
        public decimal Balance { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        public DateTime ToBeCompleted { get; set; }
        public bool Completed { get; set; } = false;
    }
}
