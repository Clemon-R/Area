﻿using Area.Enums;
using Area.Services.Triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Models
{
    public class Trigger {
        [Key]
        public int Id { get; set; }
        public ActionTypeEnum ActionType { get; set; }
        public ReactionTypeEnum ReactionType { get; set; }
        public DateTime? LastVerificationDate { get; set; }
        [NotMapped]
        public TriggerTemplate Template { get; set; }
    }
}
