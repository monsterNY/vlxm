using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Monster.AuthServer.CusConfig
{
    public class AuthConfig
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            //Requested scope not allowed: custom.profile
            var customProfile = new IdentityResource(
                name: "custom.profile",//标识名 - 唯一
                displayName: "Custom profile",//显示名
                claimTypes: new[] { "role" });//to be continue

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                customProfile
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {

            return new[]
            {
                new ApiResource(
                    name: "api1", //同上...
                    displayName: "User API"//同上...
                    ,claimTypes: new List<string>() {JwtClaimTypes.Role})
            };

        }


        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client()
                {
                    ClientId = "user.client",//标识名 - 唯一
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,//grantType 指定允许的授予类型(授权码、隐式、混合、资源所有者、客户凭证的合法组合)。

                    ClientSecrets =//客户端机密——仅与需要机密的流相关
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =//指定允许客户端请求的api范围。如果为空，客户端不能访问任何范围
                    {
                        "api1"
                    }
                }
            };
        }
    }
}
