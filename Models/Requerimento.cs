using System;
using System.ComponentModel.DataAnnotations;

namespace VibeCodeAPI.Models
{
    public class Requerimento
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        public string Rg { get; set; } = string.Empty;
        
        [Required]
        public string Endereco { get; set; } = string.Empty;
        
        [Required]
        public string Telefone { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Cargo { get; set; } = string.Empty;
        
        [Required]
        public string Escola { get; set; } = string.Empty;
        
        [Required]
        public string Tipo { get; set; } = string.Empty;
    }
}
