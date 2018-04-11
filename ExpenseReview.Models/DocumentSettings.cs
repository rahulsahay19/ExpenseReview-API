﻿using System.IO;
using System.Linq;

namespace ExpenseReview.Models
{
    public class DocumentSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string fileName)
        {
            return AcceptedFileTypes.Any(s => s == (Path.GetExtension(fileName).ToLower()));
        }
    }
}
