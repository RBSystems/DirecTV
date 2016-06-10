using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DirecTV
{   
    public class DtvController
    {
        public String dCallsign;
        public int dMajor;
        public String dTitle;
        public String disRecording;
        public ushort dChan;
        public String url = "";

        public DtvController()                                              // Default Constructor
        {
            // no work needed
        }

        public DtvController(string DtvCmd)                                 //constructor with parameter of what URL to goto
        {
            if (DtvCmd.Trim() != "")//
                url = DtvCmd;
        }

        public void GetData()                                           //void  is the return type within the function
        {
            var httpGet = new HttpClient();                             //3 parts, client,request,response
            httpGet.KeepAlive = false;                  
            HttpClientRequest lRequest = new HttpClientRequest();
            lRequest.Url.Parse(url);                                    //parse/build the variable url, this also where a POST (etc.) can be inserted
            HttpClientResponse lResponse = httpGet.Dispatch(lRequest);  //makes connection, send request, gets response and puts into variable '1Response'
            var jsontext = lResponse.ContentString;
            if (jsontext.StartsWith("{") && jsontext.Contains("callsign"))
            {
                JObject Data = JObject.Parse(jsontext);
                //String queryText = (String)Data.SelectToken("query");
                dCallsign = (String)Data.SelectToken("callsign");
                dMajor = (int)Data.SelectToken("major");
                dTitle = (String)Data.SelectToken("title");
                dChan = (ushort)dMajor;
            }
        }
    }
}
         
//          {
//"callsign": "FOODHD",
//"contentId": "1 1 89237 37",
//"duration": 1791,
//"expiration": "0",
//"isOffAir": false,
//"isPartial": false,
//"isPclocked": 1,
//"isPpv": false,
//"isRecording": false,
//"isViewed": true,
//"isVod": false,
//"keepUntilFull": true,
//"major": 231,
//"minor": 65535,
//"offset": 263,
//"programId": "4405732",
//"rating": "No Rating",
//"recType": 3,
//"startTime": 1278342008,
//"stationId": 3900976,
//"status": {
//"code": 200,
//"msg": "OK.",
//"query": "/tv/getTuned"
//},
//"title": "Tyler's Ultimate",
//"uniqueId": "6728716739474078694"
//}