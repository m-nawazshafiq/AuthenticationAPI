Authentication or login call

curl --location 'http://localhost:59953/api/auth/login' \
--header 'Content-Type: application/json' \
--data '{
    "UserName" : "Nawaz",
    "Password" : "1122"
}'

Call to get user scope and role via jwt

curl --location 'http://localhost:59953/api/User/Info' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE4LCJleHAiOjE4LCJyb2xlIjoiUGxheWVyIiwic2NvcGUiOiJiX2dhbWUifQ.AXajhROEFMu8skXuQBAYR1Al1qU3HPGDB91IBHOaBSM'