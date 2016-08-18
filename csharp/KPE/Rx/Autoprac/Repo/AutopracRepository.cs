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

namespace KPE.Rx.Autoprac.Repo
{
#pragma warning disable 0436 //(CS0436) The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly'. Using the type defined in 'assembly'.
    /// <summary>
    /// The class representing the AutopracRepository element repository.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "6.0")]
    [RepositoryFolder("3c2a055a-7ff1-4b7e-bbe3-45ec17ee02fe")]
    public partial class AutopracRepository : RepoGenBaseFolder
    {
        static AutopracRepository instance = new AutopracRepository();
        AutopracRepositoryFolders.AutopracUXAppFolder _autopracux;

        /// <summary>
        /// Gets the singleton class instance representing the AutopracRepository element repository.
        /// </summary>
        [RepositoryFolder("3c2a055a-7ff1-4b7e-bbe3-45ec17ee02fe")]
        public static AutopracRepository Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Repository class constructor.
        /// </summary>
        public AutopracRepository() 
            : base("AutopracRepository", "/", null, 0, false, "3c2a055a-7ff1-4b7e-bbe3-45ec17ee02fe", ".\\RepositoryImages\\AutopracRepository3c2a055a.rximgres")
        {
            _autopracux = new AutopracRepositoryFolders.AutopracUXAppFolder(this);
        }

#region Variables

        string _GenericKey = "";

        /// <summary>
        /// Gets or sets the value of variable GenericKey.
        /// </summary>
        [TestVariable("c0947f58-7eea-4be5-a83c-779b454133c1")]
        public string GenericKey
        {
            get { return _GenericKey; }
            set { _GenericKey = value; }
        }

        string _GenericIndex = "";

        /// <summary>
        /// Gets or sets the value of variable GenericIndex.
        /// </summary>
        [TestVariable("eac92287-644e-4298-9ed9-35153774e984")]
        public string GenericIndex
        {
            get { return _GenericIndex; }
            set { _GenericIndex = value; }
        }

        string _GenericClass = "";

        /// <summary>
        /// Gets or sets the value of variable GenericClass.
        /// </summary>
        [TestVariable("05365af5-844d-4412-8e47-47ac91924401")]
        public string GenericClass
        {
            get { return _GenericClass; }
            set { _GenericClass = value; }
        }

        string _GenericId = "";

        /// <summary>
        /// Gets or sets the value of variable GenericId.
        /// </summary>
        [TestVariable("cfa233de-da11-4ed5-b74d-d05b665285eb")]
        public string GenericId
        {
            get { return _GenericId; }
            set { _GenericId = value; }
        }

#endregion

        /// <summary>
        /// The Self item info.
        /// </summary>
        [RepositoryItemInfo("3c2a055a-7ff1-4b7e-bbe3-45ec17ee02fe")]
        public virtual RepoItemInfo SelfInfo
        {
            get
            {
                return _selfInfo;
            }
        }

        /// <summary>
        /// The AutopracUX folder.
        /// </summary>
        [RepositoryFolder("1c30666c-6877-49de-96bf-80fb025f3336")]
        public virtual AutopracRepositoryFolders.AutopracUXAppFolder AutopracUX
        {
            get { return _autopracux; }
        }
    }

    /// <summary>
    /// Inner folder classes.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "6.0")]
    public partial class AutopracRepositoryFolders
    {
        /// <summary>
        /// The AutopracUXAppFolder folder.
        /// </summary>
        [RepositoryFolder("1c30666c-6877-49de-96bf-80fb025f3336")]
        public partial class AutopracUXAppFolder : RepoGenBaseFolder
        {
            AutopracRepositoryFolders.HeaderFolder _header;
            AutopracRepositoryFolders.BodyFolder _body;
            AutopracRepositoryFolders.FooterFolder _footer;

