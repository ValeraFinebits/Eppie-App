﻿// ---------------------------------------------------------------------------- //
//                                                                              //
//   Copyright 2025 Eppie (https://eppie.io)                                    //
//                                                                              //
//   Licensed under the Apache License, Version 2.0 (the "License"),            //
//   you may not use this file except in compliance with the License.           //
//   You may obtain a copy of the License at                                    //
//                                                                              //
//       http://www.apache.org/licenses/LICENSE-2.0                             //
//                                                                              //
//   Unless required by applicable law or agreed to in writing, software        //
//   distributed under the License is distributed on an "AS IS" BASIS,          //
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.   //
//   See the License for the specific language governing permissions and        //
//   limitations under the License.                                             //
//                                                                              //
// ---------------------------------------------------------------------------- //

using System.IdentityModel.Tokens.Jwt;
using Finebits.Authorization.OAuth2.Types;
using Microsoft.IdentityModel.Tokens;

namespace Tuvi.App.ViewModels.Extensions
{
    public static class CredentialExtension
    {
        const string EmailPayloadProperty = "email";
        const string NamePayloadProperty = "name";

        public static object ReadPayloadProperty(this AuthCredential credential, string property)
        {
            if (credential is null)
            {
                throw new System.ArgumentNullException(nameof(credential));
            }

            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = handler.ReadJwtToken(credential.IdToken);

                if (jwtToken.Payload.TryGetValue(property, out object value))
                {
                    return value;
                }
            }
            catch (SecurityTokenArgumentException)
            { }

            return null;
        }

        public static string ReadEmailAddress(this AuthCredential credential)
        {
            return credential.ReadPayloadProperty(EmailPayloadProperty) as string;
        }

        public static string ReadName(this AuthCredential credential)
        {
            return credential.ReadPayloadProperty(NamePayloadProperty) as string;
        }
    }
}
