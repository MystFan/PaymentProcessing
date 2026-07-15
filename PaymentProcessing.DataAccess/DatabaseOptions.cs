using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessing.DataAccess
{
    public class DatabaseOptions
    {
        public const string SectionName = "database";

        public string ConnectionString { get; init; } = null!;
    }
}
