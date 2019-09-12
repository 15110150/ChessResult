using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;
using ECOM.Web.Filters;
using ECOM.Models;
using ECOM.Services;
using ECOM.Core.Interfaces;
using ECOM.Infrastructure.ECOMException;
using ECOM.Web.Utilities.App;
using System.Configuration;
using ECOM.Infrastructure.Utilities;
using ECOM.Resources;

namespace ECOM.Web.Controllers {
        
    public class AccountController : Controller {
        private readonly IECOMMembershipService _ecomMembershipService;
        private readonly IECOMUserService _ecomUserService;
        private readonly IECOMRoleService _ecomRoleService;
        private readonly IOACPartnerProfileService _oacPartnerProfileService;
        private readonly IECOMPartnerService _ecomPartnerService;
        public AccountController(IUnitOfWorkAsync unitOfWorkAsync,
            IECOMMembershipService ecomMembershipService,
            IECOMUserService ecomUserService,
            IECOMRoleService ecomRoleService,
            IOACPartnerProfileService oacPartnerProfileService,
            IECOMPartnerService ecomPartnerService)
        {
            this._ecomMembershipService = ecomMembershipService;
            this._ecomUserService = ecomUserService;
            this._ecomRoleService = ecomRoleService;
            this._oacPartnerProfileService = oacPartnerProfileService;
            this._ecomPartnerService = ecomPartnerService;
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login() {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ECOMUserModel_LoginGUI model, string returnUrl) {

            if (CheckLogin(model.UserName.Trim(), model.Password.Trim(), model.RememberMe))
            {
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private bool CheckLogin(string userName, string password, bool rememberMe = true)
        {
            bool result = false;
            int branchID = 0, agentID = 0, subAgentID = 0, vendorID = 0, carrierID = 0;
            int partnerID = 0, profileType = 0, parentID = 0, customerID = 0;
            string partnerCode = "", partnerName = "" ;
            try
            {              
                var userMembership = _ecomMembershipService.Login(userName, password);
                if (userMembership != null)
                {                    
                    var userInfo = _ecomUserService.FindByUserName(userName);
                    var roleInfo = _ecomRoleService.FindByRoleID(userInfo.RoleID);
                    var ePartnerInfo = _ecomPartnerService.FindByPartnerID_Login(userInfo.PartnerID);

                    if (ePartnerInfo != null)
                    {
                        parentID = ePartnerInfo.PartnerID;
                        partnerID = ePartnerInfo.PartnerID;
                        branchID = ePartnerInfo.BranchID;
                        agentID = ePartnerInfo.AgentID;
                        subAgentID = ePartnerInfo.SubAgentID;
                        if (userInfo.RoleID == (int)EnumUtility.ROLE.customer)
                            customerID = ePartnerInfo.PartnerID;
                        else
                            customerID = 0;
                        partnerCode = ePartnerInfo.PartnerCode;
                        partnerName = ePartnerInfo.PartnerName;
                    }
                    var modelUser = new ECOMUserModel_Login()
                    {
                        UserName = userMembership.UserName,
                        FullName = userInfo.FullName,
                        UserID = userInfo.UserID,
                        RoleID = roleInfo.RoleID,
                        RoleName = roleInfo.RoleName,
                        PartnerID = partnerID,
                        PartnerCode = partnerCode,
                        PartnerName = partnerName,
                        AddressID = userInfo.AddressID,
                        Email= userMembership.Email,
                        ProfileType = profileType,
                        OfficePartnerID = 1,
                        OfficeUserID = 1,
                        ParentID = parentID,
                        BranchID = branchID,
                        AgentID = agentID,
                        SubAgentID = subAgentID,
                        VendorID = vendorID,
                        CarrierID = carrierID,
                        CustomerID = customerID
                    };
                    HttpSession.SetSession(modelUser);
                }
                
                
                if (rememberMe)
                {
                    int CookieExpires = int.Parse(ConfigurationManager.AppSettings["CookieExpires"]);
                    var ticket = new FormsAuthenticationTicket(2015, userName.Trim(), DateTime.Now, DateTime.Now.AddDays(CookieExpires), true, password.Trim());
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(CommonFunction.DaikinInvRptCookieName, encryptedTicket);
                    cookie.Expires = DateTime.Now.AddDays(CookieExpires);
                    Response.Cookies.Add(cookie);
                }
                FormsAuthentication.SetAuthCookie(userName, true);
                result = true;

            }
            catch (ECOMException ex)
            {
                ModelState.AddModelError("", ex.Message);
                //Log here
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ECOM.Resources.ECOMResource.User_LoginFailed);
                //Log here
            }
            return result;

        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff() {
            //Session.Clear();
            //Session.Abandon();
            //FormsAuthentication.SignOut();
            //return RedirectToAction("Index", "Home");
            
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetNoStore();

            FormsAuthentication.SignOut();
                        
            return RedirectToAction("Login", "Account");
        }

     

 
    }
}