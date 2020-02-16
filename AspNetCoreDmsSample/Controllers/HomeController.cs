using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DMSSample.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DMSSample.Constants;
using DMSSample.Services;
using Microsoft.Extensions.Options;
using Amazon.Runtime.CredentialManagement;
using Amazon.DatabaseMigrationService;
using Amazon.DatabaseMigrationService.Model;

namespace DMSSample.Controllers
{
    public class HomeController : Controller
    {
        IOptions<AppSettings> _appSettings;
        IAmazonDatabaseMigrationService _migrationService;

        public HomeController(IOptions<AppSettings> appSettings, IAmazonDatabaseMigrationService migrationService) : base(){
            _appSettings = appSettings;
            _migrationService = migrationService;
        }
        
        public IActionResult Switch(LoginModel model){
            ModelState.Clear();

            string primaryPassword = model.PrimaryPassword;
            string primaryDatabaseName = model.PrimaryDatabaseName;
            string primaryServerName = model.PrimaryServerName;
            string primaryServerType = model.PrimaryServerType;
            string primaryUserName = model.PrimaryUserName;

            model.PrimaryPassword = model.ReplicaPassword;
            model.PrimaryDatabaseName = model.ReplicaDatabaseName;
            model.PrimaryServerName = model.ReplicaServerName;
            model.PrimaryServerType = model.ReplicaServerType;
            model.PrimaryUserName = model.ReplicaUserName;

            model.ReplicaDatabaseName = primaryDatabaseName;
            model.ReplicaPassword = primaryPassword;
            model.ReplicaServerName = primaryServerName;
            model.ReplicaServerType = primaryServerType;
            model.ReplicaUserName = primaryUserName;

            ViewData["PrimaryServerTypeList"] = buildServerTypeSelectList(model.PrimaryServerType);
            ViewData["ReplicaServerTypeList"] = buildServerTypeSelectList(model.ReplicaServerType);


            return View("Index", model);
        }

        public async Task<IActionResult> Apply(LoginModel model){

            DbContextService service = new DbContextService(_appSettings);
            try{
                BaseContext context = service.CreateDbContext(model);
                var person = context.Person.Take(1).ToList();

                ViewData["PrimaryConnectionMessage"] = "Primary Connection Test Successful";
                ViewData["PrimaryConnectionError"] = false;
            }
            catch(Exception ex){

                ViewData["PrimaryConnectionMessage"] = ex.Message;
                ViewData["PrimaryConnectionError"] = true;

            }

            if(!String.IsNullOrEmpty(model.ReplicaDatabaseName)){
                try{
                    BaseContext context = service.CreateReplicaDbContext(model);
                    var person = context.Person.Take(1).ToList();

                    ViewData["ReplicaConnectionMessage"] = "Secondary Connection Test Successful";
                    ViewData["ReplicaConnectionError"] = false;

                }catch(Exception ex){

                    ViewData["ReplicaConnectionMessage"] = ex.Message;
                    ViewData["ReplicaConnectionError"] = true;

                }
            }

            if(!String.IsNullOrEmpty(model.ReplicationTaskArn) && !String.IsNullOrEmpty(model.AccessKey) && !String.IsNullOrEmpty(model.SecretKey) && !String.IsNullOrEmpty(model.EndPointArn)){
                
                try{
                    NetSDKCredentialsFile credentialsFile = new NetSDKCredentialsFile();
                    CredentialProfileOptions options = new CredentialProfileOptions();
                    options.AccessKey = model.AccessKey;
                    options.SecretKey = model.SecretKey;
                    credentialsFile.RegisterProfile(new CredentialProfile("default", options));

                    TestConnectionRequest request = new TestConnectionRequest();
                    request.ReplicationInstanceArn = model.ReplicationTaskArn;
                    request.EndpointArn = model.EndPointArn;
                    
                    var statistics = await _migrationService.TestConnectionAsync(request);

                    ViewData["AWSConnectionMessage"] = "AWS API Connection Test Successful";
                    ViewData["AWSConnectionError"] = false;

                }
                catch(Exception ex){

                    ViewData["AWSConnectionMessage"] = ex.Message;
                    ViewData["AWSConnectionError"] = true;

                }
            }

            return Index(model);
        }

