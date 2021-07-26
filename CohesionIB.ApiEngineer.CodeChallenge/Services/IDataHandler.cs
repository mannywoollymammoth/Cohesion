using System.Collections.Generic;
using CohesionIB.ApiEngineer.CodeChallenge.Models;

namespace CohesionIB.ApiEngineer.CodeChallenge.Services
{
    public interface IDataHandler
    {
        bool addDeviceID(string username, long deviceID);
        List<long> getDeviceList(string username);
        bool getTermsAndConditionsStatus(string username);
        User getUser(string username);
        bool removeInvitationCode(string username);
        bool testIfDeviceIDRegistered(string username, long deviceID);
        bool testInvitationCode(ulong invitationCode, string username);
        bool updateInvitationCode(string username, ulong invitationCode);
        bool updateTermsAndConditions(string username);
    }
}