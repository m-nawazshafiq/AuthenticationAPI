Endpoint for login call : 
curl --location 'http://localhost:59953/api/auth/login' \
--header 'Content-Type: application/json' \
--data '{
    "UserName" : "Nawaz",
    "Password" : "1122"
}'

Endpoint for user role and region info :
curl --location 'http://localhost:59953/api/User/Info' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjMwLCJleHAiOjMwLCJyb2xlIjoiUGxheWVyIiwic2NvcGUiOiJiX2dhbWUifQ.x8O37Z8yDP89w2DBB1s-A5A5stnOlW5DYwR1aciiXSY'


Note : Just incase you run into rosylyn .net compiler platform sdk issue, you may run following command "Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r" in package manager console to resolve the issue,
I ran into this issue when i tried testing on more than one device.
