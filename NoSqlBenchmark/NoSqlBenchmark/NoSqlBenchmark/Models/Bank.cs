using System.Collections.Generic;

namespace NoSqlBenchmark.Models
{
    public class Bank : BaseModel
    {
        public string approvalfy { get; set; }
        public string board_approval_month { get; set; }
        public string boardapprovaldate { get; set; }
        public string borrower { get; set; }
        public string closingdate { get; set; }
        public string country_namecode { get; set; }
        public string countrycode { get; set; }
        public string countryname { get; set; }
        public string countryshortname { get; set; }
        public string docty { get; set; }
        public string envassesmentcategorycode { get; set; }
        public int grantamt { get; set; }
        public int ibrdcommamt { get; set; }
        public string id { get; set; }
        public int idacommamt { get; set; }
        public string impagency { get; set; }
        public string lendinginstr { get; set; }
        public string lendinginstrtype { get; set; }
        public int lendprojectcost { get; set; }
        public List<MajorsectorPercent> majorsector_percent { get; set; }
        public List<MjsectorNamecode> mjsector_namecode { get; set; }
        public List<string> mjtheme { get; set; }
        public List<MjthemeNamecode> mjtheme_namecode { get; set; }
        public string mjthemecode { get; set; }
        public string prodline { get; set; }
        public string prodlinetext { get; set; }
        public string productlinetype { get; set; }
        public ProjectAbstract project_abstract { get; set; }
        public string project_name { get; set; }
        public List<Projectdoc> projectdocs { get; set; }
        public string projectfinancialtype { get; set; }
        public string projectstatusdisplay { get; set; }
        public string regionname { get; set; }
        public List<Sector> sector { get; set; }
        public List<SectorNamecode> sector_namecode { get; set; }
        public string sectorcode { get; set; }
        public string source { get; set; }
        public string status { get; set; }
        public string supplementprojectflg { get; set; }
        public Theme Theme { get; set; }
        public List<ThemeNamecode> theme_namecode { get; set; }
        public string themecode { get; set; }
        public int totalamt { get; set; }
        public int totalcommamt { get; set; }
        public string url { get; set; }

        public static BaseModel GetDemo()
        {
            throw new System.NotImplementedException();
        }
    }


    public class MajorsectorPercent
    {
        public string Name { get; set; }
        public int Percent { get; set; }
    }

    public class MjsectorNamecode
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class MjthemeNamecode
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class ProjectAbstract
    {
        public string cdata { get; set; }
    }

    public class Projectdoc
    {
        public string DocTypeDesc { get; set; }
        public string DocType { get; set; }
        public string EntityID { get; set; }
        public string DocURL { get; set; }
        public string DocDate { get; set; }
    }

    public class Sector
    {
        public string Name { get; set; }
    }

    public class SectorNamecode
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    public class Theme
    {
        public string Name { get; set; }
        public int Percent { get; set; }
    }

    public class ThemeNamecode
    {
        public string name { get; set; }
        public string code { get; set; }
    }
}
