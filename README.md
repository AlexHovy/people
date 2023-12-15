# people

## Configs
- copy `client/example.env` and rename copied version to `.env`
- copy `Api/appsettings.example.json` and rename copied version to `appsettings.json`
- remeber to replace config values appropriately

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