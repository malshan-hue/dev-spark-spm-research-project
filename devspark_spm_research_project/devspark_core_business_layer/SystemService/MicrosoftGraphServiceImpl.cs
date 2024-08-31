using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_model.SystemModels;
using GSF.Security.Cryptography;
using devspark_core_model.SystemModels;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.SystemService
{
    public class MicrosoftGraphServiceImpl : IMicrosoftGraphService
    {
        private readonly GraphServiceClient _graphClientService;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public MicrosoftGraphServiceImpl(GraphServiceClient graphServiceClient, IUserService userService, IMailService mailService)
        {
            _graphClientService = graphServiceClient;
            _userService = userService;
            _mailService = mailService;
        }
        public async Task<bool> CreateUserInMicrosoftEntraId(DevSparkUser user)
        {
            try
            {
                var userDisplayname = user.FirstName.Trim() + " " + user.LastName.Trim();
                var userMailNickName = user.FirstName.Trim().ToLower() + "." + user.LastName.Trim().ToLower();
                var userPrincipalName = user.FirstName.Trim().ToLower() + "." + user.LastName.Trim().ToLower() + "@10gmzb.onmicrosoft.com";

                string pw1 = GenerateSecurePassword();
                string pw2 = GenerateSecurePassword();
                string tempPassword = pw1 + pw2;

                var newUser = new User
                {
                    AccountEnabled = true,
                    DisplayName = userDisplayname,
                    MailNickname = userMailNickName,
                    UserPrincipalName = userPrincipalName,
                    PasswordProfile = new PasswordProfile
                    {
                        ForceChangePasswordNextSignIn = true,
                        Password = tempPassword
                    }
                };

                var createdUser = await _graphClientService.Users.PostAsync(newUser);
                
                EntraIdUser entraIdUser = new EntraIdUser
                {
                    Id = createdUser.Id,
                    AccountEnabled = true,
                    DisplayName = createdUser.DisplayName,
                    GivenName = createdUser.GivenName,
                    Surname = createdUser.Surname,
                    UserPrincipalName = createdUser.UserPrincipalName,
                    MailNickname = createdUser.MailNickname,
                    Mail = createdUser.Mail,
                    MobilePhone = createdUser.MobilePhone,
                    OfficeLocation = createdUser.OfficeLocation,
                    StreetAddress = createdUser.StreetAddress,
                    City = createdUser.City,
                    State = createdUser.State,
                    Country = createdUser.Country,
                    PostalCode = createdUser.PostalCode,
                    JobTitle = createdUser.JobTitle,
                    Department = createdUser.Department,
                    CompanyName = createdUser.CompanyName
                };

                entraIdUser.DevSparkUser = new DevSparkUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PersonalEmail = user.PersonalEmail,
                    Password = tempPassword,
                    PasswordSalt = tempPassword
                };

                bool status = await _userService.InsertUser(entraIdUser);

                if (status)
                {
                    var emailbody = $"you microsoft email is: {createdUser.UserPrincipalName} and use this temporary password: {tempPassword} for your first login";
                    await _mailService.SendGoogleMail(user.PersonalEmail, "Microsoft Credentials", emailbody);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //generate random password
        private string GenerateSecurePassword()
        {
            var passwordGenerator = new PasswordGenerator();
            return passwordGenerator.GeneratePassword();
        }
    }
}
