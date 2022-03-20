using Awepay_Test.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Awepay_Test.Data;
using System.Net.Mail;

namespace Awepay_Test.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Produces("application/json")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        [HttpPost]
        public JsonResult CreateUser(AppUser appuser)
        {
            ResponseObj result = new ResponseObj();
            try
            {
                appuser.id = Guid.NewGuid();
                if(IsValidMail(appuser.email))
                {
                    AppUserList.AppUsers.Add(appuser);
                    result.isSuccess = true;
                    result.Data = "{'id':" + appuser.id + "}";
                    result.Message = String.Empty;
                }
                else
                {
                    result.isSuccess = false;
                    result.Data = "{}";
                    result.Message = "Please provide a valid email address.";
                }
             
            }
            catch(Exception ex)
            {
                //save exception message to log
                result.isSuccess = false;
                result.Data = "{}";
                result.Message = "Could not save user.";
            }

            JsonResult jResult = new JsonResult(result);
            return jResult;
            //return Ok(appuser);
        }

        [HttpPost]
        public JsonResult UpdateUser(AppUser appuser)
        {
            ResponseObj result = new ResponseObj();

            try
            {   if(appuser.id == Guid.Empty)
                {
                    result.isSuccess = false;
                    result.Data = "{}";
                    result.Message = "Please provide a valid user.";
                }
                 else
                {
                    var existingUser = AppUserList.AppUsers.FirstOrDefault(u => u.id == appuser.id);
                    if (existingUser != null)
                    {
                        AppUserList.AppUsers.Remove(existingUser);
                        AppUserList.AppUsers.Add(appuser);
                        result.isSuccess = true;
                        result.Data = JsonSerializer.Serialize(appuser);
                        result.Message = String.Empty;
                    }
                    else
                    {
                        result.isSuccess = false;
                        result.Data = "{}";
                        result.Message = "User did not found.";
                    }
                }
               
            }
            catch(Exception ex)
            {
                //save exception message to log
                result.isSuccess = false;
                result.Data = "{}";
                result.Message = "Could not update user.";
            }


            JsonResult jResult = new JsonResult(result);
            return jResult;
        }

        [HttpPost]
        public JsonResult DeleteUser(Guid userId)
        {
            ResponseObj result = new ResponseObj();

            try
            {
                var existingUser = AppUserList.AppUsers.FirstOrDefault(u => u.id == userId);
                if (existingUser != null)
                {
                    AppUserList.AppUsers.Remove(existingUser);
                    result.isSuccess = true;
                    result.Data = "{}";
                    result.Message = "User deleted successfully.";
                }
                else
                {
                    result.isSuccess = false;
                    result.Data = "{}";
                    result.Message = "User did not found.";
                }
            }
            catch (Exception ex)
            {
                //save exception message to log
                result.isSuccess = false;
                result.Data = "{}";
                result.Message = "Could not delete user.";
            }


            JsonResult jResult = new JsonResult(result);
            return jResult;
        }

        [HttpGet]
        public JsonResult GetUsers(string sortingField,string email="", string phone ="")
        {
            ResponseObj result = new ResponseObj();

            List<AppUser> sorteduserList = new List<AppUser>();

            try
            {
                if(!String.IsNullOrEmpty(email) && String.IsNullOrEmpty(phone))
                {
                    sorteduserList = AppUserList.AppUsers
                                    .Where(u => u.email.Contains(email))
                                    //.OrderBy(u=>u.)
                                    .ToList();
                    //sorteduserList = sorteduserList.OrderBy(sortingField);

                }
                else if (!String.IsNullOrEmpty(phone) && String.IsNullOrEmpty(email))
                {
                    sorteduserList = AppUserList.AppUsers
                                    .Where(u => u.phone.ToString().Contains(phone))
                                    //.OrderBy(u=>u.)
                                    .ToList();
                }
                //load all data if no email or phone provided
                else if (String.IsNullOrEmpty(phone) && String.IsNullOrEmpty(email))
                {
                    sorteduserList = AppUserList.AppUsers
                                    //.Where(u => u.email.Contains(phone))
                                    //.OrderBy(u=>u.)
                                    .ToList();
                }
                else
                {
                    sorteduserList = AppUserList.AppUsers
                                   .Where(u => u.email.Contains(email) && u.phone.ToString().Contains(phone))
                                   //.OrderBy(u=>u.)
                                   .ToList();
                }
                if(!String.IsNullOrEmpty(sortingField))
                {
                    sorteduserList = GetSortedData(sorteduserList, sortingField);
                }
               
                result.isSuccess = true;
                result.Data = JsonSerializer.Serialize(sorteduserList); ;
               // result.DataList = JsonSerializer.Serialize(sorteduserList);
                result.Message = "User fetched successfully.";

            }
            catch(Exception ex)
            {
                //save exception message to log
                result.isSuccess = false;
                result.Data = "{}";
                result.Message = "Could not load data.";
            }


            JsonResult jResult = new JsonResult(result);
            return jResult;
        }

        private List<AppUser> GetSortedData(List<AppUser> userList, string orderBtyFieldName)
        {
            try
            {
                switch (orderBtyFieldName.ToLower())
                {
                    case "id":
                        userList = userList.OrderBy(u => u.id).ToList();
                        break;
                    case "name":
                        userList = userList.OrderBy(u => u.name).ToList();
                        break;
                    case "email":
                        userList = userList.OrderBy(u => u.email).ToList();
                        break;
                    case "phone":
                        userList = userList.OrderBy(u => u.phone).ToList();
                        break;
                    case "age":
                        userList = userList.OrderBy(u => u.age).ToList();
                        break;
                }
            }
            catch(Exception ex)
            {
                return userList;
            }

            return userList;
        }

        public bool IsValidMail(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
