﻿using System.ComponentModel.DataAnnotations;

namespace ESProjeto_Back.Models
{
    public class Review
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; } = Guid.Empty;
        public User User { get; set; }
        [Required]
        public Guid StopPointId { get; set; } = Guid.Empty;
        public StopPoint StopPoint { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required, Range(0,5)]
        public int ReviewScore { get; set; }
        public string? Description { get; set; }
    }
}
