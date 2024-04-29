# people

## Configs
- Copy `client/example.env` and rename copied version to `.env`
- Copy `People.Api/appsettings.example.json` and rename copied version to `appsettings.json`
- Remeber to replace config values

## Postman
- You will find a Postman export at `Data/Postman`
- Remember to change the `Variables` in the parent folder

## Db Migrations
Make sure EF is installed: `dotnet tool install --global dotnet-ef`
```
dotnet ef migrations add <Migration Name> --project People.Data --startup-project People.Api
dotnet ef database update --project People.Data --startup-project People.Api
dotnet ef migrations remove --project People.Data --startup-project People.Api
```

## Admin User
```
Username: admin
Password: P@ssword123
```

## Run Server
- Navigate to the `People.Api`
- Run database migration update
- Run `dotnet run`

## Run Client
- Navigate to the `client`
- Run `npm i`
- Run `npm start`