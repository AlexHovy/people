# people

## Configs
- Copy `client/example.env` and rename copied version to `.env`
- Copy `Api/appsettings.example.json` and rename copied version to `appsettings.json`
- Remeber to replace config values

## Postman
- You will find a Postman export at `Data/Postman`
- Remember to change the `Variables` in the parent folder

## Db Migrations
```
dotnet ef migrations add <Migration Name> --project Api
dotnet ef database update --project Api
```

## Admin User
```
Username: admin
Password: P@ssword123
```