using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Configuration;
using FacebookLoginASPnetWebForms.Models;
using Newtonsoft.Json;
using Google.Apis.Analytics.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.Security.Cryptography.X509Certificates;

namespace FacebookLoginASPnetWebForms.account
{
    public partial class user : System.Web.UI.Page
    {
        private string keyFilePath = HttpRuntime.AppDomainAppPath + "/IAtest-28a2e52d2887.p12"; //private key
        private string serviceAccountEmail = "testing@iatest-140103.iam.gserviceaccount.com";
        private string keyPassword = "notasecret";
        private string websiteCode = "127257439";
        private AnalyticsService service = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the Facebook code from the querystring
                if (Request.QueryString["code"] != "")
                {
                    //GetFacebookUserData(Request.QueryString["code"]);
                }
                GaChart();
            }
        }
        private void GaChart()
        {
            System.Diagnostics.Debug.Print("T:" + HttpRuntime.AppDomainAppPath);
            var certificate = new X509Certificate2(keyFilePath, keyPassword, X509KeyStorageFlags.MachineKeySet
                                                    | X509KeyStorageFlags.PersistKeySet
                                                    | X509KeyStorageFlags.Exportable);
            var scopes =new string[] {
                             AnalyticsService.Scope.Analytics,              // view and manage your analytics data    
                             AnalyticsService.Scope.AnalyticsEdit,          // edit management actives    
                             AnalyticsService.Scope.AnalyticsManageUsers,   // manage users    
                             AnalyticsService.Scope.AnalyticsReadonly};     // View analytics data    
            var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes
            }.FromCertificate(certificate));
            service = new AnalyticsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            DataResource.GaResource.GetRequest request = service.Data.Ga.Get(
           "ga:" + websiteCode,
           DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"),
           DateTime.Today.ToString("yyyy-MM-dd"), "ga:sessions");
            request.Dimensions = "ga:year,ga:month,ga:day";
            var data = request.Execute();

            if (data.Rows == null)
            {
                //visitsData.Add(new ChartRecord("", 0));
            }
            else
            {
                foreach (var row in data.Rows)
                {
                    //Response.Write(row[3]+"<br>");
                    visitsData.Add(new ChartRecord(new DateTime(int.Parse(row[0]), int.Parse(row[1]), int.Parse(row[2])).ToString("MM-dd-yyyy"), int.Parse(row[3])));
                }
            }

            Chart1.Series[0].XValueMember = "Date";
            Chart1.Series[0].YValueMembers = "Visits";
            Chart1.DataSource = visitsData;
            Chart1.DataBind();



            // DataResource.GaResource.GetRequest userRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:users");

            // //user dimensions
            // userRequest.Dimensions = "ga:date,ga:userType";

            // var user = userRequest.Execute();

            // System.Diagnostics.Debug.Print(user.Id);
            // for (int i = 0; i < user.Rows.Count(); i++)
            // {
            //     for (int a = 0; a < user.Rows[i].Count(); a++)
            //     {
            //         System.Diagnostics.Debug.Print("[" + a + "]" + ":" + user.Rows[i][a]);
            //     }
            // }


            //DataResource.GaResource.GetRequest sessionRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:sessions," + //session metrics
            //"ga:bounces," +
            //"ga:bounceRate," +
            //"ga:sessionDuration," +
            //"ga:avgSessionDuration," +
            //"ga:hits");

            ////session dimensions
            //sessionRequest.Dimensions = "ga:sessionDurationBucket";

            //var session = sessionRequest.Execute();

            //System.Diagnostics.Debug.Print(session.Id);
            //for (int i = 0; i < session.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < session.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + session.Rows[i][a]);
            //    }

            //}

            //DataResource.GaResource.GetRequest trafficRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-5).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:organicSearches"); //traffic sources metrics

            ////traffic sources dimensions
            //trafficRequest.Dimensions = "ga:referralPath," +
            //                            "ga:fullReferrer," +
            //                            "ga:campaign," +
            //                            //"ga:source," +
            //                            //"ga:medium," +
            //                            //"ga:sourceMedium," +
            //                            //"ga:keyword," +
            //                            "ga:adContent," +
            //                            "ga:socialNetwork," +
            //                            "ga:hasSocialSourceReferral," +
            //                            "ga:campaignCode";

            //var traffic = trafficRequest.Execute();

            //System.Diagnostics.Debug.Print(traffic.Id);
            //for (int i = 0; i < traffic.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < traffic.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + traffic.Rows[i][a]);
            //    }

            //}

            //DataResource.GaResource.GetRequest adwordsRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:impressions,"+
            //"ga:adClicks," +
            //"ga:adCost," +
            //"ga:CPM," +
            //"ga:CPC," +
            ////"ga:CTR," +
            //"ga:costPerTransaction," +
            //"ga:costPerGoalConversion," +
            //"ga:costPerConversion," +
            //"ga:RPC," +
            //"ga:ROAS"); //Adwords metrics

            ////adwords dimensions
            //adwordsRequest.Dimensions = "ga:adGroup," +
            //                            //"ga:adSlot," +
            //                            //"ga:adDistributionNetwork," +
            //                            //"ga:adMatchType," +
            //                            //"ga:adKeywordMatchType," +
            //                            //"ga:adMatchedQuery," +
            //                            //"ga:adPlacementDomain,"+
            //                            //"ga:adPlacementUrl," +
            //                            //"ga:adFormat," +
            //                            //"ga:adTargetingType," +
            //                            //"ga:adTargetingOption," +
            //                            //"ga:adDisplayUrl," +
            //                            //"ga:adDestinationUrl," +
            //                            //"ga:adwordsCustomerID," +
            //                            "ga:adwordsCampaignID," +
            //                            "ga:adwordsAdGroupID," +
            //                            "ga:adwordsCreativeID," +
            //                            "ga:adwordsCriteriaID," +
            //                            "ga:adQueryWordCount," +
            //                            "ga:isTrueViewVideoAd";

            //var adwords = adwordsRequest.Execute();

            //System.Diagnostics.Debug.Print(adwords.Id);
            //for (int i = 0; i < adwords.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < adwords.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + adwords.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest goalRequest = service.Data.Ga.Get(
            //    "ga:" + websiteCode, //website id
            //    DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), //start date
            //    DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //    "ga:goalXXStarts," + //XX=1-20
            //    "ga:goalStartsAll," +
            //    "ga:goalXXCompletions," + //XX=1-20
            //    "ga:goalCompletionsAll," +
            //    "ga:goalXXValue," + //XX=1-20
            //    "ga:goalValueAll," +
            //    "ga:goalValuePerSession," +
            //    //"ga:goalXXConversionRate," + //XX=1-20
            //    //"ga:goalConversionRateAll," +
            //    //"ga:goalXXAbandons," + //XX=1-20
            //    "ga:goalAbandonsAll," +
            //    "ga:goalXXAbandonRate," + //XX=1-20
            //    "ga:goalAbandonRateAll"); //goal metrics

            ////goal dimensions
            //goalRequest.Dimensions = "ga:goalCompletionLocation," +
            //                            "ga:goalPreviousStep1," +
            //                            "ga:goalPreviousStep2," +
            //                            "ga:goalPreviousStep3";

            //var goal = goalRequest.Execute();

            //System.Diagnostics.Debug.Print(goal.Id);
            //for (int i = 0; i < goal.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < goal.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + goal.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest platformRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:goalAbandonRateAll"); //Platform or Device metrics

            ////Platform or Device dimensions
            //platformRequest.Dimensions = "ga:browser," +
            //                            //"ga:browserVersion," +
            //                            //"ga:operatingSystem," +
            //                            //"ga:operatingSystemVersion," +
            //                            //"ga:mobileDeviceBranding," +
            //                            //"ga:mobileDeviceModel," +
            //                            "ga:mobileInputSelector," +
            //                            "ga:mobileDeviceInfo," +
            //                            "ga:mobileDeviceMarketingName," +
            //                            "ga:deviceCategory," +
            //                            "ga:browserSize," +
            //                            "ga:dataSource";

            //var platform = platformRequest.Execute();

            //System.Diagnostics.Debug.Print(platform.Id);
            //for (int i = 0; i < platform.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < platform.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + platform.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest geoRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:goalAbandonRateAll"); //Geo Network metrics

            ////Geo Network dimensions
            //geoRequest.Dimensions = "ga:continent," +
            //                            "ga:subContinent," +
            //                            "ga:country," +
            //                            "ga:region," +
            //                            //"ga:metro," +
            //                            //"ga:city," +
            //                            //"ga:latitude," +
            //                            //"ga:longitude," +
            //                            //"ga:networkDomain," +
            //                            //"ga:networkLocation," +
            //                            //"ga:cityId," +
            //                            //"ga:continentId," +
            //                            //"ga:countryIsoCode," +
            //                            //"ga:metroId," +
            //                            "ga:regionId," +
            //                            "ga:regionIsoCode," +
            //                            "ga:subContinentCode";

            //var geo = geoRequest.Execute();

            //System.Diagnostics.Debug.Print(geo.Id);
            //for (int i = 0; i < geo.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < geo.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + geo.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest systemRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:goalAbandonRateAll"); //System metrics

            ////System dimensions
            //systemRequest.Dimensions = "ga:flashVersion," +
            //                            "ga:javaEnabled," +
            //                            "ga:language," +
            //                            "ga:screenColors," +
            //                            "ga:sourcePropertyDisplayName," +
            //                            "ga:sourcePropertyTrackingId," +
            //                            "ga:screenResolution";

            //var system = systemRequest.Execute();

            //System.Diagnostics.Debug.Print(system.Id);
            //for (int i = 0; i < system.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < system.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + system.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest contentRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:pageValue," +
            //"ga:pageValue," +
            //"ga:entrances," +
            //"ga:entranceRate," +
            //"ga:pageviews," +
            //"ga:pageviewsPerSession," +
            //"ga:uniquePageviews," +
            ////"ga:timeOnPage," +
            //"ga:avgTimeOnPage," +
            //"ga:exits," +
            //"ga:exitRate"); //Content Grouping metrics

            ////Content Grouping dimensions
            //contentRequest.Dimensions = "ga:hostname," +
            //                            "ga:pagePath," +
            //                            "ga:pagePathLevel1," +
            //                            "ga:pagePathLevel2," +
            //                            "ga:pagePathLevel3," +
            //                            //"ga:pagePathLevel4," +
            //                            //"ga:pageTitle," +
            //                            //"ga:landingPagePath," +
            //                            //"ga:secondPagePath," +
            //                            //"ga:exitPagePath," +
            //                            "ga:previousPagePath," +
            //                            "ga:pageDepth";

            //var content = contentRequest.Execute();

            //System.Diagnostics.Debug.Print(content.Id);
            //for (int i = 0; i < content.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < content.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + content.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest pageRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:contentGroupUniqueViewsXX");//XX=1-5 //Content Grouping metrics

            ////Content Grouping dimensions
            //pageRequest.Dimensions = "ga:landingContentGroupXX," + //XX=1-5
            //                            "ga:previousContentGroupXX," + //XX=1-5
            //                            "ga:contentGroupXX"; //XX=1-5

            //var page = pageRequest.Execute();

            //System.Diagnostics.Debug.Print(page.Id);
            //for (int i = 0; i < page.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < page.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + page.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest searchRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:searchResultViews" +
            ////"ga:searchUniques," +
            //"ga:avgSearchResultViews," +
            //"ga:searchSessions," +
            //"ga:percentSessionsWithSearch," +
            //"ga:searchDepth," +
            ////"ga:avgSearchDepth," +
            ////"ga:searchRefinements," +
            ////"ga:percentSearchRefinements," +
            ////"ga:searchDuration," +
            ////"ga:avgSearchDuration," +
            ////"ga:searchExits," +
            ////"ga:searchExitRate," +
            ////"ga:searchGoalXXConversionRate," +
            //"ga:searchGoalConversionRateAll," +
            //"ga:goalValueAllPerSearch");//Internal Search metrics

            ////Internal Search dimensions
            //searchRequest.Dimensions = "ga:searchUsed," +
            //                            "ga:searchKeyword," +
            //                            "ga:searchKeywordRefinement," +
            //                            "ga:searchCategory," +
            //                            "ga:searchStartPage," +
            //                            "ga:searchDestinationPage," +
            //                            "ga:searchAfterDestinationPage"; 

            //var search = searchRequest.Execute();

            //System.Diagnostics.Debug.Print(search.Id);
            //for (int i = 0; i < search.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < search.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + search.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest speedRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:pageLoadTime," +
            //"ga:pageLoadSample," +
            //"ga:avgPageLoadTime," +
            //"ga:domainLookupTime," +
            //"ga:avgDomainLookupTime," +
            ////"ga:pageDownloadTime," +
            ////"ga:avgPageDownloadTime," +
            ////"ga:redirectionTime," +
            ////"ga:avgRedirectionTime," +
            ////"ga:serverConnectionTime," +
            ////"ga:avgServerConnectionTime," +
            ////"ga:serverResponseTime," +
            ////"ga:avgServerResponseTime," +
            ////"ga:speedMetricsSample," +
            ////"ga:domInteractiveTime," +
            ////"ga:avgDomInteractiveTime," +
            //"ga:domContentLoadedTime," +
            //"ga:avgDomContentLoadedTime," +
            //"ga:domLatencyMetricsSample");//Site Speed metrics

            ////Site Speed dimensions
            //speedRequest.Dimensions = "ga:referralPath";

            //var speed = speedRequest.Execute();

            //System.Diagnostics.Debug.Print(speed.Id);
            //for (int i = 0; i < speed.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < speed.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + speed.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest appRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:screenviews," +
            //"ga:uniqueScreenviews," +
            //"ga:screenviewsPerSession," +
            //"ga:timeOnScreen," +
            //"ga:avgScreenviewDuration");//App Tracking metrics

            ////App Tracking dimensions
            //appRequest.Dimensions = "ga:appInstallerId,"+
            //    "ga:appVersion," +
            //    "ga:appName," +
            //    //"ga:appId," +
            //    "ga:screenName," +
            //    "ga:screenDepth," +
            //    "ga:landingScreenName," +
            //    "ga:exitScreenName";

            //var app = appRequest.Execute();

            //System.Diagnostics.Debug.Print(app.Id);
            //for (int i = 0; i < app.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < app.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + app.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest trackingRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:totalEvents," +
            //"ga:uniqueEvents," +
            //"ga:eventValue," +
            //"ga:avgEventValue," +
            //"ga:sessionsWithEvent," +
            //"ga:eventsPerSessionWithEvent");//Event Tracking metrics

            ////Event Tracking dimensions
            //trackingRequest.Dimensions = "ga:eventCategory," +
            //    "ga:eventAction," +
            //    "ga:eventLabel," +
            //    "ga:appId";

            //var tracking = trackingRequest.Execute();

            //System.Diagnostics.Debug.Print(tracking.Id);
            //for (int i = 0; i < tracking.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < tracking.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + tracking.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest ecommerceRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:transactions," +
            //"ga:transactionsPerSession," +
            //"ga:transactionRevenue");//Ecommerce metrics

            ////Ecommerce dimensions
            //ecommerceRequest.Dimensions = 
            //    "ga:transactionId," +
            //    "ga:affiliation," +
            //    "ga:sessionsToTransaction";

            //var ecommerce = ecommerceRequest.Execute();

            //System.Diagnostics.Debug.Print(ecommerce.Id);
            //for (int i = 0; i < ecommerce.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < ecommerce.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + ecommerce.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest socialRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:socialInteractions," +
            //"ga:uniqueSocialInteractions," +
            //"ga:socialInteractionsPerSession");//Social Interactions metrics

            ////Social Interactions dimensions
            //socialRequest.Dimensions =
            //    "ga:socialInteractionNetwork," +
            //    "ga:socialInteractionAction," +
            //    "ga:socialInteractionNetworkAction";

            //var social = socialRequest.Execute();

            //System.Diagnostics.Debug.Print(social.Id);
            //for (int i = 0; i < social.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < social.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + social.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest timingRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:userTimingValue," +
            //"ga:userTimingSample," +
            //"ga:avgUserTimingValue");//User Timings metrics

            ////User Timings dimensions
            //timingRequest.Dimensions =
            //    "ga:userTimingCategory," +
            //    "ga:userTimingLabel," +
            //    "ga:userTimingVariable";

            //var timing = timingRequest.Execute();

            //System.Diagnostics.Debug.Print(timing.Id);
            //for (int i = 0; i < timing.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < timing.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + timing.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest timingRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:exceptions," +
            //"ga:exceptionsPerScreenview," +
            //"ga:fatalExceptions");//Social Interactions metrics

            ////Social Interactions dimensions
            //timingRequest.Dimensions =
            //    "ga:exceptionDescription";

            //var timing = timingRequest.Execute();

            //System.Diagnostics.Debug.Print(timing.Id);
            //for (int i = 0; i < timing.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < timing.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + timing.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest experimentsRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:fatalExceptions");//Content Experiments metrics

            ////Content Experiments dimensions
            //experimentsRequest.Dimensions =
            //    "ga:experimentId";

            //var experiments = experimentsRequest.Execute();

            //System.Diagnostics.Debug.Print(experiments.Id);
            //for (int i = 0; i < experiments.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < experiments.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + experiments.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest customRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:metricXX");//XX=1-200//Custom Variables or Columns metrics

            ////Custom Variables or Columns dimensions
            //customRequest.Dimensions =
            //    "ga:dimensionXX";//XX==1-200

            //var custom = customRequest.Execute();

            //System.Diagnostics.Debug.Print(custom.Id);
            //for (int i = 0; i < custom.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < custom.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + custom.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest timeRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:sessions");//Content Experiments metrics

            ////Content Experiments dimensions
            //timeRequest.Dimensions =
            //    "ga:date,"+
            //    "ga:year," +
            //    "ga:month," +
            //    "ga:week," +
            //    "ga:day," +
            //    "ga:hour," +
            //    //"ga:minute," +
            //    //"ga:nthMonth," +
            //    "ga:nthWeek";

            //var time = timeRequest.Execute();

            //System.Diagnostics.Debug.Print(time.Id);
            //for (int i = 0; i < time.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < time.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + time.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest audienceRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:sessions");//Audience metrics

            ////Audience dimensions
            //audienceRequest.Dimensions =
            //    "ga:userAgeBracket," +
            //    "ga:userGender";

            //var audience = audienceRequest.Execute();

            //System.Diagnostics.Debug.Print(audience.Id);
            //for (int i = 0; i < audience.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < audience.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + audience.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest adsenseRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:adsenseRevenue,"+
            //"ga:adsenseAdUnitsViewed," +
            //"ga:adsenseAdsViewed," +
            //"ga:adsenseAdsClicks," +
            //"ga:adsensePageImpressions," +
            //"ga:adsenseCTR," +
            //"ga:adsenseECPM," +
            //"ga:adsenseExits," +
            //"ga:adsenseViewableImpressionPercent," +
            //"ga:adsenseCoverage");//Adsense metrics

            ////Adsense dimensions
            //adsenseRequest.Dimensions =
            //    "ga:day";

            //var adsense = adsenseRequest.Execute();

            //System.Diagnostics.Debug.Print(adsense.Id);
            //for (int i = 0; i < adsense.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < adsense.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + adsense.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest exchangeRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:adxImpressions," +
            //"ga:adxCoverage," +
            //"ga:adxMonetizedPageviews," +
            //"ga:adxImpressionsPerSession," +
            //"ga:adxViewableImpressionsPercent," +
            //"ga:adxClicks," +
            //"ga:adxCTR," +
            //"ga:adxRevenue," +
            //"ga:adxRevenuePer1000Sessions," +
            //"ga:adxECPM");//Ad Exchange metrics

            ////Ad Exchange dimensions
            //exchangeRequest.Dimensions =
            //    "ga:day";

            //var exchange = exchangeRequest.Execute();

            //System.Diagnostics.Debug.Print(exchange.Id);
            //for (int i = 0; i < exchange.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < exchange.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + exchange.Rows[i][a]);
            //    }
            //}

            //DataResource.GaResource.GetRequest exchangeRequest = service.Data.Ga.Get(
            //"ga:" + websiteCode, //website id
            //DateTime.Today.AddDays(-15).ToString("yyyy-MM-dd"), //start date
            //DateTime.Today.ToString("yyyy-MM-dd"), //end date
            //"ga:correlationScore");//Ad Exchange metrics

            ////Ad Exchange dimensions
            //exchangeRequest.Dimensions =
            //    "ga:correlationModelId";

            //var exchange = exchangeRequest.Execute();

            //System.Diagnostics.Debug.Print(exchange.Id);
            //for (int i = 0; i < exchange.Rows.Count(); i++)
            //{
            //    for (int a = 0; a < exchange.Rows[i].Count(); a++)
            //    {
            //        System.Diagnostics.Debug.Print("[" + a + "]" + ":" + exchange.Rows[i][a]);
            //    }
            //}
        }

        private List<ChartRecord> visitsData = new List<ChartRecord>();
        class ChartRecord
        {
            public ChartRecord(string date, int visits)
            {
                _date = date;
                _visits = visits;
            }
            private string _date;
            public string Date
            {
                get { return _date; }
                set { _date = value; }
            }
            private int _visits;
            public int Visits
            {
                get { return _visits; }
                set { _visits = value; }
            }
        }

        protected void GetFacebookUserData(string code)
        {
            // Exchange the code for an access token
            Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&redirect_uri=http://" + Request.ServerVariables["SERVER_NAME"] + ":" + Request.ServerVariables["SERVER_PORT"] + "/account/user.aspx&code=" + code);
            HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

            System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
            string token = str.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            string[] combined = token.Split('&');
            string accessToken = combined[0];

            // Exchange the code for an extended access token
            Uri eatTargetUri = new Uri("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id=" + ConfigurationManager.AppSettings["FacebookAppId"] + "&client_secret=" + ConfigurationManager.AppSettings["FacebookAppSecret"] + "&fb_exchange_token=" + accessToken);
            HttpWebRequest eat = (HttpWebRequest)HttpWebRequest.Create(eatTargetUri);

            StreamReader eatStr = new StreamReader(eat.GetResponse().GetResponseStream());
            string eatToken = eatStr.ReadToEnd().ToString().Replace("access_token=", "");

            // Split the access token and expiration from the single string
            string[] eatWords = eatToken.Split('&');
            string extendedAccessToken = eatWords[0];

            // Request the Facebook user information
            Uri targetUserUri = new Uri("https://graph.facebook.com/me?fields=id,about,bio,birthday,cover,devices,education,email,favorite_athletes,favorite_teams,first_name,gender,hometown,inspirational_people,install_type,installed,interested_in,is_shared_login,is_verified,languages,last_name,link,locale,location,meeting_for,middle_name,name,name_format,political,public_key,quotes,relationship_status,religion,shared_login_upgrade_required_by,significant_other,sports,test_group,third_party_id,timezone,updated_time,verified,viewer_can_send_gift,website,work,picture,age_range,context,currency,payment_pricepoints,security_settings,video_upload_limits&access_token=" + accessToken);
            HttpWebRequest user = (HttpWebRequest)HttpWebRequest.Create(targetUserUri);

            // Read the returned JSON object response
            StreamReader userInfo = new StreamReader(user.GetResponse().GetResponseStream());
            string jsonResponse = string.Empty;
            jsonResponse = userInfo.ReadToEnd();

            // Deserialize and convert the JSON object to the Facebook.User object type
            JavaScriptSerializer sr = new JavaScriptSerializer();
            string jsondata = jsonResponse;
            //Response.Write("JSON" + jsondata);
            //System.Diagnostics.Debug.WriteLine(jsondata);

            //print json data
            dynamic data = JsonConvert.DeserializeObject(jsondata);

            Response.Write("<br><br>id: " + data.id);
            Response.Write("<br>about: " + data.about);
            Response.Write("<br>bio: " + data.bio);
            Response.Write("<br>birthday: " + data.birthday);
            Response.Write("<br>cover: " + data.picture.data.url);
            Response.Write("<br>email: " + data.email);
            Response.Write("<br>first_name: " + data.first_name);
            Response.Write("<br>gender: " + data.gender);
            Response.Write("<br>install_type: " + data.install_type);
            Response.Write("<br>installed: " + data.installed);
            Response.Write("<br>is_shared_login: " + data.is_shared_login);
            Response.Write("<br>is_verified: " + data.is_verified);
            Response.Write("<br>last_name: " + data.last_name);
            Response.Write("<br>link: " + data.link);
            Response.Write("<br>locale: " + data.locale);
            Response.Write("<br>middle_name: " + data.middle_name);
            Response.Write("<br>name: " + data.name);
            Response.Write("<br>name_format: " + data.name_format);
            Response.Write("<br>political: " + data.political);
            Response.Write("<br>public_key: " + data.public_key);
            Response.Write("<br>quotes: " + data.quotes);
            Response.Write("<br>relationship_status: " + data.relationship_status);
            Response.Write("<br>religion: " + data.religion);
            Response.Write("<br>shared_login_upgrade_required_by: " + data.shared_login_upgrade_required_by);
            Response.Write("<br>test_group: " + data.test_group);
            Response.Write("<br>third_party_id: " + data.third_party_id);
            Response.Write("<br>timezone: " + data.timezone);
            Response.Write("<br>updated_time: " + data.updated_time);
            Response.Write("<br>verified: " + data.verified);
            Response.Write("<br>viewer_can_send_gift: " + data.viewer_can_send_gift);
            Response.Write("<br>website: " + data.website);

            Response.Write("<br>location: " + data.location.name);
            Response.Write("<br>hometown: " + data.hometown.name);
            Response.Write("<br>interested_in: " + data.interested_in[0]);
            Response.Write("<br>education: " + data.education[0].school.name);
            Response.Write("<br>favorite_teams: " + data.favorite_teams[0].name);
            Response.Write("<br>languages: " + data.languages[0].name);
            Response.Write("<br>work: " + data.work[0].description + " employer:" + data.work[0].employer.name);
            Response.Write("<br>age_range: " + data.age_range.min);
            Response.Write("<br>context_count: " + data.context.mutual_friends.summary.total_count);
            Response.Write("<br>payment_pricepoints: " + data.payment_pricepoints.mobile[0].user_price);
            Response.Write("<br>currency: " + data.currency.currency_offset);
            Response.Write("<br>security_settings: " + data.security_settings.secure_browsing.enabled);
            Response.Write("<br>video_upload_limits: " + data.video_upload_limits.length);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chart1.Series[0].Points.Clear();
            Chart1.DataSource = null;
            Chart1.DataBind();
            if (DropDownList1.SelectedItem.Text=="IAtest")
            {
                websiteCode = "127257439";
                GaChart();
            }
            else
            {
                websiteCode = "127639534";
                GaChart();
            }
        }
    }
}