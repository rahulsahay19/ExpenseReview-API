using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReimbursementApp.Model
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
