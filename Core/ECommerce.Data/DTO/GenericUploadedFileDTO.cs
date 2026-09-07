using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.DTO
{
    public class GenericUploadedFileDTO
    {
        public Guid MediaId { get; set; }
        public string FilePath { get; set; }
    }
}
