using System;
using System.Text.RegularExpressions;

namespace CodeCube.AspNetCore.Extensions
{
    public sealed class VersionHeaderOptions
    {
        private string _headerName;
        
        /// <summary>
        /// The name of the header to add to the response.
        /// </summary>
        public string HeaderName
        {
            get => string.IsNullOrWhiteSpace(_headerName) ? "x-application-version" : _headerName;
            set => _headerName = value;
        }
        
        /// <summary>
        /// Which assembly should be used to extract the version information from?
        /// </summary>
        public string Assembly { get; set; }

        private string _version;
        /// <summary>
        /// Provide a versionnumber which should be used as header.
        /// <example>1.0.2021.1</example>
        /// </summary>
        public string Version {
            get => _version;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !Regex.IsMatch(value, @"^\d*\.\d*(\.\d*)?(\.\d*)?$"))
                    throw new InvalidOperationException("Invalid version provided!");

                _version = value;
            }
        }
    }
}