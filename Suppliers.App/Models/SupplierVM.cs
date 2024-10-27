﻿using System.ComponentModel.DataAnnotations;

namespace Suppliers.App.Models
{
    public class SupplierVM
    {
        public int SupplierID { get; set; }
        [Required]
        [MaxLength(30)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string Representative { get; set; }
        [Required]
        [MaxLength(30)]
        public string ContactNo { get; set; }
        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

    }
}