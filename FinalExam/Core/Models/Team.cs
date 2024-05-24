using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;

public class Team:BaseEntity
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Designation { get; set; }
    [Required]
    public string Description { get; set; }
    public string? TwittlerLink { get; set; }
    public string? FacebookLink { get; set; }
    public string? InstagramLink { get; set; }
    public string? LinkedinLink { get; set; }
    public string? ImageUrl { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
