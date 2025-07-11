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

using System;
using Microsoft.Extensions.Logging;

namespace Tuvi.App.ViewModels.Services
{
    /// <summary>
    /// Service interface to store and retrieve local settings
    /// WARNING: Settings are not encrypted!
    /// </summary>
    public interface ILocalSettingsService
    {
        event EventHandler<SettingChangedEventArgs> SettingChanged;

        string Language { get; set; }
        string SelectedMailFilterForAllMessagesPage { get; set; }
        string SelectedMailFilterForFolderMessagesPage { get; set; }
        string SelectedMailFilterForContactMessagesPage { get; set; }
        int RequestReviewCount { get; set; }
        LogLevel LogLevel { get; set; }
        string LogFolderPath { get; }
    }

    public class SettingChangedEventArgs : EventArgs
    {
        public string Name { get; }

        public SettingChangedEventArgs(string name)
        {
            Name = name;
        }
    }
}
