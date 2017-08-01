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
        /// <summary>
        /// Gets user agent string.
        /// </summary>
        public string UserAgentString { get; }

        /// <summary>
        /// Gets client id.
        /// </summary>
        public int ClientId { get; internal set; }

        /// <summary>
        /// Gets class id.
        /// </summary>
        public int ClassId { get; internal set; }

        /// <summary>
        /// Gets or sets user agent class.
        /// </summary>
        public string UserAgentClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent class code.
        /// </summary>
        public string UserAgentClassCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent.
        /// </summary>
        public string UserAgent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent engine.
        /// </summary>
        public string UserAgentEngine { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent version.
        /// </summary>
        public string UserAgentVersion { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets major user agent version.
        /// </summary>
        public string UserAgentVersionMajor { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last time when crawler has been seen.
        /// </summary>
        public string CrawlerLastSeen { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets flag if crawler respects the robots.txt.
        /// </summary>
        public string CrawlerRespectRobotstxt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets crawler category.
        /// </summary>
        public string CrawlerCategory { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets crawler category code.
        /// </summary>
        public string CrawlerCategoryCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets flag if user agent is up to date.
        /// </summary>
        public string UserAgentUptodateCurrentVersion { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent family.
        /// </summary>
        public string UserAgentFamily { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent family code.
        /// </summary>
        public string UserAgentFamilyCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent home page URL.
        /// </summary>
        public string UserAgentFamilyHomepage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent family icon.
        /// </summary>
        public string UserAgentFamilyIcon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent family big icon.
        /// </summary>
        public string UserAgentFamilyIconBig { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent family vendor.
        /// </summary>
        public string UserAgentFamilyVendor { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent family vendor code.
        /// </summary>
        public string UserAgentFamilyVendorCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent family vendor home page.
        /// </summary>
        public string UserAgentFamilyVendorHomepage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets user agent family info URL.
        /// </summary>
        public string UserAgentFamilyInfoUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system family.
        /// </summary>
        public string OperatingSystemFamily { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system family code.
        /// </summary>
        public string OperatingSystemFamilyCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system.
        /// </summary>
        public string OperatingSystem { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system code.
        /// </summary>
        public string OperatingSystemCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system homepage.
        /// </summary>
        public string OperatingSystemHomePage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system icon.
        /// </summary>
        public string OperatingSystemIcon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system big icon.
        /// </summary>
        public string OperatingSystemIconBig { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system family vendor.
        /// </summary>
        public string OperatingSystemFamilyVendor { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system family vendor code.
        /// </summary>
        public string OperatingSystemFamilyVendorCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system family vendor home page.
        /// </summary>
        public string OperatingSystemFamilyVedorHomepage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets operating system information URL.
        /// </summary>
        public string OperatingSystemInfoUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device class.
        /// </summary>
        public string DeviceClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device class code.
        /// </summary>
        public string DeviceClassCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device class icon.
        /// </summary>
        public string DeviceClassIcon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device big icon.
        /// </summary>
        public string DeviceClassIconBig { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device class information URL.
        /// </summary>
        public string DeviceClassInfoUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device market name.
        /// </summary>
        public string DeviceMarketname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device brand.
        /// </summary>
        public string DeviceBrand { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device brand code.
        /// </summary>
        public string DeviceBrandCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device brand homepage.
        /// </summary>
        public string DeviceBrandHomepage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device brand icon.
        /// </summary>
        public string DeviceBrandIcon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device brand big icon.
        /// </summary>
        public string DeviceBrandIconBig { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets device brand information URL.
        /// </summary>
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
        /// Compares two instances of <see cref="UserAgentResult"/>.
        /// </summary>
        /// <param name="other">Instance to compare with current one.</param>
        /// <returns>Returns true if instances are equal, otherwise returns false.</returns>
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
        /// Compares two instances of <see cref="UserAgentResult"/>.
        /// </summary>
        /// <param name="other">Instance to compare with current one.</param>
        /// <returns>Returns true if instances are equal, otherwise returns false.</returns>
        public override bool Equals(object other)
        {
            return this.Equals(other as UserAgentResult);
        }

        /// <summary>
        /// Gets the hash code of the instance.
        /// </summary>
        /// <returns>Returns hash code of the instance.</returns>
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

        /// <summary>
        /// Gets the string presentation of the instance.
        /// </summary>
        /// <returns>Returns the string presentation of the instance.</returns>
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
