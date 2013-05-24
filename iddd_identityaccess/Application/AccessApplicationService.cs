using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.IdentityAccess.Domain.Identity;
using SaaSOvation.IdentityAccess.Domain.Access;

namespace SaaSOvation.IdentityAccess.Application
{
    public class AccessApplicationService
    {
        public AccessApplicationService(
            IGroupRepository groupRepository,
            IRoleRepository roleRepository,
            ITenantRepository tenantRepository,
            IUserRepository userRepository)
        {
            this.groupRepository = groupRepository;
            this.roleRepository = roleRepository;
            this.tenantRepository = tenantRepository;
            this.userRepository = userRepository;
        }

        readonly IGroupRepository groupRepository;
        readonly IRoleRepository roleRepository;
        readonly ITenantRepository tenantRepository;
        readonly IUserRepository userRepository;

        public void AssignUserToRole()
        {
            var tenantId = new TenantId();
        }
    }
}
