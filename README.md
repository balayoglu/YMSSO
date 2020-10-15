## SSO App to authenticate via YourMembership.com

- MVC app, using .Net Core, where user logs in with Username and Password
- On the backend it is passing login details and extra API keys to https://api.yourmembership.com/ REST API
- SDK from the offical link https://www.yourmembership.com/api-sdk/ has been used, however we converted it from Full Framework to the .Net Standard 2.0

## How to configure API settings?
On appsettings.json you need to configure following settings(you can get those information from YourMembership.com Admin Configuration page):
- YmApiKeyPublic
- YmApiKeySA
- YmApiSAPasscode

## How to run from VS?
Set YMSSO.MVC as a Startup Project and run.
