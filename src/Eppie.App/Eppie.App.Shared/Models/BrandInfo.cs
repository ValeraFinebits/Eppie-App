// ---------------------------------------------------------------------------- //
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

using System.Collections.Generic;

namespace Tuvi.App.Shared.Models
{
    public enum BrandKey
    {
        AppName,
        Support,
        Homepage,
        License,
        DevelopmentSupport,
        TwitterHandle,
        GitHubUrl,
        Translation
    }

    public class BrandInfo
    {
        public Dictionary<BrandKey, string> Values { get; set; }
    }

    public class Loader_Eppie : BrandInfo
    {
        public static readonly string[] NameIds = new string[] { "<NAMEID>", "<NAMEID>" };
        
        public Loader_Eppie()
        {
            Values = new Dictionary<BrandKey, string>()
            {
                { BrandKey.AppName, "Eppie (preview)" },
                { BrandKey.Support, "beta@eppie.io" },
                { BrandKey.Homepage, "https://eppie.io" },
                { BrandKey.License, "https://eppie.io" },
                { BrandKey.DevelopmentSupport, "https://github.com/sponsors/Eppie-io" },
                { BrandKey.TwitterHandle, "@EppieApp" },
                { BrandKey.GitHubUrl, "https://github.com/Eppie-io/Eppie-App" },
                { BrandKey.Translation, "https://eppie.crowdin.com/eppie" }
            };
        }
    }

    public class Loader_Eppie_Dev : BrandInfo
    {
        public static readonly string[] NameIds = new string[] { "<NAMEID>", "<NAMEID>" };
        
        public Loader_Eppie_Dev()
        {
            Values = new Dictionary<BrandKey, string>()
            {
                { BrandKey.AppName, "Eppie (development)" },
                { BrandKey.Support, "beta@eppie.io" },
                { BrandKey.Homepage, "https://eppie.io" },
                { BrandKey.License, "https://eppie.io" },
                { BrandKey.DevelopmentSupport, "https://github.com/sponsors/Eppie-io" },
                { BrandKey.TwitterHandle, "@EppieApp" },
                { BrandKey.GitHubUrl, "https://github.com/Eppie-io/Eppie-App" },
                { BrandKey.Translation, "https://eppie.crowdin.com/eppie" }
            };
        }
    }
}