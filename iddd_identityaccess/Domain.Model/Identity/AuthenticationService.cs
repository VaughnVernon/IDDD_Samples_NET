// Copyright 2012,2013 Vaughn Vernon
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

namespace SaaSOvation.IdentityAccess.Domain.Model.Identity
{
    using System;
    using SaaSOvation.Common.Domain.Model;

    public class AuthenticationService
    {
        public AuthenticationService(
                ITenantRepository tenantRepository,
                IUserRepository userRepository,
                IEncryptionService encryptionService)
        {
            this.encryptionService = encryptionService;
            this.tenantRepository = tenantRepository;
            this.userRepository = userRepository;
        }

        readonly IEncryptionService encryptionService;
        readonly ITenantRepository tenantRepository;
        readonly IUserRepository userRepository;

        public UserDescriptor Authenticate(TenantId tenantId, string username, string password)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "TenantId must not be null.");
            AssertionConcern.AssertArgumentNotEmpty(username, "Username must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(password, "Password must be provided.");

            var userDescriptor = UserDescriptor.NullDescriptorInstance();

            var tenant = this.tenantRepository.Get(tenantId);

            if (tenant != null && tenant.Active)
            {
                var encryptedPassword = this.encryptionService.EncryptedValue(password);
                var user = this.userRepository.UserFromAuthenticCredentials(tenantId, username, encryptedPassword);
                if (user != null && user.IsEnabled)
                {
                    userDescriptor = user.UserDescriptor;
                }
            }

            return userDescriptor;
        }
    }
}
