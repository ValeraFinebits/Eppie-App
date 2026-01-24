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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tuvi.App.ViewModels.Helpers
{
    /// <summary>
    /// Parses mailto: URIs according to RFC 6068.
    /// </summary>
    public class MailtoUriParser
    {
        /// <summary>
        /// Gets the primary recipient email address.
        /// </summary>
        public string To { get; private set; }

        /// <summary>
        /// Gets the CC (carbon copy) recipients.
        /// </summary>
        public string Cc { get; private set; }

        /// <summary>
        /// Gets the BCC (blind carbon copy) recipients.
        /// </summary>
        public string Bcc { get; private set; }

        /// <summary>
        /// Gets the email subject.
        /// </summary>
        public string Subject { get; private set; }

        /// <summary>
        /// Gets the email body.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// Parses a mailto: URI and extracts email components.
        /// </summary>
        /// <param name="mailtoUri">The mailto: URI to parse.</param>
        /// <returns>A MailtoUriParser instance with parsed data.</returns>
        /// <exception cref="ArgumentNullException">Thrown when mailtoUri is null.</exception>
        /// <exception cref="ArgumentException">Thrown when URI is not a valid mailto: URI.</exception>
        public static MailtoUriParser Parse(Uri mailtoUri)
        {
            if (mailtoUri == null)
            {
                throw new ArgumentNullException(nameof(mailtoUri));
            }

            if (!string.Equals(mailtoUri.Scheme, "mailto", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("URI must use the mailto: scheme.", nameof(mailtoUri));
            }

            var parser = new MailtoUriParser();
            parser.ParseInternal(mailtoUri);
            return parser;
        }

        /// <summary>
        /// Parses a mailto: URI string and extracts email components.
        /// </summary>
        /// <param name="mailtoUriString">The mailto: URI string to parse.</param>
        /// <returns>A MailtoUriParser instance with parsed data.</returns>
        /// <exception cref="ArgumentException">Thrown when URI string is invalid.</exception>
        public static MailtoUriParser Parse(string mailtoUriString)
        {
            if (string.IsNullOrWhiteSpace(mailtoUriString))
            {
                throw new ArgumentException("URI string cannot be null or whitespace.", nameof(mailtoUriString));
            }

            if (!Uri.TryCreate(mailtoUriString, UriKind.Absolute, out Uri uri))
            {
                throw new ArgumentException("Invalid URI format.", nameof(mailtoUriString));
            }

            return Parse(uri);
        }

        private void ParseInternal(Uri mailtoUri)
        {
            // Extract the primary recipient (the part after mailto: and before ?)
            var absoluteUri = mailtoUri.AbsoluteUri;
            var mailtoPrefix = "mailto:";

            var startIndex = absoluteUri.IndexOf(mailtoPrefix, StringComparison.OrdinalIgnoreCase) + mailtoPrefix.Length;
            var questionMarkIndex = absoluteUri.IndexOf('?', startIndex);

            string primaryRecipient = string.Empty;
            if (questionMarkIndex > startIndex)
            {
                primaryRecipient = absoluteUri.Substring(startIndex, questionMarkIndex - startIndex);
            }
            else if (questionMarkIndex == -1 && startIndex < absoluteUri.Length)
            {
                // No query string, the rest is the recipient
                primaryRecipient = absoluteUri.Substring(startIndex);
            }

            // Decode the primary recipient
            primaryRecipient = Uri.UnescapeDataString(primaryRecipient);

            // Parse query parameters
            var queryParams = ParseQueryString(mailtoUri.Query);

            // Build the To field (primary recipient + any 'to' parameters)
            var toAddresses = new List<string>();
            if (!string.IsNullOrWhiteSpace(primaryRecipient))
            {
                toAddresses.Add(primaryRecipient);
            }
            if (queryParams.ContainsKey("to"))
            {
                toAddresses.Add(queryParams["to"]);
            }
            To = string.Join(", ", toAddresses.Where(a => !string.IsNullOrWhiteSpace(a)));

            // Extract other fields
            Cc = queryParams.ContainsKey("cc") ? queryParams["cc"] : string.Empty;
            Bcc = queryParams.ContainsKey("bcc") ? queryParams["bcc"] : string.Empty;
            Subject = queryParams.ContainsKey("subject") ? queryParams["subject"] : string.Empty;
            Body = queryParams.ContainsKey("body") ? queryParams["body"] : string.Empty;
        }

        private static Dictionary<string, string> ParseQueryString(string query)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(query))
            {
                return result;
            }

            // Remove leading '?' if present
            if (query.StartsWith("?", StringComparison.Ordinal))
            {
                query = query.Substring(1);
            }

            // Split by '&' and parse each parameter
            var parameters = query.Split('&');
            foreach (var parameter in parameters)
            {
                var equalIndex = parameter.IndexOf('=');
                if (equalIndex > 0)
                {
                    var key = parameter.Substring(0, equalIndex);
                    var value = parameter.Substring(equalIndex + 1);

                    // URL decode both key and value
                    key = Uri.UnescapeDataString(key);
                    value = Uri.UnescapeDataString(value);

                    // For duplicate keys, concatenate with comma (as per email standards)
                    if (result.ContainsKey(key))
                    {
                        result[key] = result[key] + ", " + value;
                    }
                    else
                    {
                        result[key] = value;
                    }
                }
            }

            return result;
        }
    }
}