            /// <summary>
            /// Creates a new AutopracUX  folder.
            /// </summary>
            public AutopracUXAppFolder(RepoGenBaseFolder parentFolder) :
                    base("AutopracUX", "/dom[@domain~'automationpractice.com']", parentFolder, 10000, null, false, "1c30666c-6877-49de-96bf-80fb025f3336", "")
            {
                _header = new AutopracRepositoryFolders.HeaderFolder(this);
                _body = new AutopracRepositoryFolders.BodyFolder(this);
                _footer = new AutopracRepositoryFolders.FooterFolder(this);
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("1c30666c-6877-49de-96bf-80fb025f3336")]
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
            [RepositoryItemInfo("1c30666c-6877-49de-96bf-80fb025f3336")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The Header folder.
            /// </summary>
            [RepositoryFolder("fc86574a-cae0-4299-8c7d-eaeb4da63172")]
            public virtual AutopracRepositoryFolders.HeaderFolder Header
            {
                get { return _header; }
            }

            /// <summary>
            /// The Body folder.
            /// </summary>
            [RepositoryFolder("a9d7d45f-7689-45e7-b68c-7d6819adb0da")]
            public virtual AutopracRepositoryFolders.BodyFolder Body
            {
                get { return _body; }
            }

            /// <summary>
            /// The Footer folder.
            /// </summary>
            [RepositoryFolder("3d492ff4-cce0-46aa-9c61-809ef8bac183")]
            public virtual AutopracRepositoryFolders.FooterFolder Footer
            {
                get { return _footer; }
            }
        }

        /// <summary>
        /// The HeaderFolder folder.
        /// </summary>
        [RepositoryFolder("fc86574a-cae0-4299-8c7d-eaeb4da63172")]
        public partial class HeaderFolder : RepoGenBaseFolder
        {
            RepoItemInfo _loginInfo;
            RepoItemInfo _logoutInfo;

            /// <summary>
            /// Creates a new Header  folder.
            /// </summary>
            public HeaderFolder(RepoGenBaseFolder parentFolder) :
                    base("Header", ".//div[@class='header-container']", parentFolder, 10000, null, false, "fc86574a-cae0-4299-8c7d-eaeb4da63172", "")
            {
                _loginInfo = new RepoItemInfo(this, "Login", ".//a[@class='login']", 10000, null, "4f003e54-995a-458e-98cb-5384a89f4724");
                _logoutInfo = new RepoItemInfo(this, "Logout", ".//a[@class='logout']", 10000, null, "a5bd36a6-1b12-416e-b7a3-b629c62668f1");
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("fc86574a-cae0-4299-8c7d-eaeb4da63172")]
            public virtual Ranorex.DivTag Self
            {
                get
                {
                    return _selfInfo.CreateAdapter<Ranorex.DivTag>(true);
                }
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("fc86574a-cae0-4299-8c7d-eaeb4da63172")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The Login item.
            /// </summary>
            [RepositoryItem("4f003e54-995a-458e-98cb-5384a89f4724")]
            public virtual Ranorex.ATag Login
            {
                get
                {
                    return _loginInfo.CreateAdapter<Ranorex.ATag>(true);
                }
            }

            /// <summary>
            /// The Login item info.
            /// </summary>
            [RepositoryItemInfo("4f003e54-995a-458e-98cb-5384a89f4724")]
            public virtual RepoItemInfo LoginInfo
            {
                get
                {
                    return _loginInfo;
                }
            }

            /// <summary>
            /// The Logout item.
            /// </summary>
            [RepositoryItem("a5bd36a6-1b12-416e-b7a3-b629c62668f1")]
            public virtual Ranorex.ATag Logout
            {
                get
                {
                    return _logoutInfo.CreateAdapter<Ranorex.ATag>(true);
                }
            }

            /// <summary>
            /// The Logout item info.
            /// </summary>
            [RepositoryItemInfo("a5bd36a6-1b12-416e-b7a3-b629c62668f1")]
            public virtual RepoItemInfo LogoutInfo
            {
                get
                {
                    return _logoutInfo;
                }
            }
        }

        /// <summary>
        /// The BodyFolder folder.
        /// </summary>
        [RepositoryFolder("a9d7d45f-7689-45e7-b68c-7d6819adb0da")]
        public partial class BodyFolder : RepoGenBaseFolder
        {
            AutopracRepositoryFolders.HomePageFolder _homepage;

