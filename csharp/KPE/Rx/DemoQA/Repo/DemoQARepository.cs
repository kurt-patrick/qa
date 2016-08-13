﻿///////////////////////////////////////////////////////////////////////////////
//
// This file was automatically generated by RANOREX.
// DO NOT MODIFY THIS FILE! It is regenerated by the designer.
// All your modifications will be lost!
// http://www.ranorex.com
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace KPE.Rx.DemoQA.Repo
{
#pragma warning disable 0436 //(CS0436) The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'. Using the type defined in 'assembly'.
    /// <summary>
    /// The class representing the DemoQARepository element repository.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "6.0")]
    [RepositoryFolder("70ce3e01-0f75-48f7-af35-3707e9258b63")]
    public partial class DemoQARepository : RepoGenBaseFolder
    {
        static DemoQARepository instance = new DemoQARepository();
        DemoQARepositoryFolders.DemoQAAppFolder _demoqa;

        /// <summary>
        /// Gets the singleton class instance representing the DemoQARepository element repository.
        /// </summary>
        [RepositoryFolder("70ce3e01-0f75-48f7-af35-3707e9258b63")]
        public static DemoQARepository Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Repository class constructor.
        /// </summary>
        public DemoQARepository() 
            : base("DemoQARepository", "/", null, 0, false, "70ce3e01-0f75-48f7-af35-3707e9258b63", ".\\RepositoryImages\\DemoQARepository70ce3e01.rximgres")
        {
            _demoqa = new DemoQARepositoryFolders.DemoQAAppFolder(this);
        }

#region Variables

        string _GenericIndex = "11";

        /// <summary>
        /// Gets or sets the value of variable GenericIndex.
        /// </summary>
        [TestVariable("22e1f591-02bb-452e-aaf3-2846a796f4fa")]
        public string GenericIndex
        {
            get { return _GenericIndex; }
            set { _GenericIndex = value; }
        }

        string _GenericKey = "";

        /// <summary>
        /// Gets or sets the value of variable GenericKey.
        /// </summary>
        [TestVariable("5dd94f2a-03dc-44f3-8f01-80a080f1e0d2")]
        public string GenericKey
        {
            get { return _GenericKey; }
            set { _GenericKey = value; }
        }

#endregion

        /// <summary>
        /// The Self item info.
        /// </summary>
        [RepositoryItemInfo("70ce3e01-0f75-48f7-af35-3707e9258b63")]
        public virtual RepoItemInfo SelfInfo
        {
            get
            {
                return _selfInfo;
            }
        }

        /// <summary>
        /// The DemoQA folder.
        /// </summary>
        [RepositoryFolder("c11f0de0-0e89-449e-889e-37fac05869ad")]
        public virtual DemoQARepositoryFolders.DemoQAAppFolder DemoQA
        {
            get { return _demoqa; }
        }
    }

    /// <summary>
    /// Inner folder classes.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "6.0")]
    public partial class DemoQARepositoryFolders
    {
        /// <summary>
        /// The DemoQAAppFolder folder.
        /// </summary>
        [RepositoryFolder("c11f0de0-0e89-449e-889e-37fac05869ad")]
        public partial class DemoQAAppFolder : RepoGenBaseFolder
        {
            DemoQARepositoryFolders.MaritalStatusFolder _maritalstatus;
            DemoQARepositoryFolders.HobbyFolder _hobby;
            DemoQARepositoryFolders.ErrorFolder _error;
            RepoItemInfo _firstnameInfo;
            RepoItemInfo _lastnameInfo;
            RepoItemInfo _phonenumberInfo;
            RepoItemInfo _usernameInfo;
            RepoItemInfo _emailInfo;
            RepoItemInfo _passwordInfo;
            RepoItemInfo _passwordconfirmInfo;
            RepoItemInfo _headermessageInfo;
            RepoItemInfo _pageheadingInfo;
            RepoItemInfo _maincontainerInfo;
            RepoItemInfo _submitInfo;
            RepoItemInfo _countryInfo;

            /// <summary>
            /// Creates a new DemoQA  folder.
            /// </summary>
            public DemoQAAppFolder(RepoGenBaseFolder parentFolder) :
                    base("DemoQA", "/dom[@visible='True' and @caption~'Demoqa']", parentFolder, 10000, null, false, "c11f0de0-0e89-449e-889e-37fac05869ad", "")
            {
                _maritalstatus = new DemoQARepositoryFolders.MaritalStatusFolder(this);
                _hobby = new DemoQARepositoryFolders.HobbyFolder(this);
                _error = new DemoQARepositoryFolders.ErrorFolder(this);
                _firstnameInfo = new RepoItemInfo(this, "Firstname", ".//inputtag[#'name_3_firstname']", 10000, null, "31e8dbc5-e622-4190-8034-68da67b3bc85");
                _lastnameInfo = new RepoItemInfo(this, "Lastname", ".//inputtag[#'name_3_lastname']", 10000, null, "434e5e88-0ad4-46d0-a0db-3d2baa9cc0c5");
                _phonenumberInfo = new RepoItemInfo(this, "PhoneNumber", ".//inputtag[#'phone_9']", 10000, null, "2d2ea8e2-6530-4f1e-94d0-c7719ff6e31b");
                _usernameInfo = new RepoItemInfo(this, "Username", ".//inputtag[#'username']", 10000, null, "3d0196bc-18fc-4285-8a09-021f43e6c381");
                _emailInfo = new RepoItemInfo(this, "Email", ".//inputtag[#'email_1']", 10000, null, "8aaf5311-66aa-4529-88fc-4a10a838f81e");
                _passwordInfo = new RepoItemInfo(this, "Password", ".//inputtag[#'password_2']", 10000, null, "aa7624d3-9af4-483a-b69f-f930bc9832c2");
                _passwordconfirmInfo = new RepoItemInfo(this, "PasswordConfirm", ".//inputtag[#'confirm_password_password_2']", 10000, null, "f6cba3e5-e664-4efd-af5f-11c6a76c1b71");
                _headermessageInfo = new RepoItemInfo(this, "HeaderMessage", ".//article/div/p", 10000, null, "47179240-ed62-46db-9d1f-fdeb5694051a");
                _pageheadingInfo = new RepoItemInfo(this, "PageHeading", ".//h1", 10000, null, "7bf1a19c-33b0-43b1-9465-c82e6eb0094a");
                _maincontainerInfo = new RepoItemInfo(this, "MainContainer", ".//div[@class~'entry-content']", 10000, null, "b32297fc-9fe4-4a5d-baa3-60d10679df56");
                _submitInfo = new RepoItemInfo(this, "Submit", ".//input[@name='pie_submit']", 10000, null, "541f4c3f-3220-45a6-9688-7bedd6cb374d");
                _countryInfo = new RepoItemInfo(this, "Country", ".//selecttag[#'dropdown_7']", 10000, null, "b9bfca11-a513-47ba-8001-354c48161ead");
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("c11f0de0-0e89-449e-889e-37fac05869ad")]
            public virtual Ranorex.WebDocument Self
            {
                get
                {
                    return _selfInfo.CreateAdapter<Ranorex.WebDocument>(true);
                }
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("c11f0de0-0e89-449e-889e-37fac05869ad")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The Firstname item.
            /// </summary>
            [RepositoryItem("31e8dbc5-e622-4190-8034-68da67b3bc85")]
            public virtual Ranorex.InputTag Firstname
            {
                get
                {
                    return _firstnameInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The Firstname item info.
            /// </summary>
            [RepositoryItemInfo("31e8dbc5-e622-4190-8034-68da67b3bc85")]
            public virtual RepoItemInfo FirstnameInfo
            {
                get
                {
                    return _firstnameInfo;
                }
            }

            /// <summary>
            /// The Lastname item.
            /// </summary>
            [RepositoryItem("434e5e88-0ad4-46d0-a0db-3d2baa9cc0c5")]
            public virtual Ranorex.InputTag Lastname
            {
                get
                {
                    return _lastnameInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The Lastname item info.
            /// </summary>
            [RepositoryItemInfo("434e5e88-0ad4-46d0-a0db-3d2baa9cc0c5")]
            public virtual RepoItemInfo LastnameInfo
            {
                get
                {
                    return _lastnameInfo;
                }
            }

            /// <summary>
            /// The PhoneNumber item.
            /// </summary>
            [RepositoryItem("2d2ea8e2-6530-4f1e-94d0-c7719ff6e31b")]
            public virtual Ranorex.InputTag PhoneNumber
            {
                get
                {
                    return _phonenumberInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The PhoneNumber item info.
            /// </summary>
            [RepositoryItemInfo("2d2ea8e2-6530-4f1e-94d0-c7719ff6e31b")]
            public virtual RepoItemInfo PhoneNumberInfo
            {
                get
                {
                    return _phonenumberInfo;
                }
            }

            /// <summary>
            /// The Username item.
            /// </summary>
            [RepositoryItem("3d0196bc-18fc-4285-8a09-021f43e6c381")]
            public virtual Ranorex.InputTag Username
            {
                get
                {
                    return _usernameInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The Username item info.
            /// </summary>
            [RepositoryItemInfo("3d0196bc-18fc-4285-8a09-021f43e6c381")]
            public virtual RepoItemInfo UsernameInfo
            {
                get
                {
                    return _usernameInfo;
                }
            }

            /// <summary>
            /// The Email item.
            /// </summary>
            [RepositoryItem("8aaf5311-66aa-4529-88fc-4a10a838f81e")]
            public virtual Ranorex.InputTag Email
            {
                get
                {
                    return _emailInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The Email item info.
            /// </summary>
            [RepositoryItemInfo("8aaf5311-66aa-4529-88fc-4a10a838f81e")]
            public virtual RepoItemInfo EmailInfo
            {
                get
                {
                    return _emailInfo;
                }
            }

            /// <summary>
            /// The Password item.
            /// </summary>
            [RepositoryItem("aa7624d3-9af4-483a-b69f-f930bc9832c2")]
            public virtual Ranorex.InputTag Password
            {
                get
                {
                    return _passwordInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The Password item info.
            /// </summary>
            [RepositoryItemInfo("aa7624d3-9af4-483a-b69f-f930bc9832c2")]
            public virtual RepoItemInfo PasswordInfo
            {
                get
                {
                    return _passwordInfo;
                }
            }

            /// <summary>
            /// The PasswordConfirm item.
            /// </summary>
            [RepositoryItem("f6cba3e5-e664-4efd-af5f-11c6a76c1b71")]
            public virtual Ranorex.InputTag PasswordConfirm
            {
                get
                {
                    return _passwordconfirmInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The PasswordConfirm item info.
            /// </summary>
            [RepositoryItemInfo("f6cba3e5-e664-4efd-af5f-11c6a76c1b71")]
            public virtual RepoItemInfo PasswordConfirmInfo
            {
                get
                {
                    return _passwordconfirmInfo;
                }
            }

            /// <summary>
            /// The HeaderMessage item.
            /// </summary>
            [RepositoryItem("47179240-ed62-46db-9d1f-fdeb5694051a")]
            public virtual Ranorex.PTag HeaderMessage
            {
                get
                {
                    return _headermessageInfo.CreateAdapter<Ranorex.PTag>(true);
                }
            }

            /// <summary>
            /// The HeaderMessage item info.
            /// </summary>
            [RepositoryItemInfo("47179240-ed62-46db-9d1f-fdeb5694051a")]
            public virtual RepoItemInfo HeaderMessageInfo
            {
                get
                {
                    return _headermessageInfo;
                }
            }

            /// <summary>
            /// The PageHeading item.
            /// </summary>
            [RepositoryItem("7bf1a19c-33b0-43b1-9465-c82e6eb0094a")]
            public virtual Ranorex.H1Tag PageHeading
            {
                get
                {
                    return _pageheadingInfo.CreateAdapter<Ranorex.H1Tag>(true);
                }
            }

            /// <summary>
            /// The PageHeading item info.
            /// </summary>
            [RepositoryItemInfo("7bf1a19c-33b0-43b1-9465-c82e6eb0094a")]
            public virtual RepoItemInfo PageHeadingInfo
            {
                get
                {
                    return _pageheadingInfo;
                }
            }

            /// <summary>
            /// The MainContainer item.
            /// </summary>
            [RepositoryItem("b32297fc-9fe4-4a5d-baa3-60d10679df56")]
            public virtual Ranorex.DivTag MainContainer
            {
                get
                {
                    return _maincontainerInfo.CreateAdapter<Ranorex.DivTag>(true);
                }
            }

            /// <summary>
            /// The MainContainer item info.
            /// </summary>
            [RepositoryItemInfo("b32297fc-9fe4-4a5d-baa3-60d10679df56")]
            public virtual RepoItemInfo MainContainerInfo
            {
                get
                {
                    return _maincontainerInfo;
                }
            }

            /// <summary>
            /// The Submit item.
            /// </summary>
            [RepositoryItem("541f4c3f-3220-45a6-9688-7bedd6cb374d")]
            public virtual Ranorex.InputTag Submit
            {
                get
                {
                    return _submitInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The Submit item info.
            /// </summary>
            [RepositoryItemInfo("541f4c3f-3220-45a6-9688-7bedd6cb374d")]
            public virtual RepoItemInfo SubmitInfo
            {
                get
                {
                    return _submitInfo;
                }
            }

            /// <summary>
            /// The Country item.
            /// </summary>
            [RepositoryItem("b9bfca11-a513-47ba-8001-354c48161ead")]
            public virtual Ranorex.SelectTag Country
            {
                get
                {
                    return _countryInfo.CreateAdapter<Ranorex.SelectTag>(true);
                }
            }

            /// <summary>
            /// The Country item info.
            /// </summary>
            [RepositoryItemInfo("b9bfca11-a513-47ba-8001-354c48161ead")]
            public virtual RepoItemInfo CountryInfo
            {
                get
                {
                    return _countryInfo;
                }
            }

            /// <summary>
            /// The MaritalStatus folder.
            /// </summary>
            [RepositoryFolder("f6faf430-438d-4162-9189-f7469d8fabe4")]
            public virtual DemoQARepositoryFolders.MaritalStatusFolder MaritalStatus
            {
                get { return _maritalstatus; }
            }

            /// <summary>
            /// The Hobby folder.
            /// </summary>
            [RepositoryFolder("e59d3e08-3cec-4a48-8da3-4e3b662b398a")]
            public virtual DemoQARepositoryFolders.HobbyFolder Hobby
            {
                get { return _hobby; }
            }

            /// <summary>
            /// The Error folder.
            /// </summary>
            [RepositoryFolder("76c82818-9495-4dfc-8a80-f3ee63ffc947")]
            public virtual DemoQARepositoryFolders.ErrorFolder Error
            {
                get { return _error; }
            }
        }

        /// <summary>
        /// The MaritalStatusFolder folder.
        /// </summary>
        [RepositoryFolder("f6faf430-438d-4162-9189-f7469d8fabe4")]
        public partial class MaritalStatusFolder : RepoGenBaseFolder
        {
            RepoItemInfo _setmaritalstatusInfo;
            RepoItemInfo _allmaritalstatusesInfo;

            /// <summary>
            /// Creates a new MaritalStatus  folder.
            /// </summary>
            public MaritalStatusFolder(RepoGenBaseFolder parentFolder) :
                    base("MaritalStatus", "", parentFolder, 0, null, false, "f6faf430-438d-4162-9189-f7469d8fabe4", "")
            {
                _setmaritalstatusInfo = new RepoItemInfo(this, "SetMaritalStatus", ".//inputtag[@value=$GenericKey]", 10000, null, "a20b725c-ff14-4610-8288-cc10bec0c0e4");
                _allmaritalstatusesInfo = new RepoItemInfo(this, "AllMaritalStatuses", ".//ul[@id='pie_register']/li[2]//input", 10000, null, "3ccfdfa2-eef1-4169-8356-1d7b21ea3a5f");
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("f6faf430-438d-4162-9189-f7469d8fabe4")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The SetMaritalStatus item.
            /// </summary>
            [RepositoryItem("a20b725c-ff14-4610-8288-cc10bec0c0e4")]
            public virtual Ranorex.InputTag SetMaritalStatus
            {
                get
                {
                    return _setmaritalstatusInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The SetMaritalStatus item info.
            /// </summary>
            [RepositoryItemInfo("a20b725c-ff14-4610-8288-cc10bec0c0e4")]
            public virtual RepoItemInfo SetMaritalStatusInfo
            {
                get
                {
                    return _setmaritalstatusInfo;
                }
            }

            /// <summary>
            /// The AllMaritalStatuses item.
            /// </summary>
            [RepositoryItem("3ccfdfa2-eef1-4169-8356-1d7b21ea3a5f")]
            public virtual Ranorex.InputTag AllMaritalStatuses
            {
                get
                {
                    return _allmaritalstatusesInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The AllMaritalStatuses item info.
            /// </summary>
            [RepositoryItemInfo("3ccfdfa2-eef1-4169-8356-1d7b21ea3a5f")]
            public virtual RepoItemInfo AllMaritalStatusesInfo
            {
                get
                {
                    return _allmaritalstatusesInfo;
                }
            }
        }

        /// <summary>
        /// The HobbyFolder folder.
        /// </summary>
        [RepositoryFolder("e59d3e08-3cec-4a48-8da3-4e3b662b398a")]
        public partial class HobbyFolder : RepoGenBaseFolder
        {
            RepoItemInfo _allhobbiesInfo;
            RepoItemInfo _singlehobbyInfo;

            /// <summary>
            /// Creates a new Hobby  folder.
            /// </summary>
            public HobbyFolder(RepoGenBaseFolder parentFolder) :
                    base("Hobby", "", parentFolder, 0, null, false, "e59d3e08-3cec-4a48-8da3-4e3b662b398a", "")
            {
                _allhobbiesInfo = new RepoItemInfo(this, "AllHobbies", ".//ul[@id='pie_register']/li[3]//input", 10000, null, "f0be0d12-5ea4-4bc3-b4d0-55fb8ef86925");
                _singlehobbyInfo = new RepoItemInfo(this, "SingleHobby", ".//ul[@id='pie_register']/li[3]//input[@value~$GenericKey]", 10000, null, "e411620f-2765-4f14-9665-609837dec402");
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("e59d3e08-3cec-4a48-8da3-4e3b662b398a")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The AllHobbies item.
            /// </summary>
            [RepositoryItem("f0be0d12-5ea4-4bc3-b4d0-55fb8ef86925")]
            public virtual Ranorex.InputTag AllHobbies
            {
                get
                {
                    return _allhobbiesInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The AllHobbies item info.
            /// </summary>
            [RepositoryItemInfo("f0be0d12-5ea4-4bc3-b4d0-55fb8ef86925")]
            public virtual RepoItemInfo AllHobbiesInfo
            {
                get
                {
                    return _allhobbiesInfo;
                }
            }

            /// <summary>
            /// The SingleHobby item.
            /// </summary>
            [RepositoryItem("e411620f-2765-4f14-9665-609837dec402")]
            public virtual Ranorex.InputTag SingleHobby
            {
                get
                {
                    return _singlehobbyInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The SingleHobby item info.
            /// </summary>
            [RepositoryItemInfo("e411620f-2765-4f14-9665-609837dec402")]
            public virtual RepoItemInfo SingleHobbyInfo
            {
                get
                {
                    return _singlehobbyInfo;
                }
            }
        }

        /// <summary>
        /// The ErrorFolder folder.
        /// </summary>
        [RepositoryFolder("76c82818-9495-4dfc-8a80-f3ee63ffc947")]
        public partial class ErrorFolder : RepoGenBaseFolder
        {
            RepoItemInfo _errordivInfo;
            RepoItemInfo _errormessageInfo;
            RepoItemInfo _genericerrordivInfo;

            /// <summary>
            /// Creates a new Error  folder.
            /// </summary>
            public ErrorFolder(RepoGenBaseFolder parentFolder) :
                    base("Error", "", parentFolder, 0, null, false, "76c82818-9495-4dfc-8a80-f3ee63ffc947", "")
            {
                _errordivInfo = new RepoItemInfo(this, "ErrorDiv", ".//form/ul/li[$GenericIndex]/div", 10000, null, "dc7e8075-f0bb-4485-af08-7a7e34088563");
                _errormessageInfo = new RepoItemInfo(this, "ErrorMessage", ".//form/ul/li[$GenericIndex]/div//span[@class~'legend error']", 10000, null, "7d633c61-3928-49a1-bc7e-deda6fbad949");
                _genericerrordivInfo = new RepoItemInfo(this, "GenericErrorDiv", ".//form/ul/li/div", 10000, null, "7efc0346-8afc-4c16-a1c3-d7128d4ce2e6");
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("76c82818-9495-4dfc-8a80-f3ee63ffc947")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The ErrorDiv item.
            /// </summary>
            [RepositoryItem("dc7e8075-f0bb-4485-af08-7a7e34088563")]
            public virtual Ranorex.DivTag ErrorDiv
            {
                get
                {
                    return _errordivInfo.CreateAdapter<Ranorex.DivTag>(true);
                }
            }

            /// <summary>
            /// The ErrorDiv item info.
            /// </summary>
            [RepositoryItemInfo("dc7e8075-f0bb-4485-af08-7a7e34088563")]
            public virtual RepoItemInfo ErrorDivInfo
            {
                get
                {
                    return _errordivInfo;
                }
            }

            /// <summary>
            /// The ErrorMessage item.
            /// </summary>
            [RepositoryItem("7d633c61-3928-49a1-bc7e-deda6fbad949")]
            public virtual Ranorex.SpanTag ErrorMessage
            {
                get
                {
                    return _errormessageInfo.CreateAdapter<Ranorex.SpanTag>(true);
                }
            }

            /// <summary>
            /// The ErrorMessage item info.
            /// </summary>
            [RepositoryItemInfo("7d633c61-3928-49a1-bc7e-deda6fbad949")]
            public virtual RepoItemInfo ErrorMessageInfo
            {
                get
                {
                    return _errormessageInfo;
                }
            }

            /// <summary>
            /// The GenericErrorDiv item.
            /// </summary>
            [RepositoryItem("7efc0346-8afc-4c16-a1c3-d7128d4ce2e6")]
            public virtual Ranorex.DivTag GenericErrorDiv
            {
                get
                {
                    return _genericerrordivInfo.CreateAdapter<Ranorex.DivTag>(true);
                }
            }

            /// <summary>
            /// The GenericErrorDiv item info.
            /// </summary>
            [RepositoryItemInfo("7efc0346-8afc-4c16-a1c3-d7128d4ce2e6")]
            public virtual RepoItemInfo GenericErrorDivInfo
            {
                get
                {
                    return _genericerrordivInfo;
                }
            }
        }

    }
#pragma warning restore 0436
}