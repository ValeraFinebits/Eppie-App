// ---------------------------------------------------------------------------- //
//                                                                              //
//   Copyright 2026 Eppie (https://eppie.io)                                    //
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

using Foundation;
using UIKit;

namespace Eppie.App.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : Microsoft.UI.Xaml.NativeApplicationDelegate
    {
        public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
        {
            if (url != null && !string.IsNullOrEmpty(url.AbsoluteString))
            {
                // Get the app instance and set pending URL
                if (Microsoft.UI.Xaml.Application.Current is App app)
                {
                    app.SetPendingMailtoUri(url.AbsoluteString);
                }
                return true;
            }
            return false;
        }
    }
}
