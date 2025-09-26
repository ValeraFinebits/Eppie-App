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

using System.Diagnostics;
using Tuvi.App.ViewModels.Services;

namespace Tuvi.App.Shared.Models
{
    public abstract class BrandLoaderBase : IBrandService
    {
        protected readonly BrandInfo _loader;

        protected BrandLoaderBase(BrandInfo loader)
        {
            _loader = loader;
        }

        protected string GetString(BrandKey key)
        {
            if (_loader.Values.TryGetValue(key, out var res))
            {
                return res;
            }
            else
            {
                Debug.Assert(false, $"Brand key '{key}' is not found");
                return default;
            }
        }

        public string GetName()
        {
            return GetString(BrandKey.AppName);
        }

        public string GetSupport()
        {
            return GetString(BrandKey.Support);
        }

        public string GetLicense()
        {
            return GetString(BrandKey.License);
        }

        public string GetHomepage()
        {
            return GetString(BrandKey.Homepage);
        }

        public string GetDevelopmentSupport()
        {
            return GetString(BrandKey.DevelopmentSupport);
        }

        public string GetTwitterHandle()
        {
            return GetString(BrandKey.TwitterHandle);
        }

        public string GetGitHubUrl()
        {
            return GetString(BrandKey.GitHubUrl);
        }

        public string GetTranslation()
        {
            return GetString(BrandKey.Translation);
        }

        // Abstract methods that need platform-specific implementations
        public abstract string GetPublisherDisplayName();
        public abstract string GetVersion();
        public abstract string GetPackageVersion();
        public abstract string GetFileVersion();
        public abstract string GetInformationalVersion();
        public abstract string GetAppVersion();
    }
}