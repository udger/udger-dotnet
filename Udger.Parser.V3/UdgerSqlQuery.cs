// <copyright file="UdgerSqlQuery.cs" company="Udger s.r.o.">
// Copyright (c) Udger s.r.o.. All rights reserved.
// </copyright>

namespace Udger.Parser.V3
{
    /// <summary>
    /// Class for the SQL query constants.
    /// </summary>
    internal class UdgerSqlQuery
    {
        /// <summary>
        /// Contain query for crawlers.
        /// </summary>
        public const string SqlCrawler = @"
SELECT
    NULL AS client_id,
    NULL AS class_id,
    'Crawler' AS ua_class,
    'crawler' AS ua_class_code,
    name AS ua,
    NULL AS ua_engine,
    ver AS ua_version,
    ver_major AS ua_version_major,
    last_seen AS crawler_last_seen,
    respect_robotstxt AS crawler_respect_robotstxt,
    crawler_classification AS crawler_category,
    crawler_classification_code AS crawler_category_code,
    NULL AS ua_uptodate_current_version,
    family AS ua_family,
    family_code AS ua_family_code,
    family_homepage AS ua_family_homepage,
    family_icon AS ua_family_icon,
    NULL AS ua_family_icon_big,
    vendor AS ua_family_vendor,
    vendor_code AS ua_family_vendor_code,
    vendor_homepage AS ua_family_vendor_homepage,
    'https://udger.com/resources/ua-list/bot-detail?bot=' || REPLACE(family, ' ', '%20') || '#id' || udger_crawler_list.id AS ua_family_info_url
FROM
    udger_crawler_list
LEFT JOIN
    udger_crawler_class ON udger_crawler_class.id = udger_crawler_list.class_id
WHERE
    ua_string = ?";

        /// <summary>
        /// Query for clients.
        /// </summary>
        public const string SqlClient = @"";

        /// <summary>
        /// Query for operating system columns.
        /// </summary>
        public const string OsColumns = @"";


    }
}