            /// <summary>
            /// Creates a new Body  folder.
            /// </summary>
            public BodyFolder(RepoGenBaseFolder parentFolder) :
                    base("Body", ".//div[@class='columns-container']", parentFolder, 10000, null, false, "a9d7d45f-7689-45e7-b68c-7d6819adb0da", "")
            {
                _homepage = new AutopracRepositoryFolders.HomePageFolder(this);
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("a9d7d45f-7689-45e7-b68c-7d6819adb0da")]
            public virtual Ranorex.DivTag Self
            {
                get
                {
                    return _selfInfo.CreateAdapter<Ranorex.DivTag>(true);
                }
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("a9d7d45f-7689-45e7-b68c-7d6819adb0da")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The HomePage folder.
            /// </summary>
            [RepositoryFolder("683d91e1-0eab-434c-a535-8bdb791c885a")]
            public virtual AutopracRepositoryFolders.HomePageFolder HomePage
            {
                get { return _homepage; }
            }
        }

        /// <summary>
        /// The HomePageFolder folder.
        /// </summary>
        [RepositoryFolder("683d91e1-0eab-434c-a535-8bdb791c885a")]
        public partial class HomePageFolder : RepoGenBaseFolder
        {
            AutopracRepositoryFolders.PopularAndBestSellersFolder _popularandbestsellers;

            /// <summary>
            /// Creates a new HomePage  folder.
            /// </summary>
            public HomePageFolder(RepoGenBaseFolder parentFolder) :
                    base("HomePage", "", parentFolder, 0, null, false, "683d91e1-0eab-434c-a535-8bdb791c885a", "")
            {
                _popularandbestsellers = new AutopracRepositoryFolders.PopularAndBestSellersFolder(this);
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("683d91e1-0eab-434c-a535-8bdb791c885a")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The PopularAndBestSellers folder.
            /// </summary>
            [RepositoryFolder("a9d56cd9-c7eb-4a19-a8ab-a197a3a4358e")]
            public virtual AutopracRepositoryFolders.PopularAndBestSellersFolder PopularAndBestSellers
            {
                get { return _popularandbestsellers; }
            }
        }

        /// <summary>
        /// The PopularAndBestSellersFolder folder.
        /// </summary>
        [RepositoryFolder("a9d56cd9-c7eb-4a19-a8ab-a197a3a4358e")]
        public partial class PopularAndBestSellersFolder : RepoGenBaseFolder
        {
            AutopracRepositoryFolders.ActiveProductFolder _activeproduct;
            RepoItemInfo _activetabInfo;
            RepoItemInfo _tabInfo;
            RepoItemInfo _activeproductsInfo;

