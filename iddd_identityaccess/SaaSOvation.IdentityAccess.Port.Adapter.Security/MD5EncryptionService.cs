﻿// Copyright 2012,2013 Vaughn Vernon
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace SaaSOvation.IdentityAccess.Port.Adapter.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using SaaSOvation.IdentityAccess.Domain.Model.Identity;
    using SaaSOvation.Common.Domain.Model;

    public class MD5EncryptionService : IEncryptionService
    {
        public string EncryptedValue(string plainTextValue)
        {
            AssertionConcern.AssertArgumentNotEmpty(plainTextValue, "Plain text value to encrypt must be provided.");

            var encryptedValue = new StringBuilder();

            var hasher = MD5.Create();

            var data = hasher.ComputeHash(Encoding.Default.GetBytes(plainTextValue));

            for (int dataIndex = 0; dataIndex < data.Length; dataIndex++)
            {
                encryptedValue.Append(data[dataIndex].ToString("x2"));
            }

            return encryptedValue.ToString();
        }
    }
}
