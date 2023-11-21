﻿using System.ComponentModel.DataAnnotations;

namespace ToDosAPI.Models.Dtos
{
    public class AddTaskDto
    {
        [Required] public string TaskContent { get; set; } = default!;
        [Required] public int CreatedBy { get; set; }
        [Required] public int Status { get; set; }

    }
}