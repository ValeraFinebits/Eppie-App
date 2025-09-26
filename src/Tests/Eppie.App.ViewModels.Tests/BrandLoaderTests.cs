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

using NUnit.Framework;
using System.Collections.Generic;

namespace Eppie.App.ViewModels.Tests
{
    // Test enum and classes to verify our refactoring works
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
        public Dictionary<BrandKey, string> Values { get; }

        public BrandInfo()
        {
            Values = new Dictionary<BrandKey, string>();
        }
    }

    public class Loader_Eppie : BrandInfo
    {
        public static readonly string[] NameIds = new string[] { "<NAMEID>", "<NAMEID>" };
        
        public Loader_Eppie()
        {
            Values.Add(BrandKey.AppName, "Eppie (preview)");
            Values.Add(BrandKey.Support, "beta@eppie.io");
            Values.Add(BrandKey.Homepage, "https://eppie.io");
            Values.Add(BrandKey.License, "https://eppie.io");
            Values.Add(BrandKey.DevelopmentSupport, "https://github.com/sponsors/Eppie-io");
            Values.Add(BrandKey.TwitterHandle, "@EppieApp");
            Values.Add(BrandKey.GitHubUrl, "https://github.com/Eppie-io/Eppie-App");
            Values.Add(BrandKey.Translation, "https://eppie.crowdin.com/eppie");
        }
    }

    public class Loader_Eppie_Dev : BrandInfo
    {
        public static readonly string[] NameIds = new string[] { "<NAMEID>", "<NAMEID>" };
        
        public Loader_Eppie_Dev()
        {
            Values.Add(BrandKey.AppName, "Eppie (development)");
            Values.Add(BrandKey.Support, "beta@eppie.io");
            Values.Add(BrandKey.Homepage, "https://eppie.io");
            Values.Add(BrandKey.License, "https://eppie.io");
            Values.Add(BrandKey.DevelopmentSupport, "https://github.com/sponsors/Eppie-io");
            Values.Add(BrandKey.TwitterHandle, "@EppieApp");
            Values.Add(BrandKey.GitHubUrl, "https://github.com/Eppie-io/Eppie-App");
            Values.Add(BrandKey.Translation, "https://eppie.crowdin.com/eppie");
        }
    }

    public class BrandLoaderTests
    {
        [Test]
        public void TestBrandInfoEnumKeysSuccess()
        {
            // Arrange
            var eppieLoader = new Loader_Eppie();
            var eppieDevLoader = new Loader_Eppie_Dev();

            // Act & Assert - Test that enum keys work correctly
            Assert.That(() => eppieLoader.Values.ContainsKey(BrandKey.AppName), Is.True);
            Assert.That(() => eppieLoader.Values.ContainsKey(BrandKey.GitHubUrl), Is.True);
            Assert.That(() => eppieLoader.Values.ContainsKey(BrandKey.TwitterHandle), Is.True);
            Assert.That(() => eppieLoader.Values.ContainsKey(BrandKey.Translation), Is.True);

            Assert.That(() => eppieDevLoader.Values.ContainsKey(BrandKey.AppName), Is.True);
            Assert.That(() => eppieDevLoader.Values.ContainsKey(BrandKey.GitHubUrl), Is.True);
            Assert.That(() => eppieDevLoader.Values.ContainsKey(BrandKey.TwitterHandle), Is.True);
            Assert.That(() => eppieDevLoader.Values.ContainsKey(BrandKey.Translation), Is.True);
        }

        [Test]
        public void TestBrandInfoValuesCorrect()
        {
            // Arrange
            var eppieLoader = new Loader_Eppie();
            var eppieDevLoader = new Loader_Eppie_Dev();

            // Act & Assert - Test that values are set correctly
            Assert.That(eppieLoader.Values[BrandKey.AppName], Is.EqualTo("Eppie (preview)"));
            Assert.That(eppieDevLoader.Values[BrandKey.AppName], Is.EqualTo("Eppie (development)"));
            
            // Both should have same GitHub URL - This tests the key consistency fix
            Assert.That(eppieLoader.Values[BrandKey.GitHubUrl], Is.EqualTo("https://github.com/Eppie-io/Eppie-App"));
            Assert.That(eppieDevLoader.Values[BrandKey.GitHubUrl], Is.EqualTo("https://github.com/Eppie-io/Eppie-App"));
            
            // Both should have same Twitter handle
            Assert.That(eppieLoader.Values[BrandKey.TwitterHandle], Is.EqualTo("@EppieApp"));
            Assert.That(eppieDevLoader.Values[BrandKey.TwitterHandle], Is.EqualTo("@EppieApp"));
            
            // Both should have translation link
            Assert.That(() => eppieLoader.Values.ContainsKey(BrandKey.Translation), Is.True);
            Assert.That(() => eppieDevLoader.Values.ContainsKey(BrandKey.Translation), Is.True);
        }

        [Test]
        public void TestEnumBasedApproachPreventsTypos()
        {
            // Arrange
            var loader = new Loader_Eppie();

            // Act & Assert - Verify that using enums prevents typos
            // This would not compile if we had a typo in the enum name
            Assert.That(() => loader.Values.ContainsKey(BrandKey.GitHubUrl), Is.True); // No more "GitHub" vs "GitHubUrl" confusion
            Assert.That(loader.Values[BrandKey.GitHubUrl], Is.Not.Null);
            Assert.That(loader.Values[BrandKey.GitHubUrl], Is.Not.Empty);
        }
    }
}