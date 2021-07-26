using System;
using System.Collections.Generic;
using System.Linq;
using CohesionIB.ApiEngineer.CodeChallenge.DAL;
using CohesionIB.ApiEngineer.CodeChallenge.Models;

namespace CohesionIB.ApiEngineer.CodeChallenge.Services
{
    public class DataHandler : IDataHandler
    {
        IDataAccessLayer _dataAccessLayer;
        public DataHandler(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        /// <summary>
        /// gets the terms and conditions status for a user 
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <returns></returns>
        public bool getTermsAndConditionsStatus(string username)
        {
            try
            {
                UserList userList = _dataAccessLayer.getUserList();
                bool termsAndConditions = userList.users.SingleOrDefault(x => x.UserName == username).TermsAndConditions;
                return termsAndConditions;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                throw;
            }

        }

        /// <summary>
        /// updates the terms and conditions field for a user 
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <returns></returns>
        public bool updateTermsAndConditions(string username)
        {
            try
            {
                UserList userList = _dataAccessLayer.getUserList();
                userList.users.SingleOrDefault(x => x.UserName == username).TermsAndConditions = true;
                _dataAccessLayer.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
                throw;
            }

        }

        /// <summary>
        /// gets a object of all user data  
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <returns></returns>
        public User getUser(string username)
        {
            try
            {
                UserList userList = _dataAccessLayer.getUserList();
                User user = userList.users.SingleOrDefault(x => x.UserName == username);
                return user;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
                throw;
            }

        }

        /// <summary>
        /// updates the old invitation code for a user 
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <param name="invitationCode">the new invitation code for a user</param>
        /// <returns></returns>       
        public bool updateInvitationCode(string username, ulong invitationCode)
        {
            try
            {
                UserList userList = _dataAccessLayer.getUserList();
                userList.users.SingleOrDefault(x => x.UserName == username).InvitationCode = invitationCode;
                _dataAccessLayer.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
                throw;
            }

        }

        /// <summary>
        /// tests if a invitation code pertains to a user 
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <param name="invitationCode">the current invitation code of a user</param>
        /// <returns></returns>
        public bool testInvitationCode(ulong invitationCode, string username)
        {
            try
            {
                var user = this.getUser(username);
                //we test for zero in this case because that is a reserved value 
                //for when a inviation code is deleted
                if (user.InvitationCode == invitationCode & invitationCode != 0)
                    return true;
                return false;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
                throw;
            }

        }

        /// <summary>
        /// removes a invitation code by setting it to 0 for a user 
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <param name="deviceID">the ID of the device</param>
        /// <returns></returns>
        public bool removeInvitationCode(string username)
        {
            try
            {
                UserList userList = _dataAccessLayer.getUserList();
                userList.users.SingleOrDefault(x => x.UserName == username).InvitationCode = 0;
                _dataAccessLayer.SaveChanges();
                return true;

            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// tests if a particular Device has been registered for a specific user 
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <param name="deviceID">the ID of the device</param>
        /// <returns></returns>
        public bool testIfDeviceIDRegistered(string username, long deviceID)
        {
            try
            {
                User user = this.getUser(username);
                if (user.DeviceID.Contains(deviceID))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// adds a device to the list for a particular user 
        /// </summary>
        /// <param name="username">the username of the user requesting their devicelist</param>
        /// <param name="deviceID">the ID of the device to be added</param>
        /// <returns></returns>
        public bool addDeviceID(string username, long deviceID)
        {

            try
            {
                UserList userList = _dataAccessLayer.getUserList();
                if (userList.users.SingleOrDefault(x => x.UserName == username).DeviceID == null)
                    userList.users.SingleOrDefault(x => x.UserName == username).DeviceID = new List<long>();
                userList.users.SingleOrDefault(x => x.UserName == username).DeviceID.Add(deviceID);
                _dataAccessLayer.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
                throw;
            }

        }

        /// <summary>
        /// gets the device list for a particular user 
        /// </summary>
        /// <param name="username">the username of the user requesting their devicelist</param>
        /// <returns></returns>
        public List<long> getDeviceList(string username)
        {
            try
            {
                UserList userList = _dataAccessLayer.getUserList();
                var deviceList = userList.users.SingleOrDefault(x => x.UserName == username).DeviceID;
                return deviceList;

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                return new List<long>();
            }
        }

    }
}