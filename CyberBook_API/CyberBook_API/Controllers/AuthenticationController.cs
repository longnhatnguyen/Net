using CyberBook_API.Dal.Repositories;
using CyberBook_API.Interfaces;
using CyberBook_API.Models;
using CyberBook_API.ViewModel.AccountView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CyberBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountsRepository _accountRepository = new AccountsRepository();
        private readonly IUsersRepository _usersRepository = new UsersRepository();

        private readonly JWTSettings _jwtsettings;
        public AuthenticationController(IOptions<JWTSettings> jwtsettings)
        {
            _jwtsettings = jwtsettings.Value;
        }

        /// <summary>
        /// Get Login and token 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] Account account)
        {
            var output = new ServiceResponse<AccountViewModel>();
            var accountView = new AccountViewModel();
            output.Data = null;
            output.Success = false;
            try
            {
                if (_accountRepository.CheckNull(account.Username, account.Password))
                {
                    string pass = account.Password;
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    UTF8Encoding utf8 = new UTF8Encoding();
                    byte[] data = md5.ComputeHash(utf8.GetBytes(pass));
                    var passHasConvert = Convert.ToBase64String(data);

                    //var acc = (await _accountRepository.FindBy(x => x.Username.Equals(account.Username) && x.Password.Equals(passHasConvert))).FirstOrDefault();
                    var acc = await _accountRepository.CheckLogin(account.Username, passHasConvert);
                    if (acc != null)
                    {
                        //var user = (await _usersRepository.FindBy(x => x.AccountID == acc.Id)).FirstOrDefault();
                        var user = await _usersRepository.GetUserByAccountID(acc.Id);
                        if (user != null)
                        {
                            accountView.Username = account.Username;
                            accountView.Password = null;
                            accountView.Id = user.Id;
                            accountView.Fullname = user.Fullname;
                            accountView.Address = user.Address;
                            accountView.Email = user.Email;
                            accountView.PhoneNumber = user.PhoneNumber;
                            accountView.Bio = user.Bio;
                            accountView.RoleId = user.RoleId;
                            accountView.Image = user.Image;
                            accountView.Dob = user.Dob;
                            accountView.RatingPoint = user.RatingPoint;
                            accountView.AccountID = user.AccountID;

                            //create New Token
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
                            var tokenDescription = new SecurityTokenDescriptor
                            {
                                //add user session
                                Subject = new ClaimsIdentity(new Claim[] {
                                    new Claim(ClaimTypes.Name, accountView.Username)
                                }),
                                Expires = DateTime.Now.AddMinutes(30),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                SecurityAlgorithms.HmacSha256Signature)
                            };
                            var token = tokenHandler.CreateToken(tokenDescription);
                            accountView.Token = tokenHandler.WriteToken(token);


                            output.Message = "Success";
                            output.Success = true;
                            output.Data = accountView;
                            return Ok(output);
                        }
                        output.Message = "User not exist";
                        return Ok(output);
                    }
                    output.Message = "username or password was wrong";
                    return Ok(output);
                }
                output.Message = "Username or password not allow empty";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "Login Exception";
            }
            return Ok(output);
        }

        [HttpPost("Reigster")]
        public async Task<ActionResult> Reigster([FromBody] AccountViewModel accountIn)
        {
            var output = new ServiceResponse<AccountViewModel>();
            output.Data = null;
            output.Success = false;

            try
            {
                if (_accountRepository.CheckNull(accountIn.Username, accountIn.Password))
                {
                    //var a = (await _accountRepository.FindBy(x => x.Username.Equals(accountIn.Username))).FirstOrDefault();
                    var a = await _accountRepository.GetAccountByUsername(accountIn.Username);
                    if (a == null)
                    {
                        //var u = (await _usersRepository.FindBy(x => x.Email.Equals(accountIn.Email) || x.PhoneNumber.Equals(accountIn.PhoneNumber))).FirstOrDefault();
                        var u = await _usersRepository.GetUserByEmailAndPhone(accountIn.Email, accountIn.PhoneNumber);
                        if (u == null)
                        {
                            var account = new Account
                            {
                                Username = accountIn.Username,
                                Password = accountIn.Password
                            };
                            var user = new User
                            {
                                Fullname = accountIn.Fullname,
                                Email = accountIn.Email,
                                PhoneNumber = accountIn.PhoneNumber,
                                Dob = accountIn.Dob
                            };

                            //hash password to md5
                            string pass = account.Password;
                            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                            UTF8Encoding utf8 = new UTF8Encoding();
                            byte[] data = md5.ComputeHash(utf8.GetBytes(pass));
                            var passHasConvert = Convert.ToBase64String(data);
                            account.Password = passHasConvert;

                            //Create new account
                            var newacc = await _accountRepository.Create(account);
                            if (newacc != null)
                            {
                                user.AccountID = newacc.Id;
                                //Create New User
                                var newUser = await _usersRepository.Create(user);
                                //int rsCreateUser = await _usersRepository.Save();
                                if (newUser != null)
                                {
                                    var accountView = new AccountViewModel
                                    {
                                        Username = newacc.Username,
                                        Password = null,
                                        Id = newUser.Id,
                                        Fullname = newUser.Fullname,
                                        Address = newUser.Address,
                                        Email = newUser.Email,
                                        PhoneNumber = newUser.PhoneNumber,
                                        Bio = newUser.Bio,
                                        RoleId = newUser.RoleId,
                                        Image = newUser.Image,
                                        Dob = newUser.Dob,
                                        RatingPoint = newUser.RatingPoint,
                                        AccountID = newUser.AccountID
                                    };
                                    output.Data = accountView;
                                    output.Message = "Register OK";
                                    output.Success = true;
                                    return Ok(output);
                                }
                                await _accountRepository.Delete(account);
                                await _accountRepository.Save();
                                output.Message = "Cannot create new user";
                                return Ok(output);
                            }
                            output.Message = "Cannot create new Account";
                            return Ok(output);
                        }
                        output.Message = "email or phone number has exist";
                        return Ok(output);
                    }
                    output.Message = "username has exist";
                    return Ok(output);
                }
                output.Message = "Username or password not allow empty";
                return Ok(output);
            }
            catch (Exception)
            {
                output.Message = "Register Exception";
            }
            return Ok(output);
        }


        [HttpPost("SendCodeResetPassword")]
        public async Task<string> SendCodeResetPassword([FromBody] User user)
        {

            Random rd = new Random();
            var code = rd.Next(100000, 999999);

            //var addCode = (await _usersRepository.FindBy(x => x.Id == user.Id)).FirstOrDefault();
            var addCode = await _usersRepository.GetUserByUserID(user.Id);

            if (addCode != null)
            {
                addCode.ComfirmPassword = code.ToString();

                var rs = await _usersRepository.Update(addCode, addCode.Id);

            }

            MailMessage message = new MailMessage("phungduclinh.99@gmail.com", addCode.Email, code.ToString(), "Reset pass for user");
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            message.ReplyToList.Add(new MailAddress("phungduclinh.99@gmail.com"));
            message.Sender = new MailAddress("phungduclinh.99@gmail.com");

            using var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("phungduclinh.99@gmail.com", "talachum12");

            try
            {
                await smtpClient.SendMailAsync(message);
                return "Success";
            }
            catch
            {
                return "Send mail failed";
            }
        }

        [HttpPost("ConfirmPass")]
        public async Task<string> ConfirmPass([FromBody] User user, string code)
        {
            var output = new ServiceResponse<User>();

            //var confirm = (await _usersRepository.FindBy(x => x.Id == user.Id)).FirstOrDefault();
            if (await _usersRepository.CheckComfirmPass(user.Id, code))
            {
                return "verified";
            }
            else
            {
                return "wrong code";
            }

        }

        [HttpPost("SetPassword")]
        public async Task<string> SetPassword([FromBody] Account account, string pass)
        {
            //var account1 = (await _accountRepository.FindBy(x => x.Id == account.Id)).FirstOrDefault();
            var account1 = await _accountRepository.GetAccountByID(account.Id);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] data = md5.ComputeHash(utf8.GetBytes(pass));

            var passToData = Convert.ToBase64String(data);

            if (account1 != null)
            {
                account1.Password = passToData.ToString();
                var rs = await _accountRepository.Update(account1, account1.Id);
                if (rs != -1)
                {
                    return "Set Password OK ";
                }
            }
            return "Set Password FAIL ";
        }

        [HttpDelete("Logout")]
        public async Task<ActionResult> Logout()
        {
            //revoke Token Access
            return Ok();
        }
    }
}
