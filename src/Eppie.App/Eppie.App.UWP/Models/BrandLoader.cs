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

using System.Linq;
using System.Reflection;
using Windows.ApplicationModel;

namespace Tuvi.App.Shared.Models
{
    internal class BrandLoader : BrandLoaderBase
    {
        internal BrandLoader() : base(CreateBrandInfo())
        {
        }

        private static BrandInfo CreateBrandInfo()
        {
            if (Loader_Eppie.NameIds.Contains(Package.Current.Id.Name))
            {
                return new Loader_Eppie();
            }
            else
            {
                return new Loader_Eppie_Dev();
            }
        }

        public override string GetPublisherDisplayName()
        {
            return Package.Current.PublisherDisplayName;
        }

        public override string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        }

        public override string GetPackageVersion()
        {
            return $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        }

        public override string GetFileVersion()
        {
            return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
        }

        public override string GetInformationalVersion()
        {
            return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
        }

        public override string GetAppVersion()
        {
            return GetInformationalVersion() ?? GetVersion() ?? GetPackageVersion();
        }
    }
}