        public IActionResult Index(LoginModel model)
        {       

            LoginModel returnModel = new LoginModel();
            
            if(model == null){
                model = new LoginModel();
            }

            returnModel.PrimaryServerType = model.PrimaryServerType ?? HttpContext.Session.GetString("PrimaryServerType") ?? "";
            returnModel.PrimaryServerName = model.PrimaryServerName ?? HttpContext.Session.GetString("PrimaryServerName") ?? "";
            returnModel.PrimaryDatabaseName = model.PrimaryDatabaseName ?? HttpContext.Session.GetString("PrimaryDatabaseName") ?? "";
            returnModel.PrimaryUserName = model.PrimaryUserName ?? HttpContext.Session.GetString("PrimaryUserName") ?? "";
            returnModel.PrimaryPassword = model.PrimaryPassword ?? HttpContext.Session.GetString("PrimaryPassword") ?? "";

            returnModel.ReplicaServerType = model.ReplicaServerType ?? HttpContext.Session.GetString("ReplicaServerType") ?? "";
            returnModel.ReplicaServerName = model.ReplicaServerName ?? HttpContext.Session.GetString("ReplicaServerName") ?? "";
            returnModel.ReplicaDatabaseName = model.ReplicaDatabaseName ?? HttpContext.Session.GetString("ReplicaDatabaseName") ?? "";
            returnModel.ReplicaUserName = model.ReplicaUserName ?? HttpContext.Session.GetString("ReplicaUserName") ?? "";
            returnModel.ReplicaPassword = model.ReplicaPassword ?? HttpContext.Session.GetString("ReplicaPassword") ?? "";

            returnModel.AccessKey = model.AccessKey ?? HttpContext.Session.GetString("AccessKey") ?? "";
            returnModel.SecretKey = model.SecretKey ?? HttpContext.Session.GetString("SecretKey") ?? "";
            returnModel.ReplicationTaskArn = model.ReplicationTaskArn ?? HttpContext.Session.GetString("ReplicationTaskArn") ?? "";
            returnModel.EndPointArn = model.EndPointArn ?? HttpContext.Session.GetString("EndPointArn") ?? "";

            HttpContext.Session.SetString("PrimaryServerType", returnModel.PrimaryServerType);
            HttpContext.Session.SetString("PrimaryServerName", returnModel.PrimaryServerName ?? "");
            HttpContext.Session.SetString("PrimaryDatabaseName", returnModel.PrimaryDatabaseName ?? "");
            HttpContext.Session.SetString("PrimaryUserName", returnModel.PrimaryUserName ?? "");
            HttpContext.Session.SetString("PrimaryPassword", returnModel.PrimaryPassword ?? "");

            HttpContext.Session.SetString("ReplicaServerType", returnModel.ReplicaServerType);
            HttpContext.Session.SetString("ReplicaServerName", returnModel.ReplicaServerName ?? "");
            HttpContext.Session.SetString("ReplicaDatabaseName", returnModel.ReplicaDatabaseName ?? "");
            HttpContext.Session.SetString("ReplicaUserName", returnModel.ReplicaUserName ?? "");
            HttpContext.Session.SetString("ReplicaPassword", returnModel.ReplicaPassword ?? "");

            HttpContext.Session.SetString("AccessKey", returnModel.AccessKey);
            HttpContext.Session.SetString("SecretKey", returnModel.SecretKey);
            HttpContext.Session.SetString("ReplicationTaskArn", returnModel.ReplicationTaskArn);
            HttpContext.Session.SetString("EndPointArn", returnModel.EndPointArn);

            ViewData["PrimaryServerTypeList"] = buildServerTypeSelectList(returnModel.PrimaryServerType);
            ViewData["ReplicaServerTypeList"] = buildServerTypeSelectList(returnModel.ReplicaServerType);

            return View("Index", returnModel);
        }

        private SelectList buildServerTypeSelectList(String selectedItem){
            
            List<DBServerTypeModel> dbServerTypes = new List<DBServerTypeModel>();
            dbServerTypes.Add(new DBServerTypeModel("SQLServer"));
            dbServerTypes.Add(new DBServerTypeModel("MySQL"));   
            
            return new SelectList(dbServerTypes, "Id", "Name", selectedItem);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
