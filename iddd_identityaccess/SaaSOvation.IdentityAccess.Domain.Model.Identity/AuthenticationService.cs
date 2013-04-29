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

    public class AuthenticationService : AssertionConcern
    {
        public AuthenticationService(
                TenantRepository tenantRepository,
                UserRepository userRepository,
                EncryptionService encryptionService)
        {
            this.EncryptionService = encryptionService;
            this.TenantRepository = tenantRepository;
            this.UserRepository = userRepository;
        }

        private EncryptionService EncryptionService { get; set; }

        private TenantRepository TenantRepository { get; set; }

        private UserRepository UserRepository { get; set; }

        public UserDescriptor Authenticate(
                TenantId tenantId,
                string username,
                string password)
        {
            AssertionConcern.AssertArgumentNotNull(tenantId, "TenantId must not be null.");
            AssertionConcern.AssertArgumentNotEmpty(username, "Username must be provided.");
            AssertionConcern.AssertArgumentNotEmpty(password, "Password must be provided.");

            UserDescriptor userDescriptor = UserDescriptor.NullDescriptorInstance();

            Tenant tenant = this.TenantRepository.TenantOfId(tenantId);

            if (tenant != null && tenant.Active)
            {
                String encryptedPassword = this.EncryptionService.EncryptedValue(password);

                User user =
                        this.UserRepository
                            .UserFromAuthenticCredentials(
                                tenantId,
                                username,
                                encryptedPassword);

                if (user != null && user.Enabled)
                {
                    userDescriptor = user.UserDescriptor;
                }
            }

            return userDescriptor;
        }
    }
}
