﻿
using Xunit;
using System;
using Newtonsoft.Json;

namespace transactR.Tests
{
    public class ConfigTests
    {
        [Fact]
        public void CanSerializeToJSON()
        {
            var config = Utility.CreateLoanGivenAccountType();

            var json = JsonConvert.SerializeObject(config, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Console.Error.Write(json);
        }

    }
}

