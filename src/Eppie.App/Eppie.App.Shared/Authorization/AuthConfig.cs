﻿using System;
using System.Collections.Generic;
using Finebits.Authorization.OAuth2.Abstractions;
using Finebits.Authorization.OAuth2.AuthenticationBroker;

namespace Tuvi.App.Shared.Authorization
{
    internal static partial class AuthConfig
    {
        public static IReadOnlyCollection<string> GmailScope = new[]
        {
            "https://mail.google.com/",
            "profile",
            "email"
        };

        public static IReadOnlyCollection<string> OutlookScope = new[]
        {
            "offline_access",
            "https://outlook.office.com/user.read",
            "https://outlook.office.com/IMAP.AccessAsUser.All",
            "https://outlook.office.com/SMTP.Send",
        };

        private static IAuthenticationBroker GetAuthenticationBroker()
        {
#if (__ANDROID__ || __IOS__)
            throw new NotImplementedException();
#elif (HAS_UNO) // macOS; Skia.Gtk; Wasm;
            if (!DesktopAuthenticationBroker.IsSupported)
            {
                throw new InvalidOperationException("DesktopAuthenticationBroker is not supported");
            }

            return new DesktopAuthenticationBroker(new WebBrowserLauncher());
#else   // UWP;
            return new WindowsAuthenticationBroker();
#endif
        }
    }
}
