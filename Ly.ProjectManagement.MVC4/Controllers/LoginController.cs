using Ly.ProjectManagement.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ly.ProjectManagement.Model;
using Ly.ProjectManagement.Domain.Enum;
using Ly.ProjectManagement.BLL;
using Ly.ProjectManagement.IBLL;
using Ly.ProjectManagement.MVC4.Handler;

namespace Ly.ProjectManagement.MVC4.Controllers
{
    public class LoginController : Controller
    {
        private IAccountLoginLogApp logApp;
        private IStudentApp stuApp;
        private ITeacherApp teacherApp;
        private IRoleApp roleApp;
        public LoginController(IAccountLoginLogApp logApp, IStudentApp stuApp, ITeacherApp teacherApp, IRoleApp roleApp)
        {
            this.logApp = logApp;
            this.stuApp = stuApp;
            this.teacherApp = teacherApp;
            this.roleApp = roleApp;
        }
        //
        // GET: /Login/

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult GetAuthCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }


        public ActionResult CheckLogin(int? type, string name, string pwd, string code)
        {
            AccountLoginLog logEntity = new AccountLoginLog();
            //登录日志
            logEntity.loginType = type;
            logEntity.loginIpAddress = Net.Ip;
            logEntity.loginIpAddressName = "湖南长沙";
            logEntity.loginName = name;
            logEntity.logGuid = Ly.ProjectManagement.Code.Common.GuId();
            logEntity.loginTime = DateTime.Now;

            try
            {
                if (Session["ly_session_verifycode"].IsEmpty() || Md5.md5(code.ToLower(), 16) != Session["ly_session_verifycode"].ToString())
                {
                    throw new Exception("验证码错误");
                }
                //操作人
                OperatorModel operatorModel = new OperatorModel();
                operatorModel.LoginIPAddress = logEntity.loginIpAddress;
                operatorModel.LoginIPAddressName = logEntity.loginIpAddressName;
                operatorModel.LoginTime = DateTime.Now;
                operatorModel.LoginToken = DESEncrypt.Encrypt(Guid.NewGuid().ToString());

                if (type == 0)
                {
                    Student stuEntity = stuApp.CheckLogin(name, pwd);
                    operatorModel.UserGuid = stuEntity.stuGuid;
                    operatorModel.IsSystem = false;
                    operatorModel.RoleGuid = stuEntity.roleGuid;
                    operatorModel.UserCode = stuEntity.stuNo;
                    operatorModel.UserPwd = stuEntity.stuPwd;
                    operatorModel.UserName = stuEntity.stuName;
                }
                else
                {
                    Teacher teacherEntity = teacherApp.CheckLogin(name, pwd);
                    teacherEntity.Role = roleApp.FindEntity<Role>(r => r.roleGuid == teacherEntity.roleGuid);
                    operatorModel.UserGuid = teacherEntity.teacherGuid;
                    operatorModel.RoleGuid = teacherEntity.roleGuid;
                    operatorModel.IsSystem = (teacherEntity.Role.roleName == "管理员" || teacherEntity.Role.roleName == "超级管理员") ? true : false;
                    operatorModel.UserCode = teacherEntity.jobNumber;
                    operatorModel.UserPwd = teacherEntity.teacherPwd;
                    operatorModel.UserName = teacherEntity.teacherName;
                }
                OperatorProvider.Provider.AddCurrent(operatorModel);
                logEntity.userGuid = operatorModel.UserGuid;
                logEntity.loginResult = "true";
                logEntity.loginDescription = "登录成功";
                logApp.Insert<AccountLoginLog>(logEntity);
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功" }.ToJson());
            }
            catch (Exception ex)
            {
                logEntity.loginResult = "false";
                logEntity.loginDescription = "登录失败 - " + ex.Message.ToString();
                logApp.Insert<AccountLoginLog>(logEntity);
                return Content(new AjaxResult
                {
                    state = ResultType.error.ToString(),
                    message = ex.Message
                }.ToJson());
            }

        }
    }
}
