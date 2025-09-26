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

using System.Reflection;
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

        public string GetName()
        {
            return _loader.GetString(BrandKey.AppName);
        }

        public string GetSupport()
        {
            return _loader.GetString(BrandKey.Support);
        }

        public string GetLicense()
        {
            return _loader.GetString(BrandKey.License);
        }

        public string GetHomepage()
        {
            return _loader.GetString(BrandKey.Homepage);
        }

        public string GetDevelopmentSupport()
        {
            return _loader.GetString(BrandKey.DevelopmentSupport);
        }

        public string GetTwitterHandle()
        {
            return _loader.GetString(BrandKey.TwitterHandle);
        }

        public string GetGitHub()
        {
            return _loader.GetString(BrandKey.GitHub);
        }

        public string GetTranslation()
        {
            return _loader.GetString(BrandKey.Translation);
        }

        // Platform-specific implementations must provide these methods
        public abstract string GetPublisherDisplayName();
        public abstract string GetPackageVersion();

        // Common implementation for version methods
        public virtual string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        }

        public virtual string GetFileVersion()
        {
            return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
        }

        public virtual string GetInformationalVersion()
        {
            return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        }

        public virtual string GetAppVersion()
        {
            return GetInformationalVersion() ?? GetVersion() ?? GetPackageVersion();
        }
    }
}
