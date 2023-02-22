using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptApp.Shared;

public class ChatGptRequest
{
    [Required]
    public string Question { get; set; } = string.Empty;
}
