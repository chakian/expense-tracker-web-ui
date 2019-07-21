# Expense Tracker

## Version 0.2

[![HitCount](http://hits.dwyl.io/chakian/expense-tracker-web-ui.svg)](http://hits.dwyl.io/chakian/expense-tracker-web-ui) 
[OpenHub Report](https://www.openhub.net/p/756697)

### master branch

This is the [production](https://harcama.cagdaskorkut.com) code.

[![Build status](https://ci.appveyor.com/api/projects/status/brlasjaa4a8q5e42?svg=true&branch=master)](https://ci.appveyor.com/project/chakian/expense-tracker-web-ui/branch/master)
[![Maintainability](https://api.codeclimate.com/v1/badges/b8397f345904e943f1fa/maintainability)](https://codeclimate.com/github/chakian/expense-tracker-web-ui/maintainability)
[![Coverage Status](https://coveralls.io/repos/github/chakian/expense-tracker-web-ui/badge.svg?branch=master)](https://coveralls.io/github/chakian/expense-tracker-web-ui?branch=master)
[//]: # [![Known Vulnerabilities](https://snyk.io/test/github/chakian/expense-tracker-web-ui/badge.svg)](https://snyk.io/test/github/chakian/expense-tracker-web-ui) 

-----

### develop branch

This is the code that is running on the [test environment](https://harcatest.cagdaskorkut.com). This branch may not always be stable.

[![Build status](https://ci.appveyor.com/api/projects/status/brlasjaa4a8q5e42?svg=true&branch=develop)](https://ci.appveyor.com/project/chakian/expense-tracker-web-ui/branch/develop)
[![Coverage Status](https://coveralls.io/repos/github/chakian/expense-tracker-web-ui/badge.svg?branch=develop)](https://coveralls.io/github/chakian/expense-tracker-web-ui?branch=develop)

-----

## Frequently Used Snippets

#### Generate OpenCover report

packages\OpenCover.4.7.922\tools\OpenCover.Console.exe -register:user -target:"packages\xunit.runner.console.2.4.1\tools\net472\xunit.console.x86.exe" -targetargs:"ExpenseTracker.Business.Tests\bin\Debug\ExpenseTracker.Business.Tests.dll -noshadow" -filter:"+[ExpenseTracker*]* -[ExpenseTracker.Business.Tests*]*" -output:"ExpenseTracker_opencover.xml"

#### Report Generator

packages\ReportGenerator.4.2.10\tools\net47\ReportGenerator.exe -reports:"ExpenseTracker_opencover.xml" -targetdir:"CodeCoverageReport"