            /// <summary>
            /// Creates a new PopularAndBestSellers  folder.
            /// </summary>
            public PopularAndBestSellersFolder(RepoGenBaseFolder parentFolder) :
                    base("PopularAndBestSellers", "", parentFolder, 0, null, false, "a9d56cd9-c7eb-4a19-a8ab-a197a3a4358e", "")
            {
                _activeproduct = new AutopracRepositoryFolders.ActiveProductFolder(this);
                _activetabInfo = new RepoItemInfo(this, "ActiveTab", ".//ul[@id='home-page-tabs']/li[@class='active']/a", 10000, null, "782d2818-4d11-49db-ad5b-9c629ddca066");
                _tabInfo = new RepoItemInfo(this, "Tab", ".//a[@class=$GenericClass]", 10000, null, "e7c99c53-3652-44d5-b2f5-ecbcd7b594ef");
                _activeproductsInfo = new RepoItemInfo(this, "ActiveProducts", ".//div[@class='tab-content']/ul[@id=$GenericId]/li", 10000, null, "ed324fe0-b0f2-4fb6-a54d-4597833d3536");
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("a9d56cd9-c7eb-4a19-a8ab-a197a3a4358e")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The ActiveTab item.
            /// </summary>
            [RepositoryItem("782d2818-4d11-49db-ad5b-9c629ddca066")]
            public virtual Ranorex.ATag ActiveTab
            {
                get
                {
                    return _activetabInfo.CreateAdapter<Ranorex.ATag>(true);
                }
            }

            /// <summary>
            /// The ActiveTab item info.
            /// </summary>
            [RepositoryItemInfo("782d2818-4d11-49db-ad5b-9c629ddca066")]
            public virtual RepoItemInfo ActiveTabInfo
            {
                get
                {
                    return _activetabInfo;
                }
            }

            /// <summary>
            /// The Tab item.
            /// </summary>
            [RepositoryItem("e7c99c53-3652-44d5-b2f5-ecbcd7b594ef")]
            public virtual Ranorex.ATag Tab
            {
                get
                {
                    return _tabInfo.CreateAdapter<Ranorex.ATag>(true);
                }
            }

            /// <summary>
            /// The Tab item info.
            /// </summary>
            [RepositoryItemInfo("e7c99c53-3652-44d5-b2f5-ecbcd7b594ef")]
            public virtual RepoItemInfo TabInfo
            {
                get
                {
                    return _tabInfo;
                }
            }

            /// <summary>
            /// The ActiveProducts item.
            /// </summary>
            [RepositoryItem("ed324fe0-b0f2-4fb6-a54d-4597833d3536")]
            public virtual Ranorex.LiTag ActiveProducts
            {
                get
                {
                    return _activeproductsInfo.CreateAdapter<Ranorex.LiTag>(true);
                }
            }

            /// <summary>
            /// The ActiveProducts item info.
            /// </summary>
            [RepositoryItemInfo("ed324fe0-b0f2-4fb6-a54d-4597833d3536")]
            public virtual RepoItemInfo ActiveProductsInfo
            {
                get
                {
                    return _activeproductsInfo;
                }
            }

            /// <summary>
            /// The ActiveProduct folder.
            /// </summary>
            [RepositoryFolder("9c514df8-a67d-47f7-8f3e-9c18d7206435")]
            public virtual AutopracRepositoryFolders.ActiveProductFolder ActiveProduct
            {
                get { return _activeproduct; }
            }
        }

        /// <summary>
        /// The ActiveProductFolder folder.
        /// </summary>
        [RepositoryFolder("9c514df8-a67d-47f7-8f3e-9c18d7206435")]
        public partial class ActiveProductFolder : RepoGenBaseFolder
        {

            /// <summary>
            /// Creates a new ActiveProduct  folder.
            /// </summary>
            public ActiveProductFolder(RepoGenBaseFolder parentFolder) :
                    base("ActiveProduct", ".//div[@class='tab-content']/ul[@id=$GenericId]/li[$GenericIndex]", parentFolder, 10000, null, false, "9c514df8-a67d-47f7-8f3e-9c18d7206435", "")
            {
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("9c514df8-a67d-47f7-8f3e-9c18d7206435")]
            public virtual Ranorex.LiTag Self
            {
                get
                {
                    return _selfInfo.CreateAdapter<Ranorex.LiTag>(true);
                }
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("9c514df8-a67d-47f7-8f3e-9c18d7206435")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }
        }

        /// <summary>
        /// The FooterFolder folder.
        /// </summary>
        [RepositoryFolder("3d492ff4-cce0-46aa-9c61-809ef8bac183")]
        public partial class FooterFolder : RepoGenBaseFolder
        {

            /// <summary>
            /// Creates a new Footer  folder.
            /// </summary>
            public FooterFolder(RepoGenBaseFolder parentFolder) :
                    base("Footer", ".//div[@class='footer-container']", parentFolder, 10000, null, false, "3d492ff4-cce0-46aa-9c61-809ef8bac183", "")
            {
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("3d492ff4-cce0-46aa-9c61-809ef8bac183")]
            public virtual Ranorex.DivTag Self
            {
                get
                {
                    return _selfInfo.CreateAdapter<Ranorex.DivTag>(true);
                }
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("3d492ff4-cce0-46aa-9c61-809ef8bac183")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }
        }

    }
#pragma warning restore 0436
}