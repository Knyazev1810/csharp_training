﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_tests;
using NUnit.Framework;

namespace mantis_tests
{
    public class AuthTestBase : TestBase
    {
        [TestFixtureSetUp]
        public void SetupLogin()
        {
            app.Auth.Login(new AccountData("administrator", "root"));
        }
    }
}