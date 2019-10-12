using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMSSample.Models;
using DMSSample.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DMSSample.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IDbContextService _dbContextService;
        protected BaseContext _context;
        protected BaseContext _replicaContext;

        public BaseController(IDbContextService dbContextService){
            _dbContextService = dbContextService;
        }

        public override void OnActionExecuting(ActionExecutingContext context){
            _context = _dbContextService.CreateDbContext(HttpContext.Session.CreateLoginModelFromSession());
            _replicaContext = _dbContextService.CreateReplicaDbContext(HttpContext.Session.CreateLoginModelFromSession());
            base.OnActionExecuting(context);
        }

        private String ToPascalCase(String fieldName){
            StringBuilder sb = new StringBuilder();
            String[] nameParts = fieldName.Split('_');
            foreach(String part in nameParts){
                sb.Append(part.Substring(0, 1).ToUpper());
                if(part.Length > 1){
                    sb.Append(part.Substring(1, part.Length - 1));
                }
            }
            return sb.ToString();
        }

        protected JsonResult BuildModelStateJson(ModelStateDictionary modelState){
            List<ResultError> errorList = new List<ResultError>();

            IEnumerable<ModelError> errors = ModelState.Values.SelectMany(ms => ms.Errors);
            foreach(ModelError error in errors){
                errorList.Add(new ResultError(-99, error.ErrorMessage));
            }
            OperationResult result = OperationResult.Failed("Errors in data validation.", errorList);
            return Json(result);
        }

        protected void PrepareColumnHeaders(String sortOrder, String[] columns){
            foreach(String column in columns){
                if(sortOrder != null && sortOrder.StartsWith(column)){
                    ViewData[ToPascalCase(column) + "SortParam"] = sortOrder == column + "_desc" ? column : column + "_desc";
                }else{
                    ViewData[ToPascalCase(column) + "SortParam"] = column;
                } 
            }
        }
    }
}