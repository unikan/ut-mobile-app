using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UtMobileApp.Models
{
    public class ExamsJSON
    {
        public class Id
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Updated
        {
            [JsonProperty("$t")]
            public DateTime t { get; set; }
        }

        public class Category
        {
            public string scheme { get; set; }
            public string term { get; set; }
        }

        public class Title
        {
            public string type { get; set; }
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string type { get; set; }
            public string href { get; set; }
        }

        public class Name
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Email
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Author
        {
            public Name name { get; set; }
            public Email email { get; set; }
        }

        public class OpenSearchTotalResults
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class OpenSearchStartIndex
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Id2
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Updated2
        {
            [JsonProperty("$t")]
            public DateTime t { get; set; }
        }

        public class Category2
        {
            public string scheme { get; set; }
            public string term { get; set; }
        }

        public class Title2
        {
            public string type { get; set; }
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Content
        {
            public string type { get; set; }
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Link2
        {
            public string rel { get; set; }
            public string type { get; set; }
            public string href { get; set; }
        }

        public class GsxUniversitetiitetovësуниверзитетвотетовоuniversityoftetovo
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCre1l
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCkd7g
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxD2mkx
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxD9ney
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxD5fpr
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDyxo8
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxE0c8p
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDqi9q
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDrwu7
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCn6ca
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCokwr
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxChk2m
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxClrrx
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCyevm
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCztg3
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxD180g
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCssly
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCu76f
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCvlqs
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxCx0b9
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDb1zf
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDcgjs
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDdv49
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxD415a
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxD6ua4
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxD88ul
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDkvya
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDmair
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDf9om
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDgo93
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDi2tg
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDjhdx
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDw4je
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class GsxDxj3v
        {
            [JsonProperty("$t")]
            public string t { get; set; }
        }

        public class Entry
        {
            public Id2 id { get; set; }
            public Updated2 updated { get; set; }
            public List<Category2> category { get; set; }
            public Title2 title { get; set; }
            public Content content { get; set; }
            public List<Link2> link { get; set; }

            // --------------------------------------------------------------------------------
            // EXAMS
            [JsonProperty("gsx$_cn6ca")]
            public GsxCn6ca E_Semester { get; set; }
            [JsonProperty("gsx$_cre1l")]
            public GsxCre1l E_Subjects { get; set; }
            [JsonProperty("gsx$_chk2m")]
            public GsxChk2m E_Teacher { get; set; }
            [JsonProperty("gsx$_cyevm")]
            public GsxCyevm E_Date1 { get; set; }
            [JsonProperty("gsx$_cztg3")]
            public GsxCztg3 E_Time1 { get; set; }
            [JsonProperty("gsx$_d180g")]
            public GsxD180g E_Venue1 { get; set; }
            [JsonProperty("gsx$_cu76f")]
            public GsxCu76f E_Date2 { get; set; }
            [JsonProperty("gsx$_cvlqs")]
            public GsxCvlqs E_Time2 { get; set; }
            [JsonProperty("gsx$_cx0b9")]
            public GsxCx0b9 E_Venue2 { get; set; }
            [JsonProperty("gsx$_dcgjs")]
            public GsxDcgjs E_Date3 { get; set; }
            [JsonProperty("gsx$_ddv49")]
            public GsxDdv49 E_Time3 { get; set; }
            [JsonProperty("gsx$_d415a")]
            public GsxD415a E_Venue3 { get; set; }
            [JsonProperty("gsx$_d88ul")]
            public GsxD88ul E_Date4 { get; set; }
            [JsonProperty("gsx$_dkvya")]
            public GsxDkvya E_Time4 { get; set; }
            [JsonProperty("gsx$_dmair")]
            public GsxDmair E_Venue4 { get; set; }
        }

        public class Feed
        {
            public string xmlns { get; set; }
            [JsonProperty("xmlns$openSearch")]
            public string xmlnsopenSearch { get; set; }
            [JsonProperty("xmlns$gsx")]
            public string xmlnsgsx { get; set; }
            public Id id { get; set; }
            public Updated updated { get; set; }
            public List<Category> category { get; set; }
            public Title title { get; set; }
            public List<Link> link { get; set; }
            public List<Author> author { get; set; }
            [JsonProperty("openSearch$totalResults")]
            public OpenSearchTotalResults openSearchtotalResults { get; set; }
            [JsonProperty("openSearch$startIndex ")]
            public OpenSearchStartIndex openSearchstartIndex { get; set; }
            public List<Entry> entry { get; set; }
        }

        public class RootObject
        {
            public string version { get; set; }
            public string encoding { get; set; }
            public Feed feed { get; set; }
        }
    }
}
