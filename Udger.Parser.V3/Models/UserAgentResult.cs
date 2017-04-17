// <copyright file="UserAgentResult.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3.Models
{
    using System;

    /// <summary>
    /// Class implementing user agent result.
    /// </summary>
    public class UserAgentResult : IEquatable<UserAgentResult>
    {
        public string UserAgentString { get; }

        public int ClientId { get; internal set; }

        public int ClassId { get; internal set; }

        public string UserAgentClass { get; set; } = string.Empty;

        public string UserAgentClassCode { get; set; } = string.Empty;

        public string UserAgent { get; set; } = string.Empty;

        public string UserAgentEngine { get; set; } = string.Empty;

        public string UserAgentVersion { get; set; } = string.Empty;

        public string UserAgentVersionMajor { get; set; } = string.Empty;

        public string CrawlerLastSeen { get; set; } = string.Empty;

        public string CrawlerRespectRobotstxt { get; set; } = string.Empty;

        public string CrawlerCategory { get; set; } = string.Empty;

        public string CrawlerCategoryCode { get; set; } = string.Empty;

        public string UserAgentUptodateCurrentVersion { get; set; } = string.Empty;

        public string UserAgentFamily { get; set; } = string.Empty;

        public string UserAgentFamilyCode { get; set; } = string.Empty;

        public string UserAgentFamilyHomepage { get; set; } = string.Empty;

        public string UserAgentFamilyIcon { get; set; } = string.Empty;

        public string UserAgentFamilyIconBig { get; set; } = string.Empty;

        public string UserAgentFamilyVendor { get; set; } = string.Empty;

        public string UserAgentFamilyVendorCode { get; set; } = string.Empty;

        public string UserAgentFamilyVendorHomepage { get; set; } = string.Empty;

        public string UserAgentFamilyInfoUrl { get; set; } = string.Empty;

        public string OperatingSystemFamily { get; set; } = string.Empty;

        public string OperatingSystemFamilyCode { get; set; } = string.Empty;

        public string OperatingSystem { get; set; } = string.Empty;

        public string OperatingSystemCode { get; set; } = string.Empty;

        public string OperatingSystemHomePage { get; set; } = string.Empty;

        public string OperatingSystemIcon { get; set; } = string.Empty;

        public string OperatingSystemIconBig { get; set; } = string.Empty;

        public string OperatingSystemFamilyVendor { get; set; } = string.Empty;

        public string OperatingSystemFamilyVendorCode { get; set; } = string.Empty;

        public string OperatingSystemFamilyVedorHomepage { get; set; } = string.Empty;

        public string OperatingSystemInfoUrl { get; set; } = string.Empty;

        public string DeviceClass { get; set; } = string.Empty;

        public string DeviceClassCode { get; set; } = string.Empty;

        public string DeviceClassIcon { get; set; } = string.Empty;

        public string DeviceClassIconBig { get; set; } = string.Empty;

        public string DeviceClassInfoUrl { get; set; } = string.Empty;

        public string DeviceMarketname { get; set; } = string.Empty;

        public string DeviceBrand { get; set; } = string.Empty;

        public string DeviceBrandCode { get; set; } = string.Empty;

        public string DeviceBrandHomepage { get; set; } = string.Empty;

        public string DeviceBrandIcon { get; set; } = string.Empty;

        public string DeviceBrandIconBig { get; set; } = string.Empty;

        public string DeviceBrandInfoUrl { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAgentResult"/> class.
        /// </summary>
        /// <param name="userAgentString">String representing user agent (browser or robot).</param>
        public UserAgentResult(string userAgentString)
        {
            this.UserAgentString = userAgentString ?? throw new ArgumentNullException(nameof(userAgentString));
        }

        /// <summary>
        /// Comapres two isntances of <see cref="UserAgentResult"/>.
        /// </summary>
        /// <param name="other">Instance to comapare with current one.</param>
        /// <returns>Returns trie if instances are equal, otherwise returns false.</returns>
        public bool Equals(UserAgentResult other)
        {
            return other != null &&
                this.ClassId == other.ClassId &&
                this.ClientId == other.ClientId &&
                this.CrawlerCategory == other.CrawlerCategory &&
                this.CrawlerCategoryCode == other.CrawlerCategoryCode &&
                this.CrawlerLastSeen == other.CrawlerLastSeen &&
                this.CrawlerRespectRobotstxt == other.CrawlerRespectRobotstxt &&
                this.DeviceBrand == other.DeviceBrand &&
                this.DeviceBrandCode == other.DeviceBrandCode &&
                this.DeviceBrandHomepage == other.DeviceBrandHomepage &&
                this.DeviceBrandIcon == other.DeviceBrandIcon &&
                this.DeviceBrandIconBig == other.DeviceBrandIconBig &&
                this.DeviceBrandInfoUrl == other.DeviceBrandInfoUrl &&
                this.DeviceClass == other.DeviceClass &&
                this.DeviceClassCode == other.DeviceClassCode &&
                this.DeviceClassIcon == other.DeviceClassIcon &&
                this.DeviceClassIconBig == other.DeviceClassIconBig &&
                this.DeviceClassInfoUrl == other.DeviceClassInfoUrl &&
                this.DeviceMarketname == other.DeviceMarketname &&
                this.OperatingSystem == other.OperatingSystem &&
                this.OperatingSystemCode == other.OperatingSystemCode &&
                this.OperatingSystemFamily == other.OperatingSystemFamily &&
                this.OperatingSystemFamilyCode == other.OperatingSystemFamilyCode &&
                this.OperatingSystemFamilyVedorHomepage == other.OperatingSystemFamilyVedorHomepage &&
                this.OperatingSystemFamilyVendor == other.OperatingSystemFamilyVendor &&
                this.OperatingSystemFamilyVendorCode == other.OperatingSystemFamilyVendorCode &&
                this.OperatingSystemHomePage == other.OperatingSystemHomePage &&
                this.OperatingSystemIcon == other.OperatingSystemIcon &&
                this.OperatingSystemIconBig == other.OperatingSystemIconBig &&
                this.OperatingSystemInfoUrl == other.OperatingSystemInfoUrl &&
                this.UserAgent == other.UserAgent &&
                this.UserAgentClass == other.UserAgentClass &&
                this.UserAgentClassCode == other.UserAgentClassCode &&
                this.UserAgentEngine == other.UserAgentEngine &&
                this.UserAgentFamily == other.UserAgentFamily &&
                this.UserAgentFamilyCode == other.UserAgentFamilyCode &&
                this.UserAgentFamilyHomepage == other.UserAgentFamilyHomepage &&
                this.UserAgentFamilyIcon == other.UserAgentFamilyIcon &&
                this.UserAgentFamilyIconBig == other.UserAgentFamilyIconBig &&
                this.UserAgentFamilyInfoUrl == other.UserAgentFamilyInfoUrl &&
                this.UserAgentFamilyVendor == other.UserAgentFamilyVendor &&
                this.UserAgentFamilyVendorCode == other.UserAgentFamilyVendorCode &&
                this.UserAgentFamilyVendorHomepage == other.UserAgentFamilyVendorHomepage &&
                this.UserAgentString == other.UserAgentString &&
                this.UserAgentUptodateCurrentVersion == other.UserAgentUptodateCurrentVersion &&
                this.UserAgentVersion == other.UserAgentVersion &&
                this.UserAgentVersionMajor == other.UserAgentVersionMajor;
        }

        /// <summary>
        /// Comapres two isntances of <see cref="UserAgentResult"/>.
        /// </summary>
        /// <param name="other">Instance to comapare with current one.</param>
        /// <returns>Returns trie if instances are equal, otherwise returns false.</returns>
        public override bool Equals(object other)
        {
            return this.Equals(other as UserAgentResult);
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = (result * prime) + this.ClassId.GetHashCode();
            result = (result * prime) + this.ClientId.GetHashCode();
            result = (result * prime) + this.CrawlerCategory.GetHashCode();
            result = (result * prime) + this.CrawlerCategoryCode.GetHashCode();
            result = (result * prime) + this.CrawlerLastSeen.GetHashCode();
            result = (result * prime) + this.CrawlerRespectRobotstxt.GetHashCode();
            result = (result * prime) + this.DeviceBrand.GetHashCode();
            result = (result * prime) + this.DeviceBrandCode.GetHashCode();
            result = (result * prime) + this.DeviceBrandHomepage.GetHashCode();
            result = (result * prime) + this.DeviceBrandIcon.GetHashCode();
            result = (result * prime) + this.DeviceBrandIconBig.GetHashCode();
            result = (result * prime) + this.DeviceBrandInfoUrl.GetHashCode();
            result = (result * prime) + this.DeviceClass.GetHashCode();
            result = (result * prime) + this.DeviceClassCode.GetHashCode();
            result = (result * prime) + this.DeviceClassIcon.GetHashCode();
            result = (result * prime) + this.DeviceClassIconBig.GetHashCode();
            result = (result * prime) + this.DeviceClassInfoUrl.GetHashCode();
            result = (result * prime) + this.DeviceMarketname.GetHashCode();
            result = (result * prime) + this.OperatingSystem.GetHashCode();
            result = (result * prime) + this.OperatingSystemCode.GetHashCode();
            result = (result * prime) + this.OperatingSystemFamily.GetHashCode();
            result = (result * prime) + this.OperatingSystemFamilyCode.GetHashCode();
            result = (result * prime) + this.OperatingSystemFamilyVedorHomepage.GetHashCode();
            result = (result * prime) + this.OperatingSystemFamilyVendor.GetHashCode();
            result = (result * prime) + this.OperatingSystemFamilyVendorCode.GetHashCode();
            result = (result * prime) + this.OperatingSystemHomePage.GetHashCode();
            result = (result * prime) + this.OperatingSystemIcon.GetHashCode();
            result = (result * prime) + this.OperatingSystemIconBig.GetHashCode();
            result = (result * prime) + this.OperatingSystemInfoUrl.GetHashCode();
            result = (result * prime) + this.UserAgent.GetHashCode();
            result = (result * prime) + this.UserAgentClass.GetHashCode();
            result = (result * prime) + this.UserAgentClassCode.GetHashCode();
            result = (result * prime) + this.UserAgentEngine.GetHashCode();
            result = (result * prime) + this.UserAgentFamily.GetHashCode();
            result = (result * prime) + this.UserAgentFamilyCode.GetHashCode();
            result = (result * prime) + this.UserAgentFamilyHomepage.GetHashCode();
            result = (result * prime) + this.UserAgentFamilyIcon.GetHashCode();
            result = (result * prime) + this.UserAgentFamilyIconBig.GetHashCode();
            result = (result * prime) + this.UserAgentFamilyInfoUrl.GetHashCode();
            result = (result * prime) + this.UserAgentFamilyVendor.GetHashCode();
            result = (result * prime) + this.UserAgentFamilyVendorCode.GetHashCode();
            result = (result * prime) + this.UserAgentFamilyVendorHomepage.GetHashCode();
            result = (result * prime) + this.UserAgentString.GetHashCode();
            result = (result * prime) + this.UserAgentUptodateCurrentVersion.GetHashCode();
            result = (result * prime) + this.UserAgentVersion.GetHashCode();
            result = (result * prime) + this.UserAgentVersionMajor.GetHashCode();

            return result;
        }

        public override string ToString()
        {
            return $@"
                UdgerUaResult[
                uaString={this.UserAgentString}
                , clientId={this.ClientId}
                , classId={this.ClassId}
                , uaClass={this.UserAgentClass}
                , uaClassCode={this.UserAgentClassCode}
                , ua={this.UserAgent}
                , uaEngine={this.UserAgentEngine}
                , uaVersion={this.UserAgentVersion}
                , uaVersionMajor={this.UserAgentVersionMajor}
                , crawlerLastSeen= {this.CrawlerLastSeen}
                , crawlerRespectRobotstxt= + {this.CrawlerRespectRobotstxt}
                , crawlerCategory={this.CrawlerCategory}
                , crawlerCategoryCode={this.CrawlerCategoryCode}
                , uaUptodateCurrentVersion={this.UserAgentUptodateCurrentVersion}
                , uaFamily={this.UserAgentFamily}
                , uaFamilyCode={this.UserAgentFamilyCode}
                , uaFamilyHomepage={this.UserAgentFamilyHomepage}
                , uaFamilyIcon={this.UserAgentFamilyIcon}
                , uaFamilyIconBig={this.UserAgentFamilyIconBig}
                , uaFamilyVendor={this.UserAgentFamilyVendor}
                , uaFamilyVendorCode={this.UserAgentFamilyVendorCode}
                , uaFamilyVendorHomepage={this.UserAgentFamilyVendorHomepage}
                , uaFamilyInfoUrl={this.UserAgentFamilyInfoUrl}
                , osFamily={this.OperatingSystemFamily}
                , osFamilyCode={this.OperatingSystemFamilyCode}
                , os={this.OperatingSystem}
                , osCode={this.OperatingSystemCode}
                , osHomePage={this.OperatingSystemHomePage}
                , osIcon={this.OperatingSystemIcon}
                , osIconBig={this.OperatingSystemIconBig}
                , osFamilyVendor={this.OperatingSystemFamilyVendor}
                , osFamilyVendorCode={this.OperatingSystemFamilyVendorCode}
                , osFamilyVedorHomepage={this.OperatingSystemFamilyVedorHomepage}
                , osInfoUrl={this.OperatingSystemInfoUrl}
                , deviceClass={this.DeviceClass}
                , deviceClassCode={this.DeviceClassCode}
                , deviceClassIcon={this.DeviceClassIcon}
                , deviceClassIconBig={this.DeviceClassIconBig}
                , deviceClassInfoUrl={this.DeviceClassInfoUrl}
                , deviceMarketname={this.DeviceMarketname}
                , deviceBrand={this.DeviceBrand}
                , deviceBrandCode={this.DeviceBrandCode}
                , deviceBrandHomepage={this.DeviceBrandHomepage}
                , deviceBrandIcon={this.DeviceBrandIcon}
                , deviceBrandIconBig={this.DeviceBrandIconBig}
                , deviceBrandInfoUrl={this.DeviceBrandInfoUrl}
                ];
            ";
        }
    }
}
